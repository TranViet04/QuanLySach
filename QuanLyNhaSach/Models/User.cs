using QuanLyNhaSach.Models.HeThong;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuanLyNhaSach.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Username không được để trống")]
        [StringLength(50, ErrorMessage = "Username tối đa 50 ký tự")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password không hợp lệ")]
        [StringLength(256)]
        public string PasswordHash { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100)]
        public string FullName { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        // Quan hệ nhiều – nhiều
        public virtual ICollection<Role> Roles { get; set; }

        public User()
        {
            Roles = new HashSet<Role>();
        }
    }
}
