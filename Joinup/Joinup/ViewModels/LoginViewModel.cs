using GalaSoft.MvvmLight.Command;
using Joinup.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class LoginViewModel
    {
        #region Attributes
        private ApiService apiService;
        private bool isRunning;
        #endregion
        #region Properties
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
            set
            {
                isRunning = value;
            }
        }
        #endregion
        #region Constructors
        public LoginViewModel()
        {
            apiService = new ApiService();
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

        private async void Login()
        {
            var connection = await apiService.CheckConnection();

            if ( !connection.IsSuccess )
            {
                await Application.Current.MainPage.DisplayAlert( "No hay conexion a internet", "Aceptar", "Cancelar" );
            }
            if ( string.IsNullOrEmpty( Email ) )
            {
                await Application.Current.MainPage.DisplayAlert( "Email vacia", "Aceptar", "Cancelar" );
                return;
            }
            if ( string.IsNullOrEmpty( Password ) )
            {
                await Application.Current.MainPage.DisplayAlert( "Contraseña vacia", "Aceptar", "Cancelar" );
                return;
            }

            var token = await apiService.GetToken("http://apijoinup.azurewebsites.net", "/api", "/Comments" );

            if ( token == null || string.IsNullOrEmpty( token.AccessToken ) )
            {
                Password = string.Empty;
                Email = string.Empty;
                await Application.Current.MainPage.DisplayAlert( "Usuario o contraseña incorrectos", "Aceptar", "Cancelar" );
                return;
            }
        }
        #endregion
    }
}
