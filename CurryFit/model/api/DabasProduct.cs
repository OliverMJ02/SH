using System;
using System.Collections.Generic;
using System.Text;

namespace CurryFit.model.api
{
    /// <summary>
    /// A class representing a dabas product with data retrieved from their database
    /// </summary>
    public class DabasProduct
    {
        public string Artikelbenamning { get; set; }
        public string ProduktKod { get; set; }
        public List<Naringsinfon> Naringsinfo { get; set; }
        public List<NettoInnehallen> NettoInnehall { get; set; }
        public Varumarken Varumarke { get; set; }

        public class Naringsinfon
        {
            public string Tillagningsstatus { get; set; }
            public string Intagningsrekommendationstyp { get; set; }
            public string Portionsstorlek { get; set; }
            public double Basmangdsdeklaration { get; set; }
            public string Mattkvalificerarebasmangd { get; set; }
            public List<Naringsvarden> Naringsvarden { get; set; }

        }
        public class Naringsvarden
        {
            public string Benamning { get; set; }
            public string Kod { get; set; }
            public double Mangd { get; set; }
            public string Enhet { get; set; }
            public string Enhetskod { get; set; }
            public string Precision { get; set; }
            public double Dagsintag { get; set; }
        }
        public class NettoInnehallen
        {
            public string Enhet { get; set; }
            public double Mängd { get;  set; }
            public int TypKod { get; set; }
            public string Typ { get; set; }
        }

        public class Varumarken
        {
            public string Varumarke { get; set; }
            public string AgareGLN { get; set; }
            public string AgareNamn { get; set; }
            public Tillverkaren Tillverkare { get; set; }

            public class Tillverkaren
            {
                public string Namn { get; set; }
                public string EAN { get; set; }
            }

        }

    }
}
