namespace QuanLyNhaSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CapNhatBookvaInventory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PurchaseOrderDetail", "BookId", "dbo.Book");
            DropForeignKey("dbo.StockTaking", "BookId", "dbo.Book");
            DropForeignKey("dbo.StockTaking", "UserId", "dbo.User");
            AddForeignKey("dbo.PurchaseOrderDetail", "BookId", "dbo.Book", "BookId");
            AddForeignKey("dbo.StockTaking", "BookId", "dbo.Book", "BookId");
            AddForeignKey("dbo.StockTaking", "UserId", "dbo.User", "UserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StockTaking", "UserId", "dbo.User");
            DropForeignKey("dbo.StockTaking", "BookId", "dbo.Book");
            DropForeignKey("dbo.PurchaseOrderDetail", "BookId", "dbo.Book");
            AddForeignKey("dbo.StockTaking", "UserId", "dbo.User", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.StockTaking", "BookId", "dbo.Book", "BookId", cascadeDelete: true);
            AddForeignKey("dbo.PurchaseOrderDetail", "BookId", "dbo.Book", "BookId", cascadeDelete: true);
        }
    }
}
