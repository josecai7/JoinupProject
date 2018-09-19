using Joinup.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joinup.Backend.Models
{
    public class PlanView:Plan
    {
        public HttpPostedFileBase ImageFile { get; set; }

        public Plan ToPlan(string ImagePath)
        {
            return new Plan
            {
                PlanId = this.PlanId,
                UserId = this.UserId,
                PlanType = this.PlanType,
                Name = this.Name,
                Description = this.Description,
                ImagePath = ImagePath,
                Latitude = this.Latitude,
                Longitude = this.Longitude,
                MaxParticipants = this.MaxParticipants,
                PlanDate = this.PlanDate,
                EndPlanDate = this.EndPlanDate,
            };
        }
    }
}