using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Helpers;
using Joinup.Service;
using Joinup.Utils;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class RegisterViewModel:BaseViewModel
    {
        #region Attributes
        private MediaFile file;
        private ImageSource imagesource;
        private string name;
        private string surname;
        private string email;
        private string password;
        private string confirmPassword;
        #endregion
        #region Properties
        public ImageSource ImageSource
        {
            get
            {
                return imagesource;
            }
            set
            {
                imagesource = value;
                RaisePropertyChanged("ImageSource");
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                RaisePropertyChanged("Name");
            }
        }
        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
                RaisePropertyChanged("Surname");
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                RaisePropertyChanged("Email");
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                RaisePropertyChanged("Password");
            }
        }
        public string ConfirmPassword
        {
            get
            {
                return confirmPassword;
            }
            set
            {
                confirmPassword = value;
                RaisePropertyChanged("ConfirmPassword");
            }
        }
        #endregion

        #region Constructors
        public RegisterViewModel()
        { }
        #endregion
        #region Commands
        public ICommand AddImageCommand
        {
            get
            {
                return new RelayCommand(AddImage);
            }
        }
        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
        }

        #endregion

        #region Methods

        private async void AddImage()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                ToastNotificationUtils.ShowToastNotifications("Camara no disponible", "add.png", ColorUtils.ErrorColor);
                return;
            }

            file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Joinup",
                Name = DateTime.Now.ToString()
            });

            if (file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
            }
        }
        private async void Register()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlUsersController"].ToString();

            var connection = await ApiService.GetInstance().CheckConnection();

            if (!connection.IsSuccess)
            {
                ToastNotificationUtils.ShowToastNotifications("No hay conexion a internet", "",Color.IndianRed);
                return;
            }
            else if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surname))
            {
                ToastNotificationUtils.ShowToastNotifications("Nombre incorrecto", "", Color.IndianRed, Acr.UserDialogs.ToastPosition.Bottom);
                return;
            }
            else if (!RegexHelper.IsValidEmail(Email) || string.IsNullOrEmpty(Email))
            {
                ToastNotificationUtils.ShowToastNotifications("Email incorrecto", "", Color.IndianRed);
                return;
            }
            else if (Password.Length < 6)
            {
                ToastNotificationUtils.ShowToastNotifications("La contraseña debe tener al menos 6 caracteres", "", Color.IndianRed);
                return;
            }
            else
            {
                
                byte[] imageArray = null;

                if (file != null)
                {
                    imageArray = FilesHelper.ReadFully(file.GetStream());
                }
                var userRequest = new UserRequest();
                userRequest.Name = Name;
                userRequest.Surname = Surname;
                userRequest.Email = Email;
                userRequest.Password = Password;
                userRequest.ImageArray = imageArray;

                var postUserResponse = await ApiService.GetInstance().Post<UserRequest>(url, prefix, controller, userRequest);

                if (postUserResponse.IsSuccess)
                {
                    var token = await ApiService.GetInstance().GetToken(url, Email, Password);

                    if (token == null || string.IsNullOrEmpty(token.AccessToken))
                    {
                        ShowErrorMessage("Error al obtener el token. Contacte con el administrador del sistema");
                        return;
                    }


                    Settings.TokenType = token.TokenType;
                    Settings.AccessToken = token.AccessToken;
                    var getUserResponse = await ApiService.GetInstance().GetUser(url, prefix, $"{controller}/GetUser", this.Email, Settings.TokenType, token.AccessToken);
                    if (getUserResponse.IsSuccess)
                    {
                        var userASP = (MyUserASP)getUserResponse.Result;
                        Settings.UserASP = JsonConvert.SerializeObject(userASP);
                        NavigationService.NavigateToAsync<MainViewModel>();
                    }
                    else
                    {
                        ShowErrorMessage("Error al obtener el usuario. Contacte con el administrador del sistema");
                    }

                }
                else
                {
                    ShowErrorMessage(postUserResponse.Message);
                }
            }
        }

        private void ShowErrorMessage(string pMessage)
        {
            ToastNotificationUtils.ShowToastNotifications(pMessage, "", Color.IndianRed);
        }
        #endregion
    }
}
