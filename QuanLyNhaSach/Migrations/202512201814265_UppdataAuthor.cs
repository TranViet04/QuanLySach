namespace QuanLyNhaSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UppdataAuthor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Author", "Gender", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Author", "Gender");
        }
    }
}
