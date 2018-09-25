using GalaSoft.MvvmLight.Command;
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
            private Color restaurantFrameColor=ColorUtils.BackgroundColor;
            private Color restaurantTextColor = ColorUtils.PrimaryColor;
            private string restaurantIconSource = "food.svg";
        private Dictionary<string, string> replaceMap = new Dictionary<string, string>();
        #endregion
        #region Properties
        public Dictionary<string, string> ReplaceMap
        {
            get
            {
                return replaceMap;
            }
            set
            {
                SetValue( ref replaceMap, value );
            }
        }
        public Color RestaurantFrameColor
        {
            get
            {
                return restaurantFrameColor;
            }
            set
            {
                SetValue( ref restaurantFrameColor, value );
            }
        }
        public Color RestaurantTextColor
        {
            get
            {
                return restaurantTextColor;
            }
            set
            {
                SetValue( ref restaurantTextColor, value );
            }
        }
        public string RestaurantIconSource
        {
            get
            {
                return restaurantIconSource;
            }
            set
            {
                SetValue( ref restaurantIconSource, value );
            }
        }
        #endregion
        #region Constructors
        public NewPlanStep1ViewModel()
        {
            RestaurantIconSource = "food.svg";
        }
        #endregion
        #region Commands
        public ICommand RestaurantCategoryCommand
        {
            get
            {
                return new RelayCommand( SelectRestaurantCategory );
            }
        }

        #endregion
        #region Methods
        private void SelectRestaurantCategory()
        {
            RestaurantFrameColor= ColorUtils.PrimaryColor;
            RestaurantTextColor = ColorUtils.BackgroundColor;



            ReplaceMap = new Dictionary<string, string>()
                {
                    { "#FILLCOLOR", "#FFFFFF" }
                };
        }
        #endregion
    }
}
