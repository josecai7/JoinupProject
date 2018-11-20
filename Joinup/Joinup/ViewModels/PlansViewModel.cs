using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Helpers;
using Joinup.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class PlansViewModel : BaseViewModel
    {
        #region Attributes
        private ObservableCollection<Plan> planList;
        private string searchText;
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

        
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                searchText = value;
                RaisePropertyChanged( "SearchText" );
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
        #endregion

        #region Constructors
        public PlansViewModel()
        {
            planList = new ObservableCollection<Plan>();

            MessagingCenter.Subscribe<PlansViewModel>( this, "AddNewPlan", (sender) => {
                LoadPlans();
            } );

            LoadPlans();
        }
        #endregion

        #region Singleton

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

            var connection = await ApiService.GetInstance().CheckConnection();
            if ( connection.IsSuccess )
            {
                var url = Application.Current.Resources["UrlAPI"].ToString();
                var prefix = Application.Current.Resources["UrlPrefix"].ToString();
                var controller = Application.Current.Resources["UrlPlansController"].ToString();

                var response = await ApiService.GetInstance().GetList<Plan>( url, prefix, controller, Settings.TokenType, Settings.AccessToken);

                if ( !response.IsSuccess )
                {
                    await Application.Current.MainPage.DisplayAlert( "Error", "Aceptar", "Cancelar" );
                    IsRefreshing = false;
                    return;
                }

                var list = (List<Plan>) response.Result;
                PlanList = new ObservableCollection<Plan>( list );
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert( "Revisa tu conexión a internet", "Aceptar", "Cancelar" );
                IsRefreshing = false;
                return;
            }

            IsRefreshing = false;
        }
        #endregion
    }
}
