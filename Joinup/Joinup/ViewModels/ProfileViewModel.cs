using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Helpers;
using Joinup.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        #region Attributes
        private MyUserASP user;
        private ObservableCollection<Plan> planList;
        private bool plansTab;
        private bool tab2;
        private bool isRefreshing;
        #endregion
        #region Properties
        public ObservableCollection<Plan> PlanList
        {
            get
            {
                return new ObservableCollection<Plan>( planList.OrderBy( x => x.PlanDate ) );
            }
            set
            {
                planList = value;
                RaisePropertyChanged( "PlanList" );
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
            planList = new ObservableCollection<Plan>();
            user = LoggedUser;
            LoadPlans();
            SetPlansTab();
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
                PlanList = new ObservableCollection<Plan>( list.Where( item => item.UserId == User.Id) );
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