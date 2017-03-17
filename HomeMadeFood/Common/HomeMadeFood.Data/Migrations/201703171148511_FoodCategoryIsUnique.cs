namespace HomeMadeFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FoodCategoryIsUnique : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.FoodCategories", new[] { "Name" });
            CreateIndex("dbo.FoodCategories", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.FoodCategories", new[] { "Name" });
            CreateIndex("dbo.FoodCategories", "Name");
        }
    }
}
