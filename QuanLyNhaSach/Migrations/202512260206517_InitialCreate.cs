namespace QuanLyNhaSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        AuthorId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Bio = c.String(maxLength: 300),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 150),
                        Gender = c.String(),
                    })
                .PrimaryKey(t => t.AuthorId);
            
            CreateTable(
                "dbo.Book",
                c => new
                    {
                        BookId = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        AuthorId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        PublisherId = c.Int(nullable: false),
                        DistributorId = c.Int(),
                        PublishYear = c.Int(nullable: false),
                        CoverImagePath = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.BookId)
                .ForeignKey("dbo.Author", t => t.AuthorId)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .ForeignKey("dbo.Distributor", t => t.DistributorId)
                .ForeignKey("dbo.Publisher", t => t.PublisherId)
                .Index(t => t.AuthorId)
                .Index(t => t.CategoryId)
                .Index(t => t.PublisherId)
                .Index(t => t.DistributorId);
            
            CreateTable(
                "dbo.Category",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Distributor",
                c => new
                    {
                        DistributorId = c.Int(nullable: false, identity: true),
                        DistributorName = c.String(nullable: false, maxLength: 150),
                        Address = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 150),
                    })
                .PrimaryKey(t => t.DistributorId);
            
            CreateTable(
                "dbo.PurchaseOrder",
                c => new
                    {
                        PurchaseOrderId = c.Int(nullable: false, identity: true),
                        DistributorId = c.Int(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Notes = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.PurchaseOrderId)
                .ForeignKey("dbo.Distributor", t => t.DistributorId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.DistributorId)
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
                .ForeignKey("dbo.Book", t => t.BookId)
                .ForeignKey("dbo.PurchaseOrder", t => t.PurchaseOrderId, cascadeDelete: true)
                .Index(t => t.PurchaseOrderId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        PasswordHash = c.String(nullable: false, maxLength: 256),
                        FullName = c.String(nullable: false, maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Publisher",
                c => new
                    {
                        PublisherId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
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
                "dbo.Customer",
                c => new
                    {
                        CustomerId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Sex = c.String(maxLength: 10),
                        Bio = c.String(),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 150),
                        Address = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.CustomerId);
            
            CreateTable(
                "dbo.Invoice",
                c => new
                    {
                        InvoiceId = c.Int(nullable: false, identity: true),
                        InvoiceCode = c.String(nullable: false, maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        Note = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.InvoiceId)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.InvoiceDetail",
                c => new
                    {
                        InvoiceDetailId = c.Int(nullable: false, identity: true),
                        InvoiceId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.InvoiceDetailId)
                .ForeignKey("dbo.Book", t => t.BookId)
                .ForeignKey("dbo.Invoice", t => t.InvoiceId, cascadeDelete: true)
                .Index(t => t.InvoiceId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.Inventory",
                c => new
                    {
                        InventoryId = c.Int(nullable: false, identity: true),
                        BookId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        LastUpdated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.InventoryId)
                .ForeignKey("dbo.Book", t => t.BookId)
                .Index(t => t.BookId);
            
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
                .ForeignKey("dbo.Book", t => t.BookId)
                .ForeignKey("dbo.User", t => t.UserId)
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserLog", "UserId", "dbo.User");
            DropForeignKey("dbo.StockTaking", "UserId", "dbo.User");
            DropForeignKey("dbo.StockTaking", "BookId", "dbo.Book");
            DropForeignKey("dbo.Inventory", "BookId", "dbo.Book");
            DropForeignKey("dbo.Invoice", "UserId", "dbo.User");
            DropForeignKey("dbo.InvoiceDetail", "InvoiceId", "dbo.Invoice");
            DropForeignKey("dbo.InvoiceDetail", "BookId", "dbo.Book");
            DropForeignKey("dbo.Invoice", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.BackupLog", "UserId", "dbo.User");
            DropForeignKey("dbo.Book", "PublisherId", "dbo.Publisher");
            DropForeignKey("dbo.Book", "DistributorId", "dbo.Distributor");
            DropForeignKey("dbo.PurchaseOrder", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Role");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.User");
            DropForeignKey("dbo.PurchaseOrderDetail", "PurchaseOrderId", "dbo.PurchaseOrder");
            DropForeignKey("dbo.PurchaseOrderDetail", "BookId", "dbo.Book");
            DropForeignKey("dbo.PurchaseOrder", "DistributorId", "dbo.Distributor");
            DropForeignKey("dbo.Book", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Book", "AuthorId", "dbo.Author");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserLog", new[] { "UserId" });
            DropIndex("dbo.StockTaking", new[] { "UserId" });
            DropIndex("dbo.StockTaking", new[] { "BookId" });
            DropIndex("dbo.Inventory", new[] { "BookId" });
            DropIndex("dbo.InvoiceDetail", new[] { "BookId" });
            DropIndex("dbo.InvoiceDetail", new[] { "InvoiceId" });
            DropIndex("dbo.Invoice", new[] { "CustomerId" });
            DropIndex("dbo.Invoice", new[] { "UserId" });
            DropIndex("dbo.BackupLog", new[] { "UserId" });
            DropIndex("dbo.PurchaseOrderDetail", new[] { "BookId" });
            DropIndex("dbo.PurchaseOrderDetail", new[] { "PurchaseOrderId" });
            DropIndex("dbo.PurchaseOrder", new[] { "UserId" });
            DropIndex("dbo.PurchaseOrder", new[] { "DistributorId" });
            DropIndex("dbo.Book", new[] { "DistributorId" });
            DropIndex("dbo.Book", new[] { "PublisherId" });
            DropIndex("dbo.Book", new[] { "CategoryId" });
            DropIndex("dbo.Book", new[] { "AuthorId" });
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLog");
            DropTable("dbo.StockTaking");
            DropTable("dbo.Inventory");
            DropTable("dbo.InvoiceDetail");
            DropTable("dbo.Invoice");
            DropTable("dbo.Customer");
            DropTable("dbo.BackupLog");
            DropTable("dbo.Publisher");
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.PurchaseOrderDetail");
            DropTable("dbo.PurchaseOrder");
            DropTable("dbo.Distributor");
            DropTable("dbo.Category");
            DropTable("dbo.Book");
            DropTable("dbo.Author");
        }
    }
}
