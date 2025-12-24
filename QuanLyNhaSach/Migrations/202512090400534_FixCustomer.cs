namespace QuanLyNhaSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCustomer : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer");
            DropPrimaryKey("dbo.Customer");

            // Add columns only if they do not already exist to avoid duplicate column errors
            Sql(@"IF COL_LENGTH('dbo.Customer', 'ID') IS NULL
BEGIN
    ALTER TABLE dbo.Customer ADD [ID] INT IDENTITY(1,1) NOT NULL;
END");

            Sql(@"IF COL_LENGTH('dbo.Customer', 'Name') IS NULL
BEGIN
    ALTER TABLE dbo.Customer ADD [Name] NVARCHAR(150) NOT NULL DEFAULT('');
    ALTER TABLE dbo.Customer DROP CONSTRAINT IF EXISTS DF_Customer_Name;
END");

            Sql(@"IF COL_LENGTH('dbo.Customer', 'Sex') IS NULL
BEGIN
    ALTER TABLE dbo.Customer ADD [Sex] NVARCHAR(10) NULL;
END");

            Sql(@"IF COL_LENGTH('dbo.Customer', 'Bio') IS NULL
BEGIN
    ALTER TABLE dbo.Customer ADD [Bio] NVARCHAR(MAX) NULL;
END");

            AddPrimaryKey("dbo.Customer", "ID");
            AddForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer", "ID");

            // Drop old columns if they exist
            Sql(@"IF COL_LENGTH('dbo.Customer', 'CustomerId') IS NOT NULL
BEGIN
    ALTER TABLE dbo.Customer DROP COLUMN CustomerId;
END");
            Sql(@"IF COL_LENGTH('dbo.Customer', 'CustomerName') IS NOT NULL
BEGIN
    ALTER TABLE dbo.Customer DROP COLUMN CustomerName;
END");
            Sql(@"IF COL_LENGTH('dbo.Customer', 'Point') IS NOT NULL
BEGIN
    ALTER TABLE dbo.Customer DROP COLUMN Point;
END");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customer", "Point", c => c.Int());
            AddColumn("dbo.Customer", "CustomerName", c => c.String(nullable: false, maxLength: 150));
            AddColumn("dbo.Customer", "CustomerId", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer");
            DropPrimaryKey("dbo.Customer");
            DropColumn("dbo.Customer", "Bio");
            DropColumn("dbo.Customer", "Sex");
            DropColumn("dbo.Customer", "Name");
            DropColumn("dbo.Customer", "ID");
            AddPrimaryKey("dbo.Customer", "CustomerId");
            AddForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer", "CustomerId");
        }
    }
}
