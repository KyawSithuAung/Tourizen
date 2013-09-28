namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RoomType", "Name", c => c.String());
            AddColumn("dbo.Booking", "NumberNight", c => c.Int(nullable: false));
            DropColumn("dbo.RoomType", "TypeName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.RoomType", "TypeName", c => c.String());
            DropColumn("dbo.Booking", "NumberNight");
            DropColumn("dbo.RoomType", "Name");
        }
    }
}
