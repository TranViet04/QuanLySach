using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaSach.Reports
{
    /// <summary>
    /// DTO cho Báo cáo Tồn kho - Chứa dữ liệu từ Book, Category, Publisher, Inventory
    /// </summary>
    public class InventoryReportModel
    {
        // ===== THÔNG TIN SÁCH =====

        public int BookId { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(150)]
        public string AuthorName { get; set; }

        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(150)]
        public string PublisherName { get; set; }

        public int PublishYear { get; set; }


        // ===== THÔNG TIN GIÁ & TỒN KHO =====

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        /// <summary>
        /// Số lượng tồn kho từ bảng Inventory
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Giá trị tồn = Stock * Price
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal StockValue { get; set; }


        // ===== TRẠNG THÁI TỒN KHO =====

        /// <summary>
        /// "Hết hàng" (Stock = 0)
        /// "Sắp hết" (Stock < 10)
        /// "Còn hàng" (Stock >= 10)
        /// </summary>
        [StringLength(50)]
        public string Status { get; set; }


        // ===== THÔNG TIN CẬP NHẬT =====

        public DateTime? LastUpdated { get; set; }
    }
}