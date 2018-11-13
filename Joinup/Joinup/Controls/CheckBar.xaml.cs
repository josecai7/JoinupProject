using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Joinup.Controls
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CheckBar : ContentView
	{
        public static BindableProperty CheckedProperty = BindableProperty.Create( nameof( Checked ), typeof( bool ), typeof( CheckBar ), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var check = (CheckBar) bindable;

            bool enabled = (bool) newval;

            if ( enabled )
                check.Check.Icon = "checkbox-marked";
            else
                check.Check.Icon = "checkbox-blank-outline";
        } );
        public bool Checked
        {
            get
            {
                return (bool) GetValue( CheckedProperty );
            }
            set
            {
                SetValue( CheckedProperty, value );

            }
        }
        public static BindableProperty TextProperty = BindableProperty.Create( nameof( Text ), typeof( string ), typeof( CheckBar ), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var check = (CheckBar) bindable;
            check.Label.Text = (string) newval;
        } );
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
        public static BindableProperty TextColorProperty = BindableProperty.Create( nameof( TextColor ), typeof( Color ), typeof( CheckBar ), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var check = (CheckBar) bindable;
            check.Label.TextColor = (Color) newval;
        } );
        public Color TextColor
        {
            get
            {
                return (Color) GetValue( TextColorProperty );
            }
            set
            {
                SetValue( TextColorProperty, value );
            }
        }
        public static BindableProperty CheckColorProperty = BindableProperty.Create( nameof( CheckColor ), typeof( Color ), typeof( CheckBar ), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var check = (CheckBar) bindable;
            check.Check.IconColor = (Color) newval;
        } );
        public Color CheckColor
        {
            get
            {
                return (Color) GetValue( CheckColorProperty );
            }
            set
            {
                SetValue( CheckColorProperty, value );
            }
        }
        public CheckBar ()
		{
			InitializeComponent ();

            var tgr = new TapGestureRecognizer();
            tgr.Tapped += (s, e) => OnLabelClicked();
            Container.GestureRecognizers.Add( tgr );

        }

        private void OnLabelClicked()
        {
            Checked = !Checked;
        }
    }
}