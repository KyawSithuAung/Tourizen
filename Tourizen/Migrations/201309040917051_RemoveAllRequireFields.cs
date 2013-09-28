namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAllRequireFields : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RoomType", "TypeName", c => c.String());
            AlterColumn("dbo.RoomUnit", "UnitNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RoomUnit", "UnitNumber", c => c.String(nullable: false));
            AlterColumn("dbo.RoomType", "TypeName", c => c.String(nullable: false));
        }
    }
}
