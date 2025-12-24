using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace QuanLyNhaSach.Models
{
    public class Publisher
    {
        [Key]
        public int PublisherId { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
