namespace ItemAPI.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatingItemEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Item", "Index", c => c.Int(nullable: false));
            AlterColumn("dbo.Item", "Value", c => c.Double(nullable: false));
            DropColumn("dbo.Item", "IsDeleted");
            DropColumn("dbo.Item", "DateAdded");
            DropColumn("dbo.Item", "DateDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Item", "DateDeleted", c => c.DateTime());
            AddColumn("dbo.Item", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Item", "IsDeleted", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Item", "Value", c => c.String(nullable: false));
            DropColumn("dbo.Item", "Index");
        }
    }
}
