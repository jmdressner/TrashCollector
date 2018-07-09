namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "ApplicationUserID", c => c.String(maxLength: 128));
            AddColumn("dbo.Employees", "ApplicationUserID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Customers", "ApplicationUserID");
            CreateIndex("dbo.Employees", "ApplicationUserID");
            AddForeignKey("dbo.Customers", "ApplicationUserID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Employees", "ApplicationUserID", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Employees", "ApplicationUserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Customers", "ApplicationUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Employees", new[] { "ApplicationUserID" });
            DropIndex("dbo.Customers", new[] { "ApplicationUserID" });
            DropColumn("dbo.Employees", "ApplicationUserID");
            DropColumn("dbo.Customers", "ApplicationUserID");
        }
    }
}
