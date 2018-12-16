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
	public partial class ImagePicker : ContentView
	{
		public ImagePicker ()
		{
			InitializeComponent ();
        }

        public static BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(ImagePicker), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var matEntry = (ImagePicker)bindable;
            matEntry.image.Source = (ImageSource)newval;
        });

        public ImageSource ImageSource
        {
            get
            {
                return (ImageSource)GetValue(ImageSourceProperty);
            }
            set
            {
                SetValue(ImageSourceProperty, value);
            }
        }
    }
}