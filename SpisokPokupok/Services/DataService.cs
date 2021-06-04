using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SpisokPokupok.Models;

namespace SpisokPokupok.Services
{
    class DataService
    {
        
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Spisok.txt");

        ObservableCollection<Cotegory> Cotegory;

        #region вывод информации из файла
        /// <summary>
        /// получает информацию из файла с типом Cotegory
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<Cotegory> GetData()
        {
           

            if(!File.Exists(path) )
            {
                return new ObservableCollection<Cotegory>();
            }
            else
            {
              

                var CotegoryExist = File.ReadAllText(path);

                if (CotegoryExist==string.Empty) return new ObservableCollection<Cotegory>();

                var _CotegoryExist = JsonConvert.DeserializeObject<ObservableCollection<Cotegory>>(CotegoryExist);

                var NewCotegoryCollection = new ObservableCollection<Cotegory>();

                foreach (Cotegory cotegory in _CotegoryExist)
                {
                    var new_cotegory = new Cotegory //подготовка нового элемента в список категорий
                    {
                        Name = cotegory.Name,
                        Tovar = new ObservableCollection<Tovar>()   
                    };

                    foreach(Tovar tovar in cotegory.Tovar)
                    {

                        var new_tovar = new Tovar
                        {
                            Name = tovar.Name,
                            Price = tovar.Price,
                            URL = tovar.URL,
                            Status = tovar.Status,
                            Descriptions=tovar.Descriptions
                        };

                        new_cotegory.Tovar.Add(new_tovar);

                    };

                    NewCotegoryCollection.Add(new_cotegory);

                }

                return NewCotegoryCollection;
            }
            
        }
        #endregion

        #region Сохранение информации
        /// <summary>
        /// Сохраняет информацию в 
        /// </summary>
        /// <returns></returns>
        public void SaveData(ObservableCollection<Cotegory> cotegory)
        {

            var CotegorySave = JsonConvert.SerializeObject(cotegory);

            File.WriteAllText(path, CotegorySave);
        }
        #endregion
    }
}
