using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Models
{

    /// <summary>
    /// DTO (Data Transfer Object) dùng để chứa dữ liệu thống kê doanh thu theo thể loại.
    /// Class này tách biệt, không dính dáng trực tiếp tới Database.
    /// </summary>
    public class TheLoaiStats
    {
        public int MaTheLoai { get; set; }
        public string TenTheLoai { get; set; }
        public decimal DoanhThu { get; set; }
    }
}
