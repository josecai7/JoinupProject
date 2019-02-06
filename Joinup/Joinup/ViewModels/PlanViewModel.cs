using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Common.Models.DatabaseModels;
using Joinup.Common.Models.SelectablesModels;
using Joinup.Helpers;
using Joinup.Service;
using Joinup.Utils;
using Joinup.ViewModels.Base;
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
        bool isEnabled = true;
        bool isRunning = false;
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
                if (IsHost && plan.PlanDate > DateTime.Now)
                {
                    return Application.Current.Resources["ModifyPlanButton"] as Style;
                }
                else if (IsHost && plan.PlanDate <= DateTime.Now)
                {
                    IsEnabled = false;
                    return Application.Current.Resources["EndPlanButton"] as Style;
                }
                else
                {
                    if (plan.PlanDate <= DateTime.Now)
                    {
                        if (plan.Remarks.Find(item => item.UserId == LoggedUser.Id) == null)
                        {
                            return Application.Current.Resources["RemarkPlanButton"] as Style;
                        }
                        else
                        {
                            IsEnabled = false;
                            return Application.Current.Resources["EndPlanButton"] as Style;
                        }
                    }
                    else
                    {
                        if (plan.Meets.Find(meet => meet.UserId == LoggedUser.Id) == null)
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

        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                isRunning = value;
                RaisePropertyChanged("IsRunning");
            }
        }
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
                RaisePropertyChanged("IsEnabled");
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
        public ICommand RemarksCommand
        {
            get
            {
                return new RelayCommand(GoToRemarks);
            }
        }

        #endregion
        #region Methods
        private void OpenImage()
        {
            List<string> images = new List<string>();
            if (Plan.HasImage)
            {
                foreach (Common.Models.Image image in Plan.Images)
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
        private void GoToRemarks()
        {
            NavigationService.NavigateToAsync<RemarksViewModel>(plan);
        }
        private void ClickOnButton()
        {
            if ( IsHost )
            {
                EditPlan();
            }
            else
            {
                if (plan.PlanDate <= DateTime.Now)
                {
                    NavigationService.NavigateToAsync<RemarkPlanViewModel>(Plan);
                }
                else
                {
                    if (plan.Meets.Find(meet => meet.UserId == LoggedUser.Id) == null)
                    {
                        JoinPlan();
                    }
                    else
                    {
                        UnJoinPlan();
                    }
                }
            }
        }
        private void JoinUnJoinPlan()
        {
            if (plan.Meets.Find(meet => meet.UserId == LoggedUser.Id) == null)
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
            IsRunning = true;
            IsEnabled = false;
            var response = await DataService.GetInstance().JoinAPlan(plan.PlanId, LoggedUser.Id,false);

            if (!response.IsSuccess)
            {
                ToastNotificationUtils.ShowErrorToastNotifications("Ha habido un error al unirse. Intentelo de nuevo más tarde");
            }
            else
            {
                plan.Meets.Add(response.Result as Meet);
                RaisePropertyChanged("Plan");
                RaisePropertyChanged("ButtonStyle");
                MessagingCenter.Send( ViewModelLocator.Instance.Resolve<PlansViewModel>(), "RefreshPlans" );
                MessagingCenter.Send( ViewModelLocator.Instance.Resolve<MyPlansViewModel>(), "RefreshPlans" );
                MessagingCenter.Send( ViewModelLocator.Instance.Resolve<ProfileViewModel>(), "RefreshPlans" );
            }
            IsRunning = false;
            IsEnabled = true;
        }
        private async void UnJoinPlan()
        {
            IsRunning = true;
            IsEnabled = false;
            var response = await DataService.GetInstance().UnJoinAPlan(plan.PlanId, LoggedUser.Id);

            if (!response.IsSuccess)
            {
                ToastNotificationUtils.ShowErrorToastNotifications("Ha habido un error al desvincularse del plan. Intentelo de nuevo más tarde");
            }
            else
            {
                plan.Meets.Remove(plan.Meets.Single(meet => meet.UserId == LoggedUser.Id));
                RaisePropertyChanged("Plan");
                RaisePropertyChanged("ButtonStyle");
                MessagingCenter.Send( ViewModelLocator.Instance.Resolve<PlansViewModel>(), "RefreshPlans" );
                MessagingCenter.Send( ViewModelLocator.Instance.Resolve<MyPlansViewModel>(), "RefreshPlans" );
                MessagingCenter.Send( ViewModelLocator.Instance.Resolve<ProfileViewModel>(), "RefreshPlans" );
            }
            IsRunning = false;
            IsEnabled = true;
        }
        private void EditPlan()
        {
            NavigationService.NavigateToAsync<NewPlanStep1ViewModel>(Plan);
        }
        public void GoToLink()
        {
            Device.OpenUri(new Uri(plan.Link));
        }

        #endregion
    }
}
