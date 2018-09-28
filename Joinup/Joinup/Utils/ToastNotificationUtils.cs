using Plugin.Toasts;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Joinup.Utils
{
    public class ToastNotificationUtils
    {
        public static async void ShowToastNotifications(string pTitle, string pSubtitle)
        {
            var notificator = DependencyService.Get<IToastNotificator>();

            var options = new NotificationOptions()
            {
                Title = "Title",
                Description = "Description",
                AndroidOptions=new AndroidOptions { HexColor="Black",}
                
                
            };

            var result = await notificator.Notify(  options );
        }
    }
}
