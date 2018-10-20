using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Utils;
using Joinup.ViewModels.Base;
using Joinup.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class NewPlanStep2ViewModel : BaseViewModel
    {
        #region Attributes
        private Plan plan;
        #endregion
        #region Properties
        public string Title
        {
            get
            {
                return plan.Name ;
            }
            set
            {
                plan.Name = value;
                RaisePropertyChanged( "Title" );
            }
        }
        public string Description
        {
            get
            {
                return plan.Description;
            }
            set
            {
                plan.Description = value;
                RaisePropertyChanged( "Description" );
            }
        }

        #endregion
        #region Constructors
        public NewPlanStep2ViewModel()
        {
            plan = new Plan();
        }
        public override Task InitializeAsync(object navigationData)
        {
            plan = (Plan)navigationData;
            RaisePropertyChanged("Title");
            RaisePropertyChanged("Description");

            return base.InitializeAsync(navigationData);
        }
        public override Task OnDissapearing()
        {
            MessagingCenter.Send(ViewModelLocator.Instance.Resolve<NewPlanStep1ViewModel>(), "UpdatePlan", plan);
            return base.OnDissapearing();
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
        private void NextStepAsync()
        {
            if (string.IsNullOrEmpty(Title))
            {
                ToastNotificationUtils.ShowToastNotifications("Ups...Recuerda añadir un titulo a tu plan", "add.png", ColorUtils.ErrorColor);
            }
            else
            {
                NavigationService.NavigateToAsync<NewPlanStep3ViewModel>(plan);
            }
        }
        #endregion
    }
}

