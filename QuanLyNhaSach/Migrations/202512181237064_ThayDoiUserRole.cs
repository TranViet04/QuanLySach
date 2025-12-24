namespace QuanLyNhaSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThayDoiUserRole : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Role", "Description", c => c.String(maxLength: 200));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Role", "Description");
        }
    }
}
