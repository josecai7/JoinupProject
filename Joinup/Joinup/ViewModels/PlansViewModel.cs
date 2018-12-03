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
                return new ObservableCollection<Plan>( filteredList.OrderBy( x => x.PlanDate ) );
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
                return searchText;
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

        private void ApplyFilters()
        {
            List<Plan> filteredList = new List<Plan>();
            if ( !string.IsNullOrEmpty( searchText ) )
            {
                filteredList.AddRange(plans.Where(item=>item.Name.ToLower().Contains(searchText.ToLower())));
            }

            if (IsFilterNearActive)
            {

            }
            if (IsFilterLanguageActive)
            {

            }
            if (IsFilterSportActive)
            {

            }
            if (IsFilterTodayActive)
            {

            }

            FilteredPlanList = new ObservableCollection<Plan>( filteredList );
        }
        #endregion
    }
}
