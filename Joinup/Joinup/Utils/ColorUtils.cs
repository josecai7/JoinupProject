using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Joinup.Utils
{
    public class ColorUtils
    {
        public static Color PlaceHolderColor { get { return (Color)Application.Current.Resources["PlaceHolderColor"]; } }
        public static Color PrimaryColor { get { return (Color)Application.Current.Resources["PrimaryColor"]; } }
        public static Color PrimaryTextColor { get { return (Color) Application.Current.Resources["PrimaryTextColor"]; } }
        public static Color BackgroundColor { get { return (Color)Application.Current.Resources["BackgroundColor"]; } }

        public static Color ErrorColor { get { return (Color) Application.Current.Resources["ErrorColor"]; } }

        public static string ToHex(Color pColor)
        {
            int red = (int) (pColor.R * 255);
            int green = (int) (pColor.G * 255);
            int blue = (int) (pColor.B * 255);
            int alpha = (int) (pColor.A * 255);
            string hex = String.Format( "#{0:X2}{1:X2}{2:X2}", red, green, blue, alpha );
            return hex;
        }
        public static System.Drawing.Color ToColorDrawing(Color pColor)
        {
            int red = (int) (pColor.R * 255);
            int green = (int) (pColor.G * 255);
            int blue = (int) (pColor.B * 255);
            int alpha = (int) (pColor.A * 255);
            string hex = String.Format( "#{0:X2}{1:X2}{2:X2}", red, green, blue, alpha );

            return System.Drawing.Color.FromArgb(alpha,red,green,blue);
        }
    }
}
