﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CurryFit.model.api
{
    /// <summary>
    /// A class representing a food product with data retrieved from the API that is used in the program
    /// </summary>
    public class FoodProduct
    {
        public string Name { get; set; }
        public string Brand { get; set; }
        public List<Nutrient> Nutrients { get; set; }
        public Content Contents { get; set; }
        public string gtin { get; set; }


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
    
}
