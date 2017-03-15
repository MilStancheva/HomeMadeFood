namespace HomeMadeFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecipieModelAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recipies",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Title = c.String(maxLength: 50),
                        Describtion = c.String(nullable: false),
                        Preparation = c.String(nullable: false),
                        CostPerPortion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantityPerPortion = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Title, unique: true);
            
            AddColumn("dbo.Ingredients", "Recipie_Id", c => c.Guid());
            AlterColumn("dbo.Ingredients", "Name", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Ingredients", "Name", unique: true);
            CreateIndex("dbo.Ingredients", "Recipie_Id");
            AddForeignKey("dbo.Ingredients", "Recipie_Id", "dbo.Recipies", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ingredients", "Recipie_Id", "dbo.Recipies");
            DropIndex("dbo.Recipies", new[] { "Title" });
            DropIndex("dbo.Ingredients", new[] { "Recipie_Id" });
            DropIndex("dbo.Ingredients", new[] { "Name" });
            AlterColumn("dbo.Ingredients", "Name", c => c.String());
            DropColumn("dbo.Ingredients", "Recipie_Id");
            DropTable("dbo.Recipies");
        }
    }
}
