namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoomType",
                c => new
                    {
                        RoomTypeId = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                        StandardGuest = c.Int(nullable: false),
                        MaxGuest = c.Int(nullable: false),
                        PricePerNight = c.Double(nullable: false),
                        ExtraCharge = c.Double(nullable: false),
                        Cancellable = c.Boolean(nullable: false),
                        HotelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomTypeId)
                .ForeignKey("dbo.Hotel", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.HotelId);
            
            CreateTable(
                "dbo.RoomUnit",
                c => new
                    {
                        RoomUnitId = c.Int(nullable: false, identity: true),
                        UnitNumber = c.String(),
                        HasBlockTime = c.Boolean(nullable: false),
                        RoomTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RoomUnitId)
                .ForeignKey("dbo.RoomType", t => t.RoomTypeId, cascadeDelete: true)
                .Index(t => t.RoomTypeId);
            
            CreateTable(
                "dbo.Booking",
                c => new
                    {
                        BookingId = c.Int(nullable: false, identity: true),
                        CheckIn = c.DateTime(nullable: false),
                        CheckOut = c.DateTime(nullable: false),
                        NumberRoom = c.Int(nullable: false),
                        NumberGuest = c.Int(nullable: false),
                        TotalCharge = c.String(),
                        BookingStatus = c.Boolean(nullable: false),
                        RoomTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookingId)
                .ForeignKey("dbo.RoomType", t => t.RoomTypeId, cascadeDelete: true)
                .Index(t => t.RoomTypeId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Booking", new[] { "RoomTypeId" });
            DropIndex("dbo.RoomUnit", new[] { "RoomTypeId" });
            DropIndex("dbo.RoomType", new[] { "HotelId" });
            DropForeignKey("dbo.Booking", "RoomTypeId", "dbo.RoomType");
            DropForeignKey("dbo.RoomUnit", "RoomTypeId", "dbo.RoomType");
            DropForeignKey("dbo.RoomType", "HotelId", "dbo.Hotel");
            DropTable("dbo.Booking");
            DropTable("dbo.RoomUnit");
            DropTable("dbo.RoomType");
        }
    }
}
