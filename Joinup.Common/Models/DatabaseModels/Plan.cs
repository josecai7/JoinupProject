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
using Joinup.Common.Models.SelectablesModels;

namespace Joinup.Common.Models
{
    public class Plan
    {
        public Plan()
        {
            PlanImages = new List<Image>();
            AssistantUsers = new List<MyUserASP>();
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
        public DateTime PlanDate
        { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndPlanDate { get; set; }

        #region Specific atributes
        public int FoodType { get; set; }
        public int Sport { get; set; }
        public int RecommendedLevel { get; set; }
        public string Link { get; set; }
        public int Language1 { get; set; }
        public int Language2 { get; set; }
        public string DestinationAddress { get; set; }
        public double DestinationLatitude { get; set; }
        public double DestinationLongitude { get; set; }
        #endregion

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
        public List<MyUserASP> AssistantUsers
        {
            get; set;
        }
        [NotMapped]
        public List<MyUserASP> ResumeAssistantUsers
        {
            get
            {
                return AssistantUsers.GetRange( 0, Math.Min(AssistantUsers.Count,5) );
            }
        }

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
                        case PLANTYPE.FOODANDDRINK:
                            return "Comida y bebida";
                        case PLANTYPE.SPECTACLES:
                            return "Conciertos y espectaculos";
                        case PLANTYPE.SPORT:
                            return "Deportes";
                        case PLANTYPE.LANGUAGE:
                            return "Intercambio de idiomas";
                        case PLANTYPE.TRAVEL:
                            return "Viajes";
                        case PLANTYPE.SHOPPING:
                        return "Ir de compras";
                        case PLANTYPE.OTHER:
                            return "Otros";
                        default:
                            return "Categoria no definida";
                    }
            }
        }

        [NotMapped]
        [JsonIgnore]
        public string FormattedDateTime
        {
            get
            {
                return DateTimeHelper.GetCompletedPlanDate( PlanDate , EndPlanDate );
            }
        }

        [NotMapped]
        [JsonIgnore]
        public string TimeRemaining
        {
            get
            {
                TimeSpan ts = PlanDate.Date - DateTime.Now.Date;

                int differenceInDays = ts.Days;

                if (differenceInDays == 0)
                {
                    return "El plan se llevará a cabo hoy";
                }
                else
                {
                    if (PlanDate.Date < DateTime.Now.Date)
                    {
                        return "El plan se llevó a cabo hace "+differenceInDays+" días";
                    }
                    else
                    {
                        return "Faltan " + differenceInDays + " días para el plan";
                    }
                }
                return DateTimeHelper.GetCompletedPlanDate(PlanDate, EndPlanDate);
            }
        }

        [NotMapped]
        [JsonIgnore]
        public string FormattedAssistantUsers
        {
            get
            {
                if (AssistantUsers.Count == 0)
                {
                    return "Aun no hay nadie apuntado. Sé el primero.";                    
                }
                else if (AssistantUsers.Count == 1)
                {
                    return AssistantUsers.FirstOrDefault().Name + " va a ir";
                }
                else if (AssistantUsers.Count == 2)
                {
                    return AssistantUsers.FirstOrDefault().Name + " y " + (AssistantUsers.Count - 1) + " amigo van a ir";
                }
                else
                {
                    return AssistantUsers.FirstOrDefault().Name + " y " + (AssistantUsers.Count - 1) + " amigos van a ir";
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
                    return DateTimeHelper.GetFormattedDayMonth( PlanDate );
                }
                else
                {
                    return DateTimeHelper.GetFormattedDayMonth( PlanDate ) + " - " + DateTimeHelper.GetFormattedDayMonth( EndPlanDate );
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

        [NotMapped]
        [JsonIgnore]
        public string FormattedFoodType
        {
            get
            {
                if (PLANTYPE.FOODANDDRINK==PlanType)
                {
                    return FOODTYPE.GetFoodTypeById(FoodType);
                }
                else
                {
                    return String.Empty;
                }
            }
        }

    }
}
