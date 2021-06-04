using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace JSON_Test
{
    class Program
    {
      
        void _newCotegory()
        {

        }

        static void Main(string[] args)
        {

            var path = Path.Combine(Environment.CurrentDirectory, "Cotegory.json");
           ObservableCollection <Cotegory> Cotegory;

            /*   var tovar = Enumerable.Range(1, 5).Select(i => new Tovar()
               {
                   Name = $"Товар{i}",
                   price=i

               });

               var cotegory = Enumerable.Range(1, 5).Select(i => new Cotegory()
               {
                   Name = $"Категория {i}",
                   Tovar = new ObservableCollection<Tovar>(tovar)

               });

               Cotegory = new ObservableCollection<Cotegory>(cotegory);


               var CotegorySave = JsonConvert.SerializeObject(Cotegory);

               File.WriteAllText("Cotegory.json", CotegorySave);

               */

            var JStart = File.ReadAllText(path);

            var _cotegory = JsonConvert.DeserializeObject<ObservableCollection<Cotegory>>(JStart);

            Cotegory = new ObservableCollection<Cotegory>(_cotegory);


            foreach (Cotegory cotegory in Cotegory)
            {
                Console.WriteLine("Name:{0}\nTovarCount:{1}\n",cotegory.Name,cotegory.Tovar.Count);
                Console.WriteLine("Состав:\n-----------");
                foreach(Tovar tovar in cotegory.Tovar)
                {
                    Console.WriteLine("Name:{0}\nPrice{1}\n", tovar.Name, tovar.price);
                }
                Console.WriteLine("\n-----------");
            }


        }


    }
}
