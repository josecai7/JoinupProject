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
using Joinup.Common.Models.DatabaseModels;

namespace Joinup.Common.Models
{
    public class Plan
    {
        public Plan()
        {
            Images = new List<Image>();
            Remarks= new List<Remark>();
            Comments = new List<Comment>();
            Meets = new List<Meet>();
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
        #region References

        public List<Image> Images
        {
            get;set;
        }

        public List<Comment> Comments
        {
            get; set;
        }
        public List<Meet> Meets
        {
            get; set;
        }
        public List<Remark> Remarks
        {
            get; set;
        }

        public User User
        {
            get; set;
        }


        #endregion

        [NotMapped]
        public int CommentNumber
        {
            get
            {
                return Comments.Count;
            }
        }
        [NotMapped]
        [JsonIgnore]
        public string DefaultImageFullPath
        {
            get
            {
                string imagePath = GetDefaultImage();
                if (Images != null && Images.Count > 0 )
                {
                    Image image = Images.FirstOrDefault();
                    imagePath = image.ImageFullPath;
                }

                return imagePath;
            }
        }

        private string GetDefaultImage()
        {
            switch (PlanType)
            {
                case PLANTYPE.FOODANDDRINK:
                    return "food";
                case PLANTYPE.SPECTACLES:
                    return "spectacle";
                case PLANTYPE.SPORT:
                    return "sport";
                case PLANTYPE.LANGUAGE:
                    return "language";
                case PLANTYPE.TRAVEL:
                    return "travel";
                case PLANTYPE.SHOPPING:
                    return "shopping";
                case PLANTYPE.PARTY:
                    return "party";
                case PLANTYPE.OTHER:
                    return "other";
                default:
                    return "no_image";
            }
        }

        [NotMapped]
        [JsonIgnore]
        public bool HasImage
        {
            get
            {
                return Images.Count>0;
            }
        }

        [NotMapped]
        public List<Meet> ResumeAssistantUsers
        {
            get
            {
                return Meets.GetRange( 0, Math.Min(Meets.Count,5) );
            }
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
                        return "El plan se llevó a cabo hace "+Math.Abs(differenceInDays)+" días";
                    }
                    else
                    {
                        return "Faltan " + differenceInDays + " días para el plan";
                    }
                }
            }
        }

        [NotMapped]
        [JsonIgnore]
        public string FormattedAssistantUsers
        {
            get
            {
                if (Meets.Count == 0)
                {
                    return "Aun no hay nadie apuntado. Sé el primero.";                    
                }
                else if (Meets.Count == 1)
                {
                    return Meets.FirstOrDefault().User.Name + " va a ir";
                }
                else if (Meets.Count == 2)
                {
                    return Meets.FirstOrDefault().User.Name + " y " + (Meets.Count - 1) + " amigo van a ir";
                }
                else
                {
                    return Meets.FirstOrDefault().User.Name + " y " + (Meets.Count - 1) + " amigos van a ir";
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

        [NotMapped]
        [JsonIgnore]
        public string FormattedSport
        {
            get
            {
                if (PLANTYPE.SPORT == PlanType)
                {
                    return SPORT.GetSportTypeById(Sport);
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        [NotMapped]
        [JsonIgnore]
        public string FormattedRecommendedLevel
        {
            get
            {
                return SKILLLEVEL.GetLevelTypeById(RecommendedLevel);
            }
        }

        [NotMapped]
        [JsonIgnore]
        public string FormattedExchange
        {
            get
            {
                if (PLANTYPE.LANGUAGE == PlanType)
                {
                    return LANGUAGE.GetExchangeById(Language1,Language2);
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        [NotMapped]
        [JsonIgnore]
        public string FormattedAddress
        {
            get
            {
                if (PLANTYPE.TRAVEL == PlanType)
                {
                    return "Origen: "+Address;
                }
                else
                {
                    return Address;
                }
            }
        }

        [NotMapped]
        [JsonIgnore]
        public string FormattedDestinationAddress
        {
            get
            {
                if (PLANTYPE.TRAVEL == PlanType)
                {
                    return "Destino: "+DestinationAddress;
                }
                else
                {
                    return String.Empty;
                }
            }
        }

        [NotMapped]
        [JsonIgnore]
        public MyUserASP LoggedUser
        {
            get;set;
        }
        [NotMapped]
        [JsonIgnore]
        public bool IsPastPlan
        {
            get
            {
                return PlanDate <= DateTime.Now;
            }
        }
        [NotMapped]
        [JsonIgnore]
        public int Score
        {
            get
            {
                if (Remarks.Count == 0)
                    return 0;
                else
                    return (int)Remarks.Average( item => item.Score );
            }
        }
        [NotMapped]
        [JsonIgnore]
        public bool HasRemarks
        {
            get
            {
                return IsPastPlan && LoggedUser.Id != UserId && Remarks.Find(item => item.UserId == LoggedUser.Id) == null;
            }
        }
        [NotMapped]
        [JsonIgnore]
        public string FormattedRemarks
        {
            get
            {
                if (Remarks.Count == 1)
                {
                    return "1 Reseña";
                }                   
                else
                {
                    return Remarks.Count + " Reseñas";
                }
            }
        }

    }
}
