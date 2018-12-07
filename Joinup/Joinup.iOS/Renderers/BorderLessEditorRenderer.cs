using CoreAnimation;
using CoreGraphics;
using Joinup.Controls;
using Joinup.iOS.Renderers;
using System;
using System.ComponentModel;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer( typeof( BorderLessEditor ), typeof( BorderLessEditorRenderer ) )]

namespace Joinup.iOS.Renderers
{
    public class BorderLessEditorRenderer : EditorRenderer
    {
        public static void Init() { }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged( sender, e );

            Control.Layer.BorderWidth = 0;
        }
    }
}