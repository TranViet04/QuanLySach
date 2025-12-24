using QuanLyNhaSach.Data;
using QuanLyNhaSach.Models;
using System;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNhaSach
{
    public partial class FormRegister : Form
    {
        public FormLogin LoginForm { get; set; }

        public FormRegister()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;
            string confirm = txtConfirm.Text;
            string fullname = txtFullName.Text.Trim();

            // ================= VALIDATION =================
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirm) ||
                string.IsNullOrWhiteSpace(fullname))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            if (username.Length < 3)
            {
                MessageBox.Show("Tên đăng nhập phải từ 3 ký tự trở lên.");
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải từ 6 ký tự trở lên.");
                return;
            }

            if (password != confirm)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp.");
                return;
            }

            using (var db = new BookStoreContext())
            {
                // Check username tồn tại
                if (db.Users.Any(u => u.Username == username))
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại.");
                    return;
                }

                // ===== LẤY ROLE MẶC ĐỊNH THEO TÀI LIỆU =====
                var staffRole = db.Roles.FirstOrDefault(r => r.Name == "Staff");
                if (staffRole == null)
                {
                    MessageBox.Show("Chưa cấu hình Role 'Staff' trong hệ thống.");
                    return;
                }

                var newUser = new User
                {
                    Username = username,
                    PasswordHash = PasswordHelper.HashPassword(password),
                    FullName = fullname,
                    IsActive = true
                };

                // Gán role qua bảng UserRoles (n-n)
                newUser.Roles.Add(staffRole);

                db.Users.Add(newUser);
                db.SaveChanges();
            }

            MessageBox.Show("Đăng ký thành công!");

            ((FormMain)this.MdiParent).OpenLoginForm();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ((FormMain)this.MdiParent).OpenLoginForm();
        }
    }
}
