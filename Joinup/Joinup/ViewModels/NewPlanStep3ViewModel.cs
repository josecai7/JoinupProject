using GalaSoft.MvvmLight.Command;
using GooglePlaces.Xamarin;
using Joinup.Common.Models;
using Joinup.Utils;
using Joinup.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Joinup.ViewModels
{
    public class NewPlanStep3ViewModel : BaseViewModel
    {
        #region Attributes
        private Plan plan;
        private string addressText;
        Prediction selectedItem=new Prediction();
        bool suggestionBarVisible = true;
        ObservableCollection<Prediction> predictions = new ObservableCollection<Prediction>();
        ObservableCollection<Plan> pins = new ObservableCollection<Plan>();
        #endregion
        #region Properties
        public string AddressText
        {
            get
            {
                return addressText;
            }
            set
            {
                SetValue(ref addressText, value);
                TextChanged();
            }
        }
        public ObservableCollection<Plan> Pins
        {
            get { return pins; }
            set
            {
                SetValue( ref pins, value );
            }
        }
        public ObservableCollection<Prediction> Predictions
        {
            get
            {
                return predictions;
            }
            set
            {              
                SetValue(ref predictions, value);
            }
        }
        public Prediction SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {            
                SetValue(ref selectedItem, value);
                PressOnAddress();
            }
        }
        public bool SuggestionBarVisible
        {
            get
            {
                return suggestionBarVisible;
            }
            set
            {
                SetValue( ref suggestionBarVisible, value );
            }
        }

        #endregion
        #region Constructors
        public NewPlanStep3ViewModel(Plan pPlan)
        {
            plan = pPlan;
        }
        #endregion
        #region Commands
        public ICommand TextChangedCommand
        {
            get
            {
                return new RelayCommand(TextChanged);
            }
        }
        public ICommand NextStepCommand
        {
            get
            {
                return new RelayCommand(NextStepAsync);
            }
        }
        #endregion
        #region Methods
        private async void TextChanged()
        {
            PlacesAutocomplete places = new PlacesAutocomplete( "AIzaSyCFG2-DEKK7EnqzH_tiiKItD_CpaJYGCUg" );
            Predictions predictions = await places.GetAutocomplete( AddressText );
            Predictions = new ObservableCollection<Prediction>( predictions.predictions );

            ShowHiddenSuggestionBar();
        }
        private async void PressOnAddress()
        {
            PlacesGeocode placesGeocode = new PlacesGeocode("AIzaSyCFG2-DEKK7EnqzH_tiiKItD_CpaJYGCUg");
            if (SelectedItem != null)
            {
                Geocode geocode = await placesGeocode.GetGeocode(SelectedItem.Description);
                AddressText = SelectedItem.Description;

                plan.Latitude= geocode.results.FirstOrDefault().geometry.location.lat;
                plan.Longitude= geocode.results.FirstOrDefault().geometry.location.lng;


                Pins = new ObservableCollection<Plan>();
                Pins.Add( plan );
            }

            ShowHiddenSuggestionBar();
        }

        private void ShowHiddenSuggestionBar()
        {
            if ( (SelectedItem != null && AddressText == SelectedItem.Description) || string.IsNullOrEmpty(AddressText))
            {
                SuggestionBarVisible = false;
            }
            else
            {
                SuggestionBarVisible = true;
            }
        }

        private async void NextStepAsync()
        {
            if (plan.Latitude==0 || plan.Longitude==0 )
            {
                ToastNotificationUtils.ShowToastNotifications("Ups...Recuerda especificar el lugar donde se llevara a cabo tu plan", "add.png", ColorUtils.ErrorColor);
            }
            else
            {
                MainViewModel.GetInstance().NewPlanStep4 = new NewPlanStep4ViewModel(plan);
                await Application.Current.MainPage.Navigation.PushAsync(new NewPlanStep4Page());
            }
        }
        #endregion
    }
}
