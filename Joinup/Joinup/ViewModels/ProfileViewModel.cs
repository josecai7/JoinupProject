using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Helpers;
using Joinup.Service;
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
        private ObservableCollection<Plan> assistPlanList;
        private bool plansTab;
        private bool tab2;
        private bool isRefreshing;
        #endregion
        #region Properties
        public ObservableCollection<Plan> AssistPlanList
        {
            get
            {
                return new ObservableCollection<Plan>( assistPlanList.OrderBy( x => x.PlanDate ) );
            }
            set
            {
                assistPlanList = value;
                RaisePropertyChanged( "AssistPlanList" );
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
        #endregion
        #region Constructors
        public ProfileViewModel()
        {
            publishedPlanList = new ObservableCollection<Plan>();
            assistPlanList = new ObservableCollection<Plan>();
            user = LoggedUser;
            LoadPlans();
            SetTab2();
        }

        public override Task InitializeAsync(object navigationData)
        {
            User = (MyUserASP) navigationData;
            LoadPlans();

            return base.InitializeAsync( navigationData );
        }
        #endregion
        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand( LoadPlans );
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
        #endregion
        #region Methods
        private async void LoadPlans()
        {
            IsRefreshing = true;

            var connection = await ApiService.GetInstance().CheckConnection();
            if ( connection.IsSuccess )
            {
                var response = await DataService.GetInstance().GetPlans();

                if ( !response.IsSuccess )
                {
                    await Application.Current.MainPage.DisplayAlert( "Error", "Aceptar", "Cancelar" );
                    IsRefreshing = false;
                    return;
                }

                var list = (List<Plan>) response.Result;
                PublishedPlanList = new ObservableCollection<Plan>( list.Where( item => item.UserId == User.Id) );
                AssistPlanList = new ObservableCollection<Plan>( list.Where( item => item.AssistantUsers.Find(user=> user.Id==LoggedUser.Id)!=null ) );
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert( "Revisa tu conexión a internet", "Aceptar", "Cancelar" );
                IsRefreshing = false;
                return;
            }

            IsRefreshing = false;
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
        #endregion
    }
}