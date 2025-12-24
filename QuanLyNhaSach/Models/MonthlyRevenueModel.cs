using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Models
{
    public class MonthlyRevenueModel
    {
        // Năm và tháng để nhóm dữ liệu
        public int Year { get; set; }
        public int Month { get; set; }

        // Tổng doanh thu trong tháng
        [DataType(DataType.Currency)]
        public decimal TotalRevenue { get; set; }

        // Số hóa đơn trong tháng
        public int InvoiceCount { get; set; }

        // Trung bình doanh thu mỗi ngày trong tháng
        [DataType(DataType.Currency)]
        public decimal AverageDailyRevenue { get; set; }

        [StringLength(300)]
        public string Note { get; set; }
    }
}
