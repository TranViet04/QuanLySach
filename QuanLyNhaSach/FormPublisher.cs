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
    public partial class FormPublisher : Form
    {
        int edeitingPublisherId = -1;

        private int? selectedPublisherId = null; // Biến lưu trữ ID của nhà xuất bản hiện tại
        private BookStoreContext _context = new BookStoreContext(); // Biến để tương tác với cơ sở dữ liệu

        // Phương thức để bật/tắt chế độ chỉnh sửa
        private void IsEnableEditMode(bool isEditMode)
        {
            btnAdd.Enabled = !isEditMode;
            btnEdit.Enabled = isEditMode;
            btnDelete.Enabled = isEditMode;
        }
        public FormPublisher()
        {
            InitializeComponent();

            selectedPublisherId = null; // Gán giá trị ID nhà xuất bản
            LoadPublishers(); // Tải danh sách nhà xuất bản khi khởi động form
            IsEnableEditMode(false);
        }

        private void LoadPublishers()
        {
            // Phương thức để tải danh sách nhà xuất bản từ cơ sở dữ liệu và hiển thị trên DataGridView
            try
            {
                dgvPublisher.Rows.Clear();
                var publishers = _context.Publishers.ToList();
                foreach (var publisher in publishers)
                {
                    dgvPublisher.Rows.Add(publisher.PublisherId, publisher.Name, publisher.Address, publisher.Phone, publisher.Email);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải nhà xuất bản: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Nút thêm mới nhà xuất bản
            try
            {
                errWarning.SetError(txtPublisherName, "");
                txtPublisherName.BackColor = System.Drawing.Color.White;

                using (var db = new BookStoreContext()) 
                {
                    var existingPublisher = db.Publishers.FirstOrDefault(p => p.Name == txtPublisherName.Text);
                    if (existingPublisher != null)
                    {
                        MessageBox.Show("Tên nhà xuất bản đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtPublisherName.BackColor = System.Drawing.Color.MistyRose;
                        errWarning.SetError(txtPublisherName, "Tên nhà xuất bản đã tồn tại!");
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(txtPublisherName.Text) || 
                        string.IsNullOrWhiteSpace(txtAddress.Text) || 
                        string.IsNullOrWhiteSpace(txtPhone.Text) || 
                        string.IsNullOrWhiteSpace(txtEmail.Text))
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        if (string.IsNullOrWhiteSpace(txtPublisherName.Text))
                        {
                            txtPublisherName.BackColor = System.Drawing.Color.MistyRose;
                            errWarning.SetError(txtPublisherName, "Vui lòng nhập tên nhà xuất bản.");
                        }
                        return;
                    }

                    var publisher = new Publisher
                    {
                        Name = txtPublisherName.Text,
                        Address = txtAddress.Text,
                        Phone = txtPhone.Text,
                        Email = txtEmail.Text
                    };
                    db.Publishers.Add(publisher);
                    db.SaveChanges();
                    txtPublisherID.Text = publisher.PublisherId.ToString();
                    LoadPublishers();

                    MessageBox.Show("Thêm nhà xuất bản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPublisherName.Clear();
                    txtPublisherID.Clear();
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
            string publisherName = txtPublisherName.Text;
            string address = txtAddress.Text;
            string phone = txtPhone.Text;
            string email = txtEmail.Text;

            errWarning.SetError(txtPublisherName, "");
            txtPublisherName.BackColor = System.Drawing.Color.White;

            if (string.IsNullOrWhiteSpace(publisherName) || string.IsNullOrWhiteSpace(address) ||
                string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (string.IsNullOrWhiteSpace(publisherName))
                {
                    txtPublisherName.BackColor = System.Drawing.Color.MistyRose;
                    errWarning.SetError(txtPublisherName, "Vui lòng nhập tên nhà xuất bản.");
                }
                return;
            }

            try
            {
                if (dgvPublisher.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn nhà xuất bản để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow selectionRow = dgvPublisher.SelectedRows[0];
                int publisherId = Convert.ToInt32(selectionRow.Cells["dgvPublisherID"].Value);

                using (var db = new BookStoreContext())
                {
                    var publisher = db.Publishers.Find(publisherId);
                    if (publisher != null)
                    {
                        publisher.Name = publisherName;
                        publisher.Address = address;
                        publisher.Phone = phone;
                        publisher.Email = email;
                        db.SaveChanges();
                        LoadPublishers();
                        MessageBox.Show("Sửa nhà xuất bản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPublisherName.Clear();
                        txtPublisherID.Clear();
                        txtAddress.Clear();
                        txtPhone.Clear();
                        txtEmail.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Nhà xuất bản không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception es)
            {
                
                MessageBox.Show("Lỗi khi sửa nhà xuất bản: " + es.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //Nút hủy chọn trên form nhà xuất bản
            if (dgvPublisher.SelectedRows.Count > 0)
            {
                dgvPublisher.ClearSelection();
            }
            txtPublisherName.Clear();
            txtPublisherID.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            errWarning.SetError(txtPublisherName, "");
            txtPublisherName.BackColor = System.Drawing.Color.White;
            IsEnableEditMode(false);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedPublisherId == null)
                {
                    MessageBox.Show("Vui lòng chọn nhà xuất bản để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = selectedPublisherId.Value;

                using (var db = new BookStoreContext())
                {
                    // check for dependent books
                    var hasBooks = db.Books.Any(b => b.PublisherId == id);
                    if (hasBooks)
                    {
                        MessageBox.Show("Không thể xóa nhà xuất bản này vì còn sách tham chiếu đến nó. Hãy xóa hoặc chuyển các sách liên quan trước khi xóa nhà xuất bản.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    var publisher = db.Publishers.Find(id);
                    if (publisher != null)
                    {
                        db.Publishers.Remove(publisher);
                        db.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Nhà xuất bản không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                LoadPublishers();
                selectedPublisherId = null;
                txtPublisherName.Clear();
                txtAddress.Clear();
                txtPhone.Clear();
                txtEmail.Clear();
                MessageBox.Show("Xóa nhà xuất bản thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Show inner exception when available for debugging
                var msg = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                MessageBox.Show("Lỗi khi xóa nhà xuất bản: " + msg, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormPublisher_Load(object sender, EventArgs e)
        {
            LoadPublishers();
        }

        private void dgvPublisher_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Sự kiện khi chọn một dòng trong DataGridView để hiển thị thông tin lên các TextBox

            if (e.RowIndex >= 0)
            {
                var row = dgvPublisher.Rows[e.RowIndex];
                int publisherId = (int)row.Cells[0].Value;
                using (var db = new BookStoreContext())
                {
                    var publisher = db.Publishers.Find(publisherId);
                    if (publisher != null)
                    {
                        edeitingPublisherId = publisher.PublisherId;
                        txtPublisherName.Text = publisher.Name;
                        txtAddress.Text = publisher.Address;
                        txtPhone.Text = publisher.Phone;
                        txtEmail.Text = publisher.Email;

                    }
                }
                row.Selected = true;

            }

        }

        private void dgvPublisher_SelectionChanged(object sender, EventArgs e)
        {
           if (dgvPublisher.SelectedRows.Count > 0)
            {
                IsEnableEditMode(true);
                var row  = dgvPublisher.SelectedRows[0];
                int parsed;
                if (int.TryParse(row.Cells[0].Value?.ToString(), out parsed))
                {
                    selectedPublisherId = parsed;
                }
                else
                {
                    selectedPublisherId = null;
                }
                txtPublisherName.Text = row.Cells[1].Value?.ToString() ?? "";
                txtAddress.Text = row.Cells[2].Value?.ToString() ?? "";
                txtPhone.Text = row.Cells[3].Value?.ToString() ?? "";
                txtEmail.Text = row.Cells[4].Value?.ToString() ?? "";

                row.Selected = true;
            }
            else 
            {
                IsEnableEditMode(false);
                txtPublisherName.Clear();
                txtAddress.Clear();
                txtPhone.Clear();
                txtEmail.Clear();
            }
        }
    }
}
