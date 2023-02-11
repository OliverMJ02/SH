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

        /// <summary>
        /// Converts the nutrients from the dabas product to the food product
        /// </summary>
        /// <param name="foodProduct">The food product to be converted to</param>
        /// <param name="naringsvarden">The nutrients to be converted</param>
        private List<FoodProduct.Nutrient> orderNutrients(List<FoodProduct.Nutrient> nutrients)
        {
            List<FoodProduct.Nutrient> orderedNutrients = new List<FoodProduct.Nutrient>(nutrients.Count);
            List<FoodProduct.Nutrient> detailedNutrients = new List<FoodProduct.Nutrient>(nutrients.Count - 5);

            for (int i = 0; i < nutrients.Count; i++)
            {
                InsertNutrientsInOrder(nutrients, orderedNutrients, detailedNutrients, i);
            }
            
            orderedNutrients.AddRange(detailedNutrients);

            return orderedNutrients;
        }
        /// <summary>
        /// Inserts the nutrients in the correct order in the orderedNutrients list
        /// </summary>
        /// <param name="nutrients">The list of nutrients to be ordered</param>
        /// <param name="orderedNutrients">The list of nutrients to be ordered in</param>
        /// <param name="detailedNutrients">The list of nutrients that are not ordered</param>
        /// <param name="i">The index of the nutrient to be inserted</param>
        private void InsertNutrientsInOrder(List<FoodProduct.Nutrient> nutrients, List<FoodProduct.Nutrient> orderedNutrients,
          List<FoodProduct.Nutrient> detailedNutrients , int i)
        {
            switch (nutrients[i].Name)
            {
                case "Energi":
                    if (nutrients[i].Unit == "kcal")
                    {
                        orderedNutrients.Insert(0, nutrients[i]);
                    }
                    break;
                case "Kolhydrat":
                    orderedNutrients.Insert(1, nutrients[i]);
                    break;
                case "Fett":
                    orderedNutrients.Insert(2, nutrients[i]);
                    break;
                case "Protein":
                    orderedNutrients.Insert(3, nutrients[i]);
                    break;
                case "Fiber":
                    orderedNutrients.Insert(4, nutrients[i]);
                    break;
                default:
                    detailedNutrients.Add(nutrients[i]);
                    break;
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
            contents.Size = dabasProduct.NettoInnehall[0].Mangd;

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
