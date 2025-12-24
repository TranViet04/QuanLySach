using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNhaSach.Models.HeThong
{
    public class UserLog
    {
        [Key]
        public int LogId { get; set; }

        [Required]
        public int UserId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime LoginTime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? LogoutTime { get; set; }

        [Required, StringLength(100)]
        public string Action { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
