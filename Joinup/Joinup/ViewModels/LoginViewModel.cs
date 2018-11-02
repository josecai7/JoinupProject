using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Helpers;
using Joinup.Service;
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
                await Application.Current.MainPage.DisplayAlert("No hay conexion a internet", "Aceptar", "Cancelar");
            }
            if (string.IsNullOrEmpty(Email))
            {
                await Application.Current.MainPage.DisplayAlert("Email vacia", "Aceptar", "Cancelar");
                return;
            }
            if (string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Contraseña vacia", "Aceptar", "Cancelar");
                return;
            }

            var token = await ApiService.GetInstance().GetToken(url, Email, Password);

            if (token == null || string.IsNullOrEmpty(token.AccessToken))
            {
                Password = string.Empty;
                Email = string.Empty;
                await Application.Current.MainPage.DisplayAlert("Usuario o contraseña incorrectos", "Aceptar", "Cancelar");
                return;
            }

            var response = await ApiService.GetInstance().GetUser(url, prefix, $"{controller}/GetUser", this.Email, token.TokenType, token.AccessToken);
            if (response.IsSuccess)
            {
                var userASP = (MyUserASP)response.Result;
                Settings.UserASP = JsonConvert.SerializeObject(userASP);
            }

            NavigationService.NavigateToAsync<MainViewModel>();
        }
        private void Register()
        {          
            NavigationService.NavigateToAsync<RegisterViewModel>();
        }
        #endregion
    }
}
