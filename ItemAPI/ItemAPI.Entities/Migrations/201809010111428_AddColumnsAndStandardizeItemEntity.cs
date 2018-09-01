namespace ItemAPI.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnsAndStandardizeItemEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Item", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Item", "DateDeleted", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Item", "DateDeleted");
            DropColumn("dbo.Item", "DateAdded");
            DropColumn("dbo.Item", "IsDeleted");
        }
    }
}
