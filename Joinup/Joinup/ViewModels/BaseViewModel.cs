using Joinup.Common.Models;
using Joinup.Navigation;
using Joinup.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Joinup.ViewModels
{
    public class BaseViewModel:INotifyPropertyChanged
    {
        protected readonly INavigationService NavigationService;

        private static MyUserASP loggedUser;
        public MyUserASP LoggedUser
        {
            get
            {
                return loggedUser;
            }
            set
            {
                loggedUser = value;
                RaisePropertyChanged();
            }
        }

        public BaseViewModel()
        {
            NavigationService = ViewModelLocator.Instance.Resolve<INavigationService>();
        }

        public virtual Task InitializeAsync(object navigationData)
        {
            return Task.FromResult( false );
        }
        public virtual Task OnDissapearing()
        {
            return Task.FromResult(false);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName]string propertyName = "")
        {
            var handler = PropertyChanged;
            if ( handler != null )
                handler( this, new PropertyChangedEventArgs( propertyName ) );
        }

    }
}
