namespace QuanLyNhaSach
{
    partial class FrmThongKeTheoThang
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.grbLocThoiGian = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalHoaDon = new System.Windows.Forms.Label();
            this.lblTotalDoanhThu = new System.Windows.Forms.Label();
            this.lblTotalQuantityBook = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            this.ColMaHoaDon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.chartDoanhThu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.bookDataSet1 = new QuanLyNhaSach.BookDataSet();
            this.grbLocThoiGian.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bookDataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // grbLocThoiGian
            // 
            this.grbLocThoiGian.Controls.Add(this.btnSearch);
            this.grbLocThoiGian.Controls.Add(this.label3);
            this.grbLocThoiGian.Controls.Add(this.btnRefresh);
            this.grbLocThoiGian.Controls.Add(this.label4);
            this.grbLocThoiGian.Controls.Add(this.dtpEndDate);
            this.grbLocThoiGian.Controls.Add(this.dtpStartDate);
            this.grbLocThoiGian.Location = new System.Drawing.Point(12, 12);
            this.grbLocThoiGian.Name = "grbLocThoiGian";
            this.grbLocThoiGian.Size = new System.Drawing.Size(957, 111);
            this.grbLocThoiGian.TabIndex = 0;
            this.grbLocThoiGian.TabStop = false;
            this.grbLocThoiGian.Text = "Lọc theo thời gian";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(318, 64);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(137, 28);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "Xem thống kê";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Từ ngày";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(485, 64);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(90, 28);
            this.btnRefresh.TabIndex = 10;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(26, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Đến ngày";
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(119, 67);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(180, 22);
            this.dtpEndDate.TabIndex = 7;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(119, 34);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(180, 22);
            this.dtpStartDate.TabIndex = 8;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.lblTotalHoaDon);
            this.groupBox3.Controls.Add(this.lblTotalDoanhThu);
            this.groupBox3.Controls.Add(this.lblTotalQuantityBook);
            this.groupBox3.Location = new System.Drawing.Point(22, 129);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(957, 110);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thống kê";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(421, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 25);
            this.label2.TabIndex = 6;
            this.label2.Text = "Tổng số sách";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(755, 30);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(164, 25);
            this.label5.TabIndex = 7;
            this.label5.Text = "Tổng doanh thu";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.Location = new System.Drawing.Point(42, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tổng hoá đơn";
            // 
            // lblTotalHoaDon
            // 
            this.lblTotalHoaDon.AutoSize = true;
            this.lblTotalHoaDon.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTotalHoaDon.Location = new System.Drawing.Point(58, 74);
            this.lblTotalHoaDon.Name = "lblTotalHoaDon";
            this.lblTotalHoaDon.Size = new System.Drawing.Size(24, 25);
            this.lblTotalHoaDon.TabIndex = 0;
            this.lblTotalHoaDon.Text = "0";
            // 
            // lblTotalDoanhThu
            // 
            this.lblTotalDoanhThu.AutoSize = true;
            this.lblTotalDoanhThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTotalDoanhThu.ForeColor = System.Drawing.Color.Green;
            this.lblTotalDoanhThu.Location = new System.Drawing.Point(778, 74);
            this.lblTotalDoanhThu.Name = "lblTotalDoanhThu";
            this.lblTotalDoanhThu.Size = new System.Drawing.Size(36, 25);
            this.lblTotalDoanhThu.TabIndex = 0;
            this.lblTotalDoanhThu.Text = "0đ";
            // 
            // lblTotalQuantityBook
            // 
            this.lblTotalQuantityBook.AutoSize = true;
            this.lblTotalQuantityBook.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.lblTotalQuantityBook.Location = new System.Drawing.Point(440, 74);
            this.lblTotalQuantityBook.Name = "lblTotalQuantityBook";
            this.lblTotalQuantityBook.Size = new System.Drawing.Size(24, 25);
            this.lblTotalQuantityBook.TabIndex = 0;
            this.lblTotalQuantityBook.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(380, 242);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(221, 25);
            this.label6.TabIndex = 7;
            this.label6.Text = "CHI TIẾT THỐNG KÊ";
            // 
            // dgvDetails
            // 
            this.dgvDetails.AllowUserToAddRows = false;
            this.dgvDetails.AllowUserToDeleteRows = false;
            this.dgvDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetails.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColMaHoaDon,
            this.colQuantity,
            this.colAmount});
            this.dgvDetails.Location = new System.Drawing.Point(41, 270);
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.ReadOnly = true;
            this.dgvDetails.RowHeadersWidth = 51;
            this.dgvDetails.RowTemplate.Height = 24;
            this.dgvDetails.Size = new System.Drawing.Size(911, 187);
            this.dgvDetails.TabIndex = 8;
            // 
            // ColMaHoaDon
            // 
            this.ColMaHoaDon.DataPropertyName = "MaHoaDon";
            this.ColMaHoaDon.HeaderText = "Mã hoá đơn";
            this.ColMaHoaDon.MinimumWidth = 6;
            this.ColMaHoaDon.Name = "ColMaHoaDon";
            this.ColMaHoaDon.ReadOnly = true;
            // 
            // colQuantity
            // 
            this.colQuantity.DataPropertyName = "SoLuongSach";
            this.colQuantity.HeaderText = "Số lượng sách";
            this.colQuantity.MinimumWidth = 6;
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.ReadOnly = true;
            // 
            // colAmount
            // 
            this.colAmount.DataPropertyName = "TongTien";
            this.colAmount.HeaderText = "Thành tiền";
            this.colAmount.MinimumWidth = 6;
            this.colAmount.Name = "colAmount";
            this.colAmount.ReadOnly = true;
            // 
            // chartDoanhThu
            // 
            chartArea1.Name = "ChartArea1";
            this.chartDoanhThu.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartDoanhThu.Legends.Add(legend1);
            this.chartDoanhThu.Location = new System.Drawing.Point(41, 479);
            this.chartDoanhThu.Name = "chartDoanhThu";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartDoanhThu.Series.Add(series1);
            this.chartDoanhThu.Size = new System.Drawing.Size(911, 300);
            this.chartDoanhThu.TabIndex = 9;
            this.chartDoanhThu.Text = "chart1";
            // 
            // bookDataSet1
            // 
            this.bookDataSet1.DataSetName = "BookDataSet";
            this.bookDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // FrmThongKeTheoThang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(981, 858);
            this.Controls.Add(this.chartDoanhThu);
            this.Controls.Add(this.dgvDetails);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.grbLocThoiGian);
            this.Name = "FrmThongKeTheoThang";
            this.Text = "Thống kê doanh thu theo tháng";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.grbLocThoiGian.ResumeLayout(false);
            this.grbLocThoiGian.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartDoanhThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bookDataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbLocThoiGian;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblTotalHoaDon;
        private System.Windows.Forms.Label lblTotalDoanhThu;
        private System.Windows.Forms.Label lblTotalQuantityBook;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvDetails;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColMaHoaDon;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAmount;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartDoanhThu;
        private BookDataSet bookDataSet1;
    }
}