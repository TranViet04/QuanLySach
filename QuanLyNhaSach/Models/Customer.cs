using QuanLyNhaSach.Models.QuanLyNhaSach.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }  // ✅ Đổi từ ID → CustomerId

        [Required, StringLength(150)]
        public string Name { get; set; }

        [StringLength(10)]
        public string Sex { get; set; }

        public string Bio { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}