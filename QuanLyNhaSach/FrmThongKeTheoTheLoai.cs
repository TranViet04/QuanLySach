using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using QuanLyNhaSach.Data;
using QuanLyNhaSach.Models;

namespace QuanLyNhaSach
{
    public partial class FrmThongKeTheoTheLoai : Form
    {
        // ViewModel for ComboBox
        private class CategoryVM
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public FrmThongKeTheoTheLoai()
        {
            InitializeComponent();
        }

        // --- 1. SỰ KIỆN LOAD FORM ---
        private void FrmThongKeTheoTheLoai_Load(object sender, EventArgs e)
        {
            LoadComboBox();
            LoadData();
        }

        private void LoadComboBox()
        {
            try
            {
                using (var db = new BookStoreContext())
                {
                    var listTheLoai = db.Categories
                        .OrderBy(c => c.Name)
                        .Select(c => new CategoryVM
                        {
                            Id = c.CategoryId,
                            Name = c.Name
                        })
                        .ToList();

                    // --- THÊM ITEM "TẤT CẢ" ---
                    listTheLoai.Insert(0, new CategoryVM { Id = 0, Name = "--- Tất cả ---" });

                    cboSearch.DataSource = listTheLoai;
                    cboSearch.DisplayMember = "Name";
                    cboSearch.ValueMember = "Id";

                    // --- KÍCH HOẠT TÌM KIẾM NHANH ---
                    cboSearch.DropDownStyle = ComboBoxStyle.DropDown;
                    cboSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    cboSearch.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh mục: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- 2. LOAD DATA ---
        private void LoadData()
        {
            try
            {
                using (var db = new BookStoreContext())
                {
                    int selectedId = 0;
                    if (cboSearch.SelectedValue != null && int.TryParse(cboSearch.SelectedValue.ToString(), out int id))
                    {
                        selectedId = id;
                    }

                    // Group by category and sum revenue from InvoiceDetails
                    var query = from category in db.Categories
                                join invoiceDetail in db.InvoiceDetails on category.CategoryId equals invoiceDetail.Book.CategoryId into details
                                select new TheLoaiStats
                                {
                                    MaTheLoai = category.CategoryId,
                                    TenTheLoai = category.Name,
                                    DoanhThu = details.Sum(d => (decimal?)d.Quantity * d.Price) ?? 0
                                };

                    var allData = query.ToList().OrderByDescending(x => x.DoanhThu).ToList();
                    
                    DrawChart(allData);

                    List<TheLoaiStats> gridData;
                    if (selectedId > 0)
                    {
                        gridData = allData.Where(x => x.MaTheLoai == selectedId).ToList();
                    }
                    else
                    {
                        gridData = allData;
                    }

                    dgvChiTiet.DataSource = gridData;
                    ConfigGrid();
                    lblTotalTheLoai.Text = allData.Count.ToString();
                    decimal tongTien = allData.Sum(x => x.DoanhThu);
                    lblTotalDoanhThu.Text = tongTien.ToString("N0") + " VNĐ";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- 3. VẼ BIỂU ĐỒ ---
        private void DrawChart(List<TheLoaiStats> data)
        {
            try
            {
                chartTheLoai.Series.Clear();
                chartTheLoai.Titles.Clear();
                chartTheLoai.Titles.Add("TỶ TRỌNG DOANH THU THEO THỂ LOẠI");
                Series series = new Series("DoanhThu");
                series.ChartType = SeriesChartType.Pie;
                chartTheLoai.Series.Add(series);

                foreach (var item in data)
                {
                    if (item.DoanhThu > 0)
                    {
                        int index = series.Points.AddXY(item.TenTheLoai, item.DoanhThu);
                        series.Points[index].Label = "#PERCENT";
                        series.Points[index].LegendText = "#VALX";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi vẽ biểu đồ: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigGrid()
        {
            try
            {
                if (dgvChiTiet.Columns["MaTheLoai"] != null)
                {
                    dgvChiTiet.Columns["MaTheLoai"].HeaderText = "Mã thể loại";
                    dgvChiTiet.Columns["TenTheLoai"].HeaderText = "Tên thể loại";
                    dgvChiTiet.Columns["DoanhThu"].HeaderText = "Doanh thu";
                    dgvChiTiet.Columns["DoanhThu"].DefaultCellStyle.Format = "N0";
                    dgvChiTiet.Columns["DoanhThu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cấu hình grid: " + ex.Message, "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- 4. SỰ KIỆN NÚT TÌM KIẾM ---
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}