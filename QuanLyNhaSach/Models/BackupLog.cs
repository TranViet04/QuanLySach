using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyNhaSach.Models
{
    public class BackupLog
    {
        [Key]
        public int BackupId { get; set; }

        [Required]
        public int UserId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime BackupDate { get; set; }

        [StringLength(500)]
        public string FilePath { get; set; }

        [StringLength(50)]
        public string Status { get; set; }

        [StringLength(1000)]
        public string ErrorMessage { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
