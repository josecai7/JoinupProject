using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Utils;
using Joinup.ViewModels.Base;
using Joinup.Views;
using Plugin.Toasts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class NewPlanStep1ViewModel:BaseViewModel
    {
        #region Attributes

        private Plan plan;
        private int selectedCategory = 0;

        private bool isRestaurantSelected;
        private bool isTakeSomethingSelected;
        private bool isDoSportsSelected;
        private bool isSpectaclesSelected;
        private bool isLanguageExchangesSelected;
        private bool isTravelSelected;
        private bool isShoppingSelected;
        private bool isGoOutForDrinksSelected;
        private bool isOtherSelected;
        #endregion
        #region Properties
        public bool IsRestaurantSelected
        {
            get { return this.isRestaurantSelected; }
            set
            {
                isRestaurantSelected = value;
                RaisePropertyChanged( "IsRestaurantSelected" );
            }
        }
        public bool IsTakeSomethingSelected
        {
            get { return this.isTakeSomethingSelected; }
            set
            {
                isTakeSomethingSelected = value;
                RaisePropertyChanged( "IsTakeSomethingSelected" );
            }
        }
        public bool IsDoSportsSelected
        {
            get { return this.isDoSportsSelected; }
            set
            {
                isDoSportsSelected = value;
                RaisePropertyChanged( "IsDoSportsSelected" );
            }
        }
        public bool IsSpectaclesSelected
        {
            get { return this.isSpectaclesSelected; }
            set
            {
                isSpectaclesSelected = value;
                RaisePropertyChanged( "IsSpectaclesSelected" );
            }
        }
        public bool IsLanguageExchangesSelected
        {
            get { return this.isLanguageExchangesSelected; }
            set
            {
                isLanguageExchangesSelected = value;
                RaisePropertyChanged( "IsLanguageExchangesSelected" );
            }
        }
        public bool IsTravelSelected
        {
            get { return this.isTravelSelected; }
            set
            {
                isTravelSelected = value;
                RaisePropertyChanged( "IsTravelSelected" );
            }
        }
        public bool IsShoppingSelected
        {
            get { return this.isShoppingSelected; }
            set
            {
                isShoppingSelected = value;
                RaisePropertyChanged( "IsShoppingSelected" );
            }
        }
        public bool IsGoOutForDrinksSelected
        {
            get { return this.isGoOutForDrinksSelected; }
            set
            {
                isGoOutForDrinksSelected = value;
                RaisePropertyChanged( "IsGoOutForDrinksSelected" );
            }
        }
        public bool IsOtherSelected
        {
            get { return this.isOtherSelected; }
            set
            {
                isOtherSelected = value;
                RaisePropertyChanged( "IsOtherSelected" );
            }
        }

        #endregion
        #region Constructors
        public NewPlanStep1ViewModel()
        {
            plan = new Plan();
            MessagingCenter.Subscribe<NewPlanStep1ViewModel,Plan>(this, "UpdatePlan", (sender,arg) => {
                plan = arg;
            });
        }
        #endregion

        #region Commands
        public ICommand SelectRestaurantCategoryCommand
        {
            get
            {
                return new RelayCommand(SelectRestaurantCategory);
            }
        }
        public ICommand SelectTakeSomethingCategoryCommand
        {
            get
            {
                return new RelayCommand(SelectTakeSomethingCategory);
            }
        }
        public ICommand SelectDoSportsCategoryCommand
        {
            get
            {
                return new RelayCommand(SelectDoSportsCategory);
            }
        }
        public ICommand SelectSpectaclesCategoryCommand
        {
            get
            {
                return new RelayCommand(SelectSpectaclesCategory);
            }
        }
        public ICommand SelectLanguageExchangesCategoryCommand
        {
            get
            {
                return new RelayCommand(SelectLanguageExchangesCategory);
            }
        }
        public ICommand SelectTravelCategoryCommand
        {
            get
            {
                return new RelayCommand(SelectTravelCategory);
            }
        }
        public ICommand SelectShoppingCategoryCommand
        {
            get
            {
                return new RelayCommand(SelectShoppingCategory);
            }
        }
        public ICommand SelectGoOutForDrinksCategoryCommand
        {
            get
            {
                return new RelayCommand(SelectGoOutForDrinksCategory);
            }
        }
        public ICommand SelectOtherCategoryCommand
        {
            get
            {
                return new RelayCommand(SelectOtherCategory);
            }
        }
        public ICommand NextStepCommand
        {
            get
            {
                return new RelayCommand( NextStepAsync );
            }
        }
        #endregion

        #region Methods
        private void SelectRestaurantCategory()
        {

            if (IsRestaurantSelected)
            {
                selectedCategory = PlanType.UNDEFINED;
                IsRestaurantSelected = false;
            }
            else
            {
                DeselectAll();
                selectedCategory = PlanType.RESTAURANT;
                IsRestaurantSelected = true;
            }
        }

        private void SelectTakeSomethingCategory()
        {
            if (IsTakeSomethingSelected)
            {
                selectedCategory = PlanType.UNDEFINED;
                IsTakeSomethingSelected = false;
            }
            else
            {
                DeselectAll();
                selectedCategory = PlanType.TAKESOMETHING;
                IsTakeSomethingSelected = true;
            }
        }

        private void SelectDoSportsCategory()
        {
            if (IsDoSportsSelected)
            {
                selectedCategory = PlanType.UNDEFINED;
                IsDoSportsSelected = false;
            }
            else
            {
                DeselectAll();
                selectedCategory = PlanType.SPORT;
                IsDoSportsSelected = true;
            }
        }

        private void SelectSpectaclesCategory()
        {
            if (IsSpectaclesSelected)
            {
                selectedCategory = PlanType.UNDEFINED;
                IsSpectaclesSelected = false;
            }
            else
            {
                DeselectAll();
                selectedCategory = PlanType.SPECTACLE;
                IsSpectaclesSelected = true;
            }
        }

        private void SelectLanguageExchangesCategory()
        {
            if (IsLanguageExchangesSelected)
            {
                selectedCategory = PlanType.UNDEFINED;
                IsLanguageExchangesSelected = false;
            }
            else
            {
                DeselectAll();
                selectedCategory = PlanType.LANGUAGE;
                IsLanguageExchangesSelected = true;
            }
        }

        private void SelectTravelCategory()
        {
            if (IsTravelSelected)
            {
                selectedCategory = PlanType.UNDEFINED;
                IsTravelSelected = false;
            }
            else
            {
                DeselectAll();
                selectedCategory = PlanType.TRAVEL;
                IsTravelSelected = true;
            }
        }

        private void SelectShoppingCategory()
        {
            if (IsShoppingSelected)
            {
                selectedCategory = PlanType.UNDEFINED;
                IsShoppingSelected = false;
            }
            else
            {
                DeselectAll();
                selectedCategory = PlanType.SHOPPING;
                IsShoppingSelected = true;
            }
        }

        private void SelectGoOutForDrinksCategory()
        {
            if (IsGoOutForDrinksSelected)
            {
                selectedCategory = PlanType.UNDEFINED;
                IsGoOutForDrinksSelected = false;
            }
            else
            {
                DeselectAll();
                selectedCategory = PlanType.GOOUTFORDRINK;
                IsGoOutForDrinksSelected = true;
            }
        }

        private void SelectOtherCategory()
        {
            if (IsOtherSelected)
            {
                selectedCategory = PlanType.UNDEFINED;
                IsOtherSelected = false;
            }
            else
            {
                DeselectAll();
                selectedCategory = PlanType.OTHER;
                IsOtherSelected = true;
            }
        }

        private void DeselectAll()
        {
            selectedCategory = PlanType.UNDEFINED;
            IsRestaurantSelected = IsTakeSomethingSelected=IsDoSportsSelected=IsSpectaclesSelected=IsLanguageExchangesSelected=IsTravelSelected=IsShoppingSelected=IsGoOutForDrinksSelected=IsOtherSelected= false;
        }

        private void NextStepAsync()
        {
            if (selectedCategory == PlanType.UNDEFINED)
            {
                ToastNotificationUtils.ShowToastNotifications("Ups...Debes seleccionar una categoria", "add.png", ColorUtils.ErrorColor);
            }
            else
            {               
                plan.PlanType = selectedCategory;

                NavigationService.NavigateToAsync<NewPlanStep2ViewModel>( plan );
            }
        }
        #endregion
    }
}
