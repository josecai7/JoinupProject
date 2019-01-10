using DLToolkit.Forms.Controls;
using Joinup.Helpers;
using Joinup.Navigation;
using Joinup.Utils;
using Joinup.ViewModels;
using Joinup.ViewModels.Base;
using Joinup.Views;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Joinup
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            FlowListView.Init();          
		}

		protected override async void OnStart ()
		{
            base.OnStart();

            await InitNavigation();
        }
        private Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Instance.Resolve<INavigationService>();
            if (Settings.IsRemembered)
            {
                return navigationService.NavigateToAsync<MainViewModel>();
            }
            else
            {
                return navigationService.InitializeAsync();
            }                    
        }

        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
