using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Joinup.Common.Helpers;

namespace Joinup.Common.Models
{
    public class Plan
    {
        public Plan()
        {
            PlanImages = new List<Image>();
        }
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
        [JsonIgnore]
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
        public List<Image> PlanImages
        {
            get
            {
                return _planImages;
            }
            set
            {
                _planImages = value;
            }
        }
        [NonSerialized]
        private List<Image> _planImages;

        [NotMapped]
        public MyUserASP User
        {
            get; set;
        }


        [NotMapped]
        [JsonIgnore]
        public string FormattedPlanType
        {
            get
            {
                    switch ( PlanType )
                    {
                        case PLANTYPE.RESTAURANT:
                            return "Comida y bebida";
                        case PLANTYPE.SHOPPING:
                            return "Tiendas y compras";
                        case PLANTYPE.LANGUAGE:
                            return "Intercambio de idiomas";
                        case PLANTYPE.GOOUTFORDRINK:
                            return "Bares y ocio nocturno";
                        case PLANTYPE.OTHER:
                            return "Ocio y diversion";
                        case PLANTYPE.SPECTACLE:
                            return "Cine y espectaculos";
                        case PLANTYPE.TAKESOMETHING:
                            return "Tomar algo";
                        case PLANTYPE.TRAVEL:
                            return "Viajes y excursiones";
                        case PLANTYPE.SPORT:
                            return "Ejercicio físico y deporte";
                        default:
                            return "Categoria no definida";
                    }
            }
        }

        [NotMapped]
        [JsonIgnore]
        public string FormattedDate
        {
            get
            {
                if ( PlanDate.Date == EndPlanDate.Date )
                {
                    return DateTimeHelper.GetFormattedDayMonth(PlanDate);
                }
                else
                {
                    return DateTimeHelper.GetFormattedDayMonth( PlanDate )+" - "+DateTimeHelper.GetFormattedDayMonth( EndPlanDate );
                }
            }
        }
        [NotMapped]
        [JsonIgnore]
        public string FormattedHour
        {
            get
            {
                if ( PlanDate == EndPlanDate )
                {
                    return DateTimeHelper.GetFormattedHour( PlanDate );
                }
                else
                {
                    return DateTimeHelper.GetFormattedHour( PlanDate ) + " - " + DateTimeHelper.GetFormattedHour( EndPlanDate );
                }
            }
        }

    }
}
