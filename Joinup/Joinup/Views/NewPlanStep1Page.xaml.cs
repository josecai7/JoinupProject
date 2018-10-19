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
            ((MainViewModel) BindingContext).Initialize( this );
        }
    }
}