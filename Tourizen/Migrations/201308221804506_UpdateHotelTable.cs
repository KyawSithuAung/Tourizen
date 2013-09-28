namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateHotelTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Hotel", "Phone1", c => c.String(nullable: false, maxLength: 22));
            AlterColumn("dbo.Hotel", "Phone2", c => c.String(maxLength: 22));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Hotel", "Phone2", c => c.String());
            AlterColumn("dbo.Hotel", "Phone1", c => c.String(nullable: false));
        }
    }
}
