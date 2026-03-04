namespace KeelteKool.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRegistrationUserRelation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Registrations", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Registrations", "ApplicationUserId");
            AddForeignKey("dbo.Registrations", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Registrations", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Registrations", new[] { "ApplicationUserId" });
            AlterColumn("dbo.Registrations", "ApplicationUserId", c => c.String());
        }
    }
}
