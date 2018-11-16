using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Joinup.Controls;

namespace Joinup.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : CustomTabbedPage
    {
        public MainView ()
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement( ToolbarPlacement.Bottom );
        }
    }
}