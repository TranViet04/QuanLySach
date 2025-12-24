using QuanLyNhaSach.Data;
using QuanLyNhaSach.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNhaSach
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // ===============================
            // 1️⃣ VALIDATE INPUT
            // ===============================
            if (string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show(
                    "Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.",
                    "Thiếu thông tin",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                    
                );
                return;
            }

            // ===============================
            // 2️⃣ TRY–CATCH TOÀN KHỐI LOGIN
            // ===============================
            try
            {
                using (var db = new BookStoreContext())
                {
                    var user = db.Users
                        .Include(u => u.Roles)
                        .FirstOrDefault(u =>
                            u.Username == username &&
                            u.IsActive);

                    if (user == null)
                    {
                        MessageBox.Show(
                            "Sai tài khoản hoặc tài khoản đã bị khóa.",
                            "Đăng nhập thất bại",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        return;
                    }

                    if (!PasswordHelper.VerifyPassword(password, user.PasswordHash))
                    {
                        MessageBox.Show(
                            "Mật khẩu không chính xác.",
                            "Đăng nhập thất bại",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                        errorProvider1.SetError(txtPassword, "Mật khẩu không chính xác.");

                        return;
                    }

                    // ===============================
                    // 3️⃣ SET CURRENT USER
                    // ===============================
                    CurrentUser.Set(user);

                    db.SaveChanges();

                    var main = this.MdiParent as FormMain;
                    if (main != null)
                    {
                        main.ApplyAuthorization();
                        main.UpdateStatusBar();
                    }
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Lỗi hệ thống khi đăng nhập.\n\n" + ex.Message,
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            ((FormMain)this.MdiParent).OpenRegisterForm(this);
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }
    }
}
