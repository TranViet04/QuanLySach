using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Models
{
    public class StockTaking
    {
        [Key]
        public int StockTakingId { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }

        public int ActualQuantity { get; set; }
        public int Difference { get; set; }
        public DateTime CheckingDate { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public virtual Book Book { get; set; }
        public virtual User User { get; set; }
    }
}
