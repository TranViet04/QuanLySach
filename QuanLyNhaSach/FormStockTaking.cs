using QuanLyNhaSach.Data;
using QuanLyNhaSach.Models;
using System;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace QuanLyNhaSach
{
    public partial class FormStockTaking : Form
    {
        private readonly BookStoreContext _db = new BookStoreContext();
        private int _selectedBookId = 0;

        public FormStockTaking()
        {
            InitializeComponent();
        }

        // ================= FORM LOAD =================
        private void FormStockTaking_Load(object sender, EventArgs e)
        {
            InitForm();
            LoadInventory();
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
        private void InitForm()
        {
            dgvInventory.AutoGenerateColumns = false;
            dtpCheckingDate.Value = DateTime.Now;
            lblStaff.Text = CurrentUser.FullName ?? "N/A";

            // Setup autocomplete for search
            LoadBookAutoComplete();

            // Disable update button initially
            btnUpdate.Enabled = false;
            btnSaveStockTaking.Enabled = false;
        }

        private void LoadBookAutoComplete()
        {
            try
            {
                var bookTitles = _db.Books.Select(b => b.Title).ToList();

                AutoCompleteStringCollection source = new AutoCompleteStringCollection();
                source.AddRange(bookTitles.ToArray());

                txtSearchBook.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtSearchBook.AutoCompleteSource = AutoCompleteSource.CustomSource;
                txtSearchBook.AutoCompleteCustomSource = source;
            }
            catch (Exception ex)
            {
                DebugLogger.Log("LoadBookAutoComplete error: " + ex.ToString());
            }
        }

        // ================= LOAD INVENTORY (WITH DEBUG) =================
        private void LoadInventory(string searchKeyword = null)
        {
            try
            {
                dgvInventory.Rows.Clear();
                ClearSelection();

                // DEBUG: Log số sách và số inventory records
                int totalBooksInDB = _db.Books.Count();
                int totalInventoryInDB = _db.Inventories.Count();
                DebugLogger.Log($"LoadInventory: Books={totalBooksInDB}, Inventory records={totalInventoryInDB}");

                var query = _db.Books
                    .Include(b => b.Author)
                    .Include(b => b.Category)
                    .AsQueryable();

                // Apply search filter
                if (!string.IsNullOrWhiteSpace(searchKeyword))
                {
                    query = query.Where(b =>
                        b.Title.Contains(searchKeyword) ||
                        b.Author.Name.Contains(searchKeyword) ||
                        b.Category.Name.Contains(searchKeyword) ||
                        b.BookId.ToString().Contains(searchKeyword));
                }

                var books = query.OrderBy(b => b.Title).ToList();

                int totalBooks = books.Count;
                int checkedBooks = 0;
                int totalSystemQuantity = 0;
                int totalActualQuantity = 0;
                int totalDifference = 0;
                int booksWithZeroInventory = 0;

                foreach (var book in books)
                {
                    // Get system quantity from Inventory table
                    var inventory = _db.Inventories.FirstOrDefault(i => i.BookId == book.BookId);
                    int systemQuantity = inventory?.Quantity ?? 0;

                    // DEBUG: Log nếu không tìm thấy inventory hoặc quantity = 0
                    if (inventory == null)
                    {
                        DebugLogger.Log($"⚠️ BookId={book.BookId} ({book.Title}): No inventory record found");
                        booksWithZeroInventory++;
                    }
                    else if (systemQuantity == 0)
                    {
                        DebugLogger.Log($"⚠️ BookId={book.BookId} ({book.Title}): Inventory exists but Quantity=0");
                        booksWithZeroInventory++;
                    }

                    totalSystemQuantity += systemQuantity;

                    // Get actual quantity from StockTaking table (if checked today)
                    var todayStart = DateTime.Today;
                    var todayEnd = todayStart.AddDays(1).AddSeconds(-1);

                    var stockTaking = _db.StockTakings
                        .Where(st => st.BookId == book.BookId &&
                               st.CheckingDate >= todayStart &&
                               st.CheckingDate <= todayEnd)
                        .OrderByDescending(st => st.CheckingDate)
                        .FirstOrDefault();

                    int actualQuantity = stockTaking?.ActualQuantity ?? 0;
                    int difference = stockTaking?.Difference ?? 0;

                    string status;
                    if (stockTaking == null)
                    {
                        status = "Chưa kiểm";
                    }
                    else if (difference == 0)
                    {
                        status = "Khớp";
                    }
                    else if (difference > 0)
                    {
                        status = "Thừa";
                    }
                    else
                    {
                        status = "Thiếu";
                    }

                    int rowIndex = dgvInventory.Rows.Add(
                        book.BookId,
                        book.Title,
                        book.Author?.Name ?? "N/A",
                        book.Category?.Name ?? "N/A",
                        systemQuantity,
                        stockTaking != null ? actualQuantity.ToString() : "",
                        stockTaking != null ? difference.ToString() : "",
                        status
                    );

                    dgvInventory.Rows[rowIndex].Tag = book.BookId;

                    // Color coding based on status
                    if (stockTaking != null)
                    {
                        if (difference < 0)
                        {
                            dgvInventory.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightCoral;
                        }
                        else if (difference > 0)
                        {
                            dgvInventory.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightGreen;
                        }
                        else
                        {
                            dgvInventory.Rows[rowIndex].DefaultCellStyle.BackColor = Color.LightBlue;
                        }

                        checkedBooks++;
                        totalActualQuantity += actualQuantity;
                        totalDifference += Math.Abs(difference);
                    }
                }

                // DEBUG: Log summary
                DebugLogger.Log($"LoadInventory Summary: Total={totalBooks}, SystemQty={totalSystemQuantity}, ZeroQty={booksWithZeroInventory}, Checked={checkedBooks}");

                // Update statistics
                lblTotalBooks.Text = $"Tổng sách: {totalBooks} | Đã kiểm: {checkedBooks} | SL hệ thống: {totalSystemQuantity}";
                lblTotalDifference.Text = $"Tổng chênh lệch: {totalDifference}";

                // Hiển thị cảnh báo nếu có nhiều sách không có tồn kho
                if (booksWithZeroInventory > totalBooks * 0.5) // Nếu > 50% sách có qty = 0
                {
                    MessageBox.Show(
                        $"Cảnh báo: Có {booksWithZeroInventory}/{totalBooks} sách chưa có tồn kho!\n\n" +
                        "Vui lòng kiểm tra:\n" +
                        "1. Đã nhập hàng chưa?\n" +
                        "2. Dữ liệu bảng Inventories có đúng không?",
                        "Cảnh báo",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }

                dgvInventory.ClearSelection();

                // Enable save button if there are checked items
                btnSaveStockTaking.Enabled = checkedBooks > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách tồn kho: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DebugLogger.Log("LoadInventory error: " + ex.ToString());
            }
        }

        // ================= SEARCH =================
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadInventory(txtSearchBook.Text.Trim());
        }

        private void txtSearchBook_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                LoadInventory(txtSearchBook.Text.Trim());
                e.Handled = true;
            }
        }

        // ================= GRID SELECTION =================
        private void dgvInventory_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                try
                {
                    var row = dgvInventory.SelectedRows[0];

                    if (row.Tag != null)
                    {
                        _selectedBookId = (int)row.Tag;

                        txtBookId.Text = row.Cells[colBookId.Name].Value?.ToString() ?? "";
                        txtBookName.Text = row.Cells[colBookName.Name].Value?.ToString() ?? "";
                        txtSystemQuantity.Text = row.Cells[colSystemQuantity.Name].Value?.ToString() ?? "0";

                        // Load actual quantity if already checked
                        string actualStr = row.Cells[colActualQuantity.Name].Value?.ToString() ?? "";
                        if (!string.IsNullOrWhiteSpace(actualStr))
                        {
                            numActualQuantity.Value = int.Parse(actualStr);
                        }
                        else
                        {
                            numActualQuantity.Value = int.Parse(txtSystemQuantity.Text);
                        }

                        CalculateDifference();
                        LoadBookImage(_selectedBookId);

                        btnUpdate.Enabled = true;
                    }
                    else
                    {
                        ClearSelection();
                    }
                }
                catch (Exception ex)
                {
                    DebugLogger.Log("dgvInventory_SelectionChanged error: " + ex.ToString());
                    ClearSelection();
                }
            }
            else
            {
                ClearSelection();
            }
        }

        private void ClearSelection()
        {
            _selectedBookId = 0;
            txtBookId.Clear();
            txtBookName.Clear();
            txtSystemQuantity.Text = "0";
            numActualQuantity.Value = 0;
            txtDifference.Clear();
            pictureBoxCover.Image = null;
            btnUpdate.Enabled = false;
        }

        // ================= CALCULATE DIFFERENCE =================
        private void numActualQuantity_ValueChanged(object sender, EventArgs e)
        {
            CalculateDifference();
        }

        private void CalculateDifference()
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtSystemQuantity.Text))
                {
                    int systemQty = int.Parse(txtSystemQuantity.Text);
                    int actualQty = (int)numActualQuantity.Value;
                    int difference = actualQty - systemQty;

                    txtDifference.Text = difference.ToString();

                    // Color coding
                    if (difference < 0)
                    {
                        txtDifference.ForeColor = Color.Red;
                    }
                    else if (difference > 0)
                    {
                        txtDifference.ForeColor = Color.Green;
                    }
                    else
                    {
                        txtDifference.ForeColor = Color.Blue;
                    }
                }
            }
            catch (Exception ex)
            {
                DebugLogger.Log("CalculateDifference error: " + ex.ToString());
            }
        }

        // ================= UPDATE =================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (_selectedBookId == 0)
            {
                MessageBox.Show("Vui lòng chọn sách cần kiểm", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                int systemQty = int.Parse(txtSystemQuantity.Text);
                int actualQty = (int)numActualQuantity.Value;
                int difference = actualQty - systemQty;

                // Check if already checked today
                var todayStart = DateTime.Today;
                var todayEnd = todayStart.AddDays(1).AddSeconds(-1);

                var existingCheck = _db.StockTakings
                    .FirstOrDefault(st => st.BookId == _selectedBookId &&
                                   st.CheckingDate >= todayStart &&
                                   st.CheckingDate <= todayEnd);

                if (existingCheck != null)
                {
                    // Update existing record
                    existingCheck.ActualQuantity = actualQty;
                    existingCheck.Difference = difference;
                    existingCheck.CheckingDate = dtpCheckingDate.Value;
                    existingCheck.UserId = CurrentUser.UserId;
                }
                else
                {
                    // Create new record
                    var stockTaking = new StockTaking
                    {
                        BookId = _selectedBookId,
                        ActualQuantity = actualQty,
                        Difference = difference,
                        CheckingDate = dtpCheckingDate.Value,
                        UserId = CurrentUser.UserId
                    };
                    _db.StockTakings.Add(stockTaking);
                }

                _db.SaveChanges();

                MessageBox.Show("Cập nhật số lượng thực tế thành công", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DebugLogger.Log($"Stock taking updated: BookId={_selectedBookId}, Actual={actualQty}, Diff={difference}");

                // Reload to show updated data
                LoadInventory(txtSearchBook.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DebugLogger.Log("btnUpdate_Click error: " + ex.ToString());
            }
        }

        // ================= SAVE STOCK TAKING =================
        private void btnSaveStockTaking_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có chắc muốn xác nhận kết quả kiểm kho?\n\n" +
                "Lưu ý: Hệ thống sẽ cập nhật số lượng tồn kho theo số liệu thực tế đã kiểm.",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var todayStart = DateTime.Today;
                    var todayEnd = todayStart.AddDays(1).AddSeconds(-1);

                    var todayChecks = _db.StockTakings
                        .Where(st => st.CheckingDate >= todayStart && st.CheckingDate <= todayEnd)
                        .ToList();

                    if (todayChecks.Count == 0)
                    {
                        MessageBox.Show("Chưa có sách nào được kiểm trong ngày hôm nay", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                    int updatedCount = 0;
                    foreach (var check in todayChecks)
                    {
                        var inventory = _db.Inventories.Find(check.BookId);
                        if (inventory != null)
                        {
                            inventory.Quantity = check.ActualQuantity;
                            inventory.LastUpdated = DateTime.Now;
                            updatedCount++;
                        }
                        else
                        {
                            // Create inventory record if not exists
                            _db.Inventories.Add(new Inventory
                            {
                                BookId = check.BookId,
                                Quantity = check.ActualQuantity,
                                LastUpdated = DateTime.Now
                            });
                            updatedCount++;
                        }
                    }

                    _db.SaveChanges();

                    MessageBox.Show(
                        $"Hoàn thành kiểm kho!\n\n" +
                        $"Đã cập nhật tồn kho cho {updatedCount} sách.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    DebugLogger.Log($"Stock taking completed: {updatedCount} books updated");

                    // Reload
                    LoadInventory(txtSearchBook.Text.Trim());
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu kiểm kho: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DebugLogger.Log("btnSaveStockTaking_Click error: " + ex.ToString());
                }
            }
        }

        // ================= REFRESH =================
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearchBook.Clear();
            LoadInventory();
        }

        // ================= EXPORT =================
        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvInventory.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu để xuất", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
                    sfd.FileName = $"BaoCaoKiemKho_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
                    sfd.Title = "Xuất báo cáo kiểm kho";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        ExportToCSV(sfd.FileName);

                        var result = MessageBox.Show(
                            "Xuất báo cáo thành công!\n\nBạn có muốn mở file?",
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
                MessageBox.Show("Lỗi khi xuất báo cáo: " + ex.Message, "Lỗi",
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
                    writer.WriteLine("BÁO CÁO KIỂM KÊ TỒN KHO");
                    writer.WriteLine($"Ngày kiểm tra: {dtpCheckingDate.Value:dd/MM/yyyy}");
                    writer.WriteLine($"Người kiểm tra: {lblStaff.Text}");
                    writer.WriteLine();
                    writer.WriteLine("Mã Sách,Tên Sách,Tác Giả,Thể Loại,SL Hệ Thống,SL Thực Tế,Chênh Lệch,Trạng Thái");

                    // Write data
                    foreach (DataGridViewRow row in dgvInventory.Rows)
                    {
                        string line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                            row.Cells[colBookId.Name].Value,
                            row.Cells[colBookName.Name].Value?.ToString().Replace(",", ";"),
                            row.Cells[colAuthor.Name].Value?.ToString().Replace(",", ";"),
                            row.Cells[colCategory.Name].Value?.ToString().Replace(",", ";"),
                            row.Cells[colSystemQuantity.Name].Value,
                            row.Cells[colActualQuantity.Name].Value,
                            row.Cells[colDifference.Name].Value,
                            row.Cells[colStatus.Name].Value
                        );
                        writer.WriteLine(line);
                    }

                    // Write summary
                    writer.WriteLine();
                    writer.WriteLine($"{lblTotalBooks.Text}");
                    writer.WriteLine($"{lblTotalDifference.Text}");
                }

                DebugLogger.Log($"Exported stock taking report to: {filePath}");
            }
            catch (Exception ex)
            {
                DebugLogger.Log("Export CSV error: " + ex.ToString());
                throw;
            }
        }

        // ================= LOAD IMAGE =================
        private void LoadBookImage(int bookId)
        {
            try
            {
                var book = _db.Books.Find(bookId);
                if (book != null && !string.IsNullOrEmpty(book.CoverImagePath))
                {
                    if (System.IO.File.Exists(book.CoverImagePath))
                    {
                        // Dispose previous image
                        if (pictureBoxCover.Image != null)
                        {
                            pictureBoxCover.Image.Dispose();
                            pictureBoxCover.Image = null;
                        }

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
    }
}