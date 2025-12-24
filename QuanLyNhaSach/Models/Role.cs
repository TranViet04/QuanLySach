using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public Role()
        {
            Users = new HashSet<User>();
        }
    }
}
