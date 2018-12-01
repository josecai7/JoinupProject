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
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SwitchButton : ContentView
    {
        public static readonly BindableProperty TitleTextProperty = BindableProperty.Create(
                                                         propertyName: "TitleText",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(SwitchButton),
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
            var control = (SwitchButton)bindable;
            control.title.Text = newValue.ToString();
        }

        public static BindableProperty SelectedColorProperty = BindableProperty.Create(nameof(SelectedColor), typeof(Color), typeof(SwitchButton), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var switchButton = (SwitchButton)bindable;
            switchButton.title.TextColor = (Color)newval;
        });

        private static Color selectedColor=ColorUtils.WhiteColor;
        public Color SelectedColor
        {
            get
            {
                return (Color)GetValue(SelectedColorProperty);
            }
            set
            {
                selectedColor = value;
                SetValue(SelectedColorProperty, value);
            }
        }
        public static BindableProperty UnSelectedColorProperty = BindableProperty.Create(nameof(UnSelectedColor), typeof(Color), typeof(SwitchButton), defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldVal, newval) =>
        {
            var switchButton = (SwitchButton)bindable;
            switchButton.title.TextColor = (Color)newval;
        });

        private static Color unselectedColor = ColorUtils.WhiteColor;
        public Color UnSelectedColor
        {
            get
            {
                return (Color)GetValue(UnSelectedColorProperty);
            }
            set
            {
                unselectedColor = value;
                SetValue(UnSelectedColorProperty, value);
            }
        }



        public static readonly BindableProperty SelectedProperty = BindableProperty.Create(
                                        propertyName: "Selected",
                                        returnType: typeof(bool),
                                        declaringType: typeof(SwitchButton),
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
            var control = (SwitchButton)bindable;

            bool isSelected = (bool)newValue;

            if (isSelected)
            {
                control.frameContainer.BackgroundColor = selectedColor;
                control.title.TextColor = unselectedColor;
            }
            else
            {
                control.frameContainer.BackgroundColor = unselectedColor;
                control.title.TextColor = selectedColor;
            }
        }

        public SwitchButton ()
		{
			InitializeComponent ();
            var tgr = new TapGestureRecognizer();
            tgr.Tapped += (s, e) => OnLabelClicked();
            frameContainer.GestureRecognizers.Add(tgr);

        }

        private void OnLabelClicked()
        {
            Selected = !Selected;
        }
    }
}