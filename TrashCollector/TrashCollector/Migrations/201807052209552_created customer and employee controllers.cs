namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdcustomerandemployeecontrollers : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        Address = c.String(),
                        ZipcodeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Zipcodes", t => t.ZipcodeID, cascadeDelete: true)
                .Index(t => t.ZipcodeID);
            
            CreateTable(
                "dbo.Zipcodes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Zip = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "ZipcodeID", "dbo.Zipcodes");
            DropIndex("dbo.Customers", new[] { "ZipcodeID" });
            DropTable("dbo.Employees");
            DropTable("dbo.Zipcodes");
            DropTable("dbo.Customers");
        }
    }
}
