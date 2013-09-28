namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeIdToLocationId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Location", "LocationId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Location", new[] { "Id" });
            AddPrimaryKey("dbo.Location", "LocationId");
            DropColumn("dbo.Location", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Location", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Location", new[] { "LocationId" });
            AddPrimaryKey("dbo.Location", "Id");
            DropColumn("dbo.Location", "LocationId");
        }
    }
}
