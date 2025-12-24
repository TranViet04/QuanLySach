using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using QuanLyNhaSach.Data;
using QuanLyNhaSach.Models;

namespace QuanLyNhaSach
{
    public partial class FormDistributor : Form
    {
        int editingDistributorId = -1;

        private int? selectedDistributorId = null; // Biến lưu trữ ID của nhà phân phối hiện tại
        private BookStoreContext _context = new BookStoreContext(); // Biến để tương tác với cơ sở dữ liệu

        // Phương thức để bật/tắt chế độ chỉnh sửa
        private void IsEnableEditMode(bool isEditMode)
        {
            btnAdd.Enabled = !isEditMode;
            btnEdit.Enabled = isEditMode;
            btnDelete.Enabled = isEditMode;
        }
        public FormDistributor()
        {
            InitializeComponent();

            selectedDistributorId = null; // Gán giá trị ID nhà phân phối
            LoadDistributors(); // Tải danh sách nhà phân phối khi khởi động form
            IsEnableEditMode(false);
        }

        private void LoadDistributors()
        {
            // Phương thức để tải danh sách nhà phân phối từ cơ sở dữ liệu và hiển thị trên DataGridView
            try
            {
                dgvDistributer.Rows.Clear();
                var distributors = _context.Distributors.ToList();
                foreach (var distributor in distributors)
                {
                    dgvDistributer.Rows.Add(distributor.DistributorId, distributor.DistributorName, distributor.Address, distributor.Phone, distributor.Email);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải nhà phân phối: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Nút thêm mới nhà xuất bản
            try
            {
                errWarning.SetError(txtDistributorName, "");
                txtDistributorName.BackColor = System.Drawing.Color.White;

                using (var db = new BookStoreContext()) 
                {
                    var existingDistributor = db.Distributors.FirstOrDefault(p => p.DistributorName == txtDistributorName.Text);
                    if (existingDistributor != null)
                    {
                        MessageBox.Show("Tên nhà phân phối đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDistributorName.BackColor = System.Drawing.Color.MistyRose;
                        errWarning.SetError(txtDistributorName, "Tên nhà phân phối đã tồn tại!");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtDistributorName.Text))
                    {
                        MessageBox.Show("Vui lòng nhập tên nhà phân phối.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDistributorName.BackColor = System.Drawing.Color.MistyRose;
                        errWarning.SetError(txtDistributorName, "Vui lòng nhập tên nhà phân phối.");
                        return;
                    }

                    var distributor = new Distributor
                    {
                        DistributorName = txtDistributorName.Text,
                        Address = txtAddress.Text,
                        Phone = txtPhone.Text,
                        Email = txtEmail.Text
                    };
                    db.Distributors.Add(distributor);
                    db.SaveChanges();
                    txtDistributorID.Text = distributor.DistributorId.ToString();
                    LoadDistributors();

                    MessageBox.Show("Thêm nhà phân phối thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDistributorName.Clear();
                    txtDistributorID.Clear();
                    txtAddress.Clear();
                    txtPhone.Clear();
                    txtEmail.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm nhà xuất bản: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Nút sửa nhà xuất bản
            string distributorName = txtDistributorName.Text;
            string address = txtAddress.Text;
            string phone = txtPhone.Text;
            string email = txtEmail.Text;

            errWarning.SetError(txtDistributorName, "");
            txtDistributorName.BackColor = System.Drawing.Color.White;

             if (string.IsNullOrWhiteSpace(distributorName))
             {
                 MessageBox.Show("Vui lòng nhập tên nhà phân phối.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDistributorName.BackColor = System.Drawing.Color.MistyRose;
                 errWarning.SetError(txtDistributorName, "Vui lòng nhập tên nhà phân phối.");
                 return;
             }

             try
             {
                if (dgvDistributer.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn nhà phân phối để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                 DataGridViewRow selectionRow = dgvDistributer.SelectedRows[0];
                 int distributorId = Convert.ToInt32(selectionRow.Cells["dgvDistributorID"].Value); // Lấy ID nhà phân phối từ dòng được chọn

                 using (var db = new BookStoreContext())
                 {
                     var distributor = db.Distributors.Find(distributorId);
                     if (distributor != null)
                     {
                         distributor.DistributorName = distributorName;
                         distributor.Address = address;
                         distributor.Phone = phone;
                         distributor.Email = email;
                         db.SaveChanges();
                         LoadDistributors();
                         MessageBox.Show("Sửa nhà phân phối thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                         txtDistributorName.Clear();
                         txtDistributorID.Clear();
                         txtAddress.Clear();
                         txtPhone.Clear();
                         txtEmail.Clear();
                     }
                     else
                     {
                         MessageBox.Show("Nhà phân phối không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     }
                 }
             }
             catch (Exception es)
             {
                 MessageBox.Show("Lỗi khi sửa nhà phân phối: " + es.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Nút hủy chọn trên form nhà xuất bản
             if (dgvDistributer.SelectedRows.Count > 0)
             {
                 dgvDistributer.ClearSelection();
             }
            txtDistributorName.Clear();
            txtDistributorID.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            errWarning.SetError(txtDistributorName, "");
            txtDistributorName.BackColor = System.Drawing.Color.White;
            IsEnableEditMode(false);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedDistributorId == null)
                {
                    MessageBox.Show("Vui lòng chọn nhà phân phối để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = selectedDistributorId.Value;

                using (var db = new BookStoreContext())
                {
                    // check for dependent books
                    var hasBooks = db.Books.Any(b => b.DistributorId == id);
                    if (hasBooks)
                    {
                        MessageBox.Show("Không thể xóa nhà phân phối này vì còn sách tham chiếu đến nó. Hãy xóa hoặc chuyển các sách liên quan trước khi xóa nhà phân phối.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var distributor = db.Distributors.Find(id);
                    if (distributor != null)
                    {
                        db.Distributors.Remove(distributor);
                        db.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Nhà phân phối không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                LoadDistributors();
                selectedDistributorId = null;
                txtDistributorName.Clear();
                txtDistributorID.Clear();
                txtAddress.Clear();
                txtPhone.Clear();
                txtEmail.Clear();
                MessageBox.Show("Xóa nhà phân phối thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Show inner exception when available for debugging
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show("Lỗi khi xóa nhà phân phối: " + msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormDistributor_Load(object sender, EventArgs e)
        {
            LoadDistributors();
        }

        private void dgvDistributor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Sự kiện khi chọn một dòng trong DataGridView để hiển thị thông tin lên các TextBox

            if (e.RowIndex >= 0)
            {
                var row = dgvDistributer.Rows[e.RowIndex];
                int distributorId = (int)row.Cells[0].Value;
                using (var db = new BookStoreContext())
                {
                    var distributor = db.Distributors.Find(distributorId);
                    if (distributor != null)
                    {
                        editingDistributorId = distributor.DistributorId;
                        txtDistributorName.Text = distributor.DistributorName;
                        txtAddress.Text = distributor.Address;
                        txtPhone.Text = distributor.Phone;
                        txtEmail.Text = distributor.Email;

                    }
                }
                row.Selected = true;

            }

        }

        private void dgvDistributor_SelectionChanged(object sender, EventArgs e)
        {
           if (dgvDistributer.SelectedRows.Count > 0)
            {
                IsEnableEditMode(true);
                var row  = dgvDistributer.SelectedRows[0];
                int parsed;
                if (int.TryParse(row.Cells[0].Value?.ToString(), out parsed))
                {
                    selectedDistributorId = parsed;
                }
                else
                {
                    selectedDistributorId = null;
                }
                txtDistributorName.Text = row.Cells[1].Value?.ToString() ?? "";
                txtAddress.Text = row.Cells[2].Value?.ToString() ?? "";
                txtPhone.Text = row.Cells[3].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells[4].Value?.ToString() ?? "";

                row.Selected = true;
            }
            else 
            {
                IsEnableEditMode(false);
                txtDistributorName.Clear();
                txtAddress.Clear();
                txtPhone.Clear();
                txtEmail.Clear();
            }
        }
    }
}
