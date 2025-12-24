using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyNhaSach.Models
{
    public static class AuthorizationHelper
    {
        public static bool IsAdmin()
        {
            return CurrentUser.IsAuthenticated
                   && CurrentUser.IsInRole("Admin");
        }

        public static bool IsManager()
        {
            return CurrentUser.IsAuthenticated
                   && (CurrentUser.IsInRole("Manager")
                       || CurrentUser.IsInRole("Admin"));
        }

        public static bool IsStaff()
        {
            return CurrentUser.IsAuthenticated
                   && (CurrentUser.IsInRole("Staff")
                       || CurrentUser.IsInRole("Manager")
                       || CurrentUser.IsInRole("Admin"));
        }
    }


}
