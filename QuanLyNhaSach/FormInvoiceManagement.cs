using QuanLyNhaSach.Data;
using QuanLyNhaSach.Models;
using System;
using System.Data.Entity;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNhaSach
{
    public partial class FormInvoiceManagement : Form
    {
        private readonly BookStoreContext _db = new BookStoreContext();
        private int _selectedInvoiceId = 0;
        private CultureInfo vnCulture = new CultureInfo("vi-VN");

        public FormInvoiceManagement()
        {
            InitializeComponent();
        }

        // ================= FORM LOAD =================
        private void FormInvoiceManagement_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeForm();
                LoadStaff();
                LoadInvoices();
                DisableEditDeleteButtons();
                ConfigureTextBoxes();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi khởi tạo form: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= INIT FORM =================
        private void InitializeForm()
        {
            // Đặt khoảng thời gian mặc định là tháng hiện tại
            dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpToDate.Value = DateTime.Now;

            // Khởi tạo combobox trạng thái
            cmbSearchStatus.SelectedIndex = 0; // "Tất cả"

            // Cấu hình DataGridView
            dgvInvoiceManagement.AutoGenerateColumns = false;
            dgvInvoiceManagement.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInvoiceManagement.MultiSelect = false;
            dgvInvoiceManagement.AllowUserToAddRows = false;
            dgvInvoiceManagement.AllowUserToDeleteRows = false;
            dgvInvoiceManagement.ReadOnly = true;

            // Thêm event double-click để xem chi tiết
            dgvInvoiceManagement.CellDoubleClick += dgvInvoiceManagement_CellDoubleClick;

            // Style cho DataGridView
            dgvInvoiceManagement.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 123, 255);
            dgvInvoiceManagement.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvInvoiceManagement.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold);
            dgvInvoiceManagement.EnableHeadersVisualStyles = false;
            dgvInvoiceManagement.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 248, 255);
        }

        // Cấu hình các TextBox thông tin là ReadOnly
        private void ConfigureTextBoxes()
        {
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;

            // Đặt màu nền để phân biệt ReadOnly
            textBox1.BackColor = SystemColors.Control;
            textBox2.BackColor = SystemColors.Control;
            textBox3.BackColor = SystemColors.Control;
            textBox4.BackColor = SystemColors.Control;
            textBox5.BackColor = SystemColors.Control;
            textBox6.BackColor = SystemColors.Control;
        }

        // ================= LOAD STAFF =================
        private void LoadStaff()
        {
            try
            {
                var allStaff = _db.Users.OrderBy(u => u.FullName).ToList();
                allStaff.Insert(0, new User { UserId = 0, FullName = "-- Tất cả --" });

                cmbSearchStaff.DataSource = allStaff;
                cmbSearchStaff.DisplayMember = "FullName";
                cmbSearchStaff.ValueMember = "UserId";
                cmbSearchStaff.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhân viên: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= LOAD INVOICES =================
        private void LoadInvoices(string customerFilter = null, int? staffId = null,
            int? statusFilter = null, DateTime? fromDate = null, DateTime? toDate = null)
        {
            try
            {
                // Hiển thị loading
                ShowLoading(true);

                dgvInvoiceManagement.Rows.Clear();

                var query = _db.Invoices
                    .Include(i => i.Customer)
                    .Include(i => i.User)
                    .AsQueryable();

                // Áp dụng các bộ lọc
                if (fromDate.HasValue)
                    query = query.Where(i => i.CreatedDate >= fromDate.Value);

                if (toDate.HasValue)
                {
                    var endDate = toDate.Value.Date.AddDays(1).AddSeconds(-1);
                    query = query.Where(i => i.CreatedDate <= endDate);
                }

                if (staffId.HasValue && staffId.Value > 0)
                    query = query.Where(i => i.UserId == staffId.Value);

                if (!string.IsNullOrWhiteSpace(customerFilter))
                    query = query.Where(i => i.Customer.Name.Contains(customerFilter));

                if (statusFilter.HasValue)
                    query = query.Where(i => i.Status == statusFilter.Value);

                var invoices = query.OrderByDescending(i => i.CreatedDate).ToList();

                // Thêm dữ liệu vào DataGridView (KHÔNG CÒN CÁC CỘT BUTTON)
                foreach (var inv in invoices)
                {
                    string statusText = inv.Status == 1 ? "Đã thanh toán" : "Chưa thanh toán";

                    int rowIndex = dgvInvoiceManagement.Rows.Add(
                        inv.InvoiceCode,
                        inv.CreatedDate.ToString("dd/MM/yyyy"),
                        inv.Customer?.Name ?? "",
                        inv.User?.FullName ?? "",
                        inv.TotalAmount.ToString("N0", vnCulture) + " VNĐ",
                        statusText
                    );

                    dgvInvoiceManagement.Rows[rowIndex].Tag = inv.InvoiceId;

                    // Tô màu status
                    if (inv.Status == 1)
                    {
                        dgvInvoiceManagement.Rows[rowIndex].Cells["Column6"].Style.ForeColor = Color.Green;
                        dgvInvoiceManagement.Rows[rowIndex].Cells["Column6"].Style.Font = new Font(dgvInvoiceManagement.Font, FontStyle.Bold);
                    }
                    else
                    {
                        dgvInvoiceManagement.Rows[rowIndex].Cells["Column6"].Style.ForeColor = Color.Red;
                        dgvInvoiceManagement.Rows[rowIndex].Cells["Column6"].Style.Font = new Font(dgvInvoiceManagement.Font, FontStyle.Bold);
                    }
                }

                ClearInvoiceDetails();

                // Hiển thị thống kê
                UpdateStatistics(invoices);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách hóa đơn: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                ShowLoading(false);
            }
        }

        // ================= STATISTICS =================
        private void UpdateStatistics(System.Collections.Generic.List<Invoice> invoices)
        {
            if (invoices == null || invoices.Count == 0)
            {
                lblCountValue.Text = "0";
                lblRevenueValue.Text = "0 VNĐ";
                return;
            }

            decimal totalRevenue = invoices.Sum(i => i.TotalAmount);
            int paidCount = invoices.Count(i => i.Status == 1);

            lblCountValue.Text = $"{invoices.Count}";
            lblRevenueValue.Text = $"{totalRevenue.ToString("N0", vnCulture)} VNĐ";

            // Cập nhật title bar
            this.Text = $"Quản Lý Hóa Đơn - {invoices.Count} hóa đơn | Đã thanh toán: {paidCount}";
        }

        // ================= LOADING INDICATOR =================
        private void ShowLoading(bool show)
        {
            this.Cursor = show ? Cursors.WaitCursor : Cursors.Default;
            dgvInvoiceManagement.Enabled = !show;
        }

        // ================= SEARCH =================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                int? staffId = null;
                if (cmbSearchStaff.SelectedValue != null)
                {
                    int val = Convert.ToInt32(cmbSearchStaff.SelectedValue);
                    if (val > 0) staffId = val;
                }

                int? status = null;
                if (cmbSearchStatus.SelectedIndex == 1) status = 0; // Chưa thanh toán
                else if (cmbSearchStatus.SelectedIndex == 2) status = 1; // Đã thanh toán

                LoadInvoices(
                    txtCustomerSearch.Text.Trim(),
                    staffId,
                    status,
                    dtpFromDate.Value.Date,
                    dtpToDate.Value.Date
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= REFRESH =================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                // Reset các bộ lọc
                dtpFromDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpToDate.Value = DateTime.Now;
                cmbSearchStaff.SelectedIndex = 0;
                cmbSearchStatus.SelectedIndex = 0;
                txtCustomerSearch.Clear();

                LoadInvoices();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi làm mới: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= SELECTION CHANGED =================
        private void dgvInvoiceManagement_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvInvoiceManagement.SelectedRows.Count == 0)
                {
                    ClearInvoiceDetails();
                    DisableEditDeleteButtons();
                    return;
                }

                var row = dgvInvoiceManagement.SelectedRows[0];
                _selectedInvoiceId = Convert.ToInt32(row.Tag);

                LoadInvoiceDetailsPanel(_selectedInvoiceId);
                EnableEditDeleteButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi chọn hóa đơn: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= LOAD DETAILS PANEL =================
        private void LoadInvoiceDetailsPanel(int invoiceId)
        {
            try
            {
                var invoice = _db.Invoices
                    .Include(i => i.Customer)
                    .Include(i => i.User)
                    .FirstOrDefault(i => i.InvoiceId == invoiceId);

                if (invoice == null) return;

                textBox1.Text = invoice.InvoiceCode;
                textBox2.Text = invoice.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                textBox3.Text = invoice.User?.FullName ?? "";
                textBox4.Text = invoice.Customer?.Name ?? "";
                textBox5.Text = invoice.TotalAmount.ToString("N0", vnCulture) + " VNĐ";
                textBox6.Text = invoice.Status == 1 ? "Đã thanh toán" : "Chưa thanh toán";

                // Tô màu status trong textbox
                if (invoice.Status == 1)
                {
                    textBox6.ForeColor = Color.Green;
                }
                else
                {
                    textBox6.ForeColor = Color.Red;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin hóa đơn: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= CLEAR DETAILS =================
        private void ClearInvoiceDetails()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox6.ForeColor = Color.Black;
            _selectedInvoiceId = 0;
        }

        // ================= ENABLE/DISABLE BUTTONS =================
        private void EnableEditDeleteButtons()
        {
            btnView.Enabled = true;
            btnEdit.Enabled = true;
            btnPrint.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void DisableEditDeleteButtons()
        {
            btnView.Enabled = false;
            btnEdit.Enabled = false;
            btnPrint.Enabled = false;
            btnDelete.Enabled = false;
        }

        // ================= DOUBLE CLICK =================
        private void dgvInvoiceManagement_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                var row = dgvInvoiceManagement.Rows[e.RowIndex];
                int invoiceId = Convert.ToInt32(row.Tag);
                ViewInvoice(invoiceId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở chi tiết: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= BUTTON CLICK HANDLERS =================
        private void btnView_Click(object sender, EventArgs e)
        {
            if (_selectedInvoiceId > 0)
                ViewInvoice(_selectedInvoiceId);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (_selectedInvoiceId > 0)
                EditInvoice(_selectedInvoiceId);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var form = new FormReport("Invoice", _selectedInvoiceId);
            form.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedInvoiceId > 0)
                DeleteInvoice(_selectedInvoiceId);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Dispose database trước khi đóng form
            _db?.Dispose();
            Close();
        }

        // ================= VIEW INVOICE =================
        private void ViewInvoice(int invoiceId)
        {
            try
            {
                var detailForm = new FormInvoiceDetail(invoiceId);
                detailForm.ShowDialog();

                // Reload lại sau khi xem chi tiết (có thể có thay đổi)
                LoadInvoices();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem chi tiết hóa đơn: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= EDIT INVOICE =================
        private void EditInvoice(int invoiceId)
        {
            MessageBox.Show("Chức năng chỉnh sửa hóa đơn đang được phát triển", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // TODO: Implement edit functionality
            // var editForm = new FormInvoiceEdit(invoiceId);
            // if (editForm.ShowDialog() == DialogResult.OK)
            // {
            //     LoadInvoices();
            // }
        }

        // ================= PRINT INVOICE =================
        private void PrintInvoice(int invoiceId)
        {
            MessageBox.Show("Chức năng in hóa đơn đang được phát triển", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            // TODO: Implement print functionality
            // ExportHelper.PrintInvoice(invoiceId);
        }

        // ================= DELETE INVOICE =================
        private void DeleteInvoice(int invoiceId)
        {
            try
            {
                // Validate
                if (invoiceId <= 0)
                {
                    MessageBox.Show("Mã hóa đơn không hợp lệ", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var invoice = _db.Invoices.Find(invoiceId);
                if (invoice == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Kiểm tra trạng thái
                if (invoice.Status == 1)
                {
                    var confirmPaid = MessageBox.Show(
                        "Hóa đơn này đã được thanh toán. Bạn có chắc muốn xóa?",
                        "Cảnh báo",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning);

                    if (confirmPaid != DialogResult.Yes) return;
                }

                // Xác nhận xóa
                var result = MessageBox.Show(
                    $"Bạn có chắc muốn xóa hóa đơn {invoice.InvoiceCode}?\n\n" +
                    $"Khách hàng: {invoice.Customer?.Name ?? "N/A"}\n" +
                    $"Tổng tiền: {invoice.TotalAmount.ToString("N0", vnCulture)} VNĐ\n\n" +
                    "Thao tác này KHÔNG THỂ hoàn tác!",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result != DialogResult.Yes) return;

                // Xóa chi tiết hóa đơn trước
                var details = _db.InvoiceDetails
                    .Where(d => d.InvoiceId == invoiceId)
                    .ToList();

                if (details.Any())
                {
                    _db.InvoiceDetails.RemoveRange(details);
                }

                // Xóa hóa đơn
                _db.Invoices.Remove(invoice);
                _db.SaveChanges();

                MessageBox.Show(
                    $"Đã xóa hóa đơn {invoice.InvoiceCode} thành công",
                    "Thành công",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Reload danh sách
                LoadInvoices();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa hóa đơn: {ex.Message}\n\n{ex.InnerException?.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= KEYBOARD SHORTCUTS =================
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                switch (keyData)
                {
                    case Keys.F5: // Làm mới
                        btnRefresh_Click(null, null);
                        return true;

                    case Keys.Control | Keys.F: // Focus vào ô tìm kiếm
                        txtCustomerSearch.Focus();
                        txtCustomerSearch.SelectAll();
                        return true;

                    case Keys.Enter: // Xem chi tiết
                        if (_selectedInvoiceId > 0)
                            btnView_Click(null, null);
                        return true;

                    case Keys.Delete: // Xóa
                        if (_selectedInvoiceId > 0)
                            btnDelete_Click(null, null);
                        return true;

                    case Keys.Escape: // Đóng
                        Close();
                        return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xử lý phím tắt: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}