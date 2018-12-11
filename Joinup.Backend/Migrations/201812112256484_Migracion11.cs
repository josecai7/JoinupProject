namespace Joinup.Backend.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migracion11 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Messages");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        UserDisplayName = c.String(),
                        PlanId = c.String(),
                        CommentText = c.String(),
                        CommentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId);
            
        }
    }
}
