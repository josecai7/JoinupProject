using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Utils;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Joinup.ViewModels
{
    public class NewPlanStep5ViewModel:BaseViewModel
    {
        #region Attributes
        private Plan plan;
        private ObservableCollection<Image> images;
        #endregion
        #region Properties
        public ObservableCollection<Image> Images
        {
            get { return images; }
            set { SetValue( ref images, value );  }
        }
        #endregion
        #region Constructors
        public NewPlanStep5ViewModel(Plan pPlan)
        {
            plan = pPlan;

            var list = new ObservableCollection<Image>();

            string[] images = {
                "https://farm9.staticflickr.com/8625/15806486058_7005d77438.jpg",
                "https://farm5.staticflickr.com/4011/4308181244_5ac3f8239b.jpg",
                "https://farm8.staticflickr.com/7423/8729135907_79599de8d8.jpg",
                "https://farm3.staticflickr.com/2475/4058009019_ecf305f546.jpg",
                "https://farm6.staticflickr.com/5117/14045101350_113edbe20b.jpg",
                "https://farm2.staticflickr.com/1227/1116750115_b66dc3830e.jpg",
                "https://farm8.staticflickr.com/7351/16355627795_204bf423e9.jpg",
                "https://farm1.staticflickr.com/44/117598011_250aa8ffb1.jpg",
                "https://farm8.staticflickr.com/7524/15620725287_3357e9db03.jpg",
                "https://farm9.staticflickr.com/8351/8299022203_de0cb894b0.jpg",
            };

            int number = 0;
            for ( int n = 0 ; n < 20 ; n++ )
            {
                for ( int i = 0 ; i < images.Length ; i++ )
                {
                    number++;
                    var item = new Image()
                    {
                        ImageUrl = images[i],
                        FileName = string.Format( "image_{0}.jpg", number ),
                    };

                    list.Add( item );
                }
            }

            Images = list;
        }
        #endregion
        #region Commands
        public ICommand AddImageCommand
        {
            get
            {
                return new RelayCommand( AddImage );
            }
        }

        #endregion
        #region Methods
        private async void AddImage()
        {
            await CrossMedia.Current.Initialize();

            if ( !CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported )
            {
                ToastNotificationUtils.ShowToastNotifications( "Camara no disponible", "add.png", ColorUtils.ErrorColor );
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync().ConfigureAwait(true);

            if ( file == null )
                return;
          
        }
        #endregion

        public class Image
        {
            public string ImageUrl { get; set; }
            public string FileName { get; set; }
        }
    }
}
