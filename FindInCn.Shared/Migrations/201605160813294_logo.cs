namespace FindInCn.Shared.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class logo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Shops", "Logo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Shops", "Logo");
        }
    }
}
