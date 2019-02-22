using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models.DatabaseModels
{
    public class Sport
    {
        [Key]
        public int SportId { get; set; }
        public string Name { get; set; }

        public ICollection<Plan> Plans { get; set; }
    }
}
