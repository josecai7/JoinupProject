using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Joinup.ViewModels
{
    public class NewPlanViewModel:BaseViewModel
    {

        public string Name { get; set; }
        public string Description { get; set; }


        public ICommand SavePlanCommand
        {
            get
            {
                return new RelayCommand(SavePlan);
            }
        }

        private void SavePlan()
        {
            string s=Description;;
        }
    }
}
