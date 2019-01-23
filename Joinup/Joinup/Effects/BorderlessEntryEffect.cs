using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Joinup.Effects
{
    public class BorderlessEntryEffect : RoutingEffect
    {
        public BorderlessEntryEffect()
            : base( $"{nameof( Joinup )}.{nameof( BorderlessEntryEffect )}" )
        {
        }
    }
}
