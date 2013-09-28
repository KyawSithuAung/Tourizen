namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDatabaseChanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Hotel", "Name", c => c.String());
            AlterColumn("dbo.Hotel", "StreetAddress", c => c.String());
            AlterColumn("dbo.Hotel", "City", c => c.String());
            AlterColumn("dbo.Hotel", "Country", c => c.String());
            AlterColumn("dbo.Hotel", "Phone1", c => c.String(maxLength: 22));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Hotel", "Phone1", c => c.String(nullable: false, maxLength: 22));
            AlterColumn("dbo.Hotel", "Country", c => c.String(nullable: false));
            AlterColumn("dbo.Hotel", "City", c => c.String(nullable: false));
            AlterColumn("dbo.Hotel", "StreetAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Hotel", "Name", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
