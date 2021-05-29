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

namespace SpisokPokupok.Viewmodels
{
    class MainWindowViewModel : BaseViewModel
    {
        #region Title
        private string _Title = "Список покупок";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        #endregion

        /*--------------------------------------------------------------------------*/

        #region модели

        #region Коллекция категорий

        public ObservableCollection<Cotegory> Cotegory { get; }

        #endregion

        #region выбор группы

        private Cotegory _SelectedCotegory;

        public Cotegory SelectedCotegory
        { 
            get => _SelectedCotegory; 
            set => Set(ref _SelectedCotegory, value); 
        }


        #endregion

        #endregion

        #region команды

        #region Добавление новой категории
        public ICommand AddCotegoryCommand { get; }

        private bool CanAddCotegoryCommandExecute(object p) => true;

        private void OnAddCotegoryCommandExecuted(object p)
        {
            var cotegory_max_index = Cotegory.Count + 1;

            var new_cotegory = new Cotegory //подготовка нового элемента в список категорий
            {
                Name = $"Категория {cotegory_max_index}",
                Tovar = new ObservableCollection<Tovar>()
            };

            Cotegory.Add(new_cotegory); //добавление новой категории в список категорий

        }

        #endregion

        #region Удаление Котегории
        public ICommand DeleteCotegoryCommand { get; }

        private bool CanDeleteCotegoryCommandExecute(object p) => p is Cotegory cotegory && Cotegory.Contains(cotegory);

        private void OnDeleteCotegoryCommandExecuted(object p)
        {
            if (!(p is Cotegory cotegory)) return;

            Cotegory.Remove(cotegory);
        }

        #endregion


        #endregion

        public MainWindowViewModel()
        {
            #region команды

            AddCotegoryCommand = new LambdaCommand(OnAddCotegoryCommandExecuted, CanAddCotegoryCommandExecute);

            DeleteCotegoryCommand = new LambdaCommand(OnDeleteCotegoryCommandExecuted,CanDeleteCotegoryCommandExecute);


            #endregion

            var student = Enumerable.Range(1, 5).Select(i => new Tovar()
            {
                Name = $"Товар{i}",
                Price = i,
                URL = $"URL{i}",
                Status= false
            }) ;

            var cotegory = Enumerable.Range(1,20).Select(i => new Cotegory()
            {
                Name = $"Категория {i}",
                Tovar = new ObservableCollection<Tovar>(student)
                
            });

            Cotegory = new ObservableCollection<Cotegory>(cotegory);

        }


    }
}
