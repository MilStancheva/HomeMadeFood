namespace HomeMadeFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FoodCategoryIdFixed : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Ingredients", new[] { "FoodcategoryId" });
            CreateIndex("dbo.Ingredients", "FoodCategoryId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ingredients", new[] { "FoodCategoryId" });
            CreateIndex("dbo.Ingredients", "FoodcategoryId");
        }
    }
}
