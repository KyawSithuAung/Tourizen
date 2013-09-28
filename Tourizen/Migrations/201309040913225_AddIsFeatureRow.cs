namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsFeatureRow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomType", "IsFeatured", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.RoomType", "IsFeatured");
        }
    }
}
