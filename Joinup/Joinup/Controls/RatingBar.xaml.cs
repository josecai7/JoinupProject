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
	public partial class RatingBar : ContentView
	{
        public static readonly BindableProperty IsEditableProperty = BindableProperty.Create(
                 propertyName: "IsEditable",
                 returnType: typeof( bool ),
                 declaringType: typeof( RatingBar ),
                 defaultValue: true,
                 defaultBindingMode: BindingMode.TwoWay,
                 propertyChanged: IsEditablePropertyChanged );

        public bool IsEditable
        {
            get { return (bool) base.GetValue( IsEditableProperty ); }
            set { base.SetValue( IsEditableProperty, value ); }
        }

        private static void IsEditablePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RatingBar) bindable;
            control.star1.GestureRecognizers.Clear();
            control.star2.GestureRecognizers.Clear();
            control.star3.GestureRecognizers.Clear();
            control.star4.GestureRecognizers.Clear();
            control.star5.GestureRecognizers.Clear();
        }
        public static readonly BindableProperty IconSizeProperty = BindableProperty.Create(
                         propertyName: "IconSize",
                         returnType: typeof( double ),
                         declaringType: typeof( RatingBar ),
                         defaultValue: double.MinValue,
                         defaultBindingMode: BindingMode.TwoWay,
                         propertyChanged: IconSizePropertyChanged );

        public double IconSize
        {
            get { return (double) base.GetValue( IconSizeProperty ); }
            set { base.SetValue( IconSizeProperty, value ); }
        }

        private static void IconSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RatingBar) bindable;
            control.star1.IconSize = (double) newValue;
            control.star2.IconSize = (double) newValue;
            control.star3.IconSize = (double) newValue;
            control.star4.IconSize = (double) newValue;
            control.star5.IconSize = (double) newValue;
        }


        public static readonly BindableProperty ScoreProperty = BindableProperty.Create(
                                                 propertyName: "Score",
                                                 returnType: typeof( int ),
                                                 declaringType: typeof( RatingBar ),
                                                 defaultValue: 0,
                                                 defaultBindingMode: BindingMode.TwoWay,
                                                 propertyChanged: ScorePropertyChanged );

        private static void ScorePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (RatingBar) bindable;

            int score = (int) newValue;

            control.star1.Icon = control.star2.Icon = control.star3.Icon = control.star4.Icon = control.star5.Icon = "star-outline";
            if (score >= 1)
            {
                control.star1.Icon = "star";
            }
            if (score >= 2)
            {
                control.star2.Icon = "star";
            }
            if (score >= 3)
            {
                control.star3.Icon = "star";
            }
            if (score >= 4)
            {
                control.star4.Icon = "star";
            }
            if (score >= 5)
            {
                control.star5.Icon = "star";
            }

        }

        public int Score
        {
            get { return (int)base.GetValue( ScoreProperty ); }
            set
            {
                base.SetValue( ScoreProperty, value );
            }
        }

        public RatingBar ()
		{
			InitializeComponent ();

            var tgr1 = new TapGestureRecognizer();
            tgr1.Tapped += (s, e) => OnStar1Clicked();
            star1.GestureRecognizers.Add( tgr1 );

            var tgr2 = new TapGestureRecognizer();
            tgr2.Tapped += (s, e) => OnStar2Clicked();
            star2.GestureRecognizers.Add( tgr2 );

            var tgr3 = new TapGestureRecognizer();
            tgr3.Tapped += (s, e) => OnStar3Clicked();
            star3.GestureRecognizers.Add( tgr3 );

            var tgr4 = new TapGestureRecognizer();
            tgr4.Tapped += (s, e) => OnStar4Clicked();
            star4.GestureRecognizers.Add( tgr4 );

            var tgr5 = new TapGestureRecognizer();
            tgr5.Tapped += (s, e) => OnStar5Clicked();
            star5.GestureRecognizers.Add( tgr5 );

        }

        private void OnStar1Clicked()
        {
            Score = 1;
        }
        private void OnStar2Clicked()
        {
            Score = 2;
        }
        private void OnStar3Clicked()
        {
            Score = 3;
        }
        private void OnStar4Clicked()
        {
            Score = 4;
        }
        private void OnStar5Clicked()
        {
            Score = 5;
        }
    }
}