using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models
{
    public class Image
    {
        public string ImageId { get; set; }
        public string EntityId { get; set; }
        public string ImagePath { get; set; }
        public string ImageFullPath
        {
            get
            {
                if ( string.IsNullOrEmpty( ImagePath ) )
                {
                    return "no_image";
                }
                return $"http://joinupbackend20180911113817.azurewebsites.net/{ImagePath.Substring( 1 )}";
            }
        }
    }
}
