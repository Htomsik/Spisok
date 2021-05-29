using SpisokPokupok.Infrastructures.Commands.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace SpisokPokupok.Infrastructures.Commands
{
    class CloseApplicationCommand : BaseCommand
    {
        public override bool CanExecute(object parameter) => true;


        public override void Execute(object parameter)
        {
            Application.Current.Shutdown();
        }
    }
}
