using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
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
        private ApiService apiService;

        private ObservableCollection<Plan> planList;

        public ObservableCollection<Plan> PlanList
        {
            get
            {
                return planList;
            }
            set
            {
                SetValue( ref planList, value );
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
                SetValue( ref isRefreshing, value );
            }
        }

        public PlansViewModel()
        {
            apiService = new ApiService();
            LoadPlans();
        }

        private async void LoadPlans()
        {
            IsRefreshing = true;

            var connection = await apiService.CheckConnection();
            if ( connection.IsSuccess )
            {
                //string url = Application.Current.Resources["UrlAPI"].ToString();

                var response = await apiService.GetList<Plan>( "http://joinupapi.azurewebsites.net/", "/api", "/Plans" );

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

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand( LoadPlans );
            }
        }
    }
}
