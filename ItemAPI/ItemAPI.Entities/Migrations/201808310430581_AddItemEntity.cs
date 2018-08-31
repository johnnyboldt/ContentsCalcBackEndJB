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
                        name = c.String(nullable: false),
                        value = c.String(nullable: false),
                        category = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Item");
        }
    }
}
