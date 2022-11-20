using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurryFit.model.api
{
    public class FoodProduct
    {
        public string Artikelbenamning { get; set; }
        public string ProduktKod { get; set; }
        public List<Naringsinfon> Naringsinfo { get; set; }
        public List<NettoInnehallen> NettoInnehall { get; set; }

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
            public double Mangd { get;  set; }
            public int TypKod { get; set; }
            public string Typ { get; set; }
        }

    }
}
