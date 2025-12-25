using QuanLyNhaSach.Data;
using QuanLyNhaSach.Models;
using QuanLyNhaSach.Models.QuanLyNhaSach.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNhaSach
{
    public partial class FormInvoice : Form
    {
        private readonly BookStoreContext _db = new BookStoreContext();
        private Invoice _invoice;
        private Book _selectedBook;
        private bool _isLoadingCustomer = false;

        public FormInvoice()
        {
            InitializeComponent();
        }

        // ================= FORM LOAD =================
        private void FormInvoice_Load(object sender, EventArgs e)
        {
            InitInvoice();
            LoadStaff();
            LoadCustomers();
            LoadBookAutoComplete();
            InitGrid();

            // Đặt txtTotalCustomerPay là ReadOnly
            txtTotalCustomerPay.ReadOnly = true;
        }

        // ================= DISPOSE =================
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        // ================= INIT =================
        private void InitInvoice()
        {
            _invoice = new Invoice
            {
                InvoiceCode = GenerateInvoiceCode(),
                CreatedDate = DateTime.Now,
                UserId = CurrentUser.UserId
            };

            txtMaHoaDon.Text = _invoice.InvoiceCode;
            txtMaHoaDon.ReadOnly = true;
            dtpNgayTao.Value = _invoice.CreatedDate;
            rdoChuaThanhToan.Checked = true;
            txtTotal.Text = "0";
            txtDiscount.Text = "0";
            txtTotalCustomerPay.Text = "0";
        }

        private string GenerateInvoiceCode()
        {
            return "HD" + DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        // ================= LOAD STAFF =================
        private void LoadStaff()
        {
            cmbStaff.DataSource = _db.Users.ToList();
            cmbStaff.DisplayMember = "FullName";
            cmbStaff.ValueMember = "UserId";
            cmbStaff.SelectedValue = CurrentUser.UserId;
            cmbStaff.Enabled = false;
        }

        // ================= LOAD CUSTOMER =================
        private void LoadCustomers()
        {
            _isLoadingCustomer = true;

            var customers = _db.Customers.ToList();

            cmbCustomer.DataSource = null;
            cmbCustomer.DisplayMember = "Name";
            cmbCustomer.ValueMember = "ID";
            cmbCustomer.DataSource = customers;

            cmbCustomer.SelectedIndex = -1;
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();

            _isLoadingCustomer = false;
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_isLoadingCustomer) return;
            if (cmbCustomer.SelectedIndex < 0) return;

            if (cmbCustomer.SelectedItem is Customer c)
            {
                txtPhone.Text = c.Phone ?? "";
                txtEmail.Text = c.Email ?? "";
                txtAddress.Text = c.Bio ?? "";
            }
        }

        private void LoadBookAutoComplete()
        {
            var bookTitles = _db.Books.Select(b => b.Title).ToList();

            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            source.AddRange(bookTitles.ToArray());

            txtSearchBook.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchBook.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchBook.AutoCompleteCustomSource = source;
        }

        // ================= GRID =================
        private void InitGrid()
        {
            dgvInvoice.AutoGenerateColumns = false;
            dgvInvoice.Rows.Clear();

            colPrice.DefaultCellStyle.Format = "N0";
            colAmount.DefaultCellStyle.Format = "N0";

            colPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        // ================= SEARCH BOOK =================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearchBook.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                MessageBox.Show("Vui lòng nhập tên sách", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _selectedBook = _db.Books
                .Include(b => b.Author)
                .FirstOrDefault(b => b.Title.Contains(keyword));

            if (_selectedBook == null)
            {
                MessageBox.Show("Không tìm thấy sách", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearBookSelection();
                return;
            }

            // Kiểm tra tồn kho (giả sử có thuộc tính Stock trong Book)
            // if (_selectedBook.Stock <= 0)
            // {
            //     MessageBox.Show("Sách này đã hết hàng", "Cảnh báo",
            //         MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     ClearBookSelection();
            //     return;
            // }

            txtPrice.Text = _selectedBook.Price.ToString("N0");
            numQuantity.Value = 1;

            LoadBookImage(_selectedBook.CoverImagePath);
        }

        private void LoadBookImage(string imagePath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                {
                    pictureBoxCover.ImageLocation = imagePath;
                }
                else
                {
                    pictureBoxCover.Image = null;
                }
            }
            catch (Exception ex)
            {
                pictureBoxCover.Image = null;
                System.Diagnostics.Debug.WriteLine($"Lỗi load ảnh: {ex.Message}");
            }
        }

        private void ClearBookSelection()
        {
            txtSearchBook.Clear();
            txtPrice.Clear();
            numQuantity.Value = 1;
            _selectedBook = null;
            pictureBoxCover.Image = null;
        }

        // ================= ADD BOOK =================
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            if (_selectedBook == null)
            {
                MessageBox.Show("Vui lòng chọn sách trước!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int qty = (int)numQuantity.Value;
            if (qty <= 0)
            {
                MessageBox.Show("Số lượng phải lớn hơn 0", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra tồn kho (nếu có thuộc tính Stock)
            // if (qty > _selectedBook.Stock)
            // {
            //     MessageBox.Show($"Chỉ còn {_selectedBook.Stock} cuốn trong kho", "Cảnh báo",
            //         MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //     return;
            // }

            if (_selectedBook.Price <= 0)
            {
                MessageBox.Show("Giá sách không hợp lệ", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal price = _selectedBook.Price;
            decimal amount = qty * price;

            bool isExisted = false;

            // Kiểm tra sách đã có trong grid chưa
            foreach (DataGridViewRow row in dgvInvoice.Rows)
            {
                if ((int)row.Cells[colBookId.Name].Value == _selectedBook.BookId)
                {
                    // Cộng dồn số lượng thay vì ghi đè
                    int currentQty = (int)row.Cells[colQuantity.Name].Value;
                    int newQty = currentQty + qty;

                    // Kiểm tra tồn kho với số lượng mới
                    // if (newQty > _selectedBook.Stock)
                    // {
                    //     MessageBox.Show($"Chỉ còn {_selectedBook.Stock} cuốn trong kho", "Cảnh báo",
                    //         MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //     return;
                    // }

                    row.Cells[colQuantity.Name].Value = newQty;
                    row.Cells[colAmount.Name].Value = newQty * price;
                    isExisted = true;
                    break;
                }
            }

            // Thêm dòng mới nếu chưa tồn tại
            if (!isExisted)
            {
                dgvInvoice.Rows.Add(
                    _selectedBook.BookId,
                    _selectedBook.Title,
                    _selectedBook.Author?.Name ?? "N/A",
                    qty,
                    price,
                    amount,
                    false
                );
            }

            UpdateTotal();
            ClearBookSelection();
        }

        // ================= GRID CLICK =================
        private void dgvInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Nếu click cột Xóa
            if (dgvInvoice.Columns[e.ColumnIndex].Name == "Column7")
            {
                var result = MessageBox.Show("Bạn có chắc muốn xóa sách này?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    dgvInvoice.Rows.RemoveAt(e.RowIndex);
                    UpdateTotal();
                }
                return;
            }

            int bookId = (int)dgvInvoice.Rows[e.RowIndex].Cells[colBookId.Name].Value;

            _selectedBook = _db.Books
                .Include(b => b.Author)
                .FirstOrDefault(b => b.BookId == bookId);

            if (_selectedBook == null) return;

            txtSearchBook.Text = _selectedBook.Title;
            txtPrice.Text = _selectedBook.Price.ToString("N0");
            numQuantity.Value = Convert.ToInt32(
                dgvInvoice.Rows[e.RowIndex].Cells[colQuantity.Name].Value);

            LoadBookImage(_selectedBook.CoverImagePath);
        }

        // ================= TOTAL CALCULATION =================
        private void UpdateTotal()
        {
            // Tính tổng tiền chưa giảm
            decimal subtotal = 0;

            foreach (DataGridViewRow row in dgvInvoice.Rows)
            {
                subtotal += Convert.ToDecimal(row.Cells[colAmount.Name].Value);
            }

            // Hiển thị tổng tiền chưa giảm
            txtTotal.Text = subtotal.ToString("N0");

            // Tính số tiền giảm
            decimal discountAmount = 0;
            if (decimal.TryParse(txtDiscount.Text, out decimal discountPercent))
            {
                if (discountPercent < 0 || discountPercent > 100)
                {
                    txtDiscount.Text = "0";
                    discountPercent = 0;
                }
                discountAmount = subtotal * (discountPercent / 100);
            }

            // Tính số tiền khách cần trả (sau khi đã giảm giá)
            decimal finalTotal = subtotal - discountAmount;
            txtTotalCustomerPay.Text = finalTotal.ToString("N0");
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            UpdateTotal();
        }

        // Validation cho txtDiscount khi rời khỏi textbox
        private void txtDiscount_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDiscount.Text))
            {
                txtDiscount.Text = "0";
                UpdateTotal();
                return;
            }

            if (!decimal.TryParse(txtDiscount.Text, out decimal value) || value < 0 || value > 100)
            {
                MessageBox.Show("Giảm giá phải từ 0 đến 100%", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiscount.Text = "0";
                UpdateTotal();
            }
        }

        // ================= SAVE =================
        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            // Validation
            if (dgvInvoice.Rows.Count == 0)
            {
                MessageBox.Show("Hóa đơn chưa có sách", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCustomer.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbStaff.SelectedValue == null)
            {
                MessageBox.Show("Chưa có nhân viên, vui lòng đăng nhập", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Parse discount
            decimal discountPercent = 0;
            if (!string.IsNullOrWhiteSpace(txtDiscount.Text))
            {
                if (!decimal.TryParse(txtDiscount.Text, out discountPercent) ||
                    discountPercent < 0 || discountPercent > 100)
                {
                    MessageBox.Show("Giảm giá không hợp lệ (0-100%)", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                // Tính tổng tiền chưa giảm
                decimal subtotal = 0;
                foreach (DataGridViewRow row in dgvInvoice.Rows)
                {
                    subtotal += Convert.ToDecimal(row.Cells[colAmount.Name].Value);
                }

                // Tính số tiền giảm và tổng tiền khách phải trả
                decimal discountAmount = subtotal * (discountPercent / 100);
                decimal finalTotal = subtotal - discountAmount;

                // Tạo hóa đơn
                _invoice.CustomerId = (int)cmbCustomer.SelectedValue;
                _invoice.TotalAmount = finalTotal; // Lưu số tiền sau giảm giá (số tiền khách phải trả)
                _invoice.Note = txtNote.Text?.Trim();
                _invoice.Status = rdoDaThanhToan.Checked ? 1 : 0;

                _db.Invoices.Add(_invoice);

                // Lưu chi tiết hóa đơn
                foreach (DataGridViewRow row in dgvInvoice.Rows)
                {
                    int bookId = (int)row.Cells[colBookId.Name].Value;
                    int quantity = (int)row.Cells[colQuantity.Name].Value;
                    decimal price = (decimal)row.Cells[colPrice.Name].Value;

                    _db.InvoiceDetails.Add(new InvoiceDetail
                    {
                        InvoiceId = _invoice.InvoiceId,
                        BookId = bookId,
                        Quantity = quantity,
                        Price = price
                    });

                    // Cập nhật tồn kho (nếu có thuộc tính Stock)
                    // var book = _db.Books.Find(bookId);
                    // if (book != null)
                    // {
                    //     book.Stock -= quantity;
                    // }
                }

                _db.SaveChanges();

                // Hiển thị thông báo thành công
                string message = "Lưu hóa đơn thành công!\n\n";
                message += $"Mã hóa đơn: {_invoice.InvoiceCode}\n";
                message += $"Tổng tiền hàng: {subtotal.ToString("N0")} VNĐ\n";

                if (discountPercent > 0)
                {
                    message += $"Giảm giá ({discountPercent}%): -{discountAmount.ToString("N0")} VNĐ\n";
                }

                message += $"Số tiền khách phải trả: {finalTotal.ToString("N0")} VNĐ";

                MessageBox.Show(message, "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                Close();
            }
            catch (DbEntityValidationException ex)
            {
                string errorMessage = "Lỗi validation:\n";
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        errorMessage += $"- {validationError.PropertyName}: {validationError.ErrorMessage}\n";
                    }
                }
                MessageBox.Show(errorMessage, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu hóa đơn:\n{ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= CANCEL =================
        private void btnCancel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn hủy hóa đơn này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}