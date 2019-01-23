using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Joinup.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Foundation;
using UIKit;
using Joinup.iOS.Effects;

[assembly: ExportEffect(
    typeof( BorderlessEntryEffectRenderer ), nameof( BorderlessEntryEffect ) )]

namespace Joinup.iOS.Effects
{
    public class BorderlessEntryEffectRenderer : PlatformEffect
    {
        protected override void OnAttached()
        {
            var textField = Control as UITextField;
            textField.BorderStyle = UITextBorderStyle.None;

            var layer = textField.Layer;
            var primaryColor = (Color) App.Current.Resources["PrimaryColor"];
            layer.BackgroundColor = primaryColor.ToCGColor();
            var accentColor = (Color) App.Current.Resources["AccentColor"];
            layer.ShadowColor = accentColor.ToCGColor();
            layer.ShadowOffset = new CGSize( 0, 2 );
            layer.ShadowOpacity = 1;
            layer.ShadowRadius = 0;
        }

        protected override void OnDetached()
        {
        }
    }
}
