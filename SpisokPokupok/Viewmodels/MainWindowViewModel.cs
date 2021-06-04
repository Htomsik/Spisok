using SpisokPokupok.Viewmodels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using SpisokPokupok.Models;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using SpisokPokupok.Infrastructures.Commands;
using SpisokPokupok.Services;
using System.Windows;

namespace SpisokPokupok.Viewmodels
{
    class MainWindowViewModel : BaseViewModel
    {
        #region Свойства

        #region Title

        /// <summary>
        /// Заголовок программы
        /// </summary>
        private string _Title = "Список покупок";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        #region TextBoxText
        /// <summary>
        /// Текст в текстбоксе для добавления/редактирования названия котегории
        /// </summary>
        private string _TextBoxText;
        public string TextBoxText
        {
            get => _TextBoxText;
            set => Set(ref _TextBoxText, value);
        }

        #endregion

        #endregion

        #region модели

        #region Коллекция категорий

        /// <summary>
        /// Колекция (список) категорий
        /// </summary>
        public ObservableCollection<Cotegory> Cotegory { get; }


        #endregion

        #endregion

        #region Сервисы

        #region dataservice
        /// <summary>
        /// Обьект класса сервисов
        /// </summary>
        DataService DataService;

        #endregion

        #endregion

        #region Команды

        #region Команды связанные с категориями

        #region Выбор категории

        /// <summary>
        /// Регистрирует выбранную котегорию
        /// </summary>
        private Cotegory _SelectedCotegory;

        public Cotegory SelectedCotegory
        {
            get => _SelectedCotegory;
            set => Set(ref _SelectedCotegory, value);
        }

        #endregion

        #region Добавление новой категории

        /// <summary>
        /// Добавление новой котегории (Принимает значение названия из текстбокса)
        /// </summary>
        public ICommand AddCotegoryCommand { get; }

        private bool CanAddCotegoryCommandExecute(object p) => !string.IsNullOrEmpty(TextBoxText);

        private void OnAddCotegoryCommandExecuted(object p)
        {

  
            var new_cotegory = new Cotegory //подготовка нового элемента в список категорий
            {
                Name = TextBoxText,
                Tovar = new ObservableCollection<Tovar>(),
            };

            Cotegory.Add(new_cotegory); //добавление новой категории в список категорий
            

            TextBoxText = string.Empty;

        }

        #endregion

        #region Удаление Категории

        /// <summary>
        /// Удаление котегории
        /// </summary>
        public ICommand DeleteCotegoryCommand { get; }

        private bool CanDeleteCotegoryCommandExecute(object p) => p is Cotegory cotegory && Cotegory.Contains(cotegory);

        private void OnDeleteCotegoryCommandExecuted(object p)
        {
            if (!(p is Cotegory cotegory)) return;

            var cotegory_index = Cotegory.IndexOf(cotegory)-1;

            Cotegory.Remove(cotegory);

            if (cotegory_index < Cotegory.Count && Cotegory.Count!=0)
                SelectedCotegory = Cotegory[cotegory_index];

          
        }

        #endregion

        #region Редактировании названия категории

        /// <summary>
        /// Редоктирование названия котегории (Принимает значение названия из текстбокса)
        /// </summary>
        public ICommand ChangeCotegoryCommand { get; }

        private bool CanChangeCotegoryCommandExecute(object p) => p is Cotegory cotegory && Cotegory.Contains(cotegory)  && !string.IsNullOrEmpty(TextBoxText);

        private void OnChangeCotegoryCommandExecuted(object p)
        {
            if (!(p is Cotegory cotegory)) return;

            cotegory.Name = TextBoxText;

            TextBoxText = string.Empty;

        }



        #endregion

        #endregion

        #region Команды связанные с товарами

        #region index товара

        /// <summary>
        /// Возвращает Index выбраного товара
        /// </summary>

        private int _SelectedTovarIndex;

        public int SelectedTovarIndex
        {
            get => _SelectedTovarIndex;
            set => Set(ref _SelectedTovarIndex, value);
        }


        #endregion

        #region Выбор товара

        /// <summary>
        /// Регистрирует выбранный товар
        /// </summary>
        private Tovar _SelectedTovar;

        public Tovar SelectedTovar
        {
            get => _SelectedTovar;
            set => Set(ref _SelectedTovar, value);
        }

        #endregion

        #region Добавление товара

        /// <summary>
        /// Добавление товара в список категорий
        /// </summary>
        public ICommand AddTovarCommand { get; }

        private bool CanAddTovarCommandExecute(object p) => p is Cotegory cotegory && Cotegory.Contains(cotegory);

        private void OnAddTovarCommandExecuted(object p)
        {
            if (!(p is Cotegory cotegory)) return;

            var new_tovar = new Tovar
            {
                Name = $"Товар",
                Price = 0,
                URL = $"URL",
                Status = false
            };

            cotegory.Tovar.Add(new_tovar);
        }


        #endregion

        #region удаление товара

        /// <summary>
        /// Удаление товара
        /// </summary>
        public ICommand DeleteTovarCommand { get; }

        private bool CanDeleteTovarCommandExecute(object p) => p is Tovar;

        private void OnDeleteTovarCommandExecuted(object p)
        {
            Cotegory cotegory = SelectedCotegory;   
            if (!(p is Tovar tovar)) return;

            

            cotegory.Tovar.Remove(tovar);
            
          //  if(SelectedTovarIndex<cotegory.Tovar.Count)
         //  SelectedTovarIndex=cotegory.Tovar[SelectedTovarIndex];


        }

        #endregion

        #endregion

        #region команды связанные с dataservice

        #region сохранение данных при закрытии

        public ICommand SaveDataANDClosing { get; }

        private bool CanSaveDataANDClosingExecute(object p) => true;

        private void OnSaveDataANDClosingExecute(object p)
        {
            DataService = new DataService();

            DataService.SaveData(Cotegory);

            Application.Current.Shutdown();
        }
        #endregion

        #endregion


        #endregion

        public MainWindowViewModel()
        {
            #region команды

            /*--------------------Команды для категорий------------------------*/

            AddCotegoryCommand = new LambdaCommand(OnAddCotegoryCommandExecuted, CanAddCotegoryCommandExecute);

            DeleteCotegoryCommand = new LambdaCommand(OnDeleteCotegoryCommandExecuted,CanDeleteCotegoryCommandExecute);

            ChangeCotegoryCommand = new LambdaCommand(OnChangeCotegoryCommandExecuted, CanChangeCotegoryCommandExecute);


            /*--------------------Команды для товаров------------------------*/

            AddTovarCommand = new LambdaCommand(OnAddTovarCommandExecuted, CanAddTovarCommandExecute);

            DeleteTovarCommand = new LambdaCommand(OnDeleteTovarCommandExecuted, CanDeleteTovarCommandExecute);


            /*--------------------Команды dataservice------------------------*/

            SaveDataANDClosing = new LambdaCommand(OnSaveDataANDClosingExecute, CanSaveDataANDClosingExecute);

            #endregion

            #region получение данных из json файла

            DataService = new DataService();

            Cotegory = DataService.GetData();

            #endregion

        }


    }
}
