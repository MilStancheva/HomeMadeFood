namespace HomeMadeFood.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DailyMenuUpdated : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyMenus", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyMenus", "Date");
        }
    }
}
