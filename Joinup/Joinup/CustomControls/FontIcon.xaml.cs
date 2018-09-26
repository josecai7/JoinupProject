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
	[XamlCompilation(XamlCompilationOptions.Compile)]
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
            control.icon.Text = iconUtils.GetIcon(iconName);
        }

        public static readonly BindableProperty IconColorProperty = BindableProperty.Create(
                                         propertyName: "Icon",
                                         returnType: typeof( Color ),
                                         declaringType: typeof( FontIcon ),
                                         defaultValue: Color.Black,
                                         defaultBindingMode: BindingMode.TwoWay,
                                         propertyChanged: IconColorPropertyChanged );

        public Color IconColor
        {
            get { return (Color)base.GetValue( IconColorProperty ); }
            set { base.SetValue( IconColorProperty, value ); }
        }

        private static void IconColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FontIcon) bindable;
            control.icon.TextColor = (Color)newValue;
        }
        public FontIcon ()
		{
			InitializeComponent ();
		}
	}
}