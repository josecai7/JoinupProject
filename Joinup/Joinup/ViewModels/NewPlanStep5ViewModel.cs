using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Helpers;
using Joinup.Service;
using Joinup.Utils;
using Joinup.ViewModels.Base;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class NewPlanStep5ViewModel:BaseViewModel
    {
        #region Attributes
        private Plan plan;
        private ObservableCollection<LocalImage> images=new ObservableCollection<LocalImage>();
        #endregion
        #region Properties
        public ObservableCollection<LocalImage> Images
        {
            get { return images; }
            set
            {
                RaisePropertyChanged( "Images" );
            }
        }
        #endregion
        #region Constructors
        public NewPlanStep5ViewModel()
        {
            plan = new Plan();
            MessagingCenter.Subscribe<NewPlanStep1ViewModel, Plan>(this, "UpdatePlan", (sender, arg) => {
                plan = arg;
            });
        }
        public override Task InitializeAsync(object navigationData)
        {
            plan = (Plan)navigationData;

            return base.InitializeAsync(navigationData);
        }
        public override Task OnDissapearing()
        {
            MessagingCenter.Send(ViewModelLocator.Instance.Resolve<NewPlanStep1ViewModel>(), "UpdatePlan", plan);
            return base.OnDissapearing();
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
        public ICommand NextStepCommand
        {
            get
            {
                return new RelayCommand(NextStepAsync);
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

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Joinup",
                Name = DateTime.Now.ToString()
            });

            if (file == null)
            {
                return;
            }
            else
            {
                ImageSource imageSource = ImageSource.FromStream(()=>
                {
                    return file.GetStream();
                });


                LocalImage localImage = new LocalImage();
                localImage.ImageArray = FilesHelper.ReadFully(file.GetStream());
                localImage.ImageSource = imageSource;
                

                Images.Add(localImage);

            }     
        }
        private async void NextStepAsync()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlPlansController"].ToString();


            var response = await ApiService.GetInstance().Post<Plan>(url, prefix, controller, plan);
            var newplan = (Plan)response.Result;

            SaveImages(newplan.PlanId);
        }

        private async void SaveImages(int pPlanId)
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlImagesController"].ToString();

            foreach (LocalImage localImage in images)
            {
                Common.Models.Image image = localImage.ToImage();
                image.EntityId = pPlanId;
                var response = await ApiService.GetInstance().Post<Common.Models.Image>(url, prefix, controller, image);
            }
        }
        #endregion

        public class LocalImage: Common.Models.Image
        {
            public ImageSource ImageSource { get; set; }

            public Common.Models.Image ToImage()
            {
                return new Common.Models.Image
                {
                    ImageId = ImageId,
                    EntityId = EntityId,
                    ImagePath = ImagePath,
                    ImageArray=ImageArray,
                };
            }
        }
    }
}
