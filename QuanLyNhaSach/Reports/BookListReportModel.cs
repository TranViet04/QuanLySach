using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaSach.Reports
{
    /// <summary>
    /// DTO cho Báo cáo Danh sách Sách
    /// </summary>
    public class BookListReportModel
    {
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

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        /// <summary>
        /// Số lượng tồn kho
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// "Còn hàng", "Sắp hết", "Hết hàng"
        /// </summary>
        [StringLength(20)]
        public string Status { get; set; }
    }
}