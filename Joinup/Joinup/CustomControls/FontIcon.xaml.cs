using Joinup.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Joinup.CustomControls
{
    [XamlCompilation( XamlCompilationOptions.Compile )]
    public partial class FontIcon : ContentView
    {
        public static readonly BindableProperty IconProperty = BindableProperty.Create(
                                                 propertyName: "Icon",
                                                 returnType: typeof( string ),
                                                 declaringType: typeof( FontIcon ),
                                                 defaultValue: "",
                                                 defaultBindingMode: BindingMode.TwoWay,
                                                 propertyChanged: IconPropertyChanged );

        public string Icon
        {
            get { return base.GetValue( IconProperty ).ToString(); }
            set { base.SetValue( IconProperty, value ); }
        }

        private static void IconPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FontIcon) bindable;
            string iconName = newValue.ToString();
            IconUtils iconUtils = new IconUtils();
            control.icon.Text = iconUtils.GetIcon( iconName );
        }

        public static readonly BindableProperty IconColorProperty = BindableProperty.Create(
                                         propertyName: "IconColor",
                                         returnType: typeof( Color ),
                                         declaringType: typeof( FontIcon ),
                                         defaultValue: Color.Black,
                                         defaultBindingMode: BindingMode.TwoWay,
                                         propertyChanged: IconColorPropertyChanged );

        public Color IconColor
        {
            get { return (Color) base.GetValue( IconColorProperty ); }
            set { base.SetValue( IconColorProperty, value ); }
        }

        private static void IconColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FontIcon) bindable;
            control.icon.TextColor = (Color) newValue;
        }

        public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(
                                 propertyName: "IconSize",
                                 returnType: typeof( double ),
                                 declaringType: typeof( FontIcon ),
                                 defaultValue: double.MinValue,
                                 defaultBindingMode: BindingMode.TwoWay,
                                 propertyChanged: IconSizePropertyChanged );

        public double IconSize
        {
            get { return (double)base.GetValue( IconSizeProperty ); }
            set { base.SetValue( IconSizeProperty, value ); }
        }

        private static void IconSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FontIcon) bindable;
            control.icon.FontSize = (double)newValue;
        }
        public FontIcon ()
		{
			InitializeComponent ();
		}
	}
}