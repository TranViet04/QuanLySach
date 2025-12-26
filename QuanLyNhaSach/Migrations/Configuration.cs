namespace QuanLyNhaSach.Migrations
{
    using QuanLyNhaSach.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<QuanLyNhaSach.Data.BookStoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QuanLyNhaSach.Data.BookStoreContext context)
        {
            // Tạo role
            var adminRole = new Role { Name = "Admin" };
            var staffRole = new Role { Name = "Staff" };
            context.Roles.AddOrUpdate(r => r.Name, adminRole, staffRole);

            // Tạo admin user
            var admin = new User
            {
                Username = "admin",
                PasswordHash = PasswordHelper.HashPassword("123456"),
                FullName = "Quản trị viên",
                IsActive = true,
                Roles = new List<Role> { adminRole }
            };
            context.Users.AddOrUpdate(u => u.Username, admin);

            context.SaveChanges();
        }
    }
}
