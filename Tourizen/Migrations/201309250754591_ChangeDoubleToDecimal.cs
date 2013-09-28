namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeDoubleToDecimal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoomType", "PricePerNight", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Booking", "TotalCharge", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Booking", "TotalCharge", c => c.Double(nullable: false));
            AlterColumn("dbo.RoomType", "PricePerNight", c => c.Double(nullable: false));
        }
    }
}
