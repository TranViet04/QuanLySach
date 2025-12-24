using System.Collections.Generic;
using System.Linq;

namespace QuanLyNhaSach.Models
{
    public static class CurrentUser
    {
        public static int UserId { get; private set; }
        public static string Username { get; private set; }
        public static string FullName { get; private set; }

        // DANH SÁCH QUYỀN
        public static List<string> Roles { get; private set; }

        // CỜ ĐĂNG NHẬP
        public static bool IsAuthenticated => UserId > 0;

        // HIỂN THỊ QUYỀN TRÊN STATUS BAR
        public static string RoleDisplay =>
            Roles == null || Roles.Count == 0
                ? "No role"
                : string.Join(", ", Roles);

        // GỌI SAU KHI LOGIN THÀNH CÔNG
        public static void Set(User user)
        {
            UserId = user.UserId;
            Username = user.Username;
            FullName = user.FullName;

            Roles = user.Roles
                        ?.Select(r => r.Name)
                        .ToList();
        }

        public static void Reset()
        {
            UserId = 0;
            Username = null;
            FullName = null;
            Roles = null;
        }

        // KIỂM TRA QUYỀN
        public static bool IsInRole(string roleName)
        {
            return Roles != null && Roles.Contains(roleName);
        }
    }
}
