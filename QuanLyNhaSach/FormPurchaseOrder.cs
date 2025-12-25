using QuanLyNhaSach.Data;
using QuanLyNhaSach.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNhaSach
{
    public partial class FormPurchaseOrder : Form
    {
        private readonly BookStoreContext _db = new BookStoreContext();
        private PurchaseOrder _purchaseOrder;
        private Book _selectedBook;
        private CultureInfo vnCulture = new CultureInfo("vi-VN");

        public FormPurchaseOrder()
        {
            InitializeComponent();
        }

        // ================= FORM LOAD =================
        private void FormPurchaseOrder_Load(object sender, EventArgs e)
        {
            InitPurchaseOrder();
            LoadStaff();
            LoadDistributors();
            LoadBookAutoComplete();
            InitGrid();
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
        private void InitPurchaseOrder()
        {
            _purchaseOrder = new PurchaseOrder
            {
                OrderDate = DateTime.Now,
                UserId = CurrentUser.UserId
            };

            txtOrderCode.Text = GenerateOrderCode();
            txtOrderCode.ReadOnly = true;
            dtpOrderDate.Value = _purchaseOrder.OrderDate;
            txtTotal.Text = "0";
        }

        private string GenerateOrderCode()
        {
            return "PN" + DateTime.Now.ToString("yyyyMMddHHmmss");
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

        // ================= LOAD DISTRIBUTORS =================
        private void LoadDistributors()
        {
            var distributors = _db.Distributors.ToList();

            cmbDistributor.DataSource = null;
            cmbDistributor.DisplayMember = "DistributorName";
            cmbDistributor.ValueMember = "DistributorId";
            cmbDistributor.DataSource = distributors;

            if (distributors.Count > 0)
                cmbDistributor.SelectedIndex = 0;
            else
                cmbDistributor.SelectedIndex = -1;
        }

        // ================= AUTOCOMPLETE =================
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
            dgvPurchaseOrder.AutoGenerateColumns = false;
            dgvPurchaseOrder.Rows.Clear();

            colCostPrice.DefaultCellStyle.Format = "N0";
            colAmount.DefaultCellStyle.Format = "N0";

            colCostPrice.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
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

            // Hiển thị giá bán hiện tại (có thể dùng làm tham khảo)
            // Giá nhập thường sẽ thấp hơn giá bán
            txtCostPrice.Text = (_selectedBook.Price * 0.7m).ToString("N0");
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
            txtCostPrice.Clear();
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

            // Validate cost price
            if (!decimal.TryParse(txtCostPrice.Text.Replace(",", ""),
                System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowDecimalPoint,
                vnCulture, out decimal costPrice))
            {
                MessageBox.Show("Giá nhập không hợp lệ", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (costPrice <= 0)
            {
                MessageBox.Show("Giá nhập phải lớn hơn 0", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal amount = qty * costPrice;

            bool isExisted = false;

            // Kiểm tra sách đã có trong grid chưa
            foreach (DataGridViewRow row in dgvPurchaseOrder.Rows)
            {
                if ((int)row.Cells[colBookId.Name].Value == _selectedBook.BookId)
                {
                    // Cộng dồn số lượng
                    int currentQty = (int)row.Cells[colQuantity.Name].Value;
                    int newQty = currentQty + qty;

                    row.Cells[colQuantity.Name].Value = newQty;
                    row.Cells[colAmount.Name].Value = newQty * costPrice;
                    isExisted = true;
                    break;
                }
            }

            // Thêm dòng mới nếu chưa tồn tại
            if (!isExisted)
            {
                dgvPurchaseOrder.Rows.Add(
                    _selectedBook.BookId,
                    _selectedBook.Title,
                    _selectedBook.Author?.Name ?? "N/A",
                    qty,
                    costPrice,
                    amount,
                    false
                );
            }

            UpdateTotal();
            ClearBookSelection();
        }

        // ================= GRID CLICK =================
        private void dgvPurchaseOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Nếu click cột Xóa
            if (dgvPurchaseOrder.Columns[e.ColumnIndex].Name == "colDelete")
            {
                var result = MessageBox.Show("Bạn có chắc muốn xóa sách này?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    dgvPurchaseOrder.Rows.RemoveAt(e.RowIndex);
                    UpdateTotal();
                }
                return;
            }

            // Click vào các cột khác để xem/sửa thông tin
            int bookId = (int)dgvPurchaseOrder.Rows[e.RowIndex].Cells[colBookId.Name].Value;

            _selectedBook = _db.Books
                .Include(b => b.Author)
                .FirstOrDefault(b => b.BookId == bookId);

            if (_selectedBook == null) return;

            txtSearchBook.Text = _selectedBook.Title;
            txtCostPrice.Text = dgvPurchaseOrder.Rows[e.RowIndex].Cells[colCostPrice.Name].Value.ToString();
            numQuantity.Value = Convert.ToInt32(
                dgvPurchaseOrder.Rows[e.RowIndex].Cells[colQuantity.Name].Value);

            LoadBookImage(_selectedBook.CoverImagePath);
        }

        // ================= TOTAL CALCULATION =================
        private void UpdateTotal()
        {
            decimal total = 0;

            foreach (DataGridViewRow row in dgvPurchaseOrder.Rows)
            {
                total += Convert.ToDecimal(row.Cells[colAmount.Name].Value);
            }

            txtTotal.Text = total.ToString("N0");
        }

        // ================= SAVE ================= (PHẦN CẦN SỬA)
        private void btnSaveOrder_Click(object sender, EventArgs e)
        {
            // Validation
            if (dgvPurchaseOrder.Rows.Count == 0)
            {
                MessageBox.Show("Phiếu nhập chưa có sách", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbDistributor.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhà phân phối", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbStaff.SelectedValue == null)
            {
                MessageBox.Show("Chưa có nhân viên, vui lòng đăng nhập", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Tính tổng tiền
                decimal total = 0;
                foreach (DataGridViewRow row in dgvPurchaseOrder.Rows)
                {
                    total += Convert.ToDecimal(row.Cells[colAmount.Name].Value);
                }

                // Tạo phiếu nhập
                _purchaseOrder.DistributorId = (int)cmbDistributor.SelectedValue;
                _purchaseOrder.TotalAmount = total;
                _purchaseOrder.Notes = txtNotes.Text?.Trim();
                _purchaseOrder.OrderDate = dtpOrderDate.Value;

                _db.PurchaseOrders.Add(_purchaseOrder);

                // Lưu chi tiết phiếu nhập VÀ cập nhật tồn kho
                foreach (DataGridViewRow row in dgvPurchaseOrder.Rows)
                {
                    int bookId = (int)row.Cells[colBookId.Name].Value;
                    int quantity = (int)row.Cells[colQuantity.Name].Value;
                    decimal costPrice = (decimal)row.Cells[colCostPrice.Name].Value;

                    // Thêm chi tiết phiếu nhập
                    _db.PurchaseOrderDetails.Add(new PurchaseOrderDetail
                    {
                        PurchaseOrderId = _purchaseOrder.PurchaseOrderId,
                        BookId = bookId,
                        Quantity = quantity,
                        CostPrice = costPrice
                    });

                    // ⭐ CẬP NHẬT TỒN KHO - PHẦN QUAN TRỌNG ⭐
                    var inventory = _db.Inventories.Find(bookId);
                    if (inventory != null)
                    {
                        // Nếu đã có record, cộng thêm số lượng
                        inventory.Quantity += quantity;
                        inventory.LastUpdated = DateTime.Now;

                        DebugLogger.Log($"Updated inventory: BookId={bookId}, OldQty={inventory.Quantity - quantity}, NewQty={inventory.Quantity}");
                    }
                    else
                    {
                        // Nếu chưa có record, tạo mới
                        var newInventory = new Inventory
                        {
                            BookId = bookId,
                            Quantity = quantity,
                            LastUpdated = DateTime.Now
                        };
                        _db.Inventories.Add(newInventory);

                        DebugLogger.Log($"Created new inventory: BookId={bookId}, Qty={quantity}");
                    }
                }

                // LƯU TẤT CẢ THAY ĐỔI
                _db.SaveChanges();

                // Hiển thị thông báo thành công
                string message = "Lưu phiếu nhập thành công!\n\n";
                message += $"Mã phiếu: {txtOrderCode.Text}\n";
                message += $"Tổng tiền: {total.ToString("N0")} VNĐ\n";
                message += $"Nhà phân phối: {cmbDistributor.Text}\n";
                message += $"Đã cập nhật tồn kho cho {dgvPurchaseOrder.Rows.Count} sách";

                MessageBox.Show(message, "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                DebugLogger.Log($"Purchase Order created: {txtOrderCode.Text}, Total: {total}, Books: {dgvPurchaseOrder.Rows.Count}");

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
                DebugLogger.Log("Purchase Order validation error: " + errorMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu phiếu nhập:\n{ex.Message}\n\n{ex.InnerException?.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                DebugLogger.Log("Purchase Order save error: " + ex.ToString());
            }
        }

        // ================= CANCEL =================
        private void btnCancel_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc muốn hủy phiếu nhập này?", "Xác nhận",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}