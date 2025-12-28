using System;
using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaSach.Reports
{
    /// <summary>
    /// DTO cho Báo cáo Hóa đơn - Chứa dữ liệu từ Invoice, InvoiceDetail, Book, Author, Customer
    /// </summary>
    public class InvoiceReportModel
    {
        // ===== THÔNG TIN HÓA ĐƠN (từ Invoice) =====

        public int InvoiceId { get; set; }

        [StringLength(50)]
        public string InvoiceCode { get; set; }

        public DateTime CreatedDate { get; set; }

        public decimal TotalAmount { get; set; }

        /// <summary>
        /// "Đã thanh toán" hoặc "Chưa thanh toán"
        /// </summary>
        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(500)]
        public string Note { get; set; }


        // ===== THÔNG TIN KHÁCH HÀNG (từ Customer) =====

        public int CustomerId { get; set; }

        [StringLength(150)]
        public string CustomerName { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ lấy từ Customer.Bio
        /// </summary>
        public string CustomerAddress { get; set; }


        // ===== THÔNG TIN NHÂN VIÊN (từ User) =====

        public int UserId { get; set; }

        [StringLength(100)]
        public string StaffName { get; set; }


        // ===== CHI TIẾT SÁCH (từ InvoiceDetail + Book + Author) =====

        public int InvoiceDetailId { get; set; }

        public int BookId { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(150)]
        public string AuthorName { get; set; }

        public int Quantity { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        /// <summary>
        /// Thành tiền = Quantity * Price
        /// </summary>
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
    }
}