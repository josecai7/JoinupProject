using Joinup.Common.Models;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Joinup.Models
{
    public class LocalImage
    {
        public ImageSource ImageSource { get; set; }
        public byte[] ByteArray { get; set; }
        public LocalImage() { }

    }
}
