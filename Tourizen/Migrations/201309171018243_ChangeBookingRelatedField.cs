namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBookingRelatedField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomType", "GuestPerRoom", c => c.Int(nullable: false));
            AddColumn("dbo.RoomType", "RoomDescription", c => c.String());
            AlterColumn("dbo.Booking", "BookingStatus", c => c.Int(nullable: false));
            DropColumn("dbo.RoomType", "StandardGuest");
            DropColumn("dbo.RoomType", "MaxGuest");
            DropColumn("dbo.RoomType", "ExtraCharge");
            DropColumn("dbo.RoomUnit", "HasBlockTime");
            DropColumn("dbo.Booking", "NumberGuest");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Booking", "NumberGuest", c => c.Int(nullable: false));
            AddColumn("dbo.RoomUnit", "HasBlockTime", c => c.Boolean(nullable: false));
            AddColumn("dbo.RoomType", "ExtraCharge", c => c.Double(nullable: false));
            AddColumn("dbo.RoomType", "MaxGuest", c => c.Int(nullable: false));
            AddColumn("dbo.RoomType", "StandardGuest", c => c.Int(nullable: false));
            AlterColumn("dbo.Booking", "BookingStatus", c => c.Boolean(nullable: false));
            DropColumn("dbo.RoomType", "RoomDescription");
            DropColumn("dbo.RoomType", "GuestPerRoom");
        }
    }
}
