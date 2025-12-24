using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Models
{
    public class Inventory
    {
        [Key]
        public int BookId { get; set; }

        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
