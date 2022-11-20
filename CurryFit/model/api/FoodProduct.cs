using System;
using System.Collections.Generic;
using System.Text;

namespace CurryFit.model.api
{
    public class FoodProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }   
        public List<Nutrient> Nutrients { get; set; }
        public Content Content { get; set; }

    }
    public class Nutrient
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public double Amount { get; set; }
        public double DailyIntake { get; set; }
    }

    public class Content
    {
        public string Unit { get; set; }
        public double Size { get; set; }

    }
}
