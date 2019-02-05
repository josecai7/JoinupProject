using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models.DatabaseModels
{
    public class User
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string ImagePath
        {
            get
            {
                if (!string.IsNullOrEmpty(Image))
                {
                    return "https://joinupapi.azurewebsites.net/" + Image.Substring(1);
                }
                return "no_image.png";
                
            }
        }
    }
}
