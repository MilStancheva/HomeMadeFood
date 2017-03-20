namespace HomeMadeFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecipesUpdatedWithNewModels : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Recipes", "DailyMenu_Id", "dbo.DailyMenus");
            DropForeignKey("dbo.Recipes", "DailyUserOrder_Id", "dbo.DailyUserOrders");
            DropIndex("dbo.Recipes", new[] { "DailyMenu_Id" });
            DropIndex("dbo.Recipes", new[] { "DailyUserOrder_Id" });
            CreateTable(
                "dbo.RecipeDailyMenus",
                c => new
                    {
                        Recipe_Id = c.Guid(nullable: false),
                        DailyMenu_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Recipe_Id, t.DailyMenu_Id })
                .ForeignKey("dbo.Recipes", t => t.Recipe_Id, cascadeDelete: true)
                .ForeignKey("dbo.DailyMenus", t => t.DailyMenu_Id, cascadeDelete: true)
                .Index(t => t.Recipe_Id)
                .Index(t => t.DailyMenu_Id);
            
            CreateTable(
                "dbo.DailyUserOrderRecipes",
                c => new
                    {
                        DailyUserOrder_Id = c.Guid(nullable: false),
                        Recipe_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.DailyUserOrder_Id, t.Recipe_Id })
                .ForeignKey("dbo.DailyUserOrders", t => t.DailyUserOrder_Id, cascadeDelete: true)
                .ForeignKey("dbo.Recipes", t => t.Recipe_Id, cascadeDelete: true)
                .Index(t => t.DailyUserOrder_Id)
                .Index(t => t.Recipe_Id);
            
            DropColumn("dbo.Recipes", "DailyMenu_Id");
            DropColumn("dbo.Recipes", "DailyUserOrder_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Recipes", "DailyUserOrder_Id", c => c.Guid());
            AddColumn("dbo.Recipes", "DailyMenu_Id", c => c.Guid());
            DropForeignKey("dbo.DailyUserOrderRecipes", "Recipe_Id", "dbo.Recipes");
            DropForeignKey("dbo.DailyUserOrderRecipes", "DailyUserOrder_Id", "dbo.DailyUserOrders");
            DropForeignKey("dbo.RecipeDailyMenus", "DailyMenu_Id", "dbo.DailyMenus");
            DropForeignKey("dbo.RecipeDailyMenus", "Recipe_Id", "dbo.Recipes");
            DropIndex("dbo.DailyUserOrderRecipes", new[] { "Recipe_Id" });
            DropIndex("dbo.DailyUserOrderRecipes", new[] { "DailyUserOrder_Id" });
            DropIndex("dbo.RecipeDailyMenus", new[] { "DailyMenu_Id" });
            DropIndex("dbo.RecipeDailyMenus", new[] { "Recipe_Id" });
            DropTable("dbo.DailyUserOrderRecipes");
            DropTable("dbo.RecipeDailyMenus");
            CreateIndex("dbo.Recipes", "DailyUserOrder_Id");
            CreateIndex("dbo.Recipes", "DailyMenu_Id");
            AddForeignKey("dbo.Recipes", "DailyUserOrder_Id", "dbo.DailyUserOrders", "Id");
            AddForeignKey("dbo.Recipes", "DailyMenu_Id", "dbo.DailyMenus", "Id");
        }
    }
}
