namespace HomeMadeFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IngredientsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        FoodType = c.Int(nullable: false),
                        MeasuringUnit = c.Int(nullable: false),
                        PricePerMeasuringUnit = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Ingredients");
        }
    }
}
