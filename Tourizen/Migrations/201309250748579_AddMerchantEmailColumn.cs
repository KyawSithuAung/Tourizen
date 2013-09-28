namespace Tourizen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMerchantEmailColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotel", "MerchantEmail", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotel", "MerchantEmail");
        }
    }
}
