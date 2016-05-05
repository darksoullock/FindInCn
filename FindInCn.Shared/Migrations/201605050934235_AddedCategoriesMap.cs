namespace FindInCn.Shared.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedCategoriesMap : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CategoryToShopSpecificIdMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RemoteCategoryId = c.String(),
                        Category_Id = c.Int(),
                        Shop_ShopId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.Category_Id)
                .ForeignKey("dbo.Shops", t => t.Shop_ShopId)
                .Index(t => t.Category_Id)
                .Index(t => t.Shop_ShopId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CategoryToShopSpecificIdMappings", "Shop_ShopId", "dbo.Shops");
            DropForeignKey("dbo.CategoryToShopSpecificIdMappings", "Category_Id", "dbo.Categories");
            DropIndex("dbo.CategoryToShopSpecificIdMappings", new[] { "Shop_ShopId" });
            DropIndex("dbo.CategoryToShopSpecificIdMappings", new[] { "Category_Id" });
            DropTable("dbo.CategoryToShopSpecificIdMappings");
        }
    }
}
