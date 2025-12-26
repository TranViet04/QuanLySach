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
        public BookStoreContext()
            : base("name=BookStoreContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // ===== CUSTOMER CONFIGURATION =====
            modelBuilder.Entity<Customer>()
                        .HasKey(c => c.CustomerId);

            // ===== INVOICE RELATIONSHIPS =====
            modelBuilder.Entity<Invoice>()
                        .HasRequired(i => i.Customer)
                        .WithMany(c => c.Invoices)
                        .HasForeignKey(i => i.CustomerId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invoice>()
                        .HasRequired(i => i.User)
                        .WithMany()
                        .HasForeignKey(i => i.UserId)
                        .WillCascadeOnDelete(false);

            // ===== INVENTORY CONFIGURATION =====
            modelBuilder.Entity<Inventory>()
                        .HasKey(i => i.InventoryId);  // ✓ Đổi thành InventoryId

            modelBuilder.Entity<Inventory>()
                        .HasRequired(i => i.Book)
                        .WithMany()
                        .HasForeignKey(i => i.BookId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Inventory>()
                        .Property(i => i.Quantity)
                        .IsRequired()
                        .HasColumnType("int");

            modelBuilder.Entity<Inventory>()
                        .Property(i => i.LastUpdated)
                        .IsRequired()
                        .HasColumnType("datetime");

            // ===== BOOK RELATIONSHIPS =====
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

            modelBuilder.Entity<Book>()
                        .HasOptional(b => b.Distributor)
                        .WithMany()
                        .HasForeignKey(b => b.DistributorId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Book>()
                        .Property(b => b.Price)
                        .HasColumnType("decimal")
                        .HasPrecision(18, 2);

            // ===== INVOICE DETAILS RELATIONSHIPS =====
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

            // ===== PURCHASE ORDER RELATIONSHIPS =====
            modelBuilder.Entity<PurchaseOrder>()
                        .HasRequired(po => po.User)
                        .WithMany()
                        .HasForeignKey(po => po.UserId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<PurchaseOrder>()
                        .HasRequired(po => po.Distributor)
                        .WithMany(d => d.PurchaseOrders)
                        .HasForeignKey(po => po.DistributorId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<PurchaseOrderDetail>()
                        .HasRequired(d => d.PurchaseOrder)
                        .WithMany(po => po.PurchaseOrderDetails)
                        .HasForeignKey(d => d.PurchaseOrderId)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<PurchaseOrderDetail>()
                        .HasRequired(d => d.Book)
                        .WithMany()
                        .HasForeignKey(d => d.BookId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<PurchaseOrderDetail>()
                        .Property(d => d.CostPrice)
                        .HasColumnType("decimal")
                        .HasPrecision(18, 2);

            // ===== STOCK TAKING RELATIONSHIPS =====
            modelBuilder.Entity<StockTaking>()
                        .HasRequired(st => st.Book)
                        .WithMany()
                        .HasForeignKey(st => st.BookId)
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<StockTaking>()
                        .HasRequired(st => st.User)
                        .WithMany()
                        .HasForeignKey(st => st.UserId)
                        .WillCascadeOnDelete(false);

            // ===== USER LOG & BACKUP LOG RELATIONSHIPS =====
            modelBuilder.Entity<UserLog>()
                        .HasRequired(ul => ul.User)
                        .WithMany()
                        .HasForeignKey(ul => ul.UserId)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<BackupLog>()
                        .HasRequired(bl => bl.User)
                        .WithMany()
                        .HasForeignKey(bl => bl.UserId)
                        .WillCascadeOnDelete(true);

            // ===== USER-ROLE MANY-TO-MANY =====
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