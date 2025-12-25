using QuanLyNhaSach.Data;
using QuanLyNhaSach.Models;
using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNhaSach
{
    public partial class FormPurchaseOrderList : Form
    {
        private readonly BookStoreContext _db = new BookStoreContext();
        private CultureInfo vnCulture = new CultureInfo("vi-VN");
        private int _selectedPurchaseOrderId = 0;

        public FormPurchaseOrderList()
        {
            InitializeComponent();
        }

        // ================= FORM LOAD =================
        private void FormPurchaseOrderList_Load(object sender, EventArgs e)
        {
            InitGrid();
            InitDateRange();
            LoadDistributors();
            LoadPurchaseOrders(); // Load after InitDateRange

            // Disable delete button initially
            btnDelete.Enabled = false;
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
        private void InitGrid()
        {
            // Setup purchase orders grid
            dgvPurchaseOrders.AutoGenerateColumns = false;
            dgvPurchaseOrders.Rows.Clear();

            // Setup details grid
            dgvOrderDetails.AutoGenerateColumns = false;
            dgvOrderDetails.Rows.Clear();
        }

        private void InitDateRange()
        {
            // Default: tháng hiện tại
            dtpStartDate.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEndDate.Value = DateTime.Now;
        }

        // ================= LOAD DISTRIBUTORS =================
        private void LoadDistributors()
        {
            try
            {
                var distributors = _db.Distributors
                    .OrderBy(d => d.DistributorName)
                    .ToList();

                // Add "All" option
                var allOption = new Distributor
                {
                    DistributorId = 0,
                    DistributorName = "-- Tất cả nhà phân phối --"
                };
                distributors.Insert(0, allOption);

                cmbDistributor.DataSource = distributors;
                cmbDistributor.DisplayMember = "DistributorName";
                cmbDistributor.ValueMember = "DistributorId";
                cmbDistributor.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải nhà phân phối: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= LOAD PURCHASE ORDERS =================
        private void LoadPurchaseOrders()
        {
            try
            {
                dgvPurchaseOrders.Rows.Clear();
                dgvOrderDetails.Rows.Clear();
                pictureBoxCover.Image = null;
                _selectedPurchaseOrderId = 0;
                btnDelete.Enabled = false;

                var query = _db.PurchaseOrders
                    .Include(po => po.Distributor)
                    .Include(po => po.User)
                    .AsQueryable();

                // Apply filters
                if (!string.IsNullOrWhiteSpace(txtSearchCode.Text))
                {
                    string searchCode = txtSearchCode.Text.Trim().ToLower();
                    query = query.Where(po =>
                        po.PurchaseOrderId.ToString().Contains(searchCode));
                }

                if (cmbDistributor.SelectedValue != null)
                {
                    int distributorId = Convert.ToInt32(cmbDistributor.SelectedValue);
                    if (distributorId > 0)
                    {
                        query = query.Where(po => po.DistributorId == distributorId);
                    }
                }

                // Date range filter
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1);
                query = query.Where(po => po.OrderDate >= startDate && po.OrderDate <= endDate);

                var orders = query
                    .OrderByDescending(po => po.OrderDate)
                    .ToList();

                // Calculate statistics
                int totalOrders = orders.Count;
                int totalQuantity = 0;
                decimal totalAmount = 0;

                foreach (var order in orders)
                {
                    int rowIndex = dgvPurchaseOrders.Rows.Add(
                        order.PurchaseOrderId,
                        order.OrderDate.ToString("dd/MM/yyyy HH:mm"),
                        order.Distributor?.DistributorName ?? "N/A",
                        order.User?.FullName ?? "N/A",
                        order.TotalAmount,
                        order.Notes ?? ""
                    );

                    // Important: Set the Tag to PurchaseOrderId for later use
                    dgvPurchaseOrders.Rows[rowIndex].Tag = order.PurchaseOrderId;

                    totalAmount += order.TotalAmount;

                    // Count total quantity from details
                    var details = _db.PurchaseOrderDetails
                        .Where(d => d.PurchaseOrderId == order.PurchaseOrderId)
                        .ToList();
                    totalQuantity += details.Sum(d => d.Quantity);
                }

                // Update statistics
                lblTotalOrders.Text = $"Tổng phiếu nhập: {totalOrders}";
                lblTotalQuantity.Text = $"Tổng số lượng: {totalQuantity.ToString("N0")}";
                lblTotalAmount.Text = $"Tổng tiền: {totalAmount.ToString("N0")}đ";

                // Clear selection to prevent triggering SelectionChanged with invalid data
                dgvPurchaseOrders.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách phiếu nhập: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DebugLogger.Log("LoadPurchaseOrders error: " + ex.ToString());
            }
        }

        // ================= LOAD DETAILS =================
        private void LoadOrderDetails(int purchaseOrderId)
        {
            try
            {
                dgvOrderDetails.Rows.Clear();
                pictureBoxCover.Image = null;

                var details = _db.PurchaseOrderDetails
                    .Include(d => d.Book)
                    .Include(d => d.Book.Author)
                    .Where(d => d.PurchaseOrderId == purchaseOrderId)
                    .ToList();

                foreach (var detail in details)
                {
                    decimal amount = detail.Quantity * detail.CostPrice;

                    int rowIndex = dgvOrderDetails.Rows.Add(
                        detail.Book?.BookId ?? 0,
                        detail.Book?.Title ?? "N/A",
                        detail.Book?.Author?.Name ?? "N/A",
                        detail.Quantity,
                        detail.CostPrice,
                        amount
                    );

                    // Store book ID in Tag for later reference
                    dgvOrderDetails.Rows[rowIndex].Tag = detail.Book?.BookId ?? 0;
                }

                dgvOrderDetails.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết phiếu nhập: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DebugLogger.Log("LoadOrderDetails error: " + ex.ToString());
            }
        }

        // ================= GRID SELECTION =================
        private void dgvPurchaseOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPurchaseOrders.SelectedRows.Count > 0)
            {
                var row = dgvPurchaseOrders.SelectedRows[0];

                // Check if Tag is not null
                if (row.Tag != null)
                {
                    _selectedPurchaseOrderId = (int)row.Tag;
                    LoadOrderDetails(_selectedPurchaseOrderId);
                    btnDelete.Enabled = true;
                }
                else
                {
                    dgvOrderDetails.Rows.Clear();
                    _selectedPurchaseOrderId = 0;
                    btnDelete.Enabled = false;
                    pictureBoxCover.Image = null;
                }
            }
            else
            {
                dgvOrderDetails.Rows.Clear();
                _selectedPurchaseOrderId = 0;
                btnDelete.Enabled = false;
                pictureBoxCover.Image = null;
            }
        }

        private void dgvOrderDetails_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvOrderDetails.SelectedRows.Count > 0)
            {
                try
                {
                    var row = dgvOrderDetails.SelectedRows[0];

                    // Check if the cell value is not null
                    if (row.Cells[colBookId.Name].Value != null)
                    {
                        int bookId = Convert.ToInt32(row.Cells[colBookId.Name].Value);
                        LoadBookImage(bookId);
                    }
                    else
                    {
                        pictureBoxCover.Image = null;
                    }
                }
                catch (Exception ex)
                {
                    pictureBoxCover.Image = null;
                    DebugLogger.Log("dgvOrderDetails_SelectionChanged error: " + ex.ToString());
                }
            }
            else
            {
                pictureBoxCover.Image = null;
            }
        }

        private void LoadBookImage(int bookId)
        {
            try
            {
                var book = _db.Books.Find(bookId);
                if (book != null && !string.IsNullOrEmpty(book.CoverImagePath))
                {
                    if (System.IO.File.Exists(book.CoverImagePath))
                    {
                        // Dispose previous image to avoid file lock
                        if (pictureBoxCover.Image != null)
                        {
                            pictureBoxCover.Image.Dispose();
                            pictureBoxCover.Image = null;
                        }

                        // Load image from file stream to avoid locking the file
                        using (var stream = new System.IO.FileStream(book.CoverImagePath,
                            System.IO.FileMode.Open,
                            System.IO.FileAccess.Read))
                        {
                            pictureBoxCover.Image = System.Drawing.Image.FromStream(stream);
                        }
                    }
                    else
                    {
                        pictureBoxCover.Image = null;
                    }
                }
                else
                {
                    pictureBoxCover.Image = null;
                }
            }
            catch (Exception ex)
            {
                pictureBoxCover.Image = null;
                DebugLogger.Log($"LoadBookImage error (BookId: {bookId}): {ex.Message}");
            }
        }

        // ================= SEARCH =================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadPurchaseOrders();
        }

        private void txtSearchCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                LoadPurchaseOrders();
                e.Handled = true;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearchCode.Clear();

            // Only set SelectedIndex if ComboBox has items
            if (cmbDistributor.Items.Count > 0)
            {
                cmbDistributor.SelectedIndex = 0;
            }
            else
            {
                // Reload distributors if empty
                LoadDistributors();
            }

            InitDateRange();
            LoadPurchaseOrders();
        }

        // ================= CREATE NEW =================
        private void btnCreateNew_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new FormPurchaseOrder();
                form.ShowDialog();

                // Reload after creating new order
                LoadPurchaseOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form tạo phiếu nhập: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ================= DELETE =================
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedPurchaseOrderId == 0)
            {
                MessageBox.Show("Vui lòng chọn phiếu nhập cần xóa", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show(
                "Bạn có chắc muốn xóa phiếu nhập này?\n\nLưu ý: Thao tác này sẽ xóa cả chi tiết phiếu nhập và có thể ảnh hưởng đến tồn kho.",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var order = _db.PurchaseOrders
                        .Include(po => po.PurchaseOrderDetails)
                        .FirstOrDefault(po => po.PurchaseOrderId == _selectedPurchaseOrderId);

                    if (order == null)
                    {
                        MessageBox.Show("Không tìm thấy phiếu nhập", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Update inventory (subtract the quantities)
                    foreach (var detail in order.PurchaseOrderDetails)
                    {
                        var inventory = _db.Inventories.Find(detail.BookId);
                        if (inventory != null)
                        {
                            inventory.Quantity -= detail.Quantity;
                            inventory.LastUpdated = DateTime.Now;

                            // Don't allow negative inventory
                            if (inventory.Quantity < 0)
                            {
                                MessageBox.Show(
                                    $"Không thể xóa phiếu nhập vì sẽ làm tồn kho âm cho sách ID: {detail.BookId}",
                                    "Cảnh báo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                                return;
                            }
                        }
                    }

                    // Delete details first (due to foreign key)
                    _db.PurchaseOrderDetails.RemoveRange(order.PurchaseOrderDetails);

                    // Delete order
                    _db.PurchaseOrders.Remove(order);

                    _db.SaveChanges();

                    MessageBox.Show("Xóa phiếu nhập thành công", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    DebugLogger.Log($"Purchase Order deleted: ID={_selectedPurchaseOrderId}");

                    // Reload
                    LoadPurchaseOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa phiếu nhập: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DebugLogger.Log("Delete Purchase Order error: " + ex.ToString());
                }
            }
        }

        // ================= EXPORT =================
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvPurchaseOrders.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                    sfd.FileName = $"DanhSachNhapHang_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                    sfd.Title = "Xuất danh sách phiếu nhập";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExportToCSV(sfd.FileName);

                        var result = MessageBox.Show(
                            "Xuất file thành công!\n\nBạn có muốn mở file?",
                            "Thành công",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Information);

                        if (result == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportToCSV(string filePath)
        {
            try
            {
                using (var writer = new System.IO.StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    // Write header
                    writer.WriteLine("Mã Phiếu,Ngày Nhập,Nhà Phân Phối,Nhân Viên,Tổng Tiền,Ghi Chú");

                    // Write data
                    foreach (DataGridViewRow row in dgvPurchaseOrders.Rows)
                    {
                        string line = string.Format("{0},{1},{2},{3},{4},{5}",
                            row.Cells[colPurchaseOrderId.Name].Value,
                            row.Cells[colOrderDate.Name].Value,
                            row.Cells[colDistributor.Name].Value,
                            row.Cells[colStaff.Name].Value,
                            row.Cells[colTotalAmount.Name].Value,
                            row.Cells[colNotes.Name].Value?.ToString().Replace(",", ";") ?? ""
                        );
                        writer.WriteLine(line);
                    }

                    // Write summary
                    writer.WriteLine();
                    writer.WriteLine($"Tổng số phiếu,{lblTotalOrders.Text}");
                    writer.WriteLine($"Tổng số lượng,{lblTotalQuantity.Text}");
                    writer.WriteLine($"Tổng tiền,{lblTotalAmount.Text}");
                }

                DebugLogger.Log($"Exported purchase orders to: {filePath}");
            }
            catch (Exception ex)
            {
                DebugLogger.Log("Export CSV error: " + ex.ToString());
                throw;
            }
        }
    }
}