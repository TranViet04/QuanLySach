using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaSach.Reports
{
    /// <summary>
    /// DTO cho Báo cáo Doanh thu theo NGÀY
    /// </summary>
    public class RevenueByDateModel
    {
        public DateTime Date { get; set; }

        public int InvoiceCount { get; set; }

        [DataType(DataType.Currency)]
        public decimal Revenue { get; set; }
    }

    /// <summary>
    /// DTO cho Báo cáo Doanh thu theo THỂ LOẠI
    /// </summary>
    public class RevenueByCategoryModel
    {
        public int CategoryId { get; set; }

        [StringLength(100)]
        public string CategoryName { get; set; }

        public int QuantitySold { get; set; }

        [DataType(DataType.Currency)]
        public decimal Revenue { get; set; }

        public decimal Percentage { get; set; }
    }
}