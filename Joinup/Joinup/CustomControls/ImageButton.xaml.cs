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
	public partial class ImageButton : ContentView
    {
        public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
                                                         propertyName: "TitleText",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(ImageButton),
                                                         defaultValue: "",
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: TitleTextPropertyChanged);

        public string TitleText
        {
            get { return base.GetValue(TitleTextProperty).ToString(); }
            set { base.SetValue(TitleTextProperty, value); }
        }

        private static void TitleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ImageButton)bindable;
            control.title.Text = newValue.ToString();
        }

        public static readonly BindableProperty IconProperty = BindableProperty.Create(
                                                        propertyName: "Icon",
                                                        returnType: typeof(string),
                                                        declaringType: typeof(ImageButton),
                                                        defaultValue: "",
                                                        defaultBindingMode: BindingMode.TwoWay,
                                                        propertyChanged: IconSourcePropertyChanged);

        public string Icon
        {
            get { return base.GetValue(IconProperty).ToString(); }
            set { base.SetValue(IconProperty, value); }
        }

        private static void IconSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ImageButton)bindable;
            control.icon.Source = ImageSource.FromFile(newValue.ToString());
        }
        
        public static readonly BindableProperty SelectedProperty = BindableProperty.Create(
                                                propertyName: "Selected",
                                                returnType: typeof(bool),
                                                declaringType: typeof(ImageButton),
                                                defaultValue: true,
                                                defaultBindingMode: BindingMode.TwoWay,
                                                propertyChanged: SelectedPropertyChanged);

        public bool Selected
        {
            get { return (bool)base.GetValue(SelectedProperty); }
            set { base.SetValue(SelectedProperty, value); }
        }

        private static void SelectedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ImageButton)bindable;

            bool isSelected = (bool)newValue;

            if (isSelected)
            {
                control.title.TextColor= ColorUtils.BackgroundColor;
                control.frameContainer.BackgroundColor = ColorUtils.PrimaryColor;
                control.icon.ReplaceStringMap = new Dictionary<string, string>()
                {
                    { "#FILLCOLOR", ColorUtils.ToHex(ColorUtils.BackgroundColor)}
                };
            }
            else 
            {
                control.title.TextColor = ColorUtils.PrimaryColor;
                control.frameContainer.BackgroundColor = ColorUtils.BackgroundColor;
                control.icon.ReplaceStringMap = new Dictionary<string, string>()
                {
                    { "#FILLCOLOR", ColorUtils.ToHex(ColorUtils.PrimaryColor)}
                };
            }
            control.icon.ReloadImage();
        }

        public ImageButton()
        {
            InitializeComponent();
        }


    }
}