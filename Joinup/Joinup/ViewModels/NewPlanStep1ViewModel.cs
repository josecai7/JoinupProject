using Acr.UserDialogs;
using GalaSoft.MvvmLight.Command;
using GooglePlaces.Xamarin;
using Joinup.Common.Models.SelectablesModels;
using Joinup.Common.Models;
using Joinup.Helpers;
using Joinup.Service;
using Joinup.Utils;
using Joinup.ViewModels.Base;
using Joinup.Views;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Toasts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace Joinup.ViewModels
{
    public class NewPlanStep1ViewModel:BaseViewModel
    {
        #region Attributes
        private bool isEditing = false;
        private string buttonText;

        bool isEnabled = true;
        bool isRunning = false;
        private Plan plan;

        string locationText;
        string destinationText;
        Prediction selectedLocation;
        Prediction selectedDestination;
        List<Prediction> predictions = new List<Prediction>();
        List<Prediction> destinationPredictions = new List<Prediction>();
        ObservableCollection<Plan> pins = new ObservableCollection<Plan>();

        private List<Category> categories;
        private Category selectedCategory;

        private bool hasLink;
        private string linkShowedText;

        private bool hasLevel;
        private string levelShowedText;
        private List<Category> levels;
        private Category selectedRecommendedLevel;

        private bool isFoodInfoVisible;

        private Category selectedFoodType;
        private List<Category> foodTypes;
        private bool isSportInfoVisible;
        private List<Category> sports;
        private Category selectedSport;

        private bool isLanguageInfoVisible;
        private List<Category> languages;
        private Category selectedLanguage1;
        private Category selectedLanguage2;

        private bool isTravelInfoVisible;

        private ImageSource image1;
        private byte[] image1bytes;
        private ImageSource image2;
        private byte[] image2bytes;
        private ImageSource image3;
        private byte[] image3bytes;

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
        public bool IsEnabled
        {
            get
            {
                return isEnabled;
            }
            set
            {
                isEnabled = value;
                RaisePropertyChanged("IsEnabled");
            }
        }
        public string ButtonText
        {
            get
            {
                return buttonText;
            }
            set
            {
                buttonText = value;
                RaisePropertyChanged("ButtonText");
            }
        }

        public DateTime MinimumDate
        {
            get
            {
                return DateTime.Now;
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
                HiddePanels();
                RaisePropertyChanged("SelectedCategory");
            }
        }
        public List<Category> Categories
        {
            get
            {
                return categories;
            }
            set
            {
                categories=value;
                RaisePropertyChanged("Categories");
            }
        }

        public bool IsFoodInfoVisible
        {
            get
            {
                return isFoodInfoVisible;
            }
            set
            {
                isFoodInfoVisible = value;
                RaisePropertyChanged("IsFoodInfoVisible");
            }
        }
        public Category SelectedFoodType
        {
            get
            {
                return selectedFoodType;
            }
            set
            {
                selectedFoodType = value;
                plan.FoodType = selectedFoodType.Id;
                RaisePropertyChanged("SelectedFoodType");
            }
        }
        public List<Category> FoodTypes
        {
            get
            {
                return foodTypes;
            }
            set
            {
                foodTypes = value;
                RaisePropertyChanged("FoodTypes");
            }
        }

        public bool HasLink
        {
            get
            {
                return hasLink;
            }
            set
            {
                hasLink = value;
                RaisePropertyChanged( "HasLink" );
            }
        }
        public string LinkShowedText
        {
            get
            {
                return linkShowedText;
            }
            set
            {
                linkShowedText = value;
                RaisePropertyChanged( "LinkShowedText" );
            }
        }
        public string Link
        {
            get
            {
                return plan.Link;
            }
            set
            {
                plan.Link = value;
                RaisePropertyChanged( "Link" );
            }
        }

        public bool HasLevel
        {
            get
            {
                return hasLevel;
            }
            set
            {
                hasLevel = value;
                RaisePropertyChanged( "HasLevel" );
            }
        }
        public string LevelShowedText
        {
            get
            {
                return levelShowedText;
            }
            set
            {
                levelShowedText = value;
                RaisePropertyChanged( "LevelShowedText" );
            }
        }
        public Category SelectedRecommendedLevel
        {
            get
            {
                return selectedRecommendedLevel;
            }
            set
            {
                selectedRecommendedLevel = value;
                plan.RecommendedLevel = selectedRecommendedLevel.Id;
                RaisePropertyChanged( "SelectedRecommendedLevel" );
            }
        }
        public List<Category> SkillLevels
        {
            get
            {
                return levels;
            }
            set
            {
                levels = value;
                RaisePropertyChanged("SkillLevels");
            }
        }

        public bool IsSportInfoVisible
        {
            get
            {
                return isSportInfoVisible;
            }
            set
            {
                isSportInfoVisible = value;
                RaisePropertyChanged( "IsSportInfoVisible" );
            }
        }
        public Category SelectedSport
        {
            get
            {
                return selectedSport;
            }
            set
            {
                selectedSport = value;
                plan.Sport = selectedSport.Id;
                RaisePropertyChanged( "SelectedSport" );
            }
        }
        public List<Category> Sports
        {
            get
            {
                return sports;
            }
            set
            {
                sports = value;
                RaisePropertyChanged("Sports");
            }
        }

        public bool IsLanguageInfoVisible
        {
            get
            {
                return isLanguageInfoVisible;
            }
            set
            {
                isLanguageInfoVisible = value;
                RaisePropertyChanged( "IsLanguageInfoVisible" );
            }
        }
        public Category SelectedLanguage1
        {
            get
            {
                return selectedLanguage1;
            }
            set
            {
                selectedLanguage1 = value;
                plan.Language1 = selectedLanguage1.Id;
                RaisePropertyChanged( "SelectedLanguage1" );
            }
        }
        public Category SelectedLanguage2
        {
            get
            {
                return selectedLanguage2;
            }
            set
            {
                selectedLanguage2 = value;
                plan.Language2 = selectedLanguage2.Id;
                RaisePropertyChanged( "SelectedLanguage2" );
            }
        }
        public List<Category> Languages
        {
            get
            {
                return languages;
            }
            set
            {
                languages = value;
                RaisePropertyChanged("Languages");
            }
        }

        public bool IsTravelInfoVisible
        {
            get
            {
                return isTravelInfoVisible;
            }
            set
            {
                isTravelInfoVisible = value;
                RaisePropertyChanged( "IsTravelInfoVisible" );
            }
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
        public ImageSource Image1
        {
            get
            {
                return image1;
            }
            set
            {
                image1 = value;
                RaisePropertyChanged("Image1");
            }
        }
        public ImageSource Image2
        {
            get
            {
                return image2;
            }
            set
            {
                image2 = value;
                RaisePropertyChanged("Image2");
            }
        }
        public ImageSource Image3
        {
            get
            {
                return image3;
            }
            set
            {
                image3 = value;
                RaisePropertyChanged("Image3");
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
                LoadPredictions( locationText );
                RaisePropertyChanged( "LocationText" );
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
                RaisePropertyChanged( "SelectedLocation" );
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
                RaisePropertyChanged( "Predictions" );
            }
        }

        public string DestinationText
        {
            get
            {
                return destinationText;
            }
            set
            {
                destinationText = value;
                LoadDestinationPredictions( destinationText );
                RaisePropertyChanged( "DestinationText" );
            }
        }
        public Prediction SelectedDestination
        {
            get
            {
                return selectedDestination;
            }
            set
            {
                selectedDestination = value;
                SelectDestination();
                RaisePropertyChanged( "SelectedDestination" );
            }
        }   
        public List<Prediction> DestinationPredictions
        {
            get
            {
                return destinationPredictions;
            }
            set
            {
                destinationPredictions = value;
                RaisePropertyChanged( "DestinationPredictions" );
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
            Categories=PLANTYPE.GetAllPlanTypes();
            SkillLevels = SKILLLEVEL.GetAllSkillLevel();
            Languages = LANGUAGE.GetAllLanguages();
            FoodTypes = FOODTYPE.GetAllFoodTypes();
            Sports = SPORT.GetAllSports();

            plan = new Plan();

            plan.PlanDate = DateTime.Now;
            plan.EndPlanDate = DateTime.Now;

            ButtonText = "CREAR PLAN";
        }
        public override Task InitializeAsync(object navigationData)
        {
            var parameter = navigationData as Plan;
            if (parameter != null)
            {
                isEditing = true;
                ButtonText = "MODIFICAR PLAN";
                plan = parameter;
                if (plan.Latitude != Double.NaN && plan.Longitude != Double.NaN)
                {
                    pins = new ObservableCollection<Plan>();
                    pins.Add(plan);
                }

                RaisePropertyChanged("MinimumDate");

                selectedCategory = Categories.Find(item => item.Id == plan.PlanType);
                RaisePropertyChanged("SelectedCategory");

                RaisePropertyChanged("Name");
                RaisePropertyChanged("Description");
                RaisePropertyChanged("PlanDate");
                RaisePropertyChanged("PlanTime");
                RaisePropertyChanged("EndPlanDate");
                RaisePropertyChanged("EndPlanTime");
                LocationText = plan.Address;
                DestinationText = plan.DestinationAddress;
                RaisePropertyChanged("Pins");

                selectedCategory = Categories.Find(item => item.Id == plan.PlanType);
                RaisePropertyChanged("SelectedCategory");

                selectedRecommendedLevel = SkillLevels.Find(item => item.Id == plan.RecommendedLevel);
                RaisePropertyChanged("SelectedRecommendedLevel");

                selectedLanguage1 = Languages.Find(item => item.Id == plan.Language1);
                RaisePropertyChanged("SelectedLanguage1");

                selectedLanguage2 = Languages.Find(item => item.Id == plan.Language2);
                RaisePropertyChanged("SelectedLanguage2");

                selectedFoodType = FoodTypes.Find(item => item.Id == plan.Language2);
                RaisePropertyChanged("SelectedLanguage2");

                if (plan.PlanImages.Count == 1)
                {
                    Image1 = ImageSource.FromUri( new Uri(plan.PlanImages[0].ImageFullPath) );
                }
                if (plan.PlanImages.Count==2)
                {
                    Image2 = ImageSource.FromUri( new Uri( plan.PlanImages[1].ImageFullPath ) );
                }
                if (plan.PlanImages.Count == 3)
                {
                    Image3 = ImageSource.FromUri( new Uri( plan.PlanImages[2].ImageFullPath ) );
                }
            }

            return base.InitializeAsync(navigationData);
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

        public ICommand AddImage1Command
        {
            get
            {
                return new RelayCommand(AddImage1);
            }
        }

        public ICommand AddImage2Command
        {
            get
            {
                return new RelayCommand(AddImage2);
            }
        }

        public ICommand AddImage3Command
        {
            get
            {
                return new RelayCommand(AddImage3);
            }
        }

        #endregion
        #region Methods
        private void HiddePanels()
        {
            IsSportInfoVisible = IsFoodInfoVisible = HasLink= HasLevel=false;
            switch (plan.PlanType)
            {
                case PLANTYPE.FOODANDDRINK:
                    IsFoodInfoVisible = HasLink=true;
                    LinkShowedText = "Puedes incluir un link a la web del restaurante.";
                    break;
                case PLANTYPE.SPECTACLES:
                    HasLink = true;
                    LinkShowedText = "Puedes incluir un link de información sobre el espectaculo.";
                    break;
                case PLANTYPE.SPORT:
                    LevelShowedText = "Nivel en el deporte";
                    HasLevel=IsSportInfoVisible = true;
                    break;
                case PLANTYPE.LANGUAGE:
                    LevelShowedText = "Nivel en el idioma";
                    HasLevel =IsLanguageInfoVisible = true;
                    break;
                case PLANTYPE.TRAVEL:
                    IsTravelInfoVisible = true;
                    break;
            }
        }
        private async void LoadPredictions(string pLocationText)
        {
            PlacesAutocomplete places = new PlacesAutocomplete("AIzaSyCFG2-DEKK7EnqzH_tiiKItD_CpaJYGCUg");
            Predictions predictions = await places.GetAutocomplete(pLocationText);
            Predictions = predictions.predictions;
        }
        private async void LoadDestinationPredictions(string pLocationText)
        {
            PlacesAutocomplete places = new PlacesAutocomplete( "AIzaSyCFG2-DEKK7EnqzH_tiiKItD_CpaJYGCUg" );
            Predictions predictions = await places.GetAutocomplete( pLocationText );
            DestinationPredictions = predictions.predictions;
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
        private async void SelectDestination()
        {
            if (SelectedDestination != null)
            {
                PlacesGeocode placesGeocode = new PlacesGeocode( "AIzaSyCFG2-DEKK7EnqzH_tiiKItD_CpaJYGCUg" );
                Geocode geocode = await placesGeocode.GetGeocode( SelectedDestination.Description );

                plan.DestinationLatitude = geocode.results.FirstOrDefault().geometry.location.lat;
                plan.DestinationLongitude = geocode.results.FirstOrDefault().geometry.location.lng;
                plan.DestinationAddress = geocode.results.FirstOrDefault().formatted_address;


                Pins = new ObservableCollection<Plan>();
                Pins.Add( plan );
            }
            else
            {
                plan.DestinationAddress = null;
                plan.DestinationLatitude = Double.NaN;
                plan.DestinationLongitude = Double.NaN;

                Pins = new ObservableCollection<Plan>();
            }
        }
        private void CreatePlan()
        {
            if (selectedCategory == null)
            {
                ToastNotificationUtils.ShowErrorToastNotifications("Ups...Debes seleccionar una categoria");
                return;
            }
            if (string.IsNullOrEmpty(Name))
            {
                ToastNotificationUtils.ShowErrorToastNotifications("Ups...Debes ponerle nombre a tu plan");
                return;
            }
            if (plan.Latitude==0 && plan.Longitude==0)
            {
                ToastNotificationUtils.ShowErrorToastNotifications("Ups...Debes seleccionar una ubicación para tu plan");
                return;
            }
            if (string.IsNullOrEmpty(Name))
            {
                ToastNotificationUtils.ShowErrorToastNotifications("Ups...Debes ponerle nombre a tu plan");
                return;
            }
            if (PlanDate == DateTime.MinValue)
            {
                ToastNotificationUtils.ShowErrorToastNotifications("Ups...Debes establecer una fecha de inicio para tu plan");
                return;
            }
            if (PlanDate < DateTime.Now)
            {
                ToastNotificationUtils.ShowErrorToastNotifications( "Ups...Debes establecer una fecha de inicio mayor que la fecha actual" );
                return;
            }
            if (EndPlanDate == DateTime.MinValue)
            {
                ToastNotificationUtils.ShowErrorToastNotifications("Ups...Debes establecer una fecha de fin para tu plan");
                return;
            }
            if (PlanDate>EndPlanDate)
            {
                ToastNotificationUtils.ShowErrorToastNotifications( "Ups...La fecha de finalización del plan no puede ser mayor a la fecha inicial" );
                return;
            }
            if (plan.PlanType==PLANTYPE.LANGUAGE && (plan.Language1==LANGUAGE.UNDEFINED|| plan.Language2 == LANGUAGE.UNDEFINED))
            {
                ToastNotificationUtils.ShowErrorToastNotifications( "Ups...Debes establecer los idiomas que se trabajaran en el intercambio" );
                return;
            }
            if (plan.PlanType == PLANTYPE.SPORT && plan.Sport == SPORT.UNDEFINED)
            {
                ToastNotificationUtils.ShowErrorToastNotifications( "Ups...Debes especificar el deporte que se practicará en tu plan" );
                return;
            }
            if (isEditing)
            {
                EditPlan();
            }
            else
            {
                SavePlan();
            }
        }
        private async void SavePlan()
        {
            IsRunning = true;
            IsEnabled = false;
            plan.UserId = LoggedUser.Id;

            if (Image1 != null)
            {
                plan.PlanImages.Add(new Common.Models.Image()
                {
                    ImageArray = image1bytes
                });
            }
            if (Image2 != null)
            {
                plan.PlanImages.Add(new Common.Models.Image()
                {
                    ImageArray = image2bytes
                });
            }
            if (Image3 != null)
            {
                plan.PlanImages.Add(new Common.Models.Image()
                {
                    ImageArray = image3bytes
                });
            }

            var response = await DataService.GetInstance().SavePlan(plan);

            if (!response.IsSuccess)
            {
                ToastNotificationUtils.ShowErrorToastNotifications("Ha habido un error en la creación del plan. Intentelo de nuevo más tarde");
            }
            else
            {
                Plan plan = (Plan)response.Result;

                var responseJoin = await DataService.GetInstance().JoinAPlan(plan.PlanId, LoggedUser.Id);

                if (!responseJoin.IsSuccess)
                {
                    ToastNotificationUtils.ShowErrorToastNotifications("Ha habido un error en la creación del plan. Intentelo de nuevo más tarde");
                }
                else
                {
                    MessagingCenter.Send( ViewModelLocator.Instance.Resolve<PlansViewModel>(), "RefreshPlans" );
                    MessagingCenter.Send( ViewModelLocator.Instance.Resolve<MyPlansViewModel>(), "RefreshPlans" );
                    MessagingCenter.Send( ViewModelLocator.Instance.Resolve<ProfileViewModel>(), "RefreshPlans" );

                    await NavigationService.NavigateToRootAsync();
                }

            }

            IsRunning = false;
            IsEnabled = true;
        }
        private async void EditPlan()
        {
            var source = await Application.Current.MainPage.DisplayActionSheet(
                "¿Estas seguro de modificar el plan? Los usuarios serán notificados de dichos cambios",
                "Cancelar",
                null,
                "Si",
                "No");

            if (source == "No")
            {
                return;
            }
            else
            {
                IsEnabled = false;
                IsRunning = true;
                plan.UserId = LoggedUser.Id;

                var response = await DataService.GetInstance().EditPlan(plan);

                if (!response.IsSuccess)
                {
                    ToastNotificationUtils.ShowErrorToastNotifications("Ha habido un error en al editar el plan. Intentelo de nuevo más tarde");
                }
                else
                {
                    Plan plan = (Plan)response.Result;

                    SendEmails();

                    MessagingCenter.Send( ViewModelLocator.Instance.Resolve<PlansViewModel>(), "RefreshPlans" );
                    MessagingCenter.Send( ViewModelLocator.Instance.Resolve<MyPlansViewModel>(), "RefreshPlans" );
                    MessagingCenter.Send( ViewModelLocator.Instance.Resolve<ProfileViewModel>(), "RefreshPlans" );
                    await NavigationService.NavigateBackAsync();

                }

                IsRunning = false;
                IsEnabled = true;
            }
        }

        private async void SendEmails()
        {
            foreach (MyUserASP user in plan.AssistantUsers)
            {
                string subject = "Cambios en tu plan: " + plan.Name;
                string body = "El organizador del plan ha realizado cambios. No olvides entrar para revisar si el plan te sigue pareciendo interesante";
                await MailHelper.SendEmail(user.Email, subject, body);
            }
        }

        private void AddImage1()
        {
            AddImage(1);
        }
        private void AddImage2()
        {
            AddImage(2);
        }
        private void AddImage3()
        {
            AddImage(3);
        }
        private async void AddImage(int pPhoto)
        {
            if (!isEditing)
            {
                MediaFile file;

                await CrossMedia.Current.Initialize();

                var source = await Application.Current.MainPage.DisplayActionSheet(
                    "¿Desde donde desea tomar la imagen?",
                    "Cancelar",
                    null,
                    "Galeria",
                    "Camara" );

                if (source == "Cancelar")
                {
                    return;
                }

                if (source == "Camara")
                {
                    if (!CrossMedia.Current.IsTakePhotoSupported)
                    {
                        ToastNotificationUtils.ShowErrorToastNotifications( "Galeria no disponible" );
                        return;
                    }

                    file = await CrossMedia.Current.TakePhotoAsync(
                        new StoreCameraMediaOptions
                        {
                            Directory = "Joinup",
                            Name = DateTime.Now.ToString()
                        }
                    );
                }
                else
                {
                    if (!CrossMedia.Current.IsTakePhotoSupported)
                    {
                        ToastNotificationUtils.ShowErrorToastNotifications( "Camara no disponible" );
                        return;
                    }
                    file = await CrossMedia.Current.PickPhotoAsync();
                }

                if (file != null)
                {
                    if (pPhoto == 1)
                    {
                        Image1 = ImageSource.FromStream( () =>
                        {
                            var stream = file.GetStream();
                            return stream;
                        } );
                        image1bytes = FilesHelper.ReadFully( file.GetStream() );
                    }
                    else if (pPhoto == 2)
                    {
                        Image2 = ImageSource.FromStream( () =>
                        {
                            var stream = file.GetStream();
                            return stream;
                        } );
                        image2bytes = FilesHelper.ReadFully( file.GetStream() );
                    }
                    if (pPhoto == 3)
                    {
                        Image3 = ImageSource.FromStream( () =>
                        {
                            var stream = file.GetStream();
                            return stream;
                        } );
                        image3bytes = FilesHelper.ReadFully( file.GetStream() );
                    }
                }
                }
        }
        #endregion
    }
}
