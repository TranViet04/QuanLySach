using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Models
{
    public class StockReportModel
    {
        // Tham chiếu tới Book.BookId
        public int BookId { get; set; }

        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(100)]
        public string CategoryName { get; set; }

        [StringLength(150)]
        public string PublisherName { get; set; }

        // Giá nhập dùng để tính giá trị tồn kho (nếu có)
        [DataType(DataType.Currency)]
        public decimal ImportPrice { get; set; }

        // Số lượng tồn thực tế
        public int QuantityOnHand { get; set; }

        // Giá trị tồn kho = QuantityOnHand * ImportPrice
        [DataType(DataType.Currency)]
        public decimal TotalValue { get; set; }
    }
}
