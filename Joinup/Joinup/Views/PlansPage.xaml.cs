using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Joinup.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PlansPage : ContentPage
	{
		public PlansPage ()
		{
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent ();
		}
	}
}