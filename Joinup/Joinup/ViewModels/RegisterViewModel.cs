using GalaSoft.MvvmLight.Command;
using Joinup.Service;
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
        public ICommand RegisterCommand
        {
            get
            {
                return new RelayCommand(Register);
            }
        }

        #endregion
        #region Methods

        private void Register()
        {
            
        }
        #endregion
    }
}
