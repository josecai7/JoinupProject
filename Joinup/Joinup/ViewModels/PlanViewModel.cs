using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Common.Models.SelectablesModels;
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
                if ( IsHost )
                {
                    return Application.Current.Resources["ModifyPlanButton"] as Style;
                }
                else
                {
                    if ( plan.AssistantUsers.Find( assistant => assistant.Id == LoggedUser.Id ) == null )
                    {
                        return Application.Current.Resources["JoinButton"] as Style;
                    }
                    else
                    {
                        return Application.Current.Resources["UnJoinButton"] as Style;
                    }
                }
            }
        }
        public bool IsHost
        {
            get
            {
                return plan.UserId==LoggedUser.Id;
            }
        }
        public bool IsNotHost
        {
            get
            {
                return plan.UserId != LoggedUser.Id;
            }
        }
        public bool IsPlanAvaliable
        {
            get
            {
                return plan.PlanDate > DateTime.Now;
            }
        }

        public bool HasLink
        {
            get
            {
                return !string.IsNullOrEmpty(plan.Link);
            }
        }
        public bool HasLevel
        {
            get
            {
                return plan.RecommendedLevel!=0;
            }
        }

        public bool IsFoodPlan
        {
            get
            {
                return PLANTYPE.FOODANDDRINK==plan.PlanType;
            }
        }
        public bool IsSpectaclePlan
        {
            get
            {
                return PLANTYPE.SPECTACLES == plan.PlanType && !string.IsNullOrEmpty(plan.Link);
            }
        }
        public bool IsSportPlan
        {
            get
            {
                return PLANTYPE.SPORT == plan.PlanType;
            }
        }
        public bool IsLanguagePlan
        {
            get
            {
                return PLANTYPE.LANGUAGE == plan.PlanType;
            }
        }
        public bool IsTravelPlan
        {
            get
            {
                return PLANTYPE.TRAVEL == plan.PlanType;
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
                RaisePropertyChanged( "IsHost" );
                RaisePropertyChanged( "IsNotHost" );
                RaisePropertyChanged("IsPlanAvaliable");

                RaisePropertyChanged("HasLink");
                RaisePropertyChanged("HasLevel");
                RaisePropertyChanged("IsFoodPlan");
                RaisePropertyChanged("IsSpectaclePlan");
                RaisePropertyChanged("IsSportPlan");
                RaisePropertyChanged("IsLanguagePlan");
                RaisePropertyChanged("IsTravelPlan");
            }

            return base.InitializeAsync(navigationData);
        }
        #endregion
        #region Commands
        public ICommand OpenImageCommand
        {
            get
            {
                return new RelayCommand(OpenImage);
            }
        }
        public ICommand HostProfileCommand
        {
            get
            {
                return new RelayCommand( GoToHostProfile );
            }
        }

        public ICommand GoToCommentsCommand
        {
            get
            {
                return new RelayCommand(GoToComments);
            }
        }

        public ICommand ButtonCommand
        {
            get
            {
                return new RelayCommand(ClickOnButton);
            }
        }

        public ICommand GoToLinkCommand
        {
            get
            {
                return new RelayCommand(GoToLink);
            }
        }

        #endregion
        #region Methods
        private void OpenImage()
        {
            List<string> images = new List<string>();
            if (Plan.HasImage)
            {
                foreach (Common.Models.Image image in Plan.PlanImages)
                {
                    images.Add(image.ImageFullPath);
                }
                NavigationService.NavigateToAsync<ImageFullScreenViewModel>(images);
            }
        }
        private void GoToHostProfile()
        {
            NavigationService.NavigateToAsync<ProfileViewModel>(plan.User);
        }
        private void GoToComments()
        {
            NavigationService.NavigateToAsync<CommentsViewModel>(plan);
        }
        private void ClickOnButton()
        {
            if ( IsHost )
            {
                EditPlan();
            }
            else
            {
                if ( plan.AssistantUsers.Find( assistant => assistant.Id == LoggedUser.Id ) == null )
                {
                    JoinPlan();
                }
                else
                {
                    UnJoinPlan();
                }
            }
        }
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
                ToastNotificationUtils.ShowErrorToastNotifications("Ha habido un error al unirse. Intentelo de nuevo más tarde");
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
                ToastNotificationUtils.ShowErrorToastNotifications("Ha habido un error al desvincularse del plan. Intentelo de nuevo más tarde");
            }
            else
            {
                plan.AssistantUsers.Remove(plan.AssistantUsers.Single(s => s.Id == LoggedUser.Id));
                RaisePropertyChanged("Plan");
                RaisePropertyChanged("Assistants");
                RaisePropertyChanged("ButtonStyle");
            }
        }
        private void EditPlan()
        {
            NavigationService.NavigateToAsync<NewPlanStep1ViewModel>(Plan);
        }
        private async void CancelPlan()
        {
            if (Plan.AssistantUsers.Count > 1)
            {
                var source = await Application.Current.MainPage.DisplayActionSheet(
                    "Hay usuarios que asistirán a tu plan. ¿Estás seguro de eliminarlo?",
                    "Cancelar",
                    null,
                    "Aceptar");
                if (source == "Cancelar")
                {
                    return;
                }
                else if (source == "Aceptar")
                {
                    //Cancelar el plan
                }
            }
            else
            {
                var source = await Application.Current.MainPage.DisplayActionSheet(
                    "¿Estás seguro de eliminar tu plan?",
                    "Cancelar",
                    null,
                    "Aceptar");
                if (source == "Cancelar")
                {
                    return;
                }
                else if (source == "Aceptar")
                {
                    //DataService.GetInstance().CancelPlan();
                }
            }
        }   
        public void GoToLink()
        {
            Device.OpenUri(new Uri(plan.Link));
        }

        #endregion
    }
}
