using System;
using System.Collections.Generic;
using System.Text;

namespace JSON_Test
{
    public class Cotegory
    {
        public string Name;

        public ICollection<Tovar> Tovar;
    }

    public class Tovar
    {
        public string Name;

        public int price;
    }

}
