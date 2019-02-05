using Joinup.API.Models.DatabaseModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.API.Models
{
    public class DataContext: DbContext
    {
        public DataContext(): base("DefaultConnection")
        {

        }

        public System.Data.Entity.DbSet<Plan> Plans { get; set; }

        public System.Data.Entity.DbSet<Image> Images { get; set; }

        public System.Data.Entity.DbSet<Meet> Meets { get; set; }

        public System.Data.Entity.DbSet<Comment> Comments { get; set; }

        public System.Data.Entity.DbSet<Remark> Remarks { get; set; }
    }
}
