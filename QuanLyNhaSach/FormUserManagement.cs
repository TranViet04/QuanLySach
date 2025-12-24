using QuanLyNhaSach.Data;
using QuanLyNhaSach.Models;
using QuanLyNhaSach.Models.HeThong;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNhaSach
{
    public partial class FormUserManagement : Form
    {
        private int _selectedUserId = -1;

        public FormUserManagement()
        {
            InitializeComponent();
        }

        private void FormUserManagement_Load(object sender, EventArgs e)
        {
            dgvUsers.AutoGenerateColumns = false;

            LoadRoles();
            LoadUsers();
            ResetForm();
        }

        // =============================
        // LOAD DATA
        // =============================
        private void LoadRoles()
        {
            using (var db = new BookStoreContext())
            {
                var roles = db.Roles.ToList();

                clbRoles.Items.Clear();
                foreach (var role in roles)
                {
                    clbRoles.Items.Add(role, false);
                }

                clbRoles.DisplayMember = "Name";
            }
        }

        private void LoadUsers()
        {
            using (var db = new BookStoreContext())
            {
                var users = db.Users
                    .Include(u => u.Roles)
                    .ToList();

                dgvUsers.Rows.Clear();

                foreach (var u in users)
                {
                    dgvUsers.Rows.Add(
                        u.UserId,
                        u.Username,
                        u.FullName,
                        string.Join(", ", u.Roles.Select(r => r.Name)),
                        u.IsActive
                    );
                }
            }
        }

        // =============================
        // ADD USER
        // =============================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(isEdit: false))
                return;

            using (var db = new BookStoreContext())
            {
                if (db.Users.Any(u => u.Username == txtUserName.Text.Trim()))
                {
                    MessageBox.Show("Username đã tồn tại");
                    return;
                }

                var user = new User
                {
                    Username = txtUserName.Text.Trim(),
                    FullName = txtFullName.Text.Trim(),
                    PasswordHash = PasswordHelper.HashPassword(txtPassword.Text),
                    IsActive = chkIsActive.Checked
                };

                foreach (Role role in clbRoles.CheckedItems)
                {
                    user.Roles.Add(db.Roles.Find(role.RoleId));
                }

                db.Users.Add(user);
                db.SaveChanges();
            }

            LoadUsers();
            ResetForm();
        }

        // =============================
        // UPDATE USER
        // =============================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == -1)
                return;

            if (!ValidateInput(isEdit: true))
                return;

            using (var db = new BookStoreContext())
            {
                var user = db.Users
                    .Include(u => u.Roles)
                    .FirstOrDefault(u => u.UserId == _selectedUserId);

                if (user == null)
                    return;

                user.FullName = txtFullName.Text.Trim();
                user.IsActive = chkIsActive.Checked;

                if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    user.PasswordHash = PasswordHelper.HashPassword(txtPassword.Text);
                }

                user.Roles.Clear();
                foreach (Role role in clbRoles.CheckedItems)
                {
                    user.Roles.Add(db.Roles.Find(role.RoleId));
                }

                db.SaveChanges();
            }

            LoadUsers();
            ResetForm();
        }

        // =============================
        // DELETE USER
        // =============================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedUserId == -1)
                return;

            if (MessageBox.Show("Xóa user này?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            using (var db = new BookStoreContext())
            {
                var user = db.Users.Find(_selectedUserId);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                }
            }

            LoadUsers();
            ResetForm();
        }

        // =============================
        // GRID CLICK
        // =============================
        private void dgvUsers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            _selectedUserId = Convert.ToInt32(dgvUsers.Rows[e.RowIndex].Cells[0].Value);

            using (var db = new BookStoreContext())
            {
                var user = db.Users
                    .Include(u => u.Roles)
                    .FirstOrDefault(u => u.UserId == _selectedUserId);

                if (user == null)
                    return;

                txtUserName.Text = user.Username;
                txtUserName.Enabled = false;

                txtFullName.Text = user.FullName;
                chkIsActive.Checked = user.IsActive;

                txtPassword.Clear();
                ClearRoleChecks();

                foreach (var role in user.Roles)
                {
                    for (int i = 0; i < clbRoles.Items.Count; i++)
                    {
                        var r = (Role)clbRoles.Items[i];
                        if (r.RoleId == role.RoleId)
                        {
                            clbRoles.SetItemChecked(i, true);
                        }
                    }
                }
            }
        }

        // =============================
        // UTILITIES
        // =============================
        private bool ValidateInput(bool isEdit)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("Username không được trống");
                return false;
            }

            // When creating, password is required and must be at least 6 characters
            if (!isEdit)
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Password không được trống");
                    return false;
                }

                if (txtPassword.Text.Length < 6)
                {
                    MessageBox.Show("Password phải có ít nhất 6 ký tự");
                    return false;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Password và xác nhận không khớp");
                    return false;
                }

            }
            else
            {
                // When editing, password is optional, but if provided must be at least 6 characters
                if (!string.IsNullOrWhiteSpace(txtPassword.Text) && txtPassword.Text.Length < 6)
                {
                    MessageBox.Show("Khi đổi password, phải có ít nhất 6 ký tự");
                    return false;
                }
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Họ tên không được trống");
                return false;
            }

            if (clbRoles.CheckedItems.Count == 0)
            {
                MessageBox.Show("Phải chọn ít nhất 1 quyền");
                return false;
            }

            return true;
        }

        private void ResetForm()
        {
            _selectedUserId = -1;

            txtUserName.Clear();
            txtUserName.Enabled = true;

            txtFullName.Clear();
            txtPassword.Clear();

            chkIsActive.Checked = true;
            ClearRoleChecks();
        }

        private void ClearRoleChecks()
        {
            for (int i = 0; i < clbRoles.Items.Count; i++)
            {
                clbRoles.SetItemChecked(i, false);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }
    }
}
