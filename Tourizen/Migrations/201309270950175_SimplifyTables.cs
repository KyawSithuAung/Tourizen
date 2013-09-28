namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SimplifyTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Hotel", "LocationId", "dbo.Location");
            DropForeignKey("dbo.RoomType", "HotelId", "dbo.Hotel");
            DropForeignKey("dbo.RoomUnit", "RoomTypeId", "dbo.RoomType");
            DropForeignKey("dbo.Booking", "RoomTypeId", "dbo.RoomType");
            DropIndex("dbo.Hotel", new[] { "LocationId" });
            DropIndex("dbo.RoomType", new[] { "HotelId" });
            DropIndex("dbo.RoomUnit", new[] { "RoomTypeId" });
            DropIndex("dbo.Booking", new[] { "RoomTypeId" });
            AlterColumn("dbo.Hotel", "Phone1", c => c.String());
            AlterColumn("dbo.Hotel", "Phone2", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Hotel", "Phone2", c => c.String(maxLength: 22));
            AlterColumn("dbo.Hotel", "Phone1", c => c.String(maxLength: 22));
            CreateIndex("dbo.Booking", "RoomTypeId");
            CreateIndex("dbo.RoomUnit", "RoomTypeId");
            CreateIndex("dbo.RoomType", "HotelId");
            CreateIndex("dbo.Hotel", "LocationId");
            AddForeignKey("dbo.Booking", "RoomTypeId", "dbo.RoomType", "RoomTypeId", cascadeDelete: true);
            AddForeignKey("dbo.RoomUnit", "RoomTypeId", "dbo.RoomType", "RoomTypeId", cascadeDelete: true);
            AddForeignKey("dbo.RoomType", "HotelId", "dbo.Hotel", "HotelId", cascadeDelete: true);
            AddForeignKey("dbo.Hotel", "LocationId", "dbo.Location", "LocationId", cascadeDelete: true);
        }
    }
}
