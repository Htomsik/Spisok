using System;
using System.Collections.Generic;
using System.Text;
using SpisokPokupok.Viewmodels.Base;

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

    public class Cotegory: BaseViewModel
    {
        private string name;
        public string Name 
        {
            get => name;
            set => Set(ref name, value);
          
        }

        public ICollection<Tovar> Tovar { get; set; }
    }
}
