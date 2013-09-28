namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPaidColumnToBookingTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Booking", "Paid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Booking", "Paid");
        }
    }
}
