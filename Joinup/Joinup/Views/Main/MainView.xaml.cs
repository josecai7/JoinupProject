using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Joinup.Controls;
using Joinup.Utils;

namespace Joinup.Views.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainView : Xamarin.Forms.TabbedPage
    {
        public MainView ()
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement( ToolbarPlacement.Bottom );
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarItemColor(ColorUtils.SecondaryTextColor);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarSelectedItemColor(ColorUtils.PrimaryColor);
        }
    }
}