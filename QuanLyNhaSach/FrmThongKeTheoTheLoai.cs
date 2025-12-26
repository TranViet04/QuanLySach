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
            using (var db = new BookStoreContext())
            {
                var listTheLoai = db.Categories.ToList();
                listTheLoai.Insert(0, new Category { CategoryId = 0, Name = "--- Tất cả ---" });

                cboSearch.DataSource = listTheLoai;
                cboSearch.DisplayMember = "Name";
                cboSearch.ValueMember = "CategoryId";

                // --- KÍCH HOẠT TÌM KIẾM NHANH ---
                cboSearch.DropDownStyle = ComboBoxStyle.DropDown;
                cboSearch.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cboSearch.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
        }

        // --- 1.  LOAD DATA ---
        private void LoadData()
        {
            using (var db = new BookStoreContext())
            {
                int selectedId = 0;
                if (cboSearch.SelectedValue != null && int.TryParse(cboSearch.SelectedValue.ToString(), out int id))
                {
                    selectedId = id;
                }
                var query = db.Categories.Select(c => new TheLoaiStats
                {
                    MaTheLoai = c.CategoryId,
                    TenTheLoai = c.Name,
                    DoanhThu = db.InvoiceDetails
                                    .Where(d => d.Book.CategoryId == c.CategoryId)
                                    .Sum(d => (decimal?)d.Quantity * d.Price) ?? 0
                });

                var allData = query.OrderByDescending(x => x.DoanhThu).ToList();
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

        // --- 2. VẼ ---
        private void DrawChart(List<TheLoaiStats> data)
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
                    series.Points[index].Label = "#VALX (#PERCENT)";
                    series.Points[index].LegendText = "#VALX";
                }
            }
        }

        private void ConfigGrid()
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

        // --- 3. SỰ KIỆN NÚT TÌM KIẾM ---
        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
