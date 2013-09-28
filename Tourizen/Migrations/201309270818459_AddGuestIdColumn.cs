namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGuestIdColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Booking", "GuestId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Booking", "GuestId");
        }
    }
}
