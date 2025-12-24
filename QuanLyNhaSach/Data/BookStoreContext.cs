using QuanLyNhaSach.Models;
using QuanLyNhaSach.Models.HeThong;
using QuanLyNhaSach.Models.QuanLyNhaSach.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Data
{
    public class BookStoreContext : DbContext
    {
        // Nếu bạn muốn đặt connection string tên "BookStoreContext" trong app.config/web.config
        public BookStoreContext()
            : base("name=BookStoreContext")
        {
            // Tắt lazy loading nếu bạn muốn kiểm soát việc load navigation properties
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        // DbSets cho các entity chính
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }

        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }

        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<StockTaking> StockTakings { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }
        public DbSet<BackupLog> BackupLogs { get; set; }



        // NOTE: Report models are [NotMapped] so không cần DbSet cho chúng.

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remove pluralizing convention nếu bạn muốn tên bảng trùng tên DbSet
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Ví dụ: nếu Inventory.BookId là khoá chính (đã đánh [Key])
            modelBuilder.Entity<Inventory>()
                        .HasKey(i => i.BookId);

            // Thiết lập quan hệ 1-n, nếu cần (nhiều thiết lập đã có bằng FK attribute)
            modelBuilder.Entity<Book>()
                        .HasRequired(b => b.Category)
                        .WithMany(c => c.Books)
                        .HasForeignKey(b => b.CategoryId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                        .HasRequired(b => b.Publisher)
                        .WithMany(p => p.Books)
                        .HasForeignKey(b => b.PublisherId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                        .HasRequired(b => b.Author)
                        .WithMany(a => a.Books)
                        .HasForeignKey(b => b.AuthorId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<InvoiceDetail>()
                        .HasRequired(d => d.Invoice)
                        .WithMany(i => i.InvoiceDetails)
                        .HasForeignKey(d => d.InvoiceId)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<InvoiceDetail>()
                        .HasRequired(d => d.Book)
                        .WithMany()
                        .HasForeignKey(d => d.BookId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                        .Property(b => b.Price)
                        .HasColumnType("decimal")   // Chỉ ghi "decimal"
                        .HasPrecision(18, 2);       // Độ dài và số thập phân

            modelBuilder.Entity<User>()
                        .HasMany(u => u.Roles)
                        .WithMany(r => r.Users)
                        .Map(m =>
                        {
                            m.ToTable("UserRoles");
                            m.MapLeftKey("UserId");
                            m.MapRightKey("RoleId");
                        });


            base.OnModelCreating(modelBuilder);
        }
    }
}
