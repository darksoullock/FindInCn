namespace FindInCn.Shared.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Shops",
                c => new
                    {
                        ShopId = c.Int(nullable: false, identity: true),
                        ClassName = c.String(),
                        Name = c.String(),
                        SearchUrl = c.String(),
                        MainPage = c.String(),
                    })
                .PrimaryKey(t => t.ShopId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Shops");
        }
    }
}
