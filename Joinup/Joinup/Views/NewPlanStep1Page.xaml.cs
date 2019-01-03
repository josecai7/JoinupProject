using Joinup.Utils;
using Joinup.ViewModels;
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
	public partial class NewPlanStep1Page : ContentPage
	{
		public NewPlanStep1Page ()
		{
            InitializeComponent();

            ((NavigationPage) Application.Current.MainPage).BarBackgroundColor = ColorUtils.PrimaryColor;

            autocomplete.Filter = AutoCompleteSearch;
            autocompleteDestination.Filter = AutoCompleteSearch;
        }

        private bool AutoCompleteSearch(string str1, object str2)
        {
            return true;
        }
    }
}