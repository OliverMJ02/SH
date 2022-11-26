using System;
using System.Collections.Generic;
using System.Text;

namespace CurryFit.model.api
{
    class DabasAdapter
    {
        public FoodProduct ConvertToFoodProduct(DabasProduct dabasProduct)
        {
            FoodProduct foodProduct = new FoodProduct();
            List<DabasProduct.Naringsvarden> naringsvarden = dabasProduct.Naringsinfo[0].Naringsvarden;

            foodProduct.Name = dabasProduct.Artikelbenamning;

            convertNutrients(foodProduct, naringsvarden);

            convertContents(dabasProduct, foodProduct);

            return foodProduct;
        }

        private static void convertContents(DabasProduct dabasProduct, FoodProduct foodProduct)
        {
            FoodProduct.Content contents = new FoodProduct.Content();
            contents.Unit = dabasProduct.NettoInnehall[0].Typ;
            contents.Size = dabasProduct.NettoInnehall[0].Mangd;

            foodProduct.Contents = contents;
        }

        private static void convertNutrients(FoodProduct foodProduct, List<DabasProduct.Naringsvarden> naringsvarden)
        {
            List<FoodProduct.Nutrient> nutrients = new List<FoodProduct.Nutrient>(naringsvarden.Count);

            for (int i = 0; i < naringsvarden.Count; i++)
            {
                nutrients.Add(new FoodProduct.Nutrient
                {
                    Name = naringsvarden[i].Benamning,
                    Amount = naringsvarden[i].Mangd,
                    Unit = naringsvarden[i].Enhet,
                    DailyIntake = naringsvarden[i].Dagsintag
                });
            }

            foodProduct.Nutrients = nutrients;
        }
    }
}
