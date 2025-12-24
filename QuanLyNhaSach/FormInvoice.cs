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

            // Lấy danh sách từ DB
            var customers = _db.Customers.ToList();

            cmbCustomer.DataSource = null;
            // Quan trọng: Thiết lập Member TRƯỚC khi gán DataSource
            cmbCustomer.DisplayMember = "Name"; // Đảm bảo thuộc tính này có trong file Customer.cs
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
                txtPhone.Text = c.Phone;
                txtEmail.Text = c.Email;
                txtAddress.Text = c.Bio;
            }
        }

        private void LoadBookAutoComplete()
        {
            // 1. Lấy danh sách tên sách từ Database
            var bookTitles = _db.Books.Select(b => b.Title).ToList();

            // 2. Tạo nguồn dữ liệu cho AutoComplete
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            source.AddRange(bookTitles.ToArray());

            // 3. Cấu hình TextBox tìm kiếm
            txtSearchBook.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSearchBook.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSearchBook.AutoCompleteCustomSource = source;
        }

        // ================= GRID =================
        private void InitGrid()
        {
            dgvInvoice.AutoGenerateColumns = false;
            dgvInvoice.Rows.Clear();

            colPrice.DefaultCellStyle.Format = "N0"; // Định dạng số với dấu phẩy phân cách hàng nghìn
            colAmount.DefaultCellStyle.Format = "N0"; // Định dạng số với dấu phẩy phân cách hàng nghìn

            colPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colAmount.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }

        // ================= SEARCH BOOK =================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearchBook.Text.Trim();

            if (string.IsNullOrEmpty(keyword)) return;

            _selectedBook = _db.Books
                .Include(b => b.Author)
                .FirstOrDefault(b => b.Title.Contains(keyword));

            if (_selectedBook == null)
            {
                MessageBox.Show("Không tìm thấy sách");
                return;
            }

            txtPrice.Text = _selectedBook.Price.ToString("N0"); // Định dạng số với dấu phẩy phân cách hàng nghìn
            numQuantity.Value = 1;

            if (!string.IsNullOrEmpty(_selectedBook.CoverImagePath))
                pictureBoxCover.ImageLocation = _selectedBook.CoverImagePath;
        }

        // ================= ADD BOOK =================
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            if (_selectedBook == null)
            {
                MessageBox.Show("Vui lòng chọn sách trước!");
                return;
            }

            int qty = (int)numQuantity.Value;
            if (qty <= 0) return;

            decimal price = _selectedBook.Price;
            decimal amount = qty * price;

            bool isExisted = false;

            // Duyệt qua các dòng trong Grid để kiểm tra xem sách đã có chưa
            foreach (DataGridViewRow row in dgvInvoice.Rows)
            {
                if ((int)row.Cells[colBookId.Name].Value == _selectedBook.BookId)
                {
                    // Nếu đã có: CẬP NHẬT LẠI (Ghi đè số lượng mới từ numQuantity)
                    row.Cells[colQuantity.Name].Value = qty;
                    row.Cells[colAmount.Name].Value = amount;

                    isExisted = true;
                    break;
                }
            }

            // Nếu chưa có: Thêm dòng mới
            if (!isExisted)
            {
                dgvInvoice.Rows.Add(
                    _selectedBook.BookId,
                    _selectedBook.Title,
                    _selectedBook.Author.Name,
                    qty,
                    price,
                    amount,
                    false
                );
            }

            UpdateTotal();
        }

        // ================= GRID CLICK =================
        private void dgvInvoice_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Nếu click cột Xóa
            if (dgvInvoice.Columns[e.ColumnIndex].Name == "Column7")
            {
                dgvInvoice.Rows.RemoveAt(e.RowIndex);
                UpdateTotal();
                return;
            }

            int bookId = (int)dgvInvoice.Rows[e.RowIndex]
                .Cells[colBookId.Name].Value;

            _selectedBook = _db.Books
                .Include(b => b.Author)
                .FirstOrDefault(b => b.BookId == bookId);

            if (_selectedBook == null) return;

            txtSearchBook.Text = _selectedBook.Title;
            txtPrice.Text = _selectedBook.Price.ToString("N0");
            numQuantity.Value = Convert.ToInt32(
                dgvInvoice.Rows[e.RowIndex].Cells[colQuantity.Name].Value);

            // 🔥 ĐỔI ẢNH
            if (!string.IsNullOrEmpty(_selectedBook.CoverImagePath)
                && System.IO.File.Exists(_selectedBook.CoverImagePath))
            {
                pictureBoxCover.ImageLocation = _selectedBook.CoverImagePath;
            }
            else
            {
                pictureBoxCover.Image = null;
            }
        }

        // ================= TOTAL =================
        private void UpdateTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvInvoice.Rows)
            {
                total += Convert.ToDecimal(row.Cells[colAmount.Name].Value);
            }

            txtTotal.Text = total.ToString("N0");
        }

        // ================= SAVE =================
        private void btnSaveInvoice_Click(object sender, EventArgs e)
        {
            if (dgvInvoice.Rows.Count == 0)
            {
                MessageBox.Show("Hóa đơn chưa có sách");
                return;
            }

            if (cmbCustomer.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng");
                return;
            }

            if (cmbStaff.SelectedValue == null)
            {
                MessageBox.Show("Chưa có nhân viên, vui lòng đăng nhập");
                return;
            }

            _invoice.CustomerId = (int)cmbCustomer.SelectedValue;
            _invoice.TotalAmount = Convert.ToDecimal(txtTotal.Text.Replace(",", "")); // Loại bỏ dấu phẩy trước khi chuyển đổi
            _invoice.Note = txtNote.Text;

            _invoice.InvoiceCode = txtMaHoaDon.Text;
            _db.Invoices.Add(_invoice);
            try
            {
                _db.SaveChanges();
            }
            catch (DbEntityValidationException ex) // Bắt lỗi validate từ Entity Framework
            {
                var msgs = ex.EntityValidationErrors
                    .SelectMany(ev => ev.ValidationErrors)
                    .Select(v => $"{v.PropertyName}: {v.ErrorMessage}"); 
                MessageBox.Show(string.Join(Environment.NewLine, msgs));
                throw;
            }

            foreach (DataGridViewRow row in dgvInvoice.Rows)
            {
                _db.InvoiceDetails.Add(new InvoiceDetail
                {
                    InvoiceId = _invoice.InvoiceId,
                    BookId = (int)row.Cells[colBookId.Name].Value,
                    Quantity = (int)row.Cells[colQuantity.Name].Value,
                    Price = (decimal)row.Cells[colPrice.Name].Value
                });
            }

            _db.SaveChanges();
            MessageBox.Show("Lưu hóa đơn thành công");
            Close();
        }

        // ================= CANCEL =================
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
