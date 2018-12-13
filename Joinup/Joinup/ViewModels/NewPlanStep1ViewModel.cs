using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using GooglePlaces.Xamarin;
using Joinup.Common.Models;
using Joinup.Service;
using Joinup.Utils;
using Joinup.ViewModels.Base;
using Joinup.Views;
using Plugin.Toasts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class NewPlanStep1ViewModel:BaseViewModel
    {
        #region Attributes
        private bool isRunning;
        private Plan plan;
        string locationText;
        Prediction selectedLocation;
        List<Prediction> predictions = new List<Prediction>();
        ObservableCollection<Plan> pins = new ObservableCollection<Plan>();
        private Category selectedCategory;

        #endregion
        #region Properties
        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                isRunning = value;
                RaisePropertyChanged("IsRunning");
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
                plan.PlanType = selectedCategory.Id;
                RaisePropertyChanged("SelectedCategory");
            }
        }
        public List<Category> Categories
        {
            get
            {
                return GetCategories();
            }
        }
        private List<Category> GetCategories()
        {
            List<Category> categories = new List<Category>();
            categories.Add(new Category()
            {
                Id = PLANTYPE.FOODANDDRINK,
                Name = "Comida y bebida"
            });
            categories.Add(new Category()
            {
                Id = PLANTYPE.SPECTACLES,
                Name = "Conciertos y espectaculos"
            });
            categories.Add(new Category()
            {
                Id = PLANTYPE.SPORT,
                Name = "Deportes"
            });
            categories.Add(new Category()
            {
                Id = PLANTYPE.LANGUAGE,
                Name = "Intercambio de idiomas"
            });
            categories.Add(new Category()
            {
                Id = PLANTYPE.TRAVEL,
                Name = "Viajes"
            });
            categories.Add(new Category()
            {
                Id = PLANTYPE.SHOPPING,
                Name = "Ir de compras"
            });
            categories.Add(new Category()
            {
                Id = PLANTYPE.OTHER,
                Name = "Otros"
            });

            return categories;
        }
        public string Name
        {
            get
            {
                return plan.Name;
            }
            set
            {
                plan.Name = value;
                RaisePropertyChanged("Name");
            }
        }
        public string Description
        {
            get
            {
                return plan.Description;
            }
            set
            {
                plan.Description = value;
                RaisePropertyChanged("Description");
            }
        }
        public DateTime PlanDate
        {
            get
            {
                return plan.PlanDate;
            }
            set
            {
                plan.PlanDate = value;
                RaisePropertyChanged("PlanDate");
            }
        }
        public TimeSpan PlanTime
        {
            get
            {
                return plan.PlanDate.TimeOfDay;
            }
            set
            {
                plan.PlanDate = plan.PlanDate.Date+(value);
                RaisePropertyChanged("PlanTime");
            }
        }
        public DateTime EndPlanDate
        {
            get
            {
                return plan.EndPlanDate;
            }
            set
            {
                plan.EndPlanDate = value;
                RaisePropertyChanged("EndPlanDate");
            }
        }
        public TimeSpan EndPlanTime
        {
            get
            {
                return plan.EndPlanDate.TimeOfDay;
            }
            set
            {
                plan.EndPlanDate = plan.EndPlanDate.Date + (value);
                RaisePropertyChanged("EndPlanTime");
            }
        }
        public string LocationText
        {
            get
            {
                return locationText;
            }
            set
            {
                locationText = value;
                Pins = new ObservableCollection<Plan>();
                LoadPredictions(locationText);
                RaisePropertyChanged("LocationText");
            }
        }
        public Prediction SelectedLocation
        {
            get
            {
                return selectedLocation;
            }
            set
            {
                selectedLocation = value;
                SelectLocation();
                RaisePropertyChanged("SelectedLocation");
            }
        }
        public List<Prediction> Predictions
        {
            get
            {
                return predictions;
            }
            set
            {
                predictions = value;
                RaisePropertyChanged("Predictions");
            }
        }
        public ObservableCollection<Plan> Pins
        {
            get { return pins; }
            set
            {
                pins = value;
                RaisePropertyChanged("Pins");
            }
        }
        #endregion
        #region Constructors
        public NewPlanStep1ViewModel()
        {
            plan = new Plan();

            plan.PlanDate = DateTime.Now;
            plan.EndPlanDate = DateTime.Now;
        }
        #endregion
        #region Commands
        
        public ICommand CreatePlanCommand
        {
            get
            {
                return new RelayCommand( CreatePlan );
            }
        }
        public ICommand AddImageCommand
        {
            get
            {
                return new RelayCommand(AddImage);
            }
        }
        #endregion
        #region Methods

        private async void LoadPredictions(string pLocationText)
        {
            PlacesAutocomplete places = new PlacesAutocomplete("AIzaSyCFG2-DEKK7EnqzH_tiiKItD_CpaJYGCUg");
            Predictions predictions = await places.GetAutocomplete(pLocationText);
            Predictions = predictions.predictions;
        }
        private async void SelectLocation()
        {
            if (SelectedLocation != null)
            {
                PlacesGeocode placesGeocode = new PlacesGeocode("AIzaSyCFG2-DEKK7EnqzH_tiiKItD_CpaJYGCUg");
                Geocode geocode = await placesGeocode.GetGeocode(SelectedLocation.Description);

                plan.Latitude = geocode.results.FirstOrDefault().geometry.location.lat;
                plan.Longitude = geocode.results.FirstOrDefault().geometry.location.lng;
                plan.Address= geocode.results.FirstOrDefault().formatted_address;


                Pins = new ObservableCollection<Plan>();
                Pins.Add(plan);
            }
            else
            {
                plan.Address = null;
                plan.Latitude = Double.NaN;
                plan.Longitude = Double.NaN;

                Pins = new ObservableCollection<Plan>();
            }
        }
        private void CreatePlan()
        {
            if (selectedCategory == null)
            {
                ToastNotificationUtils.ShowToastNotifications("Ups...Debes seleccionar una categoria", "add.png", ColorUtils.ErrorColor);
                return;
            }
            if (string.IsNullOrEmpty(Name))
            {
                ToastNotificationUtils.ShowToastNotifications("Ups...Debes ponerle nombre a tu plan", "add.png", ColorUtils.ErrorColor);
                return;
            }
            if (SelectedLocation==null)
            {
                ToastNotificationUtils.ShowToastNotifications("Ups...Debes seleccionar una ubicación para tu plan", "add.png", ColorUtils.ErrorColor);
                return;
            }
            if (string.IsNullOrEmpty(Name))
            {
                ToastNotificationUtils.ShowToastNotifications("Ups...Debes ponerle nombre a tu plan", "add.png", ColorUtils.ErrorColor);
                return;
            }
            if (PlanDate == DateTime.MinValue)
            {
                ToastNotificationUtils.ShowToastNotifications("Ups...Debes establecer una fecha de inicio para tu plan", "add.png", ColorUtils.ErrorColor);
                return;
            }
            if (EndPlanDate == DateTime.MinValue)
            {
                ToastNotificationUtils.ShowToastNotifications("Ups...Debes establecer una fecha de fin para tu plan", "add.png", ColorUtils.ErrorColor);
                return;
            }
            SavePlan();
            plan.UserId = LoggedUser.Id;
        }
        private async void SavePlan()
        {
            IsRunning = true;
            plan.UserId = LoggedUser.Id;

            var response = await DataService.GetInstance().SavePlan(plan);

            if (!response.IsSuccess)
            {
                ToastNotificationUtils.ShowToastNotifications("Ha habido un error en la creación del plan. Intentelo de nuevo más tarde", "add.png", Color.IndianRed);
            }
            else
            {
                Plan plan = (Plan)response.Result;

                var responseJoin = await DataService.GetInstance().JoinAPlan(plan.PlanId, LoggedUser.Id);

                if (!responseJoin.IsSuccess)
                {
                    ToastNotificationUtils.ShowToastNotifications("Ha habido un error en la creación del plan. Intentelo de nuevo más tarde", "add.png", Color.IndianRed);
                }
                else
                {
                    MessagingCenter.Send(ViewModelLocator.Instance.Resolve<PlansViewModel>(), "AddNewPlan");

                    await NavigationService.NavigateToRootAsync();
                }

            }

            IsRunning = false;
        }
        private void AddImage()
        {
            
        }
        #endregion
    }
}
