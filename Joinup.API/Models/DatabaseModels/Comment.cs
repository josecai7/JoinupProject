using Joinup.Common.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.API.Models.DatabaseModels.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }
        public string UserId { get; set; }
        public string UserDisplayName { get; set; }
        public string UserDisplayImage { get; set; }
        public int PlanId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }


        [NotMapped]
        [JsonIgnore]
        public string FormattedCommentDate
        {
            get
            {
                return DateTimeHelper.GetFormattedCommentDate(CommentDate);
            }
        }

        [NotMapped]
        [JsonIgnore]
        public string LoggedUserId { get; set; }

        [NotMapped]
        [JsonIgnore]
        public bool IsMyComment
        {
            get
            {
                return UserId == LoggedUserId;
            }
        }

        [NotMapped]
        [JsonIgnore]
        public bool NotIsMyComment
        {
            get
            {
                return UserId != LoggedUserId;
            }
        }
    }
}
