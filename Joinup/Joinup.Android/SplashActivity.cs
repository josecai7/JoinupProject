using Android.OS;
using Android.App;
using Joinup.Droid;
using Android.Support.V7.App;

namespace SplashScreenDemo.Droid
{
    [Activity( Label = "Mobile App Name", Icon = "@drawable/icon", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true )]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity( typeof( MainActivity ) );
        }
    }
}