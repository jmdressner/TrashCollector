namespace TrashCollector.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class registerdropdown : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "RoleViewModel", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "RoleViewModel");
        }
    }
}
