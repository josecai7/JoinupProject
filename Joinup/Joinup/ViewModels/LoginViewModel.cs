using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Helpers;
using Joinup.Service;
using Joinup.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class LoginViewModel:BaseViewModel
    {
        #region Attributes
        private bool isRunning;
        private bool isRemembered;
        private string email;
        private string password;
        #endregion
        #region Properties
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                RaisePropertyChanged( "Email" );
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
                RaisePropertyChanged( "Password" );
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
        public bool IsRemembered
        {
            get
            {
                return isRemembered;
            }
            set
            {
                isRemembered = value;
                RaisePropertyChanged( "IsRemembered" );
            }
        }
        #endregion
        #region Constructors
        public LoginViewModel()
        {
            Email = "jasoljim92@gmail.com";
            Password = "lugubre14";
        }
        #endregion
        #region Commands
        public ICommand DoLoginCommand
        {
            get
            {
                return new RelayCommand( Login );
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
        private async void Login()
        {
            
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlUsersController"].ToString();

            var connection = await ApiService.GetInstance().CheckConnection();

            if (!connection.IsSuccess)
            {
                ToastNotificationUtils.ShowErrorToastNotifications("No hay conexion a internet");
                return;
            }
            else if (string.IsNullOrEmpty(Email))
            {
                ToastNotificationUtils.ShowErrorToastNotifications("Email no puede estar vacio");
                return;
            }
            else if (string.IsNullOrEmpty(Password))
            {
                ToastNotificationUtils.ShowErrorToastNotifications("Introduzca una contraseña");
                return;
            }
            else if (Password.Length<6)
            {
                ToastNotificationUtils.ShowErrorToastNotifications("La contraseña debe tener al menos 6 caracteres");
                return;
            }
            else
            {
                IsRunning = true;

                var token = await ApiService.GetInstance().GetToken(url, Email, Password);

                if (token == null || string.IsNullOrEmpty(token.AccessToken))
                {
                    Password = string.Empty;
                    Email = string.Empty;
                    ToastNotificationUtils.ShowErrorToastNotifications("Usuario o contraseña incorrectos");
                    IsRunning = false;
                    return;
                }
                else
                {
                    Settings.AccessToken = token.AccessToken;
                    Settings.TokenType = token.TokenType;
                    Settings.IsRemembered = IsRemembered;

                    var response = await ApiService.GetInstance().GetUser(url, prefix, $"{controller}/GetUser", this.Email, token.TokenType, token.AccessToken);
                    if (response.IsSuccess)
                    {
                        Settings.UserASP = JsonConvert.SerializeObject(response.Result);
                        IsRunning = false;
                        NavigationService.NavigateToAsync<MainViewModel>();
                    }
                    else
                    {
                        ToastNotificationUtils.ShowErrorToastNotifications("Algo fué mal. Contacte con el administrador del sistema.");
                        IsRunning = false;
                        return;
                    }
                }
            }
        }
        private void Register()
        {          
            NavigationService.NavigateToAsync<RegisterViewModel>();
        }
        #endregion
    }
}
