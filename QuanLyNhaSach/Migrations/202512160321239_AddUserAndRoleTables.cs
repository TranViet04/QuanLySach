namespace QuanLyNhaSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserAndRoleTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            AddColumn("dbo.User", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.User", "Role");
            DropColumn("dbo.User", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.User", "Role", c => c.String(maxLength: 50));
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Role");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.User");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropColumn("dbo.User", "IsActive");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Role");
        }
    }
}
