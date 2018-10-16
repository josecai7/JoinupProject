using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models
{
    public class Image
    {
        [Key]
        public int ImageId { get; set; }
        public int EntityId { get; set; }
        public string ImagePath { get; set; }
        [NotMapped]
        public byte[] ImageArray { get; set; }
        [NotMapped]
        public string ImageFullPath
        {
            get
            {
                if ( string.IsNullOrEmpty( ImagePath ) )
                {
                    return "no_image";
                }
                return $"https://joinupapi.azurewebsites.net/{ImagePath.Substring( 1 )}";
            }
        }
    }
}
