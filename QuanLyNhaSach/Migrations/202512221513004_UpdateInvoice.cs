namespace QuanLyNhaSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateInvoice : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer");
            DropIndex("dbo.Invoice", new[] { "CustomerId" });
            AddColumn("dbo.Invoice", "InvoiceCode", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Invoice", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Invoice", "Status", c => c.Int(nullable: false));
            AddColumn("dbo.Invoice", "Note", c => c.String(maxLength: 500));
            AddColumn("dbo.InvoiceDetail", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Invoice", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Invoice", "CustomerId");
            AddForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer", "ID", cascadeDelete: true);
            DropColumn("dbo.Invoice", "InvoiceDate");
            DropColumn("dbo.Invoice", "Notes");
            DropColumn("dbo.InvoiceDetail", "UnitPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.InvoiceDetail", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Invoice", "Notes", c => c.String(maxLength: 300));
            AddColumn("dbo.Invoice", "InvoiceDate", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer");
            DropIndex("dbo.Invoice", new[] { "CustomerId" });
            AlterColumn("dbo.Invoice", "CustomerId", c => c.Int());
            DropColumn("dbo.InvoiceDetail", "Price");
            DropColumn("dbo.Invoice", "Note");
            DropColumn("dbo.Invoice", "Status");
            DropColumn("dbo.Invoice", "CreatedDate");
            DropColumn("dbo.Invoice", "InvoiceCode");
            CreateIndex("dbo.Invoice", "CustomerId");
            AddForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer", "ID");
        }
    }
}
