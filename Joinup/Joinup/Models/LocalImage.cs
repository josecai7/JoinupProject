using Joinup.Common.Models;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Joinup.Models
{
    public class LocalImage : Common.Models.Image
    {
        public ImageSource ImageSource { get; set; }
        public MediaFile ImageMedia { get; set; }
        public LocalImage() { }

        public Common.Models.Image ToImage()
        {
            return new Common.Models.Image
            {
                ImageId = this.ImageId,
                ImageArray = this.ImageArray,
                ImagePath = this.ImagePath,
                ImageFullPath = this.ImageFullPath,
                EntityId = this.EntityId,
            };
        }
    }
}
