namespace QuanLyNhaSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserLogAndBackupLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        AuthorName = c.String(nullable: false, maxLength: 150),
                        Bio = c.String(maxLength: 300),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        CategoryId = c.Int(nullable: false),
                        AuthorId = c.Int(nullable: false),
                        PublisherId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ImportPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        ImagePath = c.String(),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Author", t => t.AuthorId)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .ForeignKey("dbo.Publisher", t => t.PublisherId)
                .Index(t => t.CategoryId)
                .Index(t => t.AuthorId)
                .Index(t => t.PublisherId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Publisher",
                c => new
                    {
                        PublisherId = c.Int(nullable: false, identity: true),
                        PublisherName = c.String(nullable: false, maxLength: 150),
                        Address = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.PublisherId);
            
            CreateTable(
                "dbo.BackupLog",
                c => new
                    {
                        BackupId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        BackupDate = c.DateTime(nullable: false),
                        FilePath = c.String(maxLength: 500),
                        Status = c.String(maxLength: 50),
                        ErrorMessage = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.BackupId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 100),
                        PasswordHash = c.String(nullable: false),
                        FullName = c.String(maxLength: 150),
                        Role = c.String(maxLength: 50),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Invoice",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        InvoiceDate = c.DateTime(nullable: false),
                        CustomerId = c.Int(),
                        UserId = c.Int(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Notes = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.InvoiceId)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        CustomerName = c.String(nullable: false, maxLength: 150),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 150),
                        Address = c.String(maxLength: 200),
                        Point = c.Int(),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.InvoiceDetail",
                c => new
                    {
                        InvoiceDetailId = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.InvoiceDetailId)
                .ForeignKey("dbo.Book", t => t.BookId)
                .ForeignKey("dbo.Invoice", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.PurchaseOrder",
                c => new
                    {
                        PurchaseOrderId = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Notes = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.PurchaseOrderId)
                .ForeignKey("dbo.Supplier", t => t.SupplierId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.SupplierId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PurchaseOrderDetail",
                c => new
                    {
                        PurchaseOrderDetailId = c.Int(nullable: false, identity: true),
                        PurchaseOrderId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CostPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PurchaseOrderDetailId)
                .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.PurchaseOrder", t => t.PurchaseOrderId, cascadeDelete: true)
                .Index(t => t.PurchaseOrderId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Supplier",
                c => new
                    {
                        SupplierId = c.Int(nullable: false, identity: true),
                        SupplierName = c.String(nullable: false, maxLength: 150),
                        Address = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.SupplierId);
            
            CreateTable(
                "dbo.Inventory",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BookId);
            
            CreateTable(
                "dbo.StockTaking",
                c => new
                    {
                        StockTakingId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        ActualQuantity = c.Int(nullable: false),
                        Difference = c.Int(nullable: false),
                        CheckingDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockTakingId)
                .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLog",
                c => new
                    {
                        LogId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        LoginTime = c.DateTime(nullable: false),
                        LogoutTime = c.DateTime(),
                        Action = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.LogId)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserLog", "UserId", "dbo.User");
            DropForeignKey("dbo.StockTaking", "UserId", "dbo.User");
            DropForeignKey("dbo.StockTaking", "BookId", "dbo.Book");
            DropForeignKey("dbo.BackupLog", "UserId", "dbo.User");
            DropForeignKey("dbo.PurchaseOrder", "UserId", "dbo.User");
            DropForeignKey("dbo.PurchaseOrder", "SupplierId", "dbo.Supplier");
            DropForeignKey("dbo.PurchaseOrderDetail", "PurchaseOrderId", "dbo.PurchaseOrder");
            DropForeignKey("dbo.PurchaseOrderDetail", "BookId", "dbo.Book");
            DropForeignKey("dbo.Invoice", "UserId", "dbo.User");
            DropForeignKey("dbo.InvoiceDetail", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.InvoiceDetail", "BookId", "dbo.Book");
            DropForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.Book", "PublisherId", "dbo.Publisher");
            DropForeignKey("dbo.Book", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Book", "AuthorId", "dbo.Author");
            DropIndex("dbo.UserLog", new[] { "UserId" });
            DropIndex("dbo.StockTaking", new[] { "UserId" });
            DropIndex("dbo.StockTaking", new[] { "BookId" });
            DropIndex("dbo.PurchaseOrderDetail", new[] { "BookId" });
            DropIndex("dbo.PurchaseOrderDetail", new[] { "PurchaseOrderId" });
            DropIndex("dbo.PurchaseOrder", new[] { "UserId" });
            DropIndex("dbo.PurchaseOrder", new[] { "SupplierId" });
            DropIndex("dbo.InvoiceDetail", new[] { "BookId" });
            DropIndex("dbo.InvoiceDetail", new[] { "InvoiceId" });
            DropIndex("dbo.Invoice", new[] { "UserId" });
            DropIndex("dbo.Invoice", new[] { "CustomerId" });
            DropIndex("dbo.BackupLog", new[] { "UserId" });
            DropIndex("dbo.Book", new[] { "PublisherId" });
            DropIndex("dbo.Book", new[] { "AuthorId" });
            DropIndex("dbo.Book", new[] { "CategoryId" });
            DropTable("dbo.UserLog");
            DropTable("dbo.StockTaking");
            DropTable("dbo.Inventory");
            DropTable("dbo.Supplier");
            DropTable("dbo.PurchaseOrderDetail");
            DropTable("dbo.PurchaseOrder");
            DropTable("dbo.InvoiceDetail");
            DropTable("dbo.Customer");
            DropTable("dbo.Invoice");
            DropTable("dbo.User");
            DropTable("dbo.BackupLog");
            DropTable("dbo.Publisher");
            DropTable("dbo.Category");
            DropTable("dbo.Book");
            DropTable("dbo.Author");
        }
    }
}
