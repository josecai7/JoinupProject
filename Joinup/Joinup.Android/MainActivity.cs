using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Toasts;
using Xamarin.Forms;
using Acr.UserDialogs;
using ImageCircle.Forms.Plugin.Droid;
using Plugin.CurrentActivity;

namespace Joinup.Droid
{
    [Activity(Label = "Joinup", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init( true );
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzM1MjJAMzEzNjJlMzMyZTMwR2hlSDFiVHhxUDNWT2NNSSswUVBzRmxFUEV1R0lnaWxWWTNMRHlyeVpZdz0=");
            DependencyService.Register<ToastNotification>(); // Register your dependency
            ToastNotification.Init( this );
            UserDialogs.Init( this );
            ImageCircleRenderer.Init();
            Xamarin.FormsMaps.Init(this, bundle);
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Xamarin.Essentials.Platform.Init(this, bundle); // add this line to your code
            CrossCurrentActivity.Current.Init(this, bundle);
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            try
            {
                LoadApplication( new App() );
            }
            catch ( Exception exc )
            {

            }

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

