using System;
using System.Collections.Generic;
using System.Text;
using CurryFit.model.api;

namespace CurryFit.model
{
    public class Meal
    {
        public string Name { get; set; }
        public int Calories { get; set; }
        public string Description { get; set; }
        public List<FoodProduct> foodProducts { get; set; }
    }
}
