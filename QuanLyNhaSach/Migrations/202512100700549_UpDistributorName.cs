namespace QuanLyNhaSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpDistributorName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PurchaseOrder", "SupplierId", "dbo.Supplier");
            DropIndex("dbo.PurchaseOrder", new[] { "SupplierId" });
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
            
            AddColumn("dbo.Book", "DistributorId", c => c.Int());
            AddColumn("dbo.PurchaseOrder", "DistributorId", c => c.Int(nullable: false));
            CreateIndex("dbo.Book", "DistributorId");
            CreateIndex("dbo.PurchaseOrder", "DistributorId");
            AddForeignKey("dbo.PurchaseOrder", "DistributorId", "dbo.Distributor", "DistributorId", cascadeDelete: true);
            AddForeignKey("dbo.Book", "DistributorId", "dbo.Distributor", "DistributorId");
            DropColumn("dbo.PurchaseOrder", "SupplierId");
            DropTable("dbo.Supplier");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.PurchaseOrder", "SupplierId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Book", "DistributorId", "dbo.Distributor");
            DropForeignKey("dbo.PurchaseOrder", "DistributorId", "dbo.Distributor");
            DropIndex("dbo.PurchaseOrder", new[] { "DistributorId" });
            DropIndex("dbo.Book", new[] { "DistributorId" });
            DropColumn("dbo.PurchaseOrder", "DistributorId");
            DropColumn("dbo.Book", "DistributorId");
            DropTable("dbo.Distributor");
            CreateIndex("dbo.PurchaseOrder", "SupplierId");
            AddForeignKey("dbo.PurchaseOrder", "SupplierId", "dbo.Supplier", "SupplierId", cascadeDelete: true);
        }
    }
}
