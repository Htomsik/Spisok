﻿using SpisokPokupok.Viewmodels.Base;
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

        #region Выбор группы

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

        #endregion

        #region Команды

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
                Tovar = new ObservableCollection<Tovar>()
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

            Cotegory.Remove(cotegory);
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

        public MainWindowViewModel()
        {
            #region команды

            AddCotegoryCommand = new LambdaCommand(OnAddCotegoryCommandExecuted, CanAddCotegoryCommandExecute);

            DeleteCotegoryCommand = new LambdaCommand(OnDeleteCotegoryCommandExecuted,CanDeleteCotegoryCommandExecute);

            ChangeCotegoryCommand = new LambdaCommand(OnChangeCotegoryCommandExecuted, CanChangeCotegoryCommandExecute);


            #endregion

            #region Временное заполнение данных пока программа не готова

            var student = Enumerable.Range(1, 5).Select(i => new Tovar()
            {
                Name = $"Товар{i}",
                Price = i,
                URL = $"URL{i}",
                Status= false
            }) ;

            var cotegory = Enumerable.Range(1,5).Select(i => new Cotegory()
            {
                Name = $"Категория {i}",
                Tovar = new ObservableCollection<Tovar>(student)
                
            });

            Cotegory = new ObservableCollection<Cotegory>(cotegory);

            #endregion
        }


    }
}
