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

namespace QuanLyNhaSach
{
    public partial class FrmThongKeTheoThang : Form
    {
        private BookStoreContext _context = new BookStoreContext(); // Biến để tương tác với cơ sở dữ liệu
        public FrmThongKeTheoThang()
        {
            InitializeComponent();
        }

        // ================= TỔNG THỐNG KÊ  =================
        private void LoadDashboard(DateTime tuNgay, DateTime denNgay)
        {
            using (var db = new BookStoreContext())
            {
                // --- 1. XỬ LÝ  DOANH THU ---
                var listHoaDon = db.Invoices
                                .Where(h => h.CreatedDate >= tuNgay && h.CreatedDate <= denNgay)
                                .ToList();
                decimal tongTien = listHoaDon.Sum(x => x.TotalAmount);
                lblTotalDoanhThu.Text = tongTien.ToString("N0") + " VNĐ";


                // --- 2. XỬ LÝ  ĐƠN HÀNG ---
                int tongSoDon = listHoaDon.Count;
                lblTotalHoaDon.Text = tongSoDon.ToString() + " Đơn";


                // --- 3. XỬ LÝ  SÁCH ĐÃ BÁN ---
                var tongSach = db.InvoiceDetails
                                 .Where(ct => ct.Invoice.CreatedDate >= tuNgay && ct.Invoice.CreatedDate <= denNgay)
                                 .Sum(ct => (int?)ct.Quantity) ?? 0;
                lblTotalQuantityBook.Text = tongSach.ToString() + " Cuốn";
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpStartDate.Value.Date;
            DateTime denNgay = dtpEndDate.Value.Date.AddDays(1).AddSeconds(-1);

            LoadDashboard(tuNgay, denNgay);
            LoadChiTiet(tuNgay, denNgay);
            LoadChartDoanhThu(tuNgay, denNgay);
        }

        // ================= THỐNG KÊ CHI TIẾT =================
        private void LoadChiTiet(DateTime tuNgay, DateTime denNgay)
        {
            using (var db = new BookStoreContext())
            {
                var listChiTiet = db.Invoices
                    .Where(h => h.CreatedDate >= tuNgay && h.CreatedDate <= denNgay)
                    .Select(h => new
                    {
                        MaHoaDon = h.InvoiceCode,
                        SoLuongSach = h.InvoiceDetails.Sum(d => (int?)d.Quantity) ?? 0,
                        TongTien = h.TotalAmount
                    })
                    .OrderByDescending(x => x.MaHoaDon)
                    .ToList();

                dgvDetails.DataSource = listChiTiet;

                if (dgvDetails.Columns.Count > 0)
                {
                    dgvDetails.Columns["ColMaHoaDon"].HeaderText = "Mã hoá đơn";
                    dgvDetails.Columns["colQuantity"].HeaderText = "Số lượng sách";
                    dgvDetails.Columns["colAmount"].HeaderText = "Tổng tiền";

                    dgvDetails.Columns["colAmount"].DefaultCellStyle.Format = "N0";
                }
            }
        }

        // ================= VẼ BIỂU ĐỒ =================
        private void LoadChartDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            using (var db = new BookStoreContext())
            {
                // 1. Lấy theo năm của ngày bắt đầu
                int namCanTim = tuNgay.Year;

                // 2. Clear old
                chartDoanhThu.Series.Clear();
                chartDoanhThu.Titles.Clear();
                chartDoanhThu.Titles.Add($"Biểu đồ doanh thu năm {namCanTim}");

                // 3. Tạo Series mới
                var series = new System.Windows.Forms.DataVisualization.Charting.Series("DoanhThu");
                series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
                series.IsValueShownAsLabel = true;
                series.LabelFormat = "N0";
                chartDoanhThu.Series.Add(series);

                // Cấu hình trục
                chartDoanhThu.ChartAreas[0].AxisX.Title = "Tháng";
                chartDoanhThu.ChartAreas[0].AxisX.Interval = 1; // Hiện đủ số 1-12 
                chartDoanhThu.ChartAreas[0].AxisY.Title = "Doanh số (VNĐ)";

                // 4. LẤY DỮ LIỆU TỪ DB (Chỉ lấy những tháng có doanh thu)
                var dataDB = db.Invoices
                    .Where(h => h.CreatedDate.Year == namCanTim)
                    .GroupBy(h => h.CreatedDate.Month)
                    .Select(g => new
                    {
                        Thang = g.Key,
                        TongDoanhThu = g.Sum(x => x.TotalAmount)
                    })
                    .ToList();

                // 5. VÒNG LẶP CỐ ĐỊNH 12 THÁNG (Kỹ thuật Fill Data)
                for (int thang = 1; thang <= 12; thang++)
                {
                    // Tìm xem trong dataDB có tháng này không
                    var dataThang = dataDB.FirstOrDefault(x => x.Thang == thang);

                    decimal doanhThu = 0;
                    if (dataThang != null)
                    {
                        doanhThu = dataThang.TongDoanhThu;
                    }

                    // Vẽ lên biểu đồ (Tháng nào 0đ vẫn là số 0)
                    series.Points.AddXY("T" + thang, doanhThu);
                }
            }
        }
    }
}