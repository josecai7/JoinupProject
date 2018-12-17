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
        public ICommand AddImage1Command
        {
            get
            {
                return new RelayCommand( AddImage1 );
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
        public ICommand NextStepCommand
        {
            get
            {
                return new RelayCommand(NextStepAsync);
            }
        }
        #endregion
        #region Methods
        private void AddImage1()
        {

        }
        private void AddImage2()
        {
        }
        private void AddImage3()
        {
        }
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


            }
        }

        private async void NextStepAsync()
        {
            IsRunning = true;
            plan.UserId = LoggedUser.Id;

            foreach (LocalImage localImage in Images)
            {

            }

            var response=await DataService.GetInstance().SavePlan(plan);

            

            if (!response.IsSuccess)
            {
                ToastNotificationUtils.ShowToastNotifications("Ha habido un error en la creación del plan. Intentelo de nuevo más tarde", "add.png", Color.IndianRed);
            }
            else
            {
                Plan plan = (Plan)response.Result;

                var responseJoin = await DataService.GetInstance().JoinAPlan( plan.PlanId,LoggedUser.Id );

                if ( !responseJoin.IsSuccess )
                {
                    ToastNotificationUtils.ShowToastNotifications( "Ha habido un error en la creación del plan. Intentelo de nuevo más tarde", "add.png", Color.IndianRed );
                }
                else
                {
                    MessagingCenter.Send( ViewModelLocator.Instance.Resolve<PlansViewModel>(), "AddNewPlan" );

                    await NavigationService.NavigateToRootAsync();
                }
                
            }

            IsRunning = false;
        }
        #endregion
    }
}
