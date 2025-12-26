using QuanLyNhaSach.Models.QuanLyNhaSach.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNhaSach.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        [Required, StringLength(50)]
        public string InvoiceCode { get; set; }

        public DateTime CreatedDate { get; set; }

        // Nhân viên lập hóa đơn
        [ForeignKey("User")]
        public int UserId { get; set; }

        // Khách hàng
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 0 = Chưa thanh toán
        /// 1 = Đã thanh toán
        /// </summary>
        public int Status { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        // Navigation
        public virtual User User { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public virtual Customer Customer { get; set; }

        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
