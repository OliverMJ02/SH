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

            for (int i = 0; i < naringsvarden.Count; i++)
            {
                foodProduct.Nutrients[i].Name = naringsvarden[i].Benamning;
                foodProduct.Nutrients[i].Amount = naringsvarden[i].Mangd;
                foodProduct.Nutrients[i].Unit = naringsvarden[i].Enhet;
                foodProduct.Nutrients[i].DailyIntake = naringsvarden[i].Dagsintag;
            }

            foodProduct.Content.Unit = dabasProduct.NettoInnehall[0].Typ;
            foodProduct.Content.Size = dabasProduct.NettoInnehall[0].Mangd;

            return foodProduct;
        }
    }
}
