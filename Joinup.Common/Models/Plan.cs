using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models
{
    public class Plan
    {
        [Key]
        public string PlanId { get; set; }
        public string UserId { get; set; }
        public int PlanType { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int MaxParticipants { get; set; }
        [DataType(DataType.Date)]
        public DateTime PlanDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndPlanDate { get; set; }
        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(ImagePath))
                {
                    return "no_image";
                }
                return $"http://joinupbackend20180911113817.azurewebsites.net/{ImagePath.Substring(1)}";
            }
        }

    }
}
