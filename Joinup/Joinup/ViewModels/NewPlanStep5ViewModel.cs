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
        private bool isRunning;
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
            IsRunning = true;
            plan.UserId = LoggedUser.Id;

            foreach (LocalImage localImage in Images)
            {
                Common.Models.Image image = localImage.ToImage();
                plan.PlanImages.Add(image);
            }

            var response=await DataService.GetInstance().SavePlan(plan);

            IsRunning = false;
            if (!response.IsSuccess)
            {
                ToastNotificationUtils.ShowToastNotifications("Ha habido un error en la creación del plan. Intentelo de nuevo más tarde", "add.png", Color.IndianRed);
            }
            else
            {
                await NavigationService.NavigateToRootAsync();
            }
        }
        #endregion
    }
}
