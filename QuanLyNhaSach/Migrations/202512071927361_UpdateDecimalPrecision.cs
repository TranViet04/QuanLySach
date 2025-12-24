namespace QuanLyNhaSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDecimalPrecision : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Author", "Name", c => c.String(nullable: false, maxLength: 150));
            AddColumn("dbo.Book", "PublishYear", c => c.Int(nullable: false));
            AddColumn("dbo.Category", "Name", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Publisher", "Name", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Book", "Description", c => c.String(maxLength: 500));
            DropColumn("dbo.Author", "AuthorName");
            DropColumn("dbo.Book", "ImportPrice");
            DropColumn("dbo.Book", "Quantity");
            DropColumn("dbo.Book", "ImagePath");
            DropColumn("dbo.Book", "Status");
            DropColumn("dbo.Category", "CategoryName");
            DropColumn("dbo.Publisher", "PublisherName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Publisher", "PublisherName", c => c.String(nullable: false, maxLength: 150));
            AddColumn("dbo.Category", "CategoryName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.Book", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Book", "ImagePath", c => c.String());
            AddColumn("dbo.Book", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.Book", "ImportPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Author", "AuthorName", c => c.String(nullable: false, maxLength: 150));
            AlterColumn("dbo.Book", "Description", c => c.String());
            DropColumn("dbo.Publisher", "Name");
            DropColumn("dbo.Category", "Name");
            DropColumn("dbo.Book", "PublishYear");
            DropColumn("dbo.Author", "Name");
        }
    }
}
