namespace QuanLyNhaSach.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoryUniqueIndex : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Category", "Name", unique: true, name: "IX_Category_Name_Unique");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Category", "IX_Category_Name_Unique");
        }
    }
}
