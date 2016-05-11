namespace FindInCn.Shared.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersAndFavorites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavoriteItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ItemUrl = c.String(),
                        Shop_ShopId = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shops", t => t.Shop_ShopId)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Shop_ShopId)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        PassExpiration = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavoriteItems", "User_Id", "dbo.Users");
            DropForeignKey("dbo.FavoriteItems", "Shop_ShopId", "dbo.Shops");
            DropIndex("dbo.FavoriteItems", new[] { "User_Id" });
            DropIndex("dbo.FavoriteItems", new[] { "Shop_ShopId" });
            DropTable("dbo.Users");
            DropTable("dbo.FavoriteItems");
        }
    }
}
