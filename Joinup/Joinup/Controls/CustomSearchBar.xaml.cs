using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Joinup.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CustomSearchBar : ContentView
	{
		public CustomSearchBar ()
		{
			InitializeComponent ();
            Entry.TextChanged += Entry_TextChanged;

            Cancel.IsVisible = false;
            var tgr = new TapGestureRecognizer();
            tgr.Tapped += (s, e) => ClearEntry();
            Cancel.GestureRecognizers.Add( tgr );
        }
        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Text = e.NewTextValue;
            if ( string.IsNullOrEmpty( Text ) )
            {
                Cancel.IsVisible = false;
            }
            else
            {
                Cancel.IsVisible = true;
            }
        }
        private void ClearEntry()
        {
            Entry.Text = String.Empty;
        }

        public string Text
        {
            get
            {
                return (string) GetValue( TextProperty );
            }
            set
            {
                SetValue( TextProperty, value );
            }
        }
        public static BindableProperty TextProperty = BindableProperty.Create( nameof( Text ), typeof( string ), typeof( CustomSearchBar ), defaultBindingMode: BindingMode.TwoWay );
        public string Placeholder
        {
            get
            {
                return (string) GetValue( PlaceholderProperty );
            }
            set
            {
                SetValue( PlaceholderProperty, value );
            }
        }
        public static BindableProperty PlaceholderProperty = BindableProperty.Create( nameof( Placeholder ), typeof( string ), typeof( CustomSearchBar ), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var searchbar = (CustomSearchBar) bindable;
            searchbar.Entry.Placeholder = (string) newval;
        } );

    }
}