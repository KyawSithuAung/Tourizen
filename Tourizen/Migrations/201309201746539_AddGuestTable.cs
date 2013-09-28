namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGuestTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Guest",
                c => new
                    {
                        GuestId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.GuestId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Guest");
        }
    }
}
