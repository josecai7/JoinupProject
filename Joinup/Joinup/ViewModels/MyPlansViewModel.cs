using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Service;
using Joinup.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class MyPlansViewModel:BaseViewModel
    {
        #region Attributes
        ObservableCollection<Grouping<string, Plan>> myPlans;
        private bool isRefreshing;
        #endregion
        #region Properties
        public ObservableCollection<Grouping<string, Plan>> MyPlans
        {
            get
            {
                return myPlans;
            }
            set
            {
                myPlans = value;
                RaisePropertyChanged("MyPlans");
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
                RaisePropertyChanged("IsRefreshing");
            }
        }
        #endregion
        #region Constructors
        public MyPlansViewModel()
        {
            myPlans = new ObservableCollection<Grouping<string, Plan>>();

            LoadPlans();
        }
        #endregion
        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadPlans);
            }
        }
        #endregion
        #region Methods
        private async void LoadPlans()
        {
            IsRefreshing = true;

            var connection = await ApiService.GetInstance().CheckConnection();
            if (connection.IsSuccess)
            {
                var response = await DataService.GetInstance().GetPlans();

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(response.Message, "Aceptar", "Cancelar");
                    IsRefreshing = false;
                    return;
                }

                var myPlansList = (List<Plan>)response.Result;

                myPlans.Clear();
                myPlans.Add(new Grouping<string, Plan>("Planes finalizados", new ObservableCollection<Plan>(myPlansList)));

                RaisePropertyChanged("MyPlans");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Revisa tu conexión a internet", "Aceptar", "Cancelar");
                IsRefreshing = false;
                return;
            }

            IsRefreshing = false;
        }
        #endregion
    }
}
