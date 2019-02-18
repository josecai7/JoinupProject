using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Models
{
    public class FoodType
    {
        [Key]
        public int Id { get; set; }
        public int Name { get; set; }
    }
}
