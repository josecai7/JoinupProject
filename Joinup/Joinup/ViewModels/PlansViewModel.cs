using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Helpers;
using Joinup.Service;
using Joinup.Utils;
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
        private bool isFilterWeekActive;
        private bool isFilterLanguageActive;
        private bool isFilterSportActive;
        private bool isFilterFoodActive;
        private bool isFilterSpectaclesActive;
        private bool isFilterTravelActive;
        private bool isFilterShoppingActive;
        private bool isFilterPartyActive;
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
                if (isFilterTodayActive)
                {
                    IsFilterWeekActive = false;
                }
                RaisePropertyChanged("IsFilterTodayActive");
                ApplyFilters();
            }
        }
        public bool IsFilterWeekActive
        {
            get
            {
                return isFilterWeekActive;
            }
            set
            {
                isFilterWeekActive = value;
                if (isFilterWeekActive)
                {
                    IsFilterTodayActive = false;
                }
                RaisePropertyChanged("IsFilterWeekActive");
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
        public bool IsFilterFoodActive
        {
            get
            {
                return isFilterFoodActive;
            }
            set
            {
                isFilterFoodActive = value;
                RaisePropertyChanged("IsFilterFoodActive");
                ApplyFilters();
            }
        }
        public bool IsFilterSpectaclesActive
        {
            get
            {
                return isFilterSpectaclesActive;
            }
            set
            {
                isFilterSpectaclesActive = value;
                RaisePropertyChanged("IsFilterSpectaclesActive");
                ApplyFilters();
            }
        }
        public bool IsFilterTravelActive
        {
            get
            {
                return isFilterTravelActive;
            }
            set
            {
                isFilterTravelActive = value;
                RaisePropertyChanged("IsFilterTravelActive");
                ApplyFilters();
            }
        }
        public bool IsFilterShoppingActive
        {
            get
            {
                return isFilterShoppingActive;
            }
            set
            {
                isFilterShoppingActive = value;
                RaisePropertyChanged("IsFilterShoppingActive");
                ApplyFilters();
            }
        }
        public bool IsFilterPartyActive
        {
            get
            {
                return isFilterPartyActive;
            }
            set
            {
                isFilterPartyActive = value;
                RaisePropertyChanged("IsFilterPartyActive");
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
                bool included = true;
                if (!plan.Name.ToLower().Contains(SearchText.ToLower()) && !string.IsNullOrEmpty(searchText))
                {
                    included = false;
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
                if (IsFilterFoodActive)
                {
                    types.Add(PLANTYPE.FOODANDDRINK);
                }
                if (IsFilterPartyActive)
                {
                    types.Add(PLANTYPE.PARTY);
                }
                if (IsFilterShoppingActive)
                {
                    types.Add(PLANTYPE.SHOPPING);
                }
                if (IsFilterSpectaclesActive)
                {
                    types.Add(PLANTYPE.SPECTACLES);
                }
                if (IsFilterTravelActive)
                {
                    types.Add(PLANTYPE.TRAVEL);
                }

                if (types.Count>0 && !types.Contains(plan.PlanType))
                {
                    included = false;
                }

                if (IsFilterTodayActive && plan.PlanDate.Date != DateTime.Now.Date)
                {
                    included = false;
                }

                if (IsFilterWeekActive && plan.PlanDate.Date > DateTimeUtils.GetLastWeekDay())
                {
                    included = false;
                }

                if (IsFilterNearActive && DistanceHelper.GetInstance().GetDistance(plan.Longitude, plan.Latitude).Result>15)
                {
                    included = false;
                }

                if(included)
                    filteredList.Add(plan);
            }

          
            FilteredPlanList = new ObservableCollection<Plan>( filteredList );
        }
        #endregion
    }
}
