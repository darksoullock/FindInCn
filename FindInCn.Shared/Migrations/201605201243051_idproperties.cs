
namespace FindInCn.Shared.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class idproperties : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FavoriteItems", "Shop_ShopId", "dbo.Shops");
            DropForeignKey("dbo.FavoriteItems", "User_Id", "dbo.Users");
            DropIndex("dbo.FavoriteItems", new[] { "Shop_ShopId" });
            DropIndex("dbo.FavoriteItems", new[] { "User_Id" });
            RenameColumn(table: "dbo.FavoriteItems", name: "Shop_ShopId", newName: "ShopId");
            RenameColumn(table: "dbo.FavoriteItems", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.FavoriteItems", "ShopId", c => c.Int(nullable: false));
            AlterColumn("dbo.FavoriteItems", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.FavoriteItems", "UserId");
            CreateIndex("dbo.FavoriteItems", "ShopId");
            AddForeignKey("dbo.FavoriteItems", "ShopId", "dbo.Shops", "ShopId", cascadeDelete: true);
            AddForeignKey("dbo.FavoriteItems", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavoriteItems", "UserId", "dbo.Users");
            DropForeignKey("dbo.FavoriteItems", "ShopId", "dbo.Shops");
            DropIndex("dbo.FavoriteItems", new[] { "ShopId" });
            DropIndex("dbo.FavoriteItems", new[] { "UserId" });
            AlterColumn("dbo.FavoriteItems", "UserId", c => c.Int());
            AlterColumn("dbo.FavoriteItems", "ShopId", c => c.Int());
            RenameColumn(table: "dbo.FavoriteItems", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.FavoriteItems", name: "ShopId", newName: "Shop_ShopId");
            CreateIndex("dbo.FavoriteItems", "User_Id");
            CreateIndex("dbo.FavoriteItems", "Shop_ShopId");
            AddForeignKey("dbo.FavoriteItems", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.FavoriteItems", "Shop_ShopId", "dbo.Shops", "ShopId");
        }
    }
}
