using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Helpers;
using Joinup.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class PlansViewModel : BaseViewModel
    {
        #region Attributes
        private ApiService apiService;
        private ObservableCollection<Plan> planList;
        #endregion

        #region Properties
        public ObservableCollection<Plan> PlanList
        {
            get
            {
                return planList;
            }
            set
            {
                planList = value;
                RaisePropertyChanged( "PlanList" );
            }
        }

        private bool isRefreshing;

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
        #endregion

        #region Constructors
        public PlansViewModel()
        {
            instance = this;
            apiService = new ApiService();
            LoadPlans();
        }
        #endregion

        #region Singleton

        private static PlansViewModel instance;

        public static PlansViewModel GetInstance()
        {
            if ( instance == null )
            {
                return new PlansViewModel();
            }
            else
            {
                return instance;
            }
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
        public ICommand NewPlanCommand
        {
            get
            {
                return new RelayCommand( GoToNewPlan );
            }
        }
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
        #endregion

        #region Methods
        private void GoToNewPlan()
        {
            try
            {
                NavigationService.NavigateToAsync<NewPlanStep1ViewModel>();
            }
            catch (Exception exc)
            {

            }
        }
        private async void LoadPlans()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if ( connection.IsSuccess )
            {
                var url = Application.Current.Resources["UrlAPI"].ToString();
                var prefix = Application.Current.Resources["UrlPrefix"].ToString();
                var controller = Application.Current.Resources["UrlPlansController"].ToString();

                var response = await apiService.GetList<Plan>( url, prefix, controller, Settings.TokenType, Settings.AccessToken);

                if ( !response.IsSuccess )
                {
                    await Application.Current.MainPage.DisplayAlert( "Error", "Aceptar", "Cancelar" );
                    return;
                }

                var list = (List<Plan>) response.Result;
                PlanList = new ObservableCollection<Plan>( list );
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert( "Revisa tu conexión a internet", "Aceptar", "Cancelar" );
                return;
            }

            IsRefreshing = false;
        }
        #endregion
    }
}
