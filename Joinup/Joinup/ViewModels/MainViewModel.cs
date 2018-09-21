using GalaSoft.MvvmLight.Command;
using Joinup.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class MainViewModel
    {
        public CommentsViewModel Comments { get; set; }

        public PlansViewModel Plans { get; set; }

        public LoginViewModel Login { get; set; }

        public NewPlanViewModel NewPlan { get; set; }

        public ICommand NewPlanCommand
        {
            get
            {
                return new RelayCommand( GoToNewPlan );
            }
        }

        private async void GoToNewPlan()
        {
            NewPlan = new NewPlanViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new NewPlanPage());
        }

        public MainViewModel()
        {
            Plans = new PlansViewModel();         
        }
    }
}
