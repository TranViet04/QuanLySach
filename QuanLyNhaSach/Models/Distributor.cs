using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Models
{
    public class Distributor
    {
        [Key]
        public int DistributorId { get; set; }

        [Required, StringLength(150)]
        public string DistributorName { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        public virtual ICollection<PurchaseOrder> PurchaseOrders { get; set; }
    }
}
