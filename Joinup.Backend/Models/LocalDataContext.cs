using Joinup.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joinup.Backend.Models
{
    public class LocalDataContext: DataContext
    {
        public System.Data.Entity.DbSet<Joinup.Common.Models.Image> Images { get; set; }

        public System.Data.Entity.DbSet<Joinup.Common.Models.DatabaseModels.Comment> Comments { get; set; }

        public System.Data.Entity.DbSet<Joinup.Common.Models.DatabaseModels.Remark> Remarks { get; set; }

        public System.Data.Entity.DbSet<Joinup.Common.Models.DatabaseModels.User> Users { get; set; }
    }
}