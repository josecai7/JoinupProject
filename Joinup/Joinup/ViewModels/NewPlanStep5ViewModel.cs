using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Helpers;
using Joinup.Models;
using Joinup.Service;
using Joinup.Utils;
using Joinup.ViewModels.Base;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
            get
            {
                return images;
            }
            set
            {
                images = value;
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
            MediaFile file;

            await CrossMedia.Current.Initialize();

            var source = await Application.Current.MainPage.DisplayActionSheet(
                "¿Desde donde desea tomar la imagen?",
                "Cancelar",
                null,
                "Galeria",
                "Camara");

            if (source == "Cancelar")
            {
                return;
            }

            if (source == "Camara")
            {
                if (!CrossMedia.Current.IsTakePhotoSupported)
                {
                    ToastNotificationUtils.ShowToastNotifications("Galeria no disponible", "add.png", ColorUtils.ErrorColor);
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
                    ToastNotificationUtils.ShowToastNotifications("Camara no disponible", "add.png", ColorUtils.ErrorColor);
                    return;
                }
                file = await CrossMedia.Current.PickPhotoAsync();
            }

            if (file != null)
            {               
                ImageSource imageSource = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });

                LocalImage localImage = new LocalImage();
                localImage.ImageMedia = file;
                localImage.ImageSource = imageSource;
                localImage.ImageArray = FilesHelper.ReadFully(file.GetStream());

                Images.Add(localImage);
                RaisePropertyChanged("Images");
            }
        }
        private async void NextStepAsync()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlPlansController"].ToString();

            MyUserASP user = JsonConvert.DeserializeObject<MyUserASP>(Settings.UserASP);
            plan.UserId = user.Id;

            var response = await ApiService.GetInstance().Post<Plan>(url, prefix, controller, plan,Settings.TokenType,Settings.AccessToken);
            var newplan = (Plan)response.Result;

            SaveImages(newplan.PlanId);
        }

        private async void SaveImages(int pPlanId)
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlImagesController"].ToString();

            foreach (LocalImage localImage in Images)
            {
                Common.Models.Image image = localImage.ToImage();
                image.EntityId = pPlanId;
                var response = await ApiService.GetInstance().Post<Common.Models.Image>(url, prefix, controller, image, Settings.TokenType, Settings.AccessToken);
            }
        }
        #endregion
    }
}
