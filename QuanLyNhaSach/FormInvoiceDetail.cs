using QuanLyNhaSach.Data;
using QuanLyNhaSach.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyNhaSach
{
    public partial class FormInvoiceDetail : Form
    {
        private readonly int _invoiceId;
        private readonly BookStoreContext _db = new BookStoreContext();

        // Constructor nhận invoiceId từ form gọi
        public FormInvoiceDetail(int invoiceId)
        {
            InitializeComponent();
            _invoiceId = invoiceId;
        }

        private void FormInvoiceDetail_Load(object sender, EventArgs e)
        {
            LoadInvoiceDetails();
        }

        private void LoadInvoiceDetails()
        {
            try
            {
                // Lấy hóa đơn cùng với thông tin liên quan
                var invoice = _db.Invoices
                    .Include(i => i.Customer)
                    .Include(i => i.User)
                    .Include(i => i.InvoiceDetails.Select(d => d.Book.Author))
                    .FirstOrDefault(i => i.InvoiceId == _invoiceId);

                if (invoice == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                // Hiển thị thông tin hóa đơn
                txtInvoiceCode.Text = invoice.InvoiceCode;
                txtCreatedDate.Text = invoice.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                txtStaff.Text = invoice.User?.FullName ?? "";
                txtCustomer.Text = invoice.Customer?.Name ?? "";
                txtCustomerPhone.Text = invoice.Customer?.Phone ?? "";
                txtCustomerEmail.Text = invoice.Customer?.Email ?? "";
                txtCustomerAddress.Text = invoice.Customer?.Bio ?? "";
                txtStatus.Text = invoice.Status == 1 ? "Đã thanh toán" : "Chưa thanh toán";
                txtNote.Text = invoice.Note ?? "";

                // Load chi tiết sách vào DataGridView
                dgvDetails.Rows.Clear();
                decimal total = 0;

                foreach (var detail in invoice.InvoiceDetails)
                {
                    decimal amount = detail.Quantity * detail.Price;
                    total += amount;

                    dgvDetails.Rows.Add(
                        detail.Book.BookId,
                        detail.Book.Title,
                        detail.Book.Author?.Name ?? "",
                        detail.Quantity,
                        detail.Price.ToString("N0"),
                        amount.ToString("N0")
                    );
                }

                txtTotal.Text = total.ToString("N0") + " VNĐ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết hóa đơn: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            var form = new FormReport("Invoice", _invoiceId);
            form.ShowDialog();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}