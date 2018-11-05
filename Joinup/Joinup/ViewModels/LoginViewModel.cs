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
        #endregion
        #region Constructors
        public LoginViewModel()
        {

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
                ToastNotificationUtils.ShowToastNotifications("No hay conexion a internet", "add.png", Color.IndianRed);
                return;
            }
            else if (string.IsNullOrEmpty(Email))
            {
                ToastNotificationUtils.ShowToastNotifications("Email no puede estar vacio", "add.png", Color.IndianRed,Acr.UserDialogs.ToastPosition.Bottom);
                return;
            }
            else if (string.IsNullOrEmpty(Password))
            {
                ToastNotificationUtils.ShowToastNotifications("Introduzca una contraseña", "add.png", Color.IndianRed, Acr.UserDialogs.ToastPosition.Bottom);
                return;
            }
            else if (Password.Length<6)
            {
                ToastNotificationUtils.ShowToastNotifications("La contraseña debe tener al menos 6 caracteres", "add.png", Color.IndianRed, Acr.UserDialogs.ToastPosition.Bottom);
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
                    ToastNotificationUtils.ShowToastNotifications("Usuario o contraseña incorrectos", "add.png", Color.IndianRed, Acr.UserDialogs.ToastPosition.Bottom);
                    IsRunning = false;
                    return;
                }
                else
                {
                    Settings.AccessToken = token.AccessToken;
                    Settings.TokenType = token.TokenType;

                    var response = await ApiService.GetInstance().GetUser(url, prefix, $"{controller}/GetUser", this.Email, token.TokenType, token.AccessToken);
                    if (response.IsSuccess)
                    {
                        var userASP = (MyUserASP)response.Result;
                        Settings.UserASP = JsonConvert.SerializeObject(userASP);
                        IsRunning = false;
                        NavigationService.NavigateToAsync<MainViewModel>();
                    }
                    else
                    {
                        ToastNotificationUtils.ShowToastNotifications("Algo fué mal. Contacte con el administrador del sistema.", "add.png", Color.IndianRed, Acr.UserDialogs.ToastPosition.Bottom);
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
