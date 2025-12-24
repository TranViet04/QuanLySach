using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace QuanLyNhaSach.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }   // Mã sách (auto-increment)

        [Required, StringLength(200)]
        public string Title { get; set; } // Tên sách

        [ForeignKey("Author")]
        public int AuthorId { get; set; }

        [ForeignKey("Category")]
        public int CategoryId { get; set; } // Thể loại sách

        [ForeignKey("Publisher")]
        public int PublisherId { get; set; } // Nhà xuất bản

        // Distributor (optional)
        [ForeignKey("Distributor")]
        public int? DistributorId { get; set; }

        public int PublishYear { get; set; } // Năm xuất bản

        // Hình ảnh bìa sách
        public string CoverImagePath { get; set; }

        //[Range(0, 999999)]
        public decimal Price { get; set; } // Giá tiền

        [StringLength(500)]
        public string Description { get; set; } // Mô tả sách

        // Navigation properties
        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual Distributor Distributor { get; set; }
    }
}
