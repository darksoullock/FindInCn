namespace FindInCn.Shared.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<FindInCn.Shared.Models.DB.CnContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "FindInCn.Shared.Models.DB.CnContext";
        }

        protected override void Seed(FindInCn.Shared.Models.DB.CnContext context)
        {
            context.Categories.AddOrUpdate(new Models.DB.Category[]
            {
                new Models.DB.Category() {Name="All Categories" },
                new Models.DB.Category() {Name="Mobile Phones" },
                new Models.DB.Category() {Name="Tablet PC & Accessories" },
                new Models.DB.Category() {Name="Computers & Networking" },
                new Models.DB.Category() {Name="Electrical & Tools" },
                new Models.DB.Category() {Name="Toys & Hobbies" },
                new Models.DB.Category() {Name="Consumer Electronics" },
                new Models.DB.Category() {Name="Automobiles & Motorcycle" },
                new Models.DB.Category() {Name="Home & Garden" },
                new Models.DB.Category() {Name="Outdoors & Sports" },
                new Models.DB.Category() {Name="LED Lights & Flashlights" },
                new Models.DB.Category() {Name="Apple Accessories" },
                new Models.DB.Category() {Name="Watches & Jewelry" },
                new Models.DB.Category() {Name="Apparel" }
            });

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}