using System;
using System.Collections.Generic;
using System.Linq;
using Acr.UserDialogs;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Plugin.Toasts;
using Syncfusion.SfCalendar.XForms.iOS;
using UIKit;
using Xamarin.Forms;

namespace Joinup.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzM1MjJAMzEzNjJlMzMyZTMwR2hlSDFiVHhxUDNWT2NNSSswUVBzRmxFUEV1R0lnaWxWWTNMRHlyeVpZdz0=");
            new SfCalendarRenderer();
            DependencyService.Register<ToastNotification>(); // Register your dependency
            ToastNotification.Init();
            ImageCircleRenderer.Init();
            Xamarin.FormsMaps.Init();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
