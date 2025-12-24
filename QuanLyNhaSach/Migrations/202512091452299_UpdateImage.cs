namespace QuanLyNhaSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Book", "CoverImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Book", "CoverImagePath");
        }
    }
}
