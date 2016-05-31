namespace FindInCn.Shared.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryUrl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Url", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Url");
        }
    }
}
