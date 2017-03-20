namespace HomeMadeFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DailyUserOrdersAndDailyMenusAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailyMenus",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        DayOfWeek = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DailyUserOrders",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        DailyUserOrderPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AdditionalRequirements = c.String(),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Recipes", "PricePerPortion", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Recipes", "DailyMenu_Id", c => c.Guid());
            AddColumn("dbo.Recipes", "DailyUserOrder_Id", c => c.Guid());
            AddColumn("dbo.AspNetUsers", "FullName", c => c.String());
            AddColumn("dbo.AspNetUsers", "CompanyName", c => c.String());
            AddColumn("dbo.AspNetUsers", "CompanyAddress", c => c.String());
            CreateIndex("dbo.Recipes", "DailyMenu_Id");
            CreateIndex("dbo.Recipes", "DailyUserOrder_Id");
            AddForeignKey("dbo.Recipes", "DailyMenu_Id", "dbo.DailyMenus", "Id");
            AddForeignKey("dbo.Recipes", "DailyUserOrder_Id", "dbo.DailyUserOrders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DailyUserOrders", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Recipes", "DailyUserOrder_Id", "dbo.DailyUserOrders");
            DropForeignKey("dbo.Recipes", "DailyMenu_Id", "dbo.DailyMenus");
            DropIndex("dbo.DailyUserOrders", new[] { "UserId" });
            DropIndex("dbo.Recipes", new[] { "DailyUserOrder_Id" });
            DropIndex("dbo.Recipes", new[] { "DailyMenu_Id" });
            DropColumn("dbo.AspNetUsers", "CompanyAddress");
            DropColumn("dbo.AspNetUsers", "CompanyName");
            DropColumn("dbo.AspNetUsers", "FullName");
            DropColumn("dbo.Recipes", "DailyUserOrder_Id");
            DropColumn("dbo.Recipes", "DailyMenu_Id");
            DropColumn("dbo.Recipes", "PricePerPortion");
            DropTable("dbo.DailyUserOrders");
            DropTable("dbo.DailyMenus");
        }
    }
}
