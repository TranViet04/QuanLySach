using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Models
{
    public class PurchaseOrder
    {
        [Key]
        public int PurchaseOrderId { get; set; }

        [ForeignKey("Distributor")]
        public int DistributorId { get; set; }

        public DateTime OrderDate { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public decimal TotalAmount { get; set; }

        [StringLength(300)]
        public string Notes { get; set; }

        public virtual Distributor Distributor { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    }
}
