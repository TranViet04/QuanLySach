namespace QuanLyNhaSach
{
    partial class FrmThongKeTheoTheLoai
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chartTheLoai = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblSearch = new System.Windows.Forms.Label();
            this.dgvChiTiet = new System.Windows.Forms.DataGridView();
            this.cboSearch = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalTheLoai = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblTotalDoanhThu = new System.Windows.Forms.Label();
            this.ColMaTheLoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColTenTheLoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDoanhThu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.chartTheLoai)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartTheLoai
            // 
            chartArea5.Name = "ChartArea1";
            this.chartTheLoai.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chartTheLoai.Legends.Add(legend5);
            this.chartTheLoai.Location = new System.Drawing.Point(525, 164);
            this.chartTheLoai.Name = "chartTheLoai";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.chartTheLoai.Series.Add(series5);
            this.chartTheLoai.Size = new System.Drawing.Size(434, 342);
            this.chartTheLoai.TabIndex = 0;
            this.chartTheLoai.Text = "chart1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cboSearch);
            this.groupBox3.Controls.Add(this.btnSearch);
            this.groupBox3.Controls.Add(this.lblSearch);
            this.groupBox3.Location = new System.Drawing.Point(28, 24);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(433, 57);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Chọn thể loại";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(333, 24);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Tìm ";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(16, 27);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(76, 16);
            this.lblSearch.TabIndex = 0;
            this.lblSearch.Text = "Tìm thể loại";
            // 
            // dgvChiTiet
            // 
            this.dgvChiTiet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTiet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTiet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColMaTheLoai,
            this.ColTenTheLoai,
            this.ColDoanhThu});
            this.dgvChiTiet.Location = new System.Drawing.Point(12, 144);
            this.dgvChiTiet.Name = "dgvChiTiet";
            this.dgvChiTiet.RowHeadersWidth = 51;
            this.dgvChiTiet.RowTemplate.Height = 24;
            this.dgvChiTiet.Size = new System.Drawing.Size(497, 400);
            this.dgvChiTiet.TabIndex = 6;
            // 
            // cboSearch
            // 
            this.cboSearch.FormattingEnabled = true;
            this.cboSearch.Location = new System.Drawing.Point(94, 24);
            this.cboSearch.Name = "cboSearch";
            this.cboSearch.Size = new System.Drawing.Size(233, 24);
            this.cboSearch.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(149, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(221, 25);
            this.label6.TabIndex = 8;
            this.label6.Text = "CHI TIẾT THỐNG KÊ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblTotalDoanhThu);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblTotalTheLoai);
            this.groupBox1.Location = new System.Drawing.Point(525, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(459, 110);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thống kê";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(16, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(167, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tổng số thể loại";
            // 
            // lblTotalTheLoai
            // 
            this.lblTotalTheLoai.AutoSize = true;
            this.lblTotalTheLoai.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTotalTheLoai.Location = new System.Drawing.Point(32, 74);
            this.lblTotalTheLoai.Name = "lblTotalTheLoai";
            this.lblTotalTheLoai.Size = new System.Drawing.Size(24, 25);
            this.lblTotalTheLoai.TabIndex = 0;
            this.lblTotalTheLoai.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(211, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 25);
            this.label5.TabIndex = 9;
            this.label5.Text = "Tổng doanh thu";
            // 
            // lblTotalDoanhThu
            // 
            this.lblTotalDoanhThu.AutoSize = true;
            this.lblTotalDoanhThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTotalDoanhThu.ForeColor = System.Drawing.Color.Green;
            this.lblTotalDoanhThu.Location = new System.Drawing.Point(225, 74);
            this.lblTotalDoanhThu.Name = "lblTotalDoanhThu";
            this.lblTotalDoanhThu.Size = new System.Drawing.Size(36, 25);
            this.lblTotalDoanhThu.TabIndex = 8;
            this.lblTotalDoanhThu.Text = "0đ";
            // 
            // ColMaTheLoai
            // 
            this.ColMaTheLoai.DataPropertyName = "MaTheLoai";
            this.ColMaTheLoai.HeaderText = "Mã thể loại";
            this.ColMaTheLoai.MinimumWidth = 6;
            this.ColMaTheLoai.Name = "ColMaTheLoai";
            // 
            // ColTenTheLoai
            // 
            this.ColTenTheLoai.DataPropertyName = "TenTheLoai";
            this.ColTenTheLoai.HeaderText = "Tên thể loại";
            this.ColTenTheLoai.MinimumWidth = 6;
            this.ColTenTheLoai.Name = "ColTenTheLoai";
            // 
            // ColDoanhThu
            // 
            this.ColDoanhThu.DataPropertyName = "DoanhThu";
            this.ColDoanhThu.HeaderText = "DoanhThu (VND)";
            this.ColDoanhThu.MinimumWidth = 6;
            this.ColDoanhThu.Name = "ColDoanhThu";
            // 
            // FrmThongKeTheoTheLoai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(981, 581);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dgvChiTiet);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.chartTheLoai);
            this.Name = "FrmThongKeTheoTheLoai";
            this.Text = "THỐNG KÊ THEO THỂ LOẠI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmThongKeTheoTheLoai_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartTheLoai)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTiet)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartTheLoai;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.DataGridView dgvChiTiet;
        private System.Windows.Forms.ComboBox cboSearch;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblTotalTheLoai;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblTotalDoanhThu;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMaTheLoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColTenTheLoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColDoanhThu;
    }
}