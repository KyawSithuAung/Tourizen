namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTables : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.BookEventId)
                .ForeignKey("dbo.Booking", t => t.BookingId, cascadeDelete: false)
                .Index(t => t.BookingId)
                .ForeignKey("dbo.RoomUnit", t => t.RoomUnitId, cascadeDelete: true)
                .Index(t => t.RoomUnitId);
            
            CreateTable(
                "dbo.BlockEvent",
                c => new
                    {
                        BlockEventId = c.Int(nullable: false, identity: true),
                        Caption = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        RoomUnitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BlockEventId)
                .ForeignKey("dbo.RoomUnit", t => t.RoomUnitId, cascadeDelete: true)
                .Index(t => t.RoomUnitId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.BlockEvent", new[] { "RoomUnitId" });
            DropIndex("dbo.BookEvent", new[] { "RoomUnitId" });
            DropIndex("dbo.BookEvent", new[] { "BookingId" });
            DropForeignKey("dbo.BlockEvent", "RoomUnitId", "dbo.RoomUnit");
            DropForeignKey("dbo.BookEvent", "RoomUnitId", "dbo.RoomUnit");
            DropForeignKey("dbo.BookEvent", "BookingId", "dbo.Booking");
            DropTable("dbo.BlockEvent");
            DropTable("dbo.BookEvent");
        }
    }
}
