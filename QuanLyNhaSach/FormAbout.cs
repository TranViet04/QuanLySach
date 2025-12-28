using System;
using System.Windows.Forms;

namespace QuanLyNhaSach
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void FormAbout_Load(object sender, EventArgs e)
        {
            lblVersion.Text = "Phiên bản: 1.0.0";
            lblYear.Text = $"© {DateTime.Now.Year} - Bản quyền thuộc về Nhóm Phát Triển";
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}