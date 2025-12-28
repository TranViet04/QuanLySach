namespace QuanLyNhaSach
{
    partial class FormAbout
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAbout));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblDevelopers = new System.Windows.Forms.Label();
            this.lblYear = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblTitle.Location = new System.Drawing.Point(118, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(280, 37);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "QUẢN LÝ NHÀ SÁCH";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVersion.Location = new System.Drawing.Point(120, 65);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(131, 23);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "Phiên bản: 1.0.0";
            // 
            // lblDescription
            // 
            this.lblDescription.Location = new System.Drawing.Point(18, 25);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(440, 90);
            this.lblDescription.TabIndex = 3;
            this.lblDescription.Text = "Phần mềm Quản Lý Nhà Sách là giải pháp toàn diện giúp quản lý hiệu quả các hoạt đ" +
    "ộng kinh doanh sách, từ nhập hàng, bán hàng, quản lý kho đến thống kê báo cáo.";
            // 
            // lblDevelopers
            // 
            this.lblDevelopers.Location = new System.Drawing.Point(18, 125);
            this.lblDevelopers.Name = "lblDevelopers";
            this.lblDevelopers.Size = new System.Drawing.Size(440, 120);
            this.lblDevelopers.TabIndex = 4;
            this.lblDevelopers.Text = "Đội ngũ phát triển:\r\n• Trần Quốc Việt - Trưởng nhóm\r\n• Trần Thị Ngà\r\n• Nguyễn Văn Đạt \r\n\r\nLiên hệ: contact@quanlynhasach.com";
            // 
            // lblYear
            // 
            this.lblYear.AutoSize = true;
            this.lblYear.Location = new System.Drawing.Point(120, 90);
            this.lblYear.Name = "lblYear";
            this.lblYear.Size = new System.Drawing.Size(267, 16);
            this.lblYear.TabIndex = 5;
            this.lblYear.Text = "© 2025 - Được phát triển bởi Nhóm 3 con báo";
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(364, 395);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblDescription);
            this.groupBox1.Controls.Add(this.lblDevelopers);
            this.groupBox1.Location = new System.Drawing.Point(12, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(470, 250);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin";
            // 
            // FormAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 442);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblYear);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Giới Thiệu - Quản Lý Nhà Sách";
            this.Load += new System.EventHandler(this.FormAbout_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblDevelopers;
        private System.Windows.Forms.Label lblYear;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}