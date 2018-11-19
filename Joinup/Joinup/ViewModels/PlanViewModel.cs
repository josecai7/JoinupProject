using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Helpers;
using Joinup.Service;
using Joinup.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class PlanViewModel:BaseViewModel
    {
        #region Attributes
        private Plan plan;
        ObservableCollection<Plan> pins = new ObservableCollection<Plan>();
        #endregion
        #region Properties
        public Plan Plan
        {
            get
            {
                return plan;
            }
            set
            {
                plan = value;
                RaisePropertyChanged();
            }
        }
        public ObservableCollection<MyUserASP> Assistants
        {
            get
            {
                return new ObservableCollection<MyUserASP>(plan.AssistantUsers);
            }
        }
        public ObservableCollection<Plan> Pins
        {
            get { return pins; }
            set
            {
                pins = value;
                RaisePropertyChanged( "Pins" );
            }
        }
        public Style ButtonStyle
        {
            get
            {
                if (plan.AssistantUsers.Find(assistant => assistant.Id == LoggedUser.Id) == null)
                {
                    return Application.Current.Resources["UnSelectedButton"] as Style;
                }
                else
                {
                    return Application.Current.Resources["SelectedButton"] as Style;
                }
            }
        }
        #endregion
        #region Constructors
        public PlanViewModel()
        {
            plan = new Plan();
        }
        public override Task InitializeAsync(object navigationData)
        {
            var parameter = navigationData as Plan;
            if (parameter != null)
            {
                Plan = parameter;
                if ( plan.Latitude != Double.NaN && plan.Longitude != Double.NaN )
                {
                    Pins = new ObservableCollection<Plan>();
                    Pins.Add( plan );
                }
                RaisePropertyChanged("Plan");
                RaisePropertyChanged("ButtonStyle");
                RaisePropertyChanged("Assistants");
            }

            return base.InitializeAsync(navigationData);
        }
        #endregion
        #region Commands
        public ICommand JoinCommand
        {
            get
            {
                return new RelayCommand(JoinUnJoinPlan);
            }
        }

        #endregion
        #region Methods

        private void JoinUnJoinPlan()
        {
            if (plan.AssistantUsers.Find(assistant => assistant.Id == LoggedUser.Id) == null)
            {
                JoinPlan();
            }
            else
            {
                UnJoinPlan();
            }


        }

        private async void JoinPlan()
        {
            var response = await DataService.GetInstance().JoinAPlan(plan.PlanId, LoggedUser.Id);

            if (!response.IsSuccess)
            {
                ToastNotificationUtils.ShowToastNotifications("Ha habido un error al unirse. Intentelo de nuevo más tarde", "add.png", Xamarin.Forms.Color.IndianRed);
            }
            else
            {
                plan.AssistantUsers.Add(LoggedUser);
                RaisePropertyChanged("Plan");
                RaisePropertyChanged("Assistants");
                RaisePropertyChanged("ButtonStyle");
            }
        }
        private async void UnJoinPlan()
        {
            var response = await DataService.GetInstance().UnJoinAPlan(plan.PlanId, LoggedUser.Id);

            if (!response.IsSuccess)
            {
                ToastNotificationUtils.ShowToastNotifications("Ha habido un error al desvincularse del plan. Intentelo de nuevo más tarde", "add.png", Xamarin.Forms.Color.IndianRed);
            }
            else
            {
                plan.AssistantUsers.Remove(plan.AssistantUsers.Single(s => s.Id == LoggedUser.Id));
                RaisePropertyChanged("Plan");
                RaisePropertyChanged("Assistants");
                RaisePropertyChanged("ButtonStyle");
            }
        }

        #endregion
    }
}
