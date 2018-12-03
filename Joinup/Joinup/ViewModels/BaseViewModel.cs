using GalaSoft.MvvmLight.Command;
using Joinup.Common.Models;
using Joinup.Navigation;
using Joinup.Service;
using Joinup.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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

        protected static ObservableCollection<Plan> plans=new ObservableCollection<Plan>();
        public ObservableCollection<Plan> Plans
        {
            get
            {
                return new ObservableCollection<Plan>( plans.OrderBy( x => x.PlanDate ) );
            }
            set
            {
                plans = value;
                RaisePropertyChanged( "Plans" );
            }
        }
        private bool isRefreshing;
        public bool IsRefreshing
        {
            get
            {
                return isRefreshing;
            }
            set
            {
                isRefreshing = value;
                RaisePropertyChanged(  );
            }
        }
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand( LoadPlans );
            }
        }
        protected async void LoadPlans()
        {
            IsRefreshing = true;

            var connection = await ApiService.GetInstance().CheckConnection();
            if ( connection.IsSuccess )
            {
                var response = await DataService.GetInstance().GetPlans();

                if ( !response.IsSuccess )
                {
                    await Application.Current.MainPage.DisplayAlert( "Error", "Aceptar", "Cancelar" );
                    IsRefreshing = false;
                    return;
                }

                var list = (List<Plan>) response.Result;
                Plans = new ObservableCollection<Plan>(list);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert( "Revisa tu conexión a internet", "Aceptar", "Cancelar" );
                IsRefreshing = false;
                return;
            }

            IsRefreshing = false;
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
