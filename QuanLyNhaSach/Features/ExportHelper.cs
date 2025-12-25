using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using QuanLyNhaSach.Data;
using QuanLyNhaSach.Models;
using System.Data.Entity;

namespace QuanLyNhaSach.Features
{
    /// <summary>
    /// Utility class for exporting data to various formats
    /// </summary>
    public static class ExportHelper
    {
        /// <summary>
        /// Export invoice to HTML for printing
        /// </summary>
        public static string GenerateInvoiceHtml(int invoiceId)
        {
            using (var db = new BookStoreContext())
            {
                var invoice = db.Invoices
                    .Include(i => i.Customer)
                    .Include(i => i.User)
                    .Include(i => i.InvoiceDetails.Select(d => d.Book.Author))
                    .FirstOrDefault(i => i.InvoiceId == invoiceId);

                if (invoice == null) return null;

                var html = new StringBuilder();
                html.AppendLine("<!DOCTYPE html>");
                html.AppendLine("<html>");
                html.AppendLine("<head>");
                html.AppendLine("<meta charset='UTF-8'>");
                html.AppendLine("<style>");
                html.AppendLine(@"
                    body { font-family: 'Segoe UI', Arial, sans-serif; margin: 40px; }
                    .header { text-align: center; margin-bottom: 30px; }
                    .header h1 { margin: 0; color: #2c3e50; }
                    .header p { margin: 5px 0; color: #7f8c8d; }
                    .info-section { margin: 20px 0; }
                    .info-row { display: flex; margin: 10px 0; }
                    .info-label { font-weight: bold; width: 150px; }
                    table { width: 100%; border-collapse: collapse; margin: 20px 0; }
                    th { background-color: #3498db; color: white; padding: 12px; text-align: left; }
                    td { padding: 10px; border-bottom: 1px solid #ecf0f1; }
                    .total-row { font-weight: bold; background-color: #ecf0f1; }
                    .footer { margin-top: 40px; text-align: center; color: #7f8c8d; }
                    @media print {
                        body { margin: 20px; }
                        .no-print { display: none; }
                    }
                ");
                html.AppendLine("</style>");
                html.AppendLine("</head>");
                html.AppendLine("<body>");

                // Header
                html.AppendLine("<div class='header'>");
                html.AppendLine("<h1>NHÀ SÁCH ABC</h1>");
                html.AppendLine("<p>Địa chỉ: 123 Đường ABC, Quận 1, TP.HCM</p>");
                html.AppendLine("<p>Điện thoại: (028) 1234 5678 | Email: info@nhasachabc.vn</p>");
                html.AppendLine("<h2>HÓA ĐƠN BÁN HÀNG</h2>");
                html.AppendLine("</div>");

                // Invoice Info
                html.AppendLine("<div class='info-section'>");
                html.AppendLine($"<div class='info-row'><span class='info-label'>Mã hóa đơn:</span><span>{invoice.InvoiceCode}</span></div>");
                html.AppendLine($"<div class='info-row'><span class='info-label'>Ngày tạo:</span><span>{invoice.CreatedDate:dd/MM/yyyy HH:mm}</span></div>");
                html.AppendLine($"<div class='info-row'><span class='info-label'>Nhân viên:</span><span>{invoice.User?.FullName}</span></div>");
                html.AppendLine($"<div class='info-row'><span class='info-label'>Khách hàng:</span><span>{invoice.Customer?.Name}</span></div>");
                html.AppendLine($"<div class='info-row'><span class='info-label'>Điện thoại:</span><span>{invoice.Customer?.Phone}</span></div>");
                html.AppendLine("</div>");

                // Items Table
                html.AppendLine("<table>");
                html.AppendLine("<thead><tr>");
                html.AppendLine("<th>STT</th><th>Tên sách</th><th>Tác giả</th><th>SL</th><th>Đơn giá</th><th>Thành tiền</th>");
                html.AppendLine("</tr></thead>");
                html.AppendLine("<tbody>");

                int stt = 1;
                foreach (var detail in invoice.InvoiceDetails)
                {
                    html.AppendLine("<tr>");
                    html.AppendLine($"<td>{stt++}</td>");
                    html.AppendLine($"<td>{detail.Book.Title}</td>");
                    html.AppendLine($"<td>{detail.Book.Author?.Name}</td>");
                    html.AppendLine($"<td>{detail.Quantity}</td>");
                    html.AppendLine($"<td>{detail.Price:N0} VNĐ</td>");
                    html.AppendLine($"<td>{detail.Amount:N0} VNĐ</td>");
                    html.AppendLine("</tr>");
                }

                html.AppendLine("<tr class='total-row'>");
                html.AppendLine($"<td colspan='5' style='text-align: right;'>Tổng cộng:</td>");
                html.AppendLine($"<td>{invoice.TotalAmount:N0} VNĐ</td>");
                html.AppendLine("</tr>");
                html.AppendLine("</tbody>");
                html.AppendLine("</table>");

                // Note
                if (!string.IsNullOrWhiteSpace(invoice.Note))
                {
                    html.AppendLine($"<p><strong>Ghi chú:</strong> {invoice.Note}</p>");
                }

                // Footer
                html.AppendLine("<div class='footer'>");
                html.AppendLine("<p>Cảm ơn quý khách đã mua hàng!</p>");
                html.AppendLine("<p>Hẹn gặp lại quý khách</p>");
                html.AppendLine("</div>");

                // Print button
                html.AppendLine("<div class='no-print' style='text-align: center; margin-top: 20px;'>");
                html.AppendLine("<button onclick='window.print()' style='padding: 10px 30px; font-size: 16px; cursor: pointer;'>In hóa đơn</button>");
                html.AppendLine("</div>");

                html.AppendLine("</body>");
                html.AppendLine("</html>");

                return html.ToString();
            }
        }

        /// <summary>
        /// Save HTML to temp file and open in browser
        /// </summary>
        public static void PrintInvoice(int invoiceId)
        {
            try
            {
                string html = GenerateInvoiceHtml(invoiceId);
                if (html == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string tempPath = Path.Combine(Path.GetTempPath(), $"Invoice_{invoiceId}_{DateTime.Now:yyyyMMddHHmmss}.html");
                File.WriteAllText(tempPath, html, Encoding.UTF8);

                System.Diagnostics.Process.Start(tempPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo file in: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Export books list to CSV
        /// </summary>
        public static void ExportBooksToCSV(string filePath)
        {
            try
            {
                using (var db = new BookStoreContext())
                {
                    var books = db.Books
                        .Include(b => b.Author)
                        .Include(b => b.Category)
                        .Include(b => b.Publisher)
                        .OrderBy(b => b.Title)
                        .ToList();

                    var csv = new StringBuilder();
                    csv.AppendLine("Mã sách,Tên sách,Tác giả,Thể loại,Nhà xuất bản,Năm XB,Giá tiền");

                    foreach (var book in books)
                    {
                        csv.AppendLine($"{book.BookId}," +
                            $"\"{book.Title}\"," +
                            $"\"{book.Author?.Name}\"," +
                            $"\"{book.Category?.Name}\"," +
                            $"\"{book.Publisher?.Name}\"," +
                            $"{book.PublishYear}," +
                            $"{book.Price}");
                    }

                    File.WriteAllText(filePath, csv.ToString(), Encoding.UTF8);
                    MessageBox.Show($"Xuất dữ liệu thành công!\nĐã lưu: {filePath}",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Export invoices to CSV for a date range
        /// </summary>
        public static void ExportInvoicesToCSV(string filePath, DateTime fromDate, DateTime toDate)
        {
            try
            {
                using (var db = new BookStoreContext())
                {
                    var endDate = toDate.Date.AddDays(1).AddSeconds(-1);
                    var invoices = db.Invoices
                        .Include(i => i.Customer)
                        .Include(i => i.User)
                        .Where(i => i.CreatedDate >= fromDate && i.CreatedDate <= endDate)
                        .OrderByDescending(i => i.CreatedDate)
                        .ToList();

                    var csv = new StringBuilder();
                    csv.AppendLine("Mã HĐ,Ngày tạo,Khách hàng,Nhân viên,Tổng tiền,Trạng thái,Ghi chú");

                    foreach (var inv in invoices)
                    {
                        string status = inv.Status == 1 ? "Đã thanh toán" : "Chưa thanh toán";
                        csv.AppendLine($"{inv.InvoiceCode}," +
                            $"{inv.CreatedDate:dd/MM/yyyy HH:mm}," +
                            $"\"{inv.Customer?.Name}\"," +
                            $"\"{inv.User?.FullName}\"," +
                            $"{inv.TotalAmount}," +
                            $"{status}," +
                            $"\"{inv.Note}\"");
                    }

                    File.WriteAllText(filePath, csv.ToString(), Encoding.UTF8);
                    MessageBox.Show($"Xuất dữ liệu thành công!\nĐã lưu: {filePath}",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    /// <summary>
    /// Backup and restore database helper
    /// </summary>
    public static class BackupHelper
    {
        /// <summary>
        /// Create database backup
        /// </summary>
        public static bool BackupDatabase(string backupPath)
        {
            try
            {
                using (var db = new BookStoreContext())
                {
                    var connectionString = db.Database.Connection.ConnectionString;

                    // Extract database name
                    var dbName = db.Database.Connection.Database;

                    var backupQuery = $@"
                        BACKUP DATABASE [{dbName}] 
                        TO DISK = '{backupPath}' 
                        WITH FORMAT, MEDIANAME = 'BookStoreBackup', 
                        NAME = 'Full Backup of {dbName}';";

                    db.Database.ExecuteSqlCommand(
                        System.Data.Entity.TransactionalBehavior.DoNotEnsureTransaction,
                        backupQuery);

                    // Log backup
                    db.BackupLogs.Add(new BackupLog
                    {
                        UserId = CurrentUser.UserId,
                        BackupDate = DateTime.Now,
                        FilePath = backupPath,
                        Status = "Success"
                    });
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                DebugLogger.Log($"Backup failed: {ex}");

                // Log failed backup
                try
                {
                    using (var db = new BookStoreContext())
                    {
                        db.BackupLogs.Add(new BackupLog
                        {
                            UserId = CurrentUser.UserId,
                            BackupDate = DateTime.Now,
                            FilePath = backupPath,
                            Status = "Failed",
                            ErrorMessage = ex.Message
                        });
                        db.SaveChanges();
                    }
                }
                catch { }

                return false;
            }
        }

        /// <summary>
        /// Show backup dialog and perform backup
        /// </summary>
        public static void ShowBackupDialog()
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = "SQL Backup Files (*.bak)|*.bak|All Files (*.*)|*.*";
                dialog.FileName = $"BookStore_Backup_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
                dialog.Title = "Chọn vị trí lưu backup";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (BackupDatabase(dialog.FileName))
                    {
                        MessageBox.Show($"Sao lưu thành công!\nĐã lưu: {dialog.FileName}",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Sao lưu thất bại! Vui lòng kiểm tra log để biết thêm chi tiết.",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}