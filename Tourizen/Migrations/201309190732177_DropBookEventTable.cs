namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropBookEventTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookEvent", "BookingId", "dbo.Booking");
            DropForeignKey("dbo.BookEvent", "RoomUnitId", "dbo.RoomUnit");
            DropIndex("dbo.BookEvent", new[] { "BookingId" });
            DropIndex("dbo.BookEvent", new[] { "RoomUnitId" });
            DropTable("dbo.BookEvent");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BookEvent",
                c => new
                    {
                        BookEventId = c.Int(nullable: false, identity: true),
                        GuestName = c.String(),
                        CheckInDate = c.DateTime(nullable: false),
                        CheckOutDate = c.DateTime(nullable: false),
                        BookingId = c.Int(nullable: false),
                        RoomUnitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookEventId);
            
            CreateIndex("dbo.BookEvent", "RoomUnitId");
            CreateIndex("dbo.BookEvent", "BookingId");
            AddForeignKey("dbo.BookEvent", "RoomUnitId", "dbo.RoomUnit", "RoomUnitId", cascadeDelete: true);
            AddForeignKey("dbo.BookEvent", "BookingId", "dbo.Booking", "BookingId", cascadeDelete: true);
        }
    }
}
