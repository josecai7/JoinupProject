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

        public NewPlanStep1ViewModel NewPlanStep1 { get; set; }

        public NewPlanStep2ViewModel NewPlanStep2 { get; set; }

        public NewPlanStep3ViewModel NewPlanStep3 { get; set; }

        public NewPlanStep4ViewModel NewPlanStep4 { get; set; }

        public ICommand NewPlanCommand
        {
            get
            {
                return new RelayCommand( GoToNewPlan );
            }
        }

        private async void GoToNewPlan()
        {
            NewPlanStep1 = new NewPlanStep1ViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new NewPlanStep1Page());
        }

        public MainViewModel()
        {
            instance = this;
            Plans = new PlansViewModel();         
        }
        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion
    }
}
