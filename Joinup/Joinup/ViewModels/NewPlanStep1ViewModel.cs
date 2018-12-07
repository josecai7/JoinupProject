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
        private Category selectedCategory;

        #endregion
        #region Properties
        public List<Category> Categories
        {
            get
            {
                return GetCategories();
            }
        }
        public Category SelectedCategory
        {
            get
            {
                return selectedCategory;
            }
            set
            {
                selectedCategory = value;
                RaisePropertyChanged( "SelectedCategory" );
            }
        }

        private List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            categories.Add( new Category()
            {
                Id = PLANTYPE.FOODANDDRINK,
                Name = "Comida y bebida"
            } );
            categories.Add( new Category()
            {
                Id = PLANTYPE.SPECTACLES,
                Name = "Conciertos y espectaculos"
            } );
            categories.Add( new Category()
            {
                Id = PLANTYPE.SPORT,
                Name = "Deportes"
            } );
            categories.Add( new Category()
            {
                Id = PLANTYPE.LANGUAGE,
                Name = "Intercambio de idiomas"
            } );
            categories.Add( new Category()
            {
                Id = PLANTYPE.TRAVEL,
                Name = "Viajes"
            } );
            categories.Add( new Category()
            {
                Id = PLANTYPE.SHOPPING,
                Name = "Ir de compras"
            } );
            categories.Add( new Category()
            {
                Id = PLANTYPE.OTHER,
                Name = "Otros"
            } );

            return categories;
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
        
        public ICommand NextStepCommand
        {
            get
            {
                return new RelayCommand( NextStepAsync );
            }
        }
        #endregion
        #region Methods

        private void NextStepAsync()
        {
            if (selectedCategory == null)
            {
                ToastNotificationUtils.ShowToastNotifications("Ups...Debes seleccionar una categoria", "add.png", ColorUtils.ErrorColor);
            }
            else
            {               
                plan.PlanType = selectedCategory.Id;

                NavigationService.NavigateToAsync<NewPlanStep2ViewModel>( plan );
            }
        }
        #endregion
    }
}
