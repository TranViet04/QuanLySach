using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Models
{
    public class PurchaseOrderDetail
    {
        [Key]
        public int PurchaseOrderDetailId { get; set; }

        [ForeignKey("PurchaseOrder")]
        public int PurchaseOrderId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        public int Quantity { get; set; }
        public decimal CostPrice { get; set; }

        public virtual PurchaseOrder PurchaseOrder { get; set; }
        public virtual Book Book { get; set; }
    }
}
