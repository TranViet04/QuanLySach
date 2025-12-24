namespace QuanLyNhaSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixLoiPhanQuyen : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "Username", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.User", "PasswordHash", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.User", "FullName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "FullName", c => c.String(maxLength: 150));
            AlterColumn("dbo.User", "PasswordHash", c => c.String(nullable: false));
            AlterColumn("dbo.User", "Username", c => c.String(nullable: false, maxLength: 100));
        }
    }
}
