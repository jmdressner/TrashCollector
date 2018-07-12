namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingpickuptable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PickUpModels",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomerID = c.Int(nullable: false),
                        Price = c.Int(nullable: false),
                        PickUpStatus = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Customers", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PickUpModels", "CustomerID", "dbo.Customers");
            DropIndex("dbo.PickUpModels", new[] { "CustomerID" });
            DropTable("dbo.PickUpModels");
        }
    }
}
