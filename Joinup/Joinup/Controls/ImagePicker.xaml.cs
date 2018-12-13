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

        public static BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(IconEntry), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var matEntry = (IconEntry)bindable;
            matEntry.EntryField.Placeholder = (string)newval;
        });

        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }
            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }
    }
}