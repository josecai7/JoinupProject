using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Common.Models.DatabaseModels;
using Joinup.Helpers;
using Joinup.Navigation;
using Joinup.Service;
using Joinup.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        #region Attributes
        private MyUserASP user;
        private ObservableCollection<Plan> publishedPlanList;
        private ObservableCollection<Remark> remarksList;
        private bool plansTab;
        private bool tab2;
        private bool isRefreshing;
        private bool isRefreshing2;
        #endregion
        #region Properties
        public ObservableCollection<Remark> RemarksList
        {
            get
            {
                return new ObservableCollection<Remark>(remarksList.OrderByDescending( x => x.CommentDate ) );
            }
            set
            {
                remarksList = value;
                RaisePropertyChanged("RemarksList");
                RaisePropertyChanged("Average");
            }
        }
        public ObservableCollection<Plan> PublishedPlanList
        {
            get
            {
                return new ObservableCollection<Plan>( publishedPlanList.OrderBy( x => x.PlanDate ) );
            }
            set
            {
                publishedPlanList = value;
                RaisePropertyChanged( "PublishedPlanList" );
            }
        }
        public bool IsRefreshing
        {
            get
            {
                return isRefreshing;
            }
            set
            {
                isRefreshing = value;
                RaisePropertyChanged( "IsRefreshing" );
            }
        }
        public bool IsRefreshing2
        {
            get
            {
                return isRefreshing2;
            }
            set
            {
                isRefreshing2 = value;
                RaisePropertyChanged("IsRefreshing2");
            }
        }
        public bool PlansTab
        {
            get { return plansTab; }
            set
            {
                plansTab = value;
                RaisePropertyChanged();
            }
        }
        public bool Tab2
        {
            get { return tab2; }
            set
            {
                tab2 = value;
                RaisePropertyChanged();
            }
        }
        public MyUserASP User
        {
            get
            {
                return user;
            }
            set
            {
                user = value;
                RaisePropertyChanged();
            }
        }
        public int Average
        {
            get
            {
                if (RemarksList.Count == 0)
                    return 0;
                else
                    return (int)RemarksList.Average(item => item.Score);
            }
        }
        public bool IsLoggedUser
        {
            get
            {
                return user.Id==LoggedUser.Id;
            }
        }
        #endregion
        #region Constructors
        public ProfileViewModel()
        {
            publishedPlanList = new ObservableCollection<Plan>();
            remarksList = new ObservableCollection<Remark>();
            user = LoggedUser;
            LoadPlans();
            LoadRemarks();
            SetPlansTab();
            RaisePropertyChanged( "IsLoggedUser" );
        }

        public override Task InitializeAsync(object navigationData)
        {
            User = (MyUserASP) navigationData;
            string s=Settings.AccessToken;
            LoadPlans();
            LoadRemarks();
            RaisePropertyChanged("IsLoggedUser");           

            return base.InitializeAsync( navigationData );
        }
        #endregion
        #region Commands
        public ICommand ClickOnPlan
        {
            get
            {
                return new Command((item) =>
                {
                    try
                    {
                        NavigationService.NavigateToAsync<PlanViewModel>((Plan)item);
                    }
                    catch (Exception exc)
                    {

                    }

                });
            }
        }
        public ICommand OpenImageCommand
        {
            get
            {
                return new RelayCommand(OpenImage);
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand( LoadPlans );
            }
        }
        public ICommand RefreshRemarksCommand
        {
            get
            {
                return new RelayCommand(LoadRemarks);
            }
        }
        public ICommand PlansCommand
        {
            get
            {
                return new RelayCommand(SetPlansTab);
            }
        }

        public ICommand Tab2Command
        {
            get
            {
                return new RelayCommand(SetTab2);
            }
        }
        public ICommand LogoutCommand
        {
            get
            {
                return new RelayCommand( DoLogout );
            }
        }
        #endregion
        #region Methods
        private void OpenImage()
        {
            List<string> images = new List<string>();
            if (!string.IsNullOrEmpty(User.UserImage))
            {
                images.Add(User.UserImage);
                NavigationService.NavigateToAsync<ImageFullScreenViewModel>(images);
            }              
        }
        private async void LoadPlans()
        {
            IsRefreshing = true;

            var connection = await ApiService.GetInstance().CheckConnection();
            if ( connection.IsSuccess )
            {
                var response = await DataService.GetInstance().GetPlansByUserId(User.Id);

                if ( !response.IsSuccess )
                {
                    await Application.Current.MainPage.DisplayAlert(response.Message, "Aceptar", "Cancelar" );
                    IsRefreshing = false;
                    return;
                }

                var list = (List<Plan>) response.Result;
                PublishedPlanList = new ObservableCollection<Plan>( list.Where(item=>item.UserId==User.Id).OrderByDescending(item=>item.PlanDate) );                
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert( "Revisa tu conexión a internet", "Aceptar", "Cancelar" );
                IsRefreshing = false;
                return;
            }

            IsRefreshing = false;
        }
        private async void LoadRemarks()
        {
            IsRefreshing2 = true;

            var connection = await ApiService.GetInstance().CheckConnection();
            if (connection.IsSuccess)
            {
                var response = await DataService.GetInstance().GetRemarksByUserId(User.Id);

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(response.Message, "Aceptar", "Cancelar");
                    IsRefreshing2 = false;
                    return;
                }

                var list = (List<Remark>)response.Result;
                RemarksList = new ObservableCollection<Remark>(list.OrderByDescending(item=>item.CommentDate));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Revisa tu conexión a internet", "Aceptar", "Cancelar");
                IsRefreshing2 = false;
                return;
            }

            IsRefreshing2 = false;
        }

        private void SetPlansTab()
        {
            PlansTab = true;
            Tab2 = false;
        }

        private void SetTab2()
        {
            PlansTab = false;
            Tab2 = true;
        }

        private void DoLogout()
        {
            Settings.IsRemembered = false;
            var navigationService = ViewModelLocator.Instance.Resolve<INavigationService>();
            navigationService.InitializeAsync();
        }
        #endregion
    }
}