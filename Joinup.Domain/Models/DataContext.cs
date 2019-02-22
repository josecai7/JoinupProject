using Joinup.Common.Models.DatabaseModels;
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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating( modelBuilder );

            modelBuilder.Entity<Plan>()
                .HasOptional<Sport>( s => s.Sport )
                .WithMany( p => p.Plans )
                .HasForeignKey( p => p.SportId );

            modelBuilder.Entity<Plan>()
                .HasOptional<FoodType>( f => f.FoodType )
                .WithMany( p => p.Plans )
                .HasForeignKey( p => p.FoodTypeId );

            modelBuilder.Entity<Plan>()
                .HasOptional<SkillLevel>( s => s.SkillLevel )
                .WithMany( p => p.Plans )
                .HasForeignKey( p => p.SkillLevelId );

            modelBuilder.Entity<PlanLanguage>().HasKey( sc => new { sc.PlanId, sc.LanguageId } );

            modelBuilder.Entity<PlanLanguage>()
                .HasRequired<Plan>( sc => sc.Plan )
                .WithMany( s => s.PlanLanguages )
                .HasForeignKey( sc => sc.PlanId );


            modelBuilder.Entity<PlanLanguage>()
                .HasRequired<Language>( sc => sc.Language )
                .WithMany( s => s.PlanLanguages )
                .HasForeignKey( sc => sc.LanguageId );
        }

        public System.Data.Entity.DbSet<Joinup.Common.Models.DatabaseModels.Plan> Plans { get; set; }

        public System.Data.Entity.DbSet<Joinup.Common.Models.DatabaseModels.Image> Images { get; set; }

        public System.Data.Entity.DbSet<Joinup.Common.Models.DatabaseModels.Meet> Meets { get; set; }

        public System.Data.Entity.DbSet<Joinup.Common.Models.DatabaseModels.Comment> Comments { get; set; }

        public System.Data.Entity.DbSet<Joinup.Common.Models.DatabaseModels.Remark> Remarks { get; set; }

        public System.Data.Entity.DbSet<Joinup.Common.Models.DatabaseModels.User> Users { get; set; }

        public System.Data.Entity.DbSet<Joinup.Common.Models.DatabaseModels.Sport> Sports { get; set; }

        public System.Data.Entity.DbSet<Joinup.Common.Models.DatabaseModels.SkillLevel> SkillLevel { get; set; }

        public System.Data.Entity.DbSet<Joinup.Common.Models.DatabaseModels.FoodType> FoodType { get; set; }

        public System.Data.Entity.DbSet<Joinup.Common.Models.DatabaseModels.Language> Languages { get; set; }
    }
}
