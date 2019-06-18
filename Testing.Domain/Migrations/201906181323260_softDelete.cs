namespace Testing.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class softDelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tests", "isDelete", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tests", "isDelete");
        }
    }
}
