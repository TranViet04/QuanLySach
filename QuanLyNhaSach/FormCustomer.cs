using QuanLyNhaSach.Data;
using QuanLyNhaSach.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Validation;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaSach
{
    public partial class FormCustomer : Form
    {
        public FormCustomer()
        {
            InitializeComponent();
            IsEnableEditDelete(false);
        }

        private void IsEnableEditDelete(bool isEnabled) // Enable or disable Edit and Delete buttons
        {
            btnEdit.Enabled = isEnabled;
            btnDelete.Enabled = isEnabled;
            btnAdd.Enabled = !isEnabled;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // clear previous error
            errWarning.SetError(txtName, "");

            string name = txtName.Text.Trim(); 
            string sex = optMale.Checked ? "Nam" : optFemale.Checked ? "Nữ" : null; 
            string desc = txtBio.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                txtName.BackColor = System.Drawing.Color.MistyRose;
                MessageBox.Show("Vui lòng nhập tên khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //set error provider on txtName
                errWarning.SetError(txtName, "Vui lòng nhập tên khách hàng!");
                return;
            }
            txtName.BackColor = System.Drawing.Color.White; 

            try
            {
                using (var db = new BookStoreContext())
                {
                    if (db.Customers.Any(c => c.Name.ToLower() == name.ToLower()))
                    {
                        MessageBox.Show("Tên khách hàng đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        errWarning.SetError(txtName, "Tên khách hàng đã tồn tại.");
                        return;
                    }

                    var customer = new Customer { Name = name, Sex = sex, Bio = desc, Phone = phone, Email = email };
                    db.Customers.Add(customer);
                    db.SaveChanges();
                }

                LoadCustomers();
                txtID.Clear();
                txtName.Clear();
                txtBio.Clear();
                txtPhone.Clear();
                txtEmail.Clear();
                optMale.Checked = optFemale.Checked = false;
                MessageBox.Show("Thêm khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //set error provider on txtName
                errWarning.SetError(txtName, "Lỗi khi thêm khách hàng: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm khách hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Load customers from database
        private void LoadCustomers()
        {
            dgvCustomer.Rows.Clear();
            using (var db = new BookStoreContext())
            {
                var customers = db.Customers.ToList();
                foreach (var customer in customers)
                {
                    dgvCustomer.Rows.Add(
                        customer.CustomerId,
                        Convert.ToString(customer.Name),
                        Convert.ToString(customer.Sex),
                        Convert.ToString(customer.Bio),
                        Convert.ToString(customer.Phone),
                        Convert.ToString(customer.Email)
                    );
                }
            }
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            LoadCustomers();
        }




        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvCustomer.Rows[e.RowIndex];
            if (row.IsNewRow) return;

            txtID.Text = Convert.ToString(row.Cells[0].Value);
            txtName.Text = Convert.ToString(row.Cells[1].Value);

            string GioiTinh = Convert.ToString(row.Cells[2].Value);
            if (string.Equals(GioiTinh, "Nam", StringComparison.OrdinalIgnoreCase))
            {
                optMale.Checked = true;
                optFemale.Checked = false; // Nếu là Nam thì bỏ chọn Nữ
            }
            else if (string.Equals(GioiTinh, "Nữ", StringComparison.OrdinalIgnoreCase))
            {
                optFemale.Checked = true;
                optMale.Checked = false; // Nếu là Nữ thì bỏ chọn Nam
            }
            else
            {
                optMale.Checked = optFemale.Checked = false;
            }

            txtBio.Text = Convert.ToString(row.Cells[3].Value);
            txtPhone.Text = Convert.ToString(row.Cells[4].Value);
            txtEmail.Text = Convert.ToString(row.Cells[5].Value);

            IsEnableEditDelete(true);
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            txtID.Clear();
            txtName.Clear();
            txtBio.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            optMale.Checked = optFemale.Checked = false;
            IsEnableEditDelete(false);
            LoadCustomers();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Edit selected customer
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int id;
            if (!int.TryParse(txtID.Text, out id))
            {
                MessageBox.Show("Mã khách hàng không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var db = new BookStoreContext())
                {
                    var customer = db.Customers.FirstOrDefault(c => c.CustomerId == id);
                    if (customer == null)
                    {
                        MessageBox.Show("Không tìm thấy khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // check duplicate name (excluding current)
                    if (db.Customers.Any(c => c.CustomerId != id && c.Name.ToLower() == name.ToLower()))
                    {
                        MessageBox.Show("Tên khách hàng đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    customer.Name = name;
                    customer.Sex = optMale.Checked ? "Nam" : optFemale.Checked ? "Nữ" : null;
                    customer.Bio = txtBio.Text.Trim();
                    customer.Phone = txtPhone.Text.Trim();
                    customer.Email = txtEmail.Text.Trim();

                    db.SaveChanges();
                }

                LoadCustomers();
                txtID.Clear();
                txtName.Clear();
                txtBio.Clear();
                txtPhone.Clear();
                txtEmail.Clear();
                optMale.Checked = optFemale.Checked = false;
                IsEnableEditDelete(false);
                MessageBox.Show("Cập nhật khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật khách hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn khách hàng để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id;
            if (!int.TryParse(txtID.Text, out id))
            {
                MessageBox.Show("Mã khách hàng không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using (var db = new BookStoreContext())
                {
                    var customer = db.Customers.FirstOrDefault(c => c.CustomerId == id);
                    if (customer == null)
                    {
                        MessageBox.Show("Không tìm thấy khách hàng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    db.Customers.Remove(customer);
                    db.SaveChanges();
                }

                LoadCustomers();
                txtID.Clear();
                txtName.Clear();
                txtBio.Clear();
                txtPhone.Clear();
                txtEmail.Clear();
                optMale.Checked = optFemale.Checked = false;
                IsEnableEditDelete(false);
                MessageBox.Show("Xóa khách hàng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa khách hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
