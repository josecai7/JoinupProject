using GalaSoft.MvvmLight.Command;
using GooglePlaces.Xamarin;
using Joinup.Common.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace Joinup.ViewModels
{
    public class NewPlanStep3ViewModel : BaseViewModel
    {
        #region Attributes
        private Plan plan;
        private string addressText;
        Prediction selectedItem=new Prediction();
        ObservableCollection<Prediction> predictions = new ObservableCollection<Prediction>();
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
                CogerCoordenadas();
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

        private async void TextChanged()
        {
            PlacesAutocomplete places = new PlacesAutocomplete("AIzaSyCFG2-DEKK7EnqzH_tiiKItD_CpaJYGCUg");
            Predictions predictions = await places.GetAutocomplete(AddressText);

            Predictions = new ObservableCollection<Prediction>(predictions.predictions);
        }
        #endregion
        #region Commands
        #endregion
        #region Methods
        private async void CogerCoordenadas()
        {
            PlacesGeocode placesGeocode = new PlacesGeocode("AIzaSyCFG2-DEKK7EnqzH_tiiKItD_CpaJYGCUg");
            if (SelectedItem != null)
            {
                Geocode geocode = await placesGeocode.GetGeocode(SelectedItem.Description);
                Debug.WriteLine("Hola" + geocode.results.FirstOrDefault().geometry.location.lat);
            }
            
        }
        #endregion
    }
}
