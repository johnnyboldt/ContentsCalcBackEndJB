namespace ItemAPI.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddItemEntity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Item",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Value = c.Double(nullable: false),
                        Category = c.String(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Item");
        }
    }
}
