using QuanLyNhaSach.Data;
using QuanLyNhaSach.Models;
using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNhaSach
{
    public partial class FormCategory : Form
    {
        // category id for editing/deleting
        int edittingCategoryId = -1;

        private int? selectedCategoryId = null;

        private string oldName = string.Empty;
        private string oldDescription = string.Empty;

        public FormCategory()
        {
            InitializeComponent();
            this.dgvCategories.SelectionChanged += dgvCategories_SelectionChanged;
            LoadCategories();
            IsEnableEditDelete(false);
        }

        private void IsEnableEditDelete(bool isEnabled) // enable or disable edit/delete buttons
        {
            btnEdit.Enabled = isEnabled;
            btnDelete.Enabled = isEnabled;
            btnAdd.Enabled = !isEnabled;
        }

        // Phương thức để tải lại danh mục từ các form khác giúp cho đồng bộ dữ liệu
        public void ReloadCategories()
        {
            if (this.IsDisposed || this.Disposing) return;
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => { try { LoadCategories(); } catch { } }));
            }
            else
            {
                try { LoadCategories(); } catch { }
            }
        }

        private void LoadCategories() //Lớp nạp danh mục từ cơ sở dữ liệu và hiển thị trong DataGridView
        {
            try
            {
                dgvCategories.Rows.Clear();
                using (var db = new BookStoreContext())
                {
                    var categories = db.Categories.OrderBy(c => c.Name).ToList();
                    foreach (var c in categories)
                    {
                        dgvCategories.Rows.Add(c.CategoryId, c.Name, c.Description); // add rows to datagridview
                    }
                }

                NotifyBookFormsToReloadCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // clear previous error
            errWarning.SetError(txtCategory, "");

            string name = txtCategory.Text.Trim();
            string desc = txtDescription.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                txtCategory.BackColor = System.Drawing.Color.MistyRose;
                MessageBox.Show("Vui lòng nhập tên danh mục!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //set error provider on txtCategory
                errWarning.SetError(txtCategory, "Vui lòng nhập tên danh mục!");
                return;
            }
            txtCategory.BackColor = System.Drawing.Color.White;

            try
            {
                using (var db = new BookStoreContext())
                {
                    if (db.Categories.Any(c => c.Name.ToLower() == name.ToLower()))
                    {
                        MessageBox.Show("Tên danh mục đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        errWarning.SetError(txtCategory, "Tên danh mục đã tồn tại.");
                        return;
                    }

                    var category = new Category { Name = name, Description = desc };
                    db.Categories.Add(category);
                    db.SaveChanges();
                }

                LoadCategories();
                txtCategory.Clear();
                txtDescription.Clear();
                MessageBox.Show("Thêm danh mục thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (DbEntityValidationException ex)
            {
                MessageBox.Show("Lỗi khi thêm danh mục: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                //set error provider on txtCategory
                errWarning.SetError(txtCategory, "Lỗi khi thêm danh mục: " + ex.Message);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            string name = txtCategory.Text.Trim();
            string desc = txtDescription.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                txtCategory.BackColor = System.Drawing.Color.MistyRose;
                MessageBox.Show("Vui lòng nhập tên danh mục!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            txtCategory.BackColor = System.Drawing.Color.White;

            var result = MessageBox.Show(
                "Bạn có muốn cập nhật dữ liệu mới không?\n- YES để lưu, NO để phục hồi cũ.",
                "Xác nhận sửa", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            if (result == DialogResult.Cancel) return;
            if (result == DialogResult.No)
            {
                txtCategory.Text = oldName;
                txtDescription.Text = oldDescription;
                return;
            }

            try
            {
                using (var db = new BookStoreContext())
                {
                    var category = db.Categories.Find(selectedCategoryId);
                    if (category != null)
                    {
                        if (db.Categories.Any(c => c.CategoryId != category.CategoryId && c.Name.ToLower() == name.ToLower()))
                        {
                            MessageBox.Show("Tên danh mục đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        category.Name = name;
                        category.Description = desc;
                        db.SaveChanges();
                    }
                }

                LoadCategories();
                selectedCategoryId = null;
                txtCategory.Clear();
                txtDescription.Clear();
                MessageBox.Show("Sửa danh mục thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Xóa danh mục này? Sách vẫn tồn tại nhưng mất danh mục.",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

            try
            {
                using (var db = new BookStoreContext())
                {
                    var category = db.Categories.Find(selectedCategoryId);
                    if (category != null)
                    {
                        db.Categories.Remove(category);
                        db.SaveChanges();
                    }
                }

                LoadCategories();
                selectedCategoryId = null;
                txtCategory.Clear();
                txtDescription.Clear();
                MessageBox.Show("Xóa danh mục thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvCategories_SelectionChanged(object sender, EventArgs e) // Khi lựa chọn thay đổi trong DataGridView
        {
            if (dgvCategories.SelectedRows.Count > 0)
            {
                var row = dgvCategories.SelectedRows[0];
                int parsed;
                if (int.TryParse(row.Cells[0].Value?.ToString(), out parsed))
                    selectedCategoryId = parsed;
                else
                    selectedCategoryId = null;

                txtCategory.Text = row.Cells[1].Value?.ToString() ?? "";
                txtDescription.Text = row.Cells[2].Value?.ToString() ?? "";
                oldName = txtCategory.Text;
                oldDescription = txtDescription.Text;
                row.Selected = true;
            }
            else
            {
                selectedCategoryId = null;
                txtCategory.Clear();
                txtDescription.Clear();
            }
        }

        private void NotifyBookFormsToReloadCategories() // Thông báo các form sách để tải lại danh mục
        {
            if (this.MdiParent == null) return;
            foreach (Form child in this.MdiParent.MdiChildren)
            {
                if (child is FormBook bookForm)
                {
                    try { bookForm.ReloadCategories(); } catch { }
                }
            }
        }

        private void FormCategory_Load(object sender, EventArgs e) // Khi form được tải
        {
            LoadCategories();
        }

        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e) // Khi một ô trong DataGridView được nhấp
        {
            // get selected row first cell value (ID)
            if (e.RowIndex >= 0)
            {
                var row = dgvCategories.Rows[e.RowIndex];
                int categoryId = (int)row.Cells[0].Value;
                using (var db = new BookStoreContext())
                {
                    var category = db.Categories.Find(categoryId);
                    if (category != null)
                    {
                        edittingCategoryId = category.CategoryId;
                        txtCategory.Text = category.Name;
                        txtDescription.Text = category.Description;
                        IsEnableEditDelete(true);
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Nút hủy bỏ chỉnh sửa
            edittingCategoryId = -1;
            txtCategory.Clear();
            txtDescription.Clear();
            IsEnableEditDelete(false);

        }

        private void btnInThongKe_Click(object sender, EventArgs e)
        {
            FormReport formInDanhMuc = new FormReport("Category", selectedCategoryId);
            formInDanhMuc.ShowDialog();
        }
    }
}
