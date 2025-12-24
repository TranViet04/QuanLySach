using QuanLyNhaSach.Data;
using QuanLyNhaSach.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNhaSach
{
    public partial class FormRoleManagement : Form
    {
        private int _selectedRoleId = 0;

        public FormRoleManagement()
        {
            InitializeComponent();
        }

        // =============================
        // FORM LOAD
        // =============================
        private void FormRoleManagement_Load(object sender, EventArgs e)
        {
            LoadRoles();
            ClearForm();
        }

        // =============================
        // LOAD ROLE LIST
        // =============================
        private void LoadRoles()
        {
            using (var db = new BookStoreContext())
            {
                dgvRoles.AutoGenerateColumns = false;

                dgvRoles.DataSource = db.Roles
                    .Select(r => new
                    {
                        r.RoleId,
                        r.Name,
                        r.Description
                    })
                    .ToList();
            }
        }

        // =============================
        // CLEAR FORM
        // =============================
        private void ClearForm()
        {
            _selectedRoleId = 0;
            txtRoleName.Clear();
            txtDescription.Clear();
            txtRoleName.Focus();
        }

        // =============================
        // ADD ROLE
        // =============================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string roleName = txtRoleName.Text.Trim();
            string description = txtDescription.Text.Trim();

            if (string.IsNullOrEmpty(roleName))
            {
                MessageBox.Show("Tên quyền không được để trống");
                return;
            }

            using (var db = new BookStoreContext())
            {
                bool exists = db.Roles.Any(r => r.Name == roleName);
                if (exists)
                {
                    MessageBox.Show("Tên quyền đã tồn tại");
                    return;
                }

                var role = new Role
                {
                    Name = roleName,
                    Description = description
                };

                db.Roles.Add(role);
                db.SaveChanges();
            }

            LoadRoles();
            ClearForm();
            MessageBox.Show("Thêm quyền thành công");
        }

        // =============================
        // UPDATE ROLE
        // =============================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedRoleId == 0)
            {
                MessageBox.Show("Vui lòng chọn quyền cần sửa");
                return;
            }

            string roleName = txtRoleName.Text.Trim();
            string description = txtDescription.Text.Trim();

            if (string.IsNullOrEmpty(roleName))
            {
                MessageBox.Show("Tên quyền không được để trống");
                return;
            }

            using (var db = new BookStoreContext())
            {
                bool duplicate = db.Roles.Any(r =>
                    r.Name == roleName && r.RoleId != _selectedRoleId);

                if (duplicate)
                {
                    MessageBox.Show("Tên quyền đã tồn tại");
                    return;
                }

                var role = db.Roles.Find(_selectedRoleId);
                if (role == null) return;

                role.Name = roleName;
                role.Description = description;

                db.SaveChanges();
            }

            LoadRoles();
            ClearForm();
            MessageBox.Show("Cập nhật quyền thành công");
        }

        // =============================
        // DELETE ROLE
        // =============================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedRoleId == 0)
            {
                MessageBox.Show("Vui lòng chọn quyền cần xóa");
                return;
            }

            var confirm = MessageBox.Show(
                "Bạn có chắc muốn xóa quyền này?",
                "Xác nhận",
                MessageBoxButtons.YesNo);

            if (confirm != DialogResult.Yes)
                return;

            using (var db = new BookStoreContext())
            {
                var role = db.Roles
                    .Include(r => r.Users)
                    .FirstOrDefault(r => r.RoleId == _selectedRoleId);

                if (role == null) return;

                if (role.Users.Any())
                {
                    MessageBox.Show("Không thể xóa quyền đang được gán cho người dùng");
                    return;
                }

                db.Roles.Remove(role);
                db.SaveChanges();
            }

            LoadRoles();
            ClearForm();
            MessageBox.Show("Xóa quyền thành công");
        }

        // =============================
        // CLEAR BUTTON
        // =============================
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        // =============================
        // GRID CLICK
        // =============================
        private void dgvRoles_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvRoles.Rows[e.RowIndex];

            _selectedRoleId = Convert.ToInt32(row.Cells["colID"].Value);
            txtRoleName.Text = row.Cells["colName"].Value.ToString();
            txtDescription.Text = row.Cells["colDecription"].Value?.ToString();
        }
    }
}
