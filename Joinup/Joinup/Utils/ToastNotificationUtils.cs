using Acr.UserDialogs;
using Plugin.Toasts;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Joinup.Utils
{
    public class ToastNotificationUtils
    {
        public static async void ShowErrorToastNotifications(string pTitle)
        {
            var toastConfig = new ToastConfig(pTitle);
            toastConfig.SetDuration(3000);
            toastConfig.SetBackgroundColor(ColorUtils.ErrorColor);
            toastConfig.Position = ToastPosition.Top;

            UserDialogs.Instance.Toast(toastConfig);
        }
    }
}
