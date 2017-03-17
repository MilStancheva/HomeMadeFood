namespace HomeMadeFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialAfterChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RecipeIngredients", "Recipe_Id", "dbo.Recipes");
            DropForeignKey("dbo.RecipeIngredients", "Ingredient_Id", "dbo.Ingredients");
            DropIndex("dbo.Ingredients", new[] { "Name" });
            DropIndex("dbo.RecipeIngredients", new[] { "Recipe_Id" });
            DropIndex("dbo.RecipeIngredients", new[] { "Ingredient_Id" });
            CreateTable(
                "dbo.FoodCategories",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        FoodType = c.Int(nullable: false),
                        MeasuringUnit = c.Int(nullable: false),
                        CostOfAllCategoryIngredients = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantityOfAllCategoryIngredients = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name);
            
            AddColumn("dbo.Ingredients", "QuantityInMeasuringUnit", c => c.Double(nullable: false));
            AddColumn("dbo.Ingredients", "FoodcategoryId", c => c.Guid(nullable: false));
            AddColumn("dbo.Ingredients", "RecipeId", c => c.Guid(nullable: false));
            AddColumn("dbo.Recipes", "DishType", c => c.Int(nullable: false));
            CreateIndex("dbo.Ingredients", "Name");
            CreateIndex("dbo.Ingredients", "FoodcategoryId");
            CreateIndex("dbo.Ingredients", "RecipeId");
            AddForeignKey("dbo.Ingredients", "FoodcategoryId", "dbo.FoodCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Ingredients", "RecipeId", "dbo.Recipes", "Id", cascadeDelete: true);
            DropColumn("dbo.Ingredients", "FoodType");
            DropColumn("dbo.Ingredients", "MeasuringUnit");
            DropColumn("dbo.Ingredients", "Quantity");
            DropTable("dbo.RecipeIngredients");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RecipeIngredients",
                c => new
                    {
                        Recipe_Id = c.Guid(nullable: false),
                        Ingredient_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Recipe_Id, t.Ingredient_Id });
            
            AddColumn("dbo.Ingredients", "Quantity", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Ingredients", "MeasuringUnit", c => c.Int(nullable: false));
            AddColumn("dbo.Ingredients", "FoodType", c => c.Int(nullable: false));
            DropForeignKey("dbo.Ingredients", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.Ingredients", "FoodcategoryId", "dbo.FoodCategories");
            DropIndex("dbo.FoodCategories", new[] { "Name" });
            DropIndex("dbo.Ingredients", new[] { "RecipeId" });
            DropIndex("dbo.Ingredients", new[] { "FoodcategoryId" });
            DropIndex("dbo.Ingredients", new[] { "Name" });
            DropColumn("dbo.Recipes", "DishType");
            DropColumn("dbo.Ingredients", "RecipeId");
            DropColumn("dbo.Ingredients", "FoodcategoryId");
            DropColumn("dbo.Ingredients", "QuantityInMeasuringUnit");
            DropTable("dbo.FoodCategories");
            CreateIndex("dbo.RecipeIngredients", "Ingredient_Id");
            CreateIndex("dbo.RecipeIngredients", "Recipe_Id");
            CreateIndex("dbo.Ingredients", "Name", unique: true);
            AddForeignKey("dbo.RecipeIngredients", "Ingredient_Id", "dbo.Ingredients", "Id", cascadeDelete: true);
            AddForeignKey("dbo.RecipeIngredients", "Recipe_Id", "dbo.Recipes", "Id", cascadeDelete: true);
        }
    }
}
