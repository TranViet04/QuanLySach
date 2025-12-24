using Microsoft.Reporting.WinForms;
using QuanLyNhaSach.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyNhaSach
{
    public partial class FormInDanhMuc : Form
    {
        public FormInDanhMuc()
        {
            InitializeComponent();
        }

        private void FormInDanhMuc_Load(object sender, EventArgs e)
        {
            //lấy danh sách danh mục từ cơ sở dữ liệu và hiển thị trong báo cáo
            using (var db = new BookStoreContext())
            {
                //lấy danh sách danh mục từ cơ sở dữ liệu sử dụng Entity Framework lấy chỉ id và tên danh mục
                var categories = db.Categories
                    .Select(c => new
                    {
                        c.CategoryId,
                        c.Name,
                        c.Description,
                    })
                    .ToList();

                //Thiết lập file rdlc cho ReportViewer
                rpvDanhMuc.LocalReport.ReportPath = "Reports/ReportCategory.rdlc";
                string ngayThang = "Ngày " + DateTime.Now.Day + " Tháng " + DateTime.Now.Month + " Năm " + DateTime.Now.Year;
                int soLuongDanhMuc = categories.Count;

                // pass value cho tham số trong báo cáo
                Microsoft.Reporting.WinForms.ReportParameter[] reportParameters = new Microsoft.Reporting.WinForms.ReportParameter[]
                {
                    new Microsoft.Reporting.WinForms.ReportParameter("ngayThang", ngayThang),
                    new Microsoft.Reporting.WinForms.ReportParameter("soLuong", soLuongDanhMuc.ToString()),
                };

                this.rpvDanhMuc.LocalReport.SetParameters(reportParameters);
                this.rpvDanhMuc.LocalReport.DataSources.Clear();
                this.rpvDanhMuc.LocalReport.DataSources.Add(new ReportDataSource("DataSetCategory", categories));
                this.rpvDanhMuc.RefreshReport();
            }
        }
    }
}
