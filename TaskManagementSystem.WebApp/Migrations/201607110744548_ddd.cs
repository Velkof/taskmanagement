namespace TaskManagementSystem.WebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ddd : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TaskComments", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TaskComments", "UserId", c => c.Int(nullable: false));
        }
    }
}
