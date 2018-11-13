using Joinup.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Joinup.Controls
{
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class IconEntry : ContentView
    {
        public static void Init() { }
        public static BindableProperty TextProperty = BindableProperty.Create( nameof( Text ), typeof( string ), typeof( IconEntry ), defaultBindingMode: BindingMode.TwoWay );
        public static BindableProperty IconProperty = BindableProperty.Create( nameof( IconSource ), typeof( string ), typeof( IconEntry ), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var matEntry = (IconEntry) bindable;
            matEntry.Icon.Icon = (string) newval;
        } );
        public static BindableProperty IconColorProperty = BindableProperty.Create( nameof( IconColor ), typeof( Color ), typeof( IconEntry ), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var matEntry = (IconEntry) bindable;
            matEntry.Icon.IconColor = (Color) newval;
        } );
        public static BindableProperty PlaceholderProperty = BindableProperty.Create( nameof( Placeholder ), typeof( string ), typeof( IconEntry ), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var matEntry = (IconEntry) bindable;
            matEntry.EntryField.Placeholder = (string) newval;
        } );
        public static BindableProperty PlaceholderColorProperty = BindableProperty.Create( nameof( PlaceholderColor ), typeof( Color ), typeof( IconEntry ), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var matEntry = (IconEntry) bindable;
            matEntry.EntryField.PlaceholderColor = (Color) newval;
        } );
        public static BindableProperty IsPasswordProperty = BindableProperty.Create( nameof( IsPassword ), typeof( bool ), typeof( IconEntry ), defaultValue: false, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (IconEntry) bindable;
            matEntry.EntryField.IsPassword = (bool) newVal;
        } );
        public static BindableProperty KeyboardProperty = BindableProperty.Create( nameof( Keyboard ), typeof( Keyboard ), typeof( IconEntry ), defaultValue: Keyboard.Default, propertyChanged: (bindable, oldVal, newVal) =>
        {
            var matEntry = (IconEntry) bindable;
            matEntry.EntryField.Keyboard = (Keyboard) newVal;
        } );
        public static BindableProperty AccentColorProperty = BindableProperty.Create( nameof( AccentColor ), typeof( Color ), typeof( IconEntry ), defaultValue: Color.Accent );
        public Color AccentColor
        {
            get
            {
                return (Color) GetValue( AccentColorProperty );
            }
            set
            {
                SetValue( AccentColorProperty, value );
            }
        }
        public Keyboard Keyboard
        {
            get
            {
                return (Keyboard) GetValue( KeyboardProperty );
            }
            set
            {
                SetValue( KeyboardProperty, value );
            }
        }
        public bool IsPassword
        {
            get
            {
                return (bool) GetValue( IsPasswordProperty );
            }
            set
            {
                SetValue( IsPasswordProperty, value );
            }
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
        public string IconSource
        {
            get
            {
                return (string) GetValue( IconProperty );
            }
            set
            {
                SetValue( IconProperty, value );
            }
        }
        public Color IconColor
        {
            get
            {
                return (Color) GetValue( IconColorProperty );
            }
            set
            {
                SetValue( IconColorProperty, value );
            }
        }
        public Color PlaceholderColor
        {
            get
            {
                return (Color) GetValue( PlaceholderProperty );
            }
            set
            {
                SetValue( PlaceholderProperty, value );
            }
        }
        public IconEntry()
        {
            InitializeComponent();
            EntryField.BindingContext = this;
            EntryField.Focused += async (s, a) =>
            {
                HiddenBottomBorder.BackgroundColor = AccentColor;
                if ( string.IsNullOrEmpty( EntryField.Text ) )
                {
                    // animate both at the same time
                    await Task.WhenAll(
                        HiddenBottomBorder.LayoutTo( new Rectangle( BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height ), 200 )
                     );
                    EntryField.Placeholder = null;
                }
                else
                {
                    await HiddenBottomBorder.LayoutTo( new Rectangle( BottomBorder.X, BottomBorder.Y, BottomBorder.Width, BottomBorder.Height ), 200 );
                }
            };
            EntryField.Unfocused += async (s, a) =>
            {
                if ( string.IsNullOrEmpty( EntryField.Text ) )
                {
                    // animate both at the same time
                    await Task.WhenAll(
                        HiddenBottomBorder.LayoutTo( new Rectangle( BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height ), 200 )
                     );
                    EntryField.Placeholder = Placeholder;
                }
                else
                {
                    await HiddenBottomBorder.LayoutTo( new Rectangle( BottomBorder.X, BottomBorder.Y, 0, BottomBorder.Height ), 200 );
                }
            };
        }
    }

}