using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models.DatabaseModels
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string UserId { get; set; }
        public string UserDisplayName { get; set; }
        public int PlanId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
    }
}
