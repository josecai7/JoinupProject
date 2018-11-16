using Android.OS;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;
using Joinup;
using Joinup.Droid;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Support.V4.View;
using Android.Support.Design.Widget;
using Android.Content.Res;
using Joinup.Controls;
using Android.Content;
using Joinup.Droid.Renderers;

[assembly: ExportRenderer(typeof(CustomTabbedPage), typeof( CustomTabbedPageRenderer ) )]
namespace Joinup.Droid.Renderers
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        bool setup;
        Android.Views.View pager;
        TabLayout layout;

        public CustomTabbedPageRenderer(Context context) : base( context )
        {
            AutoPackage = false;
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged( sender, e );
            if ( setup )
                return;
            if ( e.PropertyName == "Renderer" )
            {
                pager = (ViewPager) ViewGroup.GetChildAt( 0 );
                layout = (TabLayout) ViewGroup.GetChildAt( 1 );

                setup = true;
                ColorStateList colors = null;

                //using xml file
                if ( (int) Build.VERSION.SdkInt >= 23 )
                    colors = Resources.GetColorStateList( Resource.Color.icon_tab, Forms.Context.Theme );
                else
                    colors = Resources.GetColorStateList( Resource.Color.icon_tab );

                for ( int i = 0 ; i < layout.TabCount ; i++ )
                {
                    var tab = layout.GetTabAt( i );
                    var icon = tab.Icon;

                    Android.Views.View view = GetChildAt( i );
                    if ( view is TabLayout )
                        layout = (TabLayout) view;

                    if ( icon != null )
                    {
                        icon = Android.Support.V4.Graphics.Drawable.DrawableCompat.Wrap( icon );
                        Android.Support.V4.Graphics.Drawable.DrawableCompat.SetTintList( icon, colors );
                    }
                }
            }
        }
        }
}