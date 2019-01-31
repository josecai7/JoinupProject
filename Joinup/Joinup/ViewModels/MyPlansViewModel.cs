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
        private async void LoadPlans()
        {
            IsRefreshing = true;

            var connection = await ApiService.GetInstance().CheckConnection();
            if (connection.IsSuccess)
            {
                var response = await DataService.GetInstance().GetPlansByUserId(LoggedUser.Id);

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert(response.Message, "Aceptar", "Cancelar");
                    IsRefreshing = false;
                    return;
                }

                var myPlansList = (List<Plan>)response.Result;

                myPlans.Clear();

                var nextPlans = myPlansList.Where(item => item.EndPlanDate >= DateTime.Now).OrderBy(item=>item.PlanDate);
                nextPlans.Select(p => { p.LoggedUser = LoggedUser; return p; }).ToList();
                myPlans.Add(new Grouping<string, Plan>("Próximos planes", new ObservableCollection<Plan>(nextPlans)));

                var finishedPlans = myPlansList.Where(item=>item.EndPlanDate<DateTime.Now).OrderByDescending(item => item.PlanDate);
                finishedPlans.Select(p => { p.LoggedUser = LoggedUser; return p; }).ToList();
                myPlans.Add(new Grouping<string, Plan>("Planes finalizados", new ObservableCollection<Plan>(finishedPlans)));

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
