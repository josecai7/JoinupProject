using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Utils;
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
            set { this.SetValue(ref this.isRestaurantSelected, value); }
        }
        public bool IsTakeSomethingSelected
        {
            get { return this.isTakeSomethingSelected; }
            set { this.SetValue(ref this.isTakeSomethingSelected, value); }
        }
        public bool IsDoSportsSelected
        {
            get { return this.isDoSportsSelected; }
            set { this.SetValue(ref this.isDoSportsSelected, value); }
        }
        public bool IsSpectaclesSelected
        {
            get { return this.isSpectaclesSelected; }
            set { this.SetValue(ref this.isSpectaclesSelected, value); }
        }
        public bool IsLanguageExchangesSelected
        {
            get { return this.isLanguageExchangesSelected; }
            set { this.SetValue(ref this.isLanguageExchangesSelected, value); }
        }
        public bool IsTravelSelected
        {
            get { return this.isTravelSelected; }
            set { this.SetValue(ref this.isTravelSelected, value); }
        }
        public bool IsShoppingSelected
        {
            get { return this.isShoppingSelected; }
            set { this.SetValue(ref this.isShoppingSelected, value); }
        }
        public bool IsGoOutForDrinksSelected
        {
            get { return this.isGoOutForDrinksSelected; }
            set { this.SetValue(ref this.isGoOutForDrinksSelected, value); }
        }
        public bool IsOtherSelected
        {
            get { return this.isOtherSelected; }
            set { this.SetValue(ref this.isOtherSelected, value); }
        }

        #endregion
        #region Constructors
        public NewPlanStep1ViewModel()
        {
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
                return new RelayCommand( NextStep );
            }
        }
        #endregion

        #region Methods
        private void SelectRestaurantCategory()
        {

            if (IsRestaurantSelected)
            {
                selectedCategory = PlanType.RESTAURANT;
                IsRestaurantSelected = false;
            }
            else
            {
                DeselectAll();
                IsRestaurantSelected = true;
            }
        }

        private void SelectTakeSomethingCategory()
        {
            if (IsTakeSomethingSelected)
            {
                selectedCategory = PlanType.TAKESOMETHING;
                IsTakeSomethingSelected = false;
            }
            else
            {
                DeselectAll();
                IsTakeSomethingSelected = true;
            }
        }

        private void SelectDoSportsCategory()
        {
            if (IsDoSportsSelected)
            {
                selectedCategory = PlanType.SPORT;
                IsDoSportsSelected = false;
            }
            else
            {
                DeselectAll();
                IsDoSportsSelected = true;
            }
        }

        private void SelectSpectaclesCategory()
        {
            if (IsSpectaclesSelected)
            {
                selectedCategory = PlanType.SPECTACLE;
                IsSpectaclesSelected = false;
            }
            else
            {
                DeselectAll();
                IsSpectaclesSelected = true;
            }
        }

        private void SelectLanguageExchangesCategory()
        {
            if (IsLanguageExchangesSelected)
            {
                selectedCategory = PlanType.LANGUAGE;
                IsLanguageExchangesSelected = false;
            }
            else
            {
                DeselectAll();
                IsLanguageExchangesSelected = true;
            }
        }

        private void SelectTravelCategory()
        {
            if (IsTravelSelected)
            {
                selectedCategory = PlanType.TRAVEL;
                IsTravelSelected = false;
            }
            else
            {
                DeselectAll();
                IsTravelSelected = true;
            }
        }

        private void SelectShoppingCategory()
        {
            if (IsShoppingSelected)
            {
                selectedCategory = PlanType.SHOPPING;
                IsShoppingSelected = false;
            }
            else
            {
                IsShoppingSelected = true;
            }
        }

        private void SelectGoOutForDrinksCategory()
        {
            if (IsGoOutForDrinksSelected)
            {
                selectedCategory = PlanType.GOOUTFORDRINK;
                IsGoOutForDrinksSelected = false;
            }
            else
            {
                DeselectAll();
                IsGoOutForDrinksSelected = true;
            }
        }

        private void SelectOtherCategory()
        {
            if (IsOtherSelected)
            {
                selectedCategory = PlanType.OTHER;
                IsOtherSelected = false;
            }
            else
            {
                DeselectAll();
                IsOtherSelected = true;
            }
        }

        private void DeselectAll()
        {
            selectedCategory = PlanType.UNDEFINED;
            IsRestaurantSelected = IsTakeSomethingSelected=IsDoSportsSelected=IsSpectaclesSelected=IsLanguageExchangesSelected=IsTravelSelected=IsShoppingSelected=IsGoOutForDrinksSelected=IsOtherSelected= false;
        }

        private void NextStep()
        {

        }
        #endregion
    }
}
