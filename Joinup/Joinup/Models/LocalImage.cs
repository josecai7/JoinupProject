using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace Joinup.Models
{
    public class LocalImage : Common.Models.Image
    {
        private ImageSource imagesource;
        public LocalImage() { }

        public LocalImage(Common.Models.Image pImage)
        {
            ImageId = pImage.ImageId;
            EntityId = pImage.EntityId;
            ImagePath = pImage.ImagePath;
            ImageArray = pImage.ImageArray;
            ImageFullPath = pImage.ImageFullPath;
            Stream stream = new MemoryStream(pImage.ImageArray);
            ImageSource = ImageSource.FromStream(() => { return stream; });
        }

        public ImageSource ImageSource
        {
            get
            {
                if (string.IsNullOrEmpty(ImageFullPath) || string.Equals("no_image", ImageFullPath))
                {
                    return imagesource;
                }
                else
                {
                    return ImageSource.FromUri(new Uri(ImageFullPath));
                }
            }
            set { imagesource = value; }
        }

        public Common.Models.Image ToImage()
        {
            return new Common.Models.Image
            {
                ImageId = ImageId,
                EntityId = EntityId,
                ImagePath = ImagePath,
                ImageArray = ImageArray,
            };
        }
    }
}
