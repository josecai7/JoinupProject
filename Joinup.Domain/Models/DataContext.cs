using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Domain.Models
{
    public class DataContext: DbContext
    {
        public DataContext(): base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<Joinup.Common.Models.Comment> Comments { get; set; }

        public System.Data.Entity.DbSet<Joinup.Common.Models.Plan> Plans { get; set; }
    }
}
