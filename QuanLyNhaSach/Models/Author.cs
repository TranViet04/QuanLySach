using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace QuanLyNhaSach.Models
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required, StringLength(150)]
        public string Name { get; set; }

        [StringLength(300)]
        public string Bio { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(150)]
        public string Email { get; set; }

        public string Gender { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
