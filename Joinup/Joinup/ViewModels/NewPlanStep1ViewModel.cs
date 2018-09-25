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
        #endregion
        #region Properties
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
        #endregion
        #region Constructors
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
        }
        #endregion
    }
}
