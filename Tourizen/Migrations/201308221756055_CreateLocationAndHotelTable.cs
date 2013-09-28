namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateLocationAndHotelTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hotel",
                c => new
                    {
                        HotelId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Rate = c.Int(nullable: false),
                        StreetAddress = c.String(nullable: false),
                        City = c.String(nullable: false),
                        Country = c.String(nullable: false),
                        Phone1 = c.String(nullable: false),
                        Phone2 = c.String(),
                        Website = c.String(),
                        Content = c.String(),
                        Status = c.Boolean(nullable: false),
                        LocationId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HotelId)
                .ForeignKey("dbo.Location", t => t.LocationId, cascadeDelete: true)
                .Index(t => t.LocationId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Hotel", new[] { "LocationId" });
            DropForeignKey("dbo.Hotel", "LocationId", "dbo.Location");
            DropTable("dbo.Hotel");
        }
    }
}
