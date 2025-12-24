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
    public partial class FormAuthor : Form
    {
        public FormAuthor()
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
            string gender = optMale.Checked ? "Nam" : optFemale.Checked ? "Nữ" : null; 
            string desc = txtBio.Text.Trim();
            string phone = txtPhone.Text.Trim();
            string email = txtEmail.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                txtName.BackColor = System.Drawing.Color.MistyRose;
                MessageBox.Show("Vui lòng nhập tên tác giả!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //set error provider on txtName
                errWarning.SetError(txtName, "Vui lòng nhập tên tác giả!");
                return;
            }
            txtName.BackColor = System.Drawing.Color.White; 

            try
            {
                using (var db = new BookStoreContext())
                {
                    if (db.Authors.Any(c => c.Name.ToLower() == name.ToLower()))
                    {
                        MessageBox.Show("Tên tác giả đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        errWarning.SetError(txtName, "Tên tác giả đã tồn tại.");
                        return;
                    }

                    var author = new Author { Name = name, Gender = gender, Bio = desc, Phone = phone, Email = email };
                    db.Authors.Add(author);
                    db.SaveChanges();
                }

                LoadAuthors();
                txtID.Clear();
                txtName.Clear();
                txtBio.Clear();
                txtPhone.Clear();
                txtEmail.Clear();
                optMale.Checked = optFemale.Checked = false;
                MessageBox.Show("Thêm tác giả thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show("Lỗi khi thêm tác giả: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //set error provider on txtName
                errWarning.SetError(txtName, "Lỗi khi thêm tác giả: " + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm tác giả: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Load authors from database
        private void LoadAuthors()
        {
            dgvAuthor.Rows.Clear();
            using (var db = new BookStoreContext())
            {
                var authors = db.Authors.ToList();
                foreach (var author in authors)
                {
                    dgvAuthor.Rows.Add(
                        author.AuthorId,
                        Convert.ToString(author.Name),
                        Convert.ToString(author.Gender),
                        Convert.ToString(author.Bio),
                        Convert.ToString(author.Phone),
                        Convert.ToString(author.Email)
                    );
                }
            }
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            LoadAuthors();
        }




        private void dgvAuthor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dgvAuthor.Rows[e.RowIndex];
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
            LoadAuthors();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Edit selected author
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn tác giả để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string name = txtName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập tên tác giả!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int id;
            if (!int.TryParse(txtID.Text, out id))
            {
                MessageBox.Show("Mã tác giả không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (var db = new BookStoreContext())
                {
                    var author = db.Authors.FirstOrDefault(c => c.AuthorId == id);
                    if (author == null)
                    {
                        MessageBox.Show("Không tìm thấy tác giả.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // check duplicate name (excluding current)
                    if (db.Authors.Any(c => c.AuthorId != id && c.Name.ToLower() == name.ToLower()))
                    {
                        MessageBox.Show("Tên tác giả đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    author.Name = name;
                    author.Gender = optMale.Checked ? "Nam" : optFemale.Checked ? "Nữ" : null;
                    author.Bio = txtBio.Text.Trim();
                    author.Phone = txtPhone.Text.Trim();
                    author.Email = txtEmail.Text.Trim();

                    db.SaveChanges();
                }

                LoadAuthors();
                txtID.Clear();
                txtName.Clear();
                txtBio.Clear();
                txtPhone.Clear();
                txtEmail.Clear();
                optMale.Checked = optFemale.Checked = false;
                IsEnableEditDelete(false);
                MessageBox.Show("Cập nhật tác giả thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật tác giả: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn tác giả để xóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int id;
            if (!int.TryParse(txtID.Text, out id))
            {
                MessageBox.Show("Mã tác giả không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa tác giả này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes) return;

            try
            {
                using (var db = new BookStoreContext())
                {
                    var author = db.Authors.FirstOrDefault(c => c.AuthorId == id);
                    if (author == null)
                    {
                        MessageBox.Show("Không tìm thấy tác giả.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    db.Authors.Remove(author);
                    db.SaveChanges();
                }

                LoadAuthors();
                txtID.Clear();
                txtName.Clear();
                txtBio.Clear();
                txtPhone.Clear();
                txtEmail.Clear();
                optMale.Checked = optFemale.Checked = false;
                IsEnableEditDelete(false);
                MessageBox.Show("Xóa tác giả thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa tác giả: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
