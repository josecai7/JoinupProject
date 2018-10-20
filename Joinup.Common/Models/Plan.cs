using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models
{
    public class Plan
    {
        [Key]
        public int PlanId { get; set; }
        public string UserId { get; set; }
        public int PlanType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int MaxParticipants { get; set; }
        [DataType(DataType.Date)]
        public DateTime PlanDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndPlanDate { get; set; }

        [NotMapped]
        public string DefaultImageFullPath
        {
            get
            {
                string imagePath = "no_image";
                if ( PlanImages != null && PlanImages.Count > 0 )
                {
                    Image image = PlanImages.FirstOrDefault();
                    imagePath = image.ImageFullPath;
                }

                return imagePath;
            }
        }
        [NotMapped]
        public List<Image> PlanImages { get; set; }

    }
}
