﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Joinup.Controls;
using Joinup.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer( typeof( BorderLessTimePicker ), typeof( BorderLessTimePickerRenderer ) )]

namespace Joinup.Droid.Renderers
{
    class BorderLessTimePickerRenderer : TimePickerRenderer
    {
        public BorderLessTimePickerRenderer(Context context) : base( context )
        {
            AutoPackage = false;
        }
        public static void Init() { }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.TimePicker> e)
        {
            base.OnElementChanged( e );
            if (e.OldElement == null)
            {
                Control.Background = null;

                var layoutParams = new MarginLayoutParams( Control.LayoutParameters );
                layoutParams.SetMargins( 0, 0, 0, 0 );
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding( 0, 0, 0, 0 );
                SetPadding( 0, 0, 0, 0 );
            }
        }
    }
}