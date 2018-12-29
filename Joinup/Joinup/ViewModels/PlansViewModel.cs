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
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class PlansViewModel : BaseViewModel
    {
        #region Attributes
        private Dictionary<string,string> filters=new Dictionary<string, string>();

        private ObservableCollection<Plan> planList;
        private ObservableCollection<Plan> filteredList;
        private string searchText;
        private bool isRefreshing;
        private bool isFilterNearActive;
        private bool isFilterTodayActive;
        private bool isFilterLanguageActive;
        private bool isFilterSportActive;
        #endregion

        #region Properties
        public ObservableCollection<Plan> FilteredPlanList
        {
            get
            {
                return new ObservableCollection<Plan>( filteredList.Where(x=>x.PlanDate>DateTime.Now).OrderBy( x => x.PlanDate ) );
            }
            set
            {
                filteredList = value;
                RaisePropertyChanged( "FilteredPlanList" );
            }
        }
     
        public string SearchText
        {
            get
            {
                return string.IsNullOrEmpty(searchText)?string.Empty:searchText;
            }
            set
            {
                searchText = value;
                RaisePropertyChanged( "SearchText" );
                ApplyFilters();
            }
        }
        public bool IsFilterNearActive
        {
            get
            {
                return isFilterNearActive;
            }
            set
            {
                isFilterNearActive = value;
                RaisePropertyChanged("IsFilterNearActive");
                ApplyFilters();
            }
        }
        public bool IsFilterTodayActive
        {
            get
            {
                return isFilterTodayActive;
            }
            set
            {
                isFilterTodayActive = value;
                RaisePropertyChanged("IsFilterTodayActive");
                ApplyFilters();
            }
        }
        public bool IsFilterLanguageActive
        {
            get
            {
                return isFilterLanguageActive;
            }
            set
            {
                isFilterLanguageActive = value;
                RaisePropertyChanged("IsFilterLanguageActive");
                ApplyFilters();
            }
        }
        public bool IsFilterSportActive
        {
            get
            {
                return isFilterSportActive;
            }
            set
            {
                isFilterSportActive = value;
                RaisePropertyChanged("IsFilterSportActive");
                ApplyFilters();
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

        public double ThirdScreenSize
        {
            get
            {
                return (Application.Current.MainPage.Width-40)/3;
            }
        }
        #endregion

        #region Constructors
        public PlansViewModel()
        {
            planList = new ObservableCollection<Plan>();
            filteredList = new ObservableCollection<Plan>();

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
            if (connection.IsSuccess)
            {
                var response = await DataService.GetInstance().GetPlans();

                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Aceptar", "Cancelar");
                    IsRefreshing = false;
                    return;
                }

                List<Plan> list = (List<Plan>)response.Result;
                list = list.Where(item => item.PlanDate >= DateTime.Now).ToList();
                FilteredPlanList = new ObservableCollection<Plan>(list);
                planList = new ObservableCollection<Plan>(list);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Revisa tu conexión a internet", "Aceptar", "Cancelar");
                IsRefreshing = false;
                return;
            }

            IsRefreshing = false;
        }

        private void ApplyFilters()
        {
            List<Plan> filteredList = new List<Plan>();

            foreach (Plan plan in planList)
            {
                if (!plan.Name.ToLower().Contains(SearchText.ToLower()) && !string.IsNullOrEmpty(searchText))
                {
                    break;
                }
                List<int> types = new List<int>();
                if (IsFilterLanguageActive)
                {
                    types.Add(PLANTYPE.LANGUAGE);
                }
                if (IsFilterSportActive)
                {
                    types.Add(PLANTYPE.SPORT);
                }

                if (types.Count>0 && !types.Contains(plan.PlanType))
                {
                    break;
                }

                if (IsFilterTodayActive && plan.PlanDate.Date != DateTime.Now.Date)
                {
                    break;
                }

                if(IsFilterNearActive && DistanceHelper.GetInstance().GetDistance(plan.Longitude, plan.Latitude).Result>15)
                {
                    break;
                }

                filteredList.Add(plan);
            }

          
            FilteredPlanList = new ObservableCollection<Plan>( filteredList );
        }
        #endregion
    }
}
