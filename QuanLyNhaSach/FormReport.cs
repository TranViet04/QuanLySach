using Microsoft.Reporting.WinForms;
using QuanLyNhaSach.Data;
using QuanLyNhaSach.Reports;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace QuanLyNhaSach
{
    public partial class FormReport : Form
    {
        private readonly string _reportType;
        private readonly object _parameter;


        /// <summary>
        /// Constructor đa năng cho nhiều loại báo cáo
        /// </summary>
        /// <param name="reportType">Loại báo cáo: "Invoice", "Inventory", "Revenue", "Category"</param>
        /// <param name="parameter">Tham số (invoiceId, fromDate/toDate, null...)</param>
        public FormReport(string reportType, object parameter = null)
        {
            InitializeComponent();
            _reportType = reportType;
            _parameter = parameter;

        }

        private void FormReport_Load(object sender, EventArgs e)
        {
            try
            {
                switch (_reportType.ToLower())
                {
                    case "invoice":
                        LoadInvoiceReport();
                        break;
                    case "category":
                        LoadCategoryReport();
                        break;
                    case "inventory":
                        LoadInventoryReport();
                        break;
                    case "revenue":
                        LoadRevenueReport();
                        break;
                    case "booklist":
                        LoadBookListReport();
                        break;
                    default:
                        MessageBox.Show($"Loại báo cáo không hợp lệ: [{_reportType}]", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải báo cáo: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== BÁO CÁO HÓA ĐƠN ==========
        private void LoadInvoiceReport()
        {
            int invoiceId = (int)_parameter;

            using (var db = new BookStoreContext())
            {
                // Lấy thông tin hóa đơn (header) - Dùng cho Parameters
                var invoice = db.Invoices
                    .Where(i => i.InvoiceId == invoiceId)
                    .Select(i => new
                    {
                        i.InvoiceCode,
                        i.CreatedDate,
                        CustomerName = i.Customer.Name,
                        Phone = i.Customer.Phone ?? "",
                        Email = i.Customer.Email ?? "",
                        Address = i.Customer.Bio ?? "",  // Customer.Bio = Địa chỉ
                        i.TotalAmount,
                        Status = i.Status == 1 ? "Đã thanh toán" : "Chưa thanh toán",
                        StaffName = i.User.FullName
                    })
                    .FirstOrDefault();

                if (invoice == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                // Lấy chi tiết và map sang InvoiceReportModel
                var details = db.InvoiceDetails
                    .Where(d => d.InvoiceId == invoiceId)
                    .Select(d => new InvoiceReportModel
                    {
                        // Thông tin hóa đơn
                        InvoiceId = d.InvoiceId,
                        InvoiceCode = d.Invoice.InvoiceCode,
                        CreatedDate = d.Invoice.CreatedDate,
                        TotalAmount = d.Invoice.TotalAmount,
                        Status = d.Invoice.Status == 1 ? "Đã thanh toán" : "Chưa thanh toán",
                        Note = d.Invoice.Note ?? "",

                        // Thông tin khách hàng
                        CustomerId = d.Invoice.CustomerId,
                        CustomerName = d.Invoice.Customer.Name,
                        Phone = d.Invoice.Customer.Phone ?? "",
                        Email = d.Invoice.Customer.Email ?? "",
                        CustomerAddress = d.Invoice.Customer.Bio ?? "",  // Bio = Địa chỉ

                        // Thông tin nhân viên
                        UserId = d.Invoice.UserId,
                        StaffName = d.Invoice.User.FullName,

                        // Chi tiết sách
                        InvoiceDetailId = d.InvoiceDetailId,
                        BookId = d.BookId,
                        Title = d.Book.Title,
                        AuthorName = d.Book.Author.Name,
                        Quantity = d.Quantity,
                        Price = d.Price,
                        Amount = d.Quantity * d.Price
                    })
                    .ToList();

                // Load RDLC
                string rdlcPath = GetEmbeddedReportPath("ReportInvoice.rdlc");
                reportViewer1.LocalReport.ReportPath = rdlcPath;

                // Set Parameters (cho Header)
                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("MaHoaDon", invoice.InvoiceCode),
                    new ReportParameter("NgayLap", invoice.CreatedDate.ToString("dd/MM/yyyy HH:mm")),
                    new ReportParameter("KhachHang", invoice.CustomerName),
                    new ReportParameter("DienThoai", invoice.Phone),
                    new ReportParameter("DiaChi", invoice.Address),
                    new ReportParameter("TongTien", invoice.TotalAmount.ToString("N0") + " đ"),
                    new ReportParameter("TrangThai", invoice.Status),
                    new ReportParameter("NhanVien", invoice.StaffName)
                };
                reportViewer1.LocalReport.SetParameters(parameters);

                // Set DataSource (cho Table)
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("DataSetInvoice", details));

                reportViewer1.RefreshReport();
            }
        }

        // ========== BÁO CÁO DANH SÁCH SÁCH ==========
        private void LoadBookListReport()
        {
            try
            {
                using (var db = new BookStoreContext())
                {
                    // Lấy danh sách sách và MAP SANG BookListReportModel
                    var books = db.Books
                        .Select(b => new BookListReportModel  // ✅ Dùng class có sẵn
                        {
                            BookId = b.BookId,
                            Title = b.Title,
                            AuthorName = b.Author.Name,
                            CategoryName = b.Category.Name,
                            PublisherName = b.Publisher.Name,
                            PublishYear = b.PublishYear,
                            Price = b.Price,
                            Stock = db.Inventories
                                .Where(i => i.BookId == b.BookId)
                                .Select(i => (int?)i.Quantity)
                                .FirstOrDefault() ?? 0
                        })
                        .ToList()
                        .Select(b => new BookListReportModel  // ✅ Map lại để tính Status
                        {
                            BookId = b.BookId,
                            Title = b.Title,
                            AuthorName = b.AuthorName,
                            CategoryName = b.CategoryName,
                            PublisherName = b.PublisherName,
                            PublishYear = b.PublishYear,
                            Price = b.Price,
                            Stock = b.Stock,
                            Status = b.Stock == 0 ? "Hết hàng" :
                                     b.Stock < 10 ? "Sắp hết" : "Còn hàng"
                        })
                        .OrderBy(b => b.CategoryName)
                        .ThenBy(b => b.Title)
                        .ToList();

                    // Tính tổng
                    int tongSoSach = books.Count;
                    int tongTonKho = books.Sum(b => b.Stock);
                    decimal tongGiaTri = books.Sum(b => b.Price * b.Stock);

                    // Load RDLC
                    string rdlcPath = GetEmbeddedReportPath("ReportBookList.rdlc");

                    if (!File.Exists(rdlcPath))
                    {
                        MessageBox.Show($"Không tìm thấy file RDLC!\nĐường dẫn: {Path.GetFullPath(rdlcPath)}",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    reportViewer1.LocalReport.ReportPath = rdlcPath;

                    // Set Parameters
                    ReportParameter[] parameters = new ReportParameter[]
                    {
                        new ReportParameter("NgayIn", DateTime.Now.ToString("dd/MM/yyyy HH:mm")),
                        new ReportParameter("TongSoSach", tongSoSach.ToString()),
                        new ReportParameter("TongTonKho", tongTonKho.ToString()),
                        new ReportParameter("TongGiaTri", tongGiaTri.ToString("N0") + " đ")
                    };
                    reportViewer1.LocalReport.SetParameters(parameters);

                    // Set DataSource với BookListReportModel
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("DataSetBookList", books));  // ✅ Đúng type

                    reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}\n\n{ex.InnerException?.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== BÁO CÁO DANH MỤC ==========
        private void LoadCategoryReport()
        {
            using (var db = new BookStoreContext())
            {
                var categories = db.Categories
                    .Select(c => new
                    {
                        c.CategoryId,
                        c.Name,
                        c.Description
                    })
                    .ToList();

                // Load RDLC
                string rdlcPath = GetEmbeddedReportPath("ReportCategory.rdlc");
                reportViewer1.LocalReport.ReportPath = rdlcPath;

                // Parameters
                string ngayThang = "Ngày " + DateTime.Now.Day + " Tháng " +
                                  DateTime.Now.Month + " Năm " + DateTime.Now.Year;
                int soLuong = categories.Count;

                ReportParameter[] parameters = new ReportParameter[]
                {
                    new ReportParameter("ngayThang", ngayThang),
                    new ReportParameter("soLuong", soLuong.ToString())
                };
                reportViewer1.LocalReport.SetParameters(parameters);

                // DataSource
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.LocalReport.DataSources.Add(
                    new ReportDataSource("DataSetCategory", categories));

                reportViewer1.RefreshReport();
            }
        }

        // ========== BÁO CÁO TỒN KHO ==========
        private void LoadInventoryReport()
        {
            try
            {
                using (var db = new BookStoreContext())
                {
                    // Lấy danh sách tồn kho
                    var inventory = db.Books
                        .Select(b => new
                        {
                            b.BookId,
                            b.Title,
                            AuthorName = b.Author.Name,
                            CategoryName = b.Category.Name,
                            PublisherName = b.Publisher.Name,
                            b.PublishYear,
                            b.Price,
                            Stock = db.Inventories
                                .Where(i => i.BookId == b.BookId)
                                .Select(i => (int?)i.Quantity)
                                .FirstOrDefault() ?? 0
                        })
                        .ToList()
                        .Select(b => new
                        {
                            b.BookId,
                            b.Title,
                            b.AuthorName,
                            b.CategoryName,
                            b.PublisherName,
                            b.PublishYear,
                            b.Price,
                            b.Stock,
                            StockValue = b.Price * b.Stock,
                            Status = b.Stock == 0 ? "Hết hàng" :
                                     b.Stock < 10 ? "Sắp hết" : "Còn hàng"
                        })
                        .OrderBy(b => b.Stock)
                        .ThenBy(b => b.Title)
                        .ToList();

                    // Tính tổng
                    int tongSoLoai = inventory.Count;
                    int tongSoLuong = inventory.Sum(i => i.Stock);
                    decimal tongGiaTri = inventory.Sum(i => i.StockValue);
                    int soLoaiHetHang = inventory.Count(i => i.Stock == 0);
                    int soLoaiSapHet = inventory.Count(i => i.Stock > 0 && i.Stock < 10);

                    // Load RDLC
                    reportViewer1.LocalReport.ReportPath = "Reports/ReportInventory.rdlc";

                    // Set Parameters (ĐẦY ĐỦ 6 CÁI)
                    ReportParameter[] parameters = new ReportParameter[]
                    {
                        new ReportParameter("NgayIn", DateTime.Now.ToString("dd/MM/yyyy HH:mm")),
                        new ReportParameter("TongSoLoai", tongSoLoai.ToString()),
                        new ReportParameter("TongSoLuong", tongSoLuong.ToString("N0")),
                        new ReportParameter("TongGiaTri", tongGiaTri.ToString("N0") + " đ"),
                        new ReportParameter("SoLoaiHetHang", soLoaiHetHang.ToString()),
                        new ReportParameter("SoLoaiSapHet", soLoaiSapHet.ToString())
                    };
                    reportViewer1.LocalReport.SetParameters(parameters);

                    // Set DataSource
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("DataSetInventory", inventory));

                    reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}\n\n{ex.InnerException?.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== BÁO CÁO DOANH THU ==========
        private void LoadRevenueReport()
        {
            try
            {
                if (_parameter == null)
                {
                    MessageBox.Show("Thiếu khoảng thời gian!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }

                dynamic param = _parameter;
                DateTime fromDate = param.FromDate;
                DateTime toDate = param.ToDate.AddDays(1).AddSeconds(-1);

                using (var db = new BookStoreContext())
                {
                    // ===== 1. DOANH THU THEO NGÀY =====
                    var revenueByDate = db.Invoices
                        .Where(i => i.CreatedDate >= fromDate &&
                                   i.CreatedDate <= toDate &&
                                   i.Status == 1)
                        .GroupBy(i => DbFunctions.TruncateTime(i.CreatedDate))
                        .Select(g => new
                        {
                            Date = g.Key.Value,
                            InvoiceCount = g.Count(),
                            Revenue = g.Sum(i => i.TotalAmount)
                        })
                        .OrderBy(r => r.Date)
                        .ToList();

                    // ===== 2. DOANH THU THEO THỂ LOẠI =====
                    var revenueByCategory = db.InvoiceDetails
                        .Where(d => d.Invoice.CreatedDate >= fromDate &&
                                   d.Invoice.CreatedDate <= toDate &&
                                   d.Invoice.Status == 1)
                        .GroupBy(d => new { d.Book.CategoryId, d.Book.Category.Name })
                        .Select(g => new
                        {
                            CategoryId = g.Key.CategoryId,
                            CategoryName = g.Key.Name,
                            QuantitySold = g.Sum(d => d.Quantity),
                            Revenue = g.Sum(d => d.Quantity * d.Price)
                        })
                        .OrderByDescending(r => r.Revenue)
                        .ToList();

                    // ⭐ Tính phần trăm (KHÔNG NHÂN 100)
                    decimal totalRevenue = revenueByCategory.Sum(r => r.Revenue);
                    var categoryList = revenueByCategory.Select(r => new
                    {
                        r.CategoryId,
                        r.CategoryName,
                        r.QuantitySold,
                        r.Revenue,
                        Percentage = totalRevenue > 0 ? (r.Revenue / totalRevenue) : 0  // ✅ 0.5753 thay vì 57.53
                    }).ToList();

                    // ===== 3. TỔNG HỢP =====
                    int tongHoaDon = revenueByDate.Sum(r => r.InvoiceCount);
                    decimal tongDoanhThu = revenueByDate.Sum(r => r.Revenue);

                    // Load RDLC
                    reportViewer1.LocalReport.ReportPath = "Reports/ReportRevenue.rdlc";

                    // Parameters
                    ReportParameter[] parameters = new ReportParameter[]
                    {
                        new ReportParameter("TuNgay", fromDate.ToString("dd/MM/yyyy")),
                        new ReportParameter("DenNgay", toDate.AddSeconds(-1).ToString("dd/MM/yyyy")),
                        new ReportParameter("TongHoaDon", tongHoaDon.ToString()),
                        new ReportParameter("TongDoanhThu", tongDoanhThu.ToString("N0") + " đ"),
                        new ReportParameter("NgayIn", DateTime.Now.ToString("dd/MM/yyyy HH:mm"))
                    };
                    reportViewer1.LocalReport.SetParameters(parameters);

                    // DataSources
                    reportViewer1.LocalReport.DataSources.Clear();
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("DataSetByDate", revenueByDate));
                    reportViewer1.LocalReport.DataSources.Add(
                        new ReportDataSource("DataSetByCategory", categoryList));

                    reportViewer1.RefreshReport();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}\n\n{ex.InnerException?.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== HELPER METHOD ==========
        private string GetEmbeddedReportPath(string reportName)
        {
            try
            {
                string tempFolder = Path.Combine(Path.GetTempPath(), "QuanLyNhaSach_Reports");
                if (!Directory.Exists(tempFolder))
                {
                    Directory.CreateDirectory(tempFolder);
                }

                string outputPath = Path.Combine(tempFolder, reportName);
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = "QuanLyNhaSach.Reports." + reportName;

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        throw new Exception($"Không tìm thấy embedded resource: {resourceName}");
                    }

                    using (FileStream fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                    {
                        stream.CopyTo(fileStream);
                    }
                }

                return outputPath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tải báo cáo: {ex.Message}", ex);
            }
        }
    }
}