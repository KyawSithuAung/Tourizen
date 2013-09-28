namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBookedUnitTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BlockEvent", "RoomUnitId", "dbo.RoomUnit");
            DropIndex("dbo.BlockEvent", new[] { "RoomUnitId" });
            DropTable("dbo.BlockEvent");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.BlockEventId);
            
            CreateIndex("dbo.BlockEvent", "RoomUnitId");
            AddForeignKey("dbo.BlockEvent", "RoomUnitId", "dbo.RoomUnit", "RoomUnitId", cascadeDelete: true);
        }
    }
}
