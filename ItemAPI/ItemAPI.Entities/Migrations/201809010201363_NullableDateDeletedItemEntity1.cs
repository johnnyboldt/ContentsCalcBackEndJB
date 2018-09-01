namespace ItemAPI.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableDateDeletedItemEntity1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Item", "DateDeleted", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Item", "DateDeleted", c => c.DateTime(nullable: false));
        }
    }
}
