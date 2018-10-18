using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Infrastructure;
using Joinup.Utils;
using Joinup.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class NewPlanStep2ViewModel : BaseViewModel
    {
        #region Attributes
        private Plan plan;
        private string title;
        private string description;
        #endregion
        #region Properties
        public string Title
        {
            get
            {
                return title ;
            }
            set
            {
                Xamarin.Forms.MessagingCenter.Send( MainViewModel.GetInstance().NewPlanStep1, "Hi", plan );
                title = value;
                SetValue(ref title, value);
            }
        }
        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                Xamarin.Forms.MessagingCenter.Send( MainViewModel.GetInstance().NewPlanStep1, "Hi", plan );
                description = value;
                SetValue(ref description, value);
            }
        }

        #endregion
        #region Constructors
        public NewPlanStep2ViewModel(Plan pPlan)
        {
            plan = pPlan;

            Xamarin.Forms.MessagingCenter.Send( MainViewModel.GetInstance().NewPlanStep1, "Hi", plan );
        }
        #endregion

        #region Commands
        public ICommand NextStepCommand
        {
            get
            {
                return new RelayCommand(NextStepAsync);
            }
        }
        #endregion

        #region Methods
        private async void NextStepAsync()
        {
            if (string.IsNullOrEmpty(Title))
            {
                ToastNotificationUtils.ShowToastNotifications("Ups...Recuerda añadir un titulo a tu plan", "add.png", ColorUtils.ErrorColor);
            }
            else
            {
                plan.Name = Title;
                plan.Description = Description;

                MainViewModel.GetInstance().NewPlanStep3 = new NewPlanStep3ViewModel(plan);
                await Application.Current.MainPage.Navigation.PushAsync(new NewPlanStep3Page());
            }
        }
        #endregion
    }
}

