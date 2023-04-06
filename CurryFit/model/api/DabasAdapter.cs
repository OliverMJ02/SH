using CurryFit.model.util;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurryFit.model.api
{
    /// <summary>
    /// A class for converting a dabas product to a food product
    /// </summary>
    class DabasAdapter
    {
        /// <summary>
        /// Converts the dabas product to a food product
        /// </summary>
        /// <param name="dabasProduct">The dabas product to be converted</param>
        /// <returns>A food product</returns>
        public FoodProduct ConvertToFoodProduct(DabasProduct dabasProduct)
        {
            FoodProduct foodProduct = new FoodProduct();
            List<DabasProduct.Naringsvarden> naringsvarden = dabasProduct.Naringsinfo[0].Naringsvarden;

            foodProduct.Name = dabasProduct.Artikelbenamning;
            foodProduct.Brand = dabasProduct.Varumarke.Tillverkare.Namn;

            convertNutrients(foodProduct, naringsvarden);
            convertContents(dabasProduct, foodProduct);

            foodProduct.Nutrients.Sort((x, y) => x.Name.CompareTo(y.Name));   
            foodProduct.Nutrients = orderNutrients(foodProduct.Nutrients);

            return foodProduct;
        }

        private Dictionary<string, int> nutrientOrder = new Dictionary<string, int>
            {
                { "Energi", 0 },
                { "Kolhydrat", 1 },
                { "Fett", 2 },
                { "Protein", 3 },
                { "Salt (SALTEQ)", 4 }
            };

        private List<FoodProduct.Nutrient> orderNutrients(List<FoodProduct.Nutrient> nutrients)
        {
            List<FoodProduct.Nutrient> orderedNutrients = new List<FoodProduct.Nutrient>(nutrients.Count);
            List<FoodProduct.Nutrient> detailedNutrients = new List<FoodProduct.Nutrient>(nutrients.Count - 5);
            splitNutrients(nutrients, orderedNutrients, detailedNutrients);

            orderedNutrients.Sort((n1, n2) => nutrientOrder[n1.Name].CompareTo(nutrientOrder[n2.Name]));
            orderedNutrients.AddRange(detailedNutrients);

            return orderedNutrients;
        }

        private void splitNutrients(List<FoodProduct.Nutrient> nutrients, List<FoodProduct.Nutrient> orderedNutrients, List<FoodProduct.Nutrient> detailedNutrients)
        {
            foreach (var nutrient in nutrients)
            {
                if (nutrientOrder.ContainsKey(nutrient.Name))
                {
                    if (nutrient.Name == "Energi" && nutrient.Unit != "kcal")
                    {
                        detailedNutrients.Add(nutrient);
                    }
                    else
                    {
                        orderedNutrients.Add(nutrient);
                    }
                }
                else
                {
                    detailedNutrients.Add(nutrient);
                }
            }
        }

        /// <summary>
        /// Converts the contents of a DabasProduct to a FoodProduct
        /// </summary>
        /// <param name="dabasProduct">The DabasProduct to be converted</param>
        /// <param name="foodProduct">The FoodProduct to be converted to</param>
        private void convertContents(DabasProduct dabasProduct, FoodProduct foodProduct)
        {
            FoodProduct.Content contents = new FoodProduct.Content();
            contents.Unit = dabasProduct.NettoInnehall[0].Typ;
            contents.Size = dabasProduct.NettoInnehall[0].Mängd;

            foodProduct.Contents = contents;
        }

        /// <summary>
        /// Converts the nutrients of a DabasProduct to a FoodProduct
        /// </summary>
        /// <param name="foodProduct">The FoodProduct to be converted to</param>
        /// <param name="naringsvarden">The nutrients to be converted</param>
        private void convertNutrients(FoodProduct foodProduct, List<DabasProduct.Naringsvarden> naringsvarden)
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
