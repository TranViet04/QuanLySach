using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace QuanLyNhaSach.Models
    {
        public class InvoiceDetail
        {
            [Key]
            public int InvoiceDetailId { get; set; }

            [ForeignKey("Invoice")]
            public int InvoiceId { get; set; }

            [ForeignKey("Book")]
            public int BookId { get; set; }

            public int Quantity { get; set; }

            public decimal Price { get; set; }

            [NotMapped]
            public decimal Amount => Quantity * Price;

            // Navigation
            public virtual Invoice Invoice { get; set; }
            public virtual Book Book { get; set; }
        }
    }

}
