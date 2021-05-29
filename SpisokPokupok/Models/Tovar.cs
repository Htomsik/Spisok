using System;
using System.Collections.Generic;
using System.Text;

namespace SpisokPokupok.Models
{
    public class Tovar
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public string URL { get; set; }

        public bool Status { get; set; }

        public string Descriptions { get; set; }
    }

    public class Cotegory
    {
        public string Name { get; set; }

        public ICollection<Tovar> Tovar { get; set; }
    }
}
