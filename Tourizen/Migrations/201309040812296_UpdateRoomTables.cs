namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRoomTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomType", "RoomImageUrl", c => c.String());
            AlterColumn("dbo.RoomType", "TypeName", c => c.String(nullable: false));
            AlterColumn("dbo.RoomUnit", "UnitNumber", c => c.String(nullable: false));
            AlterColumn("dbo.Booking", "TotalCharge", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Booking", "TotalCharge", c => c.String());
            AlterColumn("dbo.RoomUnit", "UnitNumber", c => c.String());
            AlterColumn("dbo.RoomType", "TypeName", c => c.String());
            DropColumn("dbo.RoomType", "RoomImageUrl");
        }
    }
}
