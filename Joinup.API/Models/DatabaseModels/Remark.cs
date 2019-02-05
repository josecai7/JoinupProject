using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.API.Models.DatabaseModels.Models
{
    public class Remark
    {
        [Key]
        public int RemarkId { get; set; }
        public string UserId { get; set; }
        public string UserDisplayName { get; set; }
        public string UserDisplayImage { get; set; }
        public int PlanId { get; set; }
        public string PlanName { get; set; }
        public int Score { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
