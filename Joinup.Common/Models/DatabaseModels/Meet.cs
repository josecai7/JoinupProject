using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models.DatabaseModels
{
    public class Meet
    {
        [Key]
        public int MeetId { get; set; }
        public int PlanId { get; set; }
        public string UserId { get; set; }
        public User User
        {
            get; set;
        }
    }
}
