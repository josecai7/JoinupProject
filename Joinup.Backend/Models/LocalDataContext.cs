using Joinup.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Joinup.Backend.Models
{
    public class LocalDataContext: DataContext
    {
        public System.Data.Entity.DbSet<Joinup.Common.Models.Comment> Comments { get; set; }
    }
}