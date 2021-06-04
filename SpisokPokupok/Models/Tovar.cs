using System;
using System.Collections.Generic;
using System.Text;
using SpisokPokupok.Viewmodels.Base;

namespace SpisokPokupok.Models
{
    public class Tovar
    {
        /// <summary>
        /// Имя товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цена товара
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Ссылка на товар
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Статус покупки (куплено/некуплено)
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Заметки о товаре
        /// </summary>
        public string Descriptions { get; set; }
    }

    public class Cotegory: BaseViewModel
    {

        /// <summary>
        /// Название котегории
        /// </summary>
        private string name;
        public string Name 
        {
            get => name;
            set => Set(ref name, value);
          
        }

        /// <summary>
        /// Коллекция товаров
        /// </summary>
        private ICollection<Tovar> tovar;

        public ICollection<Tovar> Tovar 
        {
            get => tovar;
            set => Set(ref tovar, value);
        }

        
    }
}
