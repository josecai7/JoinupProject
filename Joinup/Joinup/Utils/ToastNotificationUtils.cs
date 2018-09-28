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
        public static async void ShowToastNotifications(string pTitle, string pIcon,Color pBackgroundColor,ToastPosition pToastPosition=ToastPosition.Top,int pDuration=3000)
        {
            var toastConfig = new ToastConfig(pTitle);
            toastConfig.SetDuration(pDuration);
            toastConfig.Icon = pIcon;
            toastConfig.SetBackgroundColor(pBackgroundColor);
            toastConfig.Position = pToastPosition;

            UserDialogs.Instance.Toast(toastConfig);
        }
    }
}
