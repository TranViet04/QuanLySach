namespace QuanLyNhaSach
{
    partial class FormInvoiceManagement
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbSearchStatus = new System.Windows.Forms.ComboBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.cmbSearchStaff = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dtpToDate = new System.Windows.Forms.DateTimePicker();
            this.lblSearchCustomer = new System.Windows.Forms.Label();
            this.lblToDate = new System.Windows.Forms.Label();
            this.lblSearchStatus = new System.Windows.Forms.Label();
            this.dtpFromDate = new System.Windows.Forms.DateTimePicker();
            this.lblSearchStaff = new System.Windows.Forms.Label();
            this.txtCustomerSearch = new System.Windows.Forms.TextBox();
            this.lblFromDate = new System.Windows.Forms.Label();
            this.dgvInvoiceManagement = new System.Windows.Forms.DataGridView();
            this.InvoiceCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.InvoiceDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Staff = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.lblTotal = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.lblStaff = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.lblInvoiceDate = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblInvoiceCode = new System.Windows.Forms.Label();
            this.btnView = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoiceManagement)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(378, 36);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(211, 16);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ DANH SÁCH HÓA ĐƠN";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbSearchStatus);
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.cmbSearchStaff);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.dtpToDate);
            this.groupBox1.Controls.Add(this.lblSearchCustomer);
            this.groupBox1.Controls.Add(this.lblToDate);
            this.groupBox1.Controls.Add(this.lblSearchStatus);
            this.groupBox1.Controls.Add(this.dtpFromDate);
            this.groupBox1.Controls.Add(this.lblSearchStaff);
            this.groupBox1.Controls.Add(this.txtCustomerSearch);
            this.groupBox1.Controls.Add(this.lblFromDate);
            this.groupBox1.Location = new System.Drawing.Point(56, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(316, 221);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tìm kiếm hóa đơn";
            // 
            // cmbSearchStatus
            // 
            this.cmbSearchStatus.FormattingEnabled = true;
            this.cmbSearchStatus.Items.AddRange(new object[] {
            "Tất cả",
            "Chưa thanh toán",
            "Đã thanh toán"});
            this.cmbSearchStatus.Location = new System.Drawing.Point(90, 146);
            this.cmbSearchStatus.Name = "cmbSearchStatus";
            this.cmbSearchStatus.Size = new System.Drawing.Size(200, 24);
            this.cmbSearchStatus.TabIndex = 2;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(215, 178);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "Làm Mới";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // cmbSearchStaff
            // 
            this.cmbSearchStaff.FormattingEnabled = true;
            this.cmbSearchStaff.Location = new System.Drawing.Point(90, 82);
            this.cmbSearchStaff.Name = "cmbSearchStaff";
            this.cmbSearchStaff.Size = new System.Drawing.Size(200, 24);
            this.cmbSearchStaff.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(90, 178);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Tìm Kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // dtpToDate
            // 
            this.dtpToDate.Location = new System.Drawing.Point(90, 51);
            this.dtpToDate.Name = "dtpToDate";
            this.dtpToDate.Size = new System.Drawing.Size(200, 22);
            this.dtpToDate.TabIndex = 1;
            // 
            // lblSearchCustomer
            // 
            this.lblSearchCustomer.AutoSize = true;
            this.lblSearchCustomer.Location = new System.Drawing.Point(7, 118);
            this.lblSearchCustomer.Name = "lblSearchCustomer";
            this.lblSearchCustomer.Size = new System.Drawing.Size(80, 16);
            this.lblSearchCustomer.TabIndex = 0;
            this.lblSearchCustomer.Text = "Khách Hàng";
            // 
            // lblToDate
            // 
            this.lblToDate.AutoSize = true;
            this.lblToDate.Location = new System.Drawing.Point(7, 54);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Size = new System.Drawing.Size(67, 16);
            this.lblToDate.TabIndex = 0;
            this.lblToDate.Text = "Đến Ngày";
            // 
            // lblSearchStatus
            // 
            this.lblSearchStatus.AutoSize = true;
            this.lblSearchStatus.Location = new System.Drawing.Point(7, 150);
            this.lblSearchStatus.Name = "lblSearchStatus";
            this.lblSearchStatus.Size = new System.Drawing.Size(73, 16);
            this.lblSearchStatus.TabIndex = 0;
            this.lblSearchStatus.Text = "Trạng Thái";
            // 
            // dtpFromDate
            // 
            this.dtpFromDate.Location = new System.Drawing.Point(90, 19);
            this.dtpFromDate.Name = "dtpFromDate";
            this.dtpFromDate.Size = new System.Drawing.Size(200, 22);
            this.dtpFromDate.TabIndex = 1;
            // 
            // lblSearchStaff
            // 
            this.lblSearchStaff.AutoSize = true;
            this.lblSearchStaff.Location = new System.Drawing.Point(7, 86);
            this.lblSearchStaff.Name = "lblSearchStaff";
            this.lblSearchStaff.Size = new System.Drawing.Size(69, 16);
            this.lblSearchStaff.TabIndex = 0;
            this.lblSearchStaff.Text = "Nhân Viên";
            // 
            // txtCustomerSearch
            // 
            this.txtCustomerSearch.Location = new System.Drawing.Point(90, 115);
            this.txtCustomerSearch.Name = "txtCustomerSearch";
            this.txtCustomerSearch.Size = new System.Drawing.Size(200, 22);
            this.txtCustomerSearch.TabIndex = 1;
            // 
            // lblFromDate
            // 
            this.lblFromDate.AutoSize = true;
            this.lblFromDate.Location = new System.Drawing.Point(7, 22);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Size = new System.Drawing.Size(59, 16);
            this.lblFromDate.TabIndex = 0;
            this.lblFromDate.Text = "Từ Ngày";
            // 
            // dgvInvoiceManagement
            // 
            this.dgvInvoiceManagement.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInvoiceManagement.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvoiceManagement.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.InvoiceCode,
            this.InvoiceDate,
            this.Name,
            this.Staff,
            this.TotalAmount,
            this.Column6,
            this.Column7,
            this.Column10,
            this.Column8,
            this.Column9});
            this.dgvInvoiceManagement.Location = new System.Drawing.Point(55, 347);
            this.dgvInvoiceManagement.Name = "dgvInvoiceManagement";
            this.dgvInvoiceManagement.RowHeadersWidth = 51;
            this.dgvInvoiceManagement.RowTemplate.Height = 24;
            this.dgvInvoiceManagement.Size = new System.Drawing.Size(870, 122);
            this.dgvInvoiceManagement.TabIndex = 2;
            // 
            // InvoiceCode
            // 
            this.InvoiceCode.HeaderText = "Mã HD";
            this.InvoiceCode.MinimumWidth = 6;
            this.InvoiceCode.Name = "InvoiceCode";
            // 
            // InvoiceDate
            // 
            dataGridViewCellStyle3.Format = "d";
            dataGridViewCellStyle3.NullValue = null;
            this.InvoiceDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.InvoiceDate.HeaderText = "Ngày";
            this.InvoiceDate.MinimumWidth = 6;
            this.InvoiceDate.Name = "InvoiceDate";
            // 
            // Name
            // 
            this.Name.HeaderText = "Khách Hàng";
            this.Name.MinimumWidth = 6;
            this.Name.Name = "Name";
            // 
            // Staff
            // 
            this.Staff.HeaderText = "Nhân Viên";
            this.Staff.MinimumWidth = 6;
            this.Staff.Name = "Staff";
            // 
            // TotalAmount
            // 
            dataGridViewCellStyle4.Format = "C2";
            dataGridViewCellStyle4.NullValue = null;
            this.TotalAmount.DefaultCellStyle = dataGridViewCellStyle4;
            this.TotalAmount.HeaderText = "Tổng Tiền";
            this.TotalAmount.MinimumWidth = 6;
            this.TotalAmount.Name = "TotalAmount";
            // 
            // Column6
            // 
            this.Column6.HeaderText = "Trạng Thái";
            this.Column6.MinimumWidth = 6;
            this.Column6.Name = "Column6";
            // 
            // Column7
            // 
            this.Column7.HeaderText = "Xem";
            this.Column7.MinimumWidth = 6;
            this.Column7.Name = "Column7";
            // 
            // Column10
            // 
            this.Column10.HeaderText = "In";
            this.Column10.MinimumWidth = 6;
            this.Column10.Name = "Column10";
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Sửa";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Xóa";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox6);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(this.lblStatus);
            this.groupBox2.Controls.Add(this.lblCustomer);
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Controls.Add(this.lblTotal);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.lblStaff);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.lblInvoiceDate);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.lblInvoiceCode);
            this.groupBox2.Location = new System.Drawing.Point(571, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(322, 204);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin hóa đơn";
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(102, 161);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(200, 22);
            this.textBox6.TabIndex = 1;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(102, 105);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(200, 22);
            this.textBox4.TabIndex = 1;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(10, 162);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(73, 16);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Trạng Thái";
            // 
            // lblCustomer
            // 
            this.lblCustomer.AutoSize = true;
            this.lblCustomer.Location = new System.Drawing.Point(10, 106);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(80, 16);
            this.lblCustomer.TabIndex = 0;
            this.lblCustomer.Text = "Khách Hàng";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(102, 133);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(200, 22);
            this.textBox5.TabIndex = 1;
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Location = new System.Drawing.Point(10, 134);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(69, 16);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Tổng Tiền";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(102, 77);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(200, 22);
            this.textBox3.TabIndex = 1;
            // 
            // lblStaff
            // 
            this.lblStaff.AutoSize = true;
            this.lblStaff.Location = new System.Drawing.Point(10, 78);
            this.lblStaff.Name = "lblStaff";
            this.lblStaff.Size = new System.Drawing.Size(69, 16);
            this.lblStaff.TabIndex = 0;
            this.lblStaff.Text = "Nhân Viên";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(102, 49);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(200, 22);
            this.textBox2.TabIndex = 1;
            // 
            // lblInvoiceDate
            // 
            this.lblInvoiceDate.AutoSize = true;
            this.lblInvoiceDate.Location = new System.Drawing.Point(10, 50);
            this.lblInvoiceDate.Name = "lblInvoiceDate";
            this.lblInvoiceDate.Size = new System.Drawing.Size(68, 16);
            this.lblInvoiceDate.TabIndex = 0;
            this.lblInvoiceDate.Text = "Ngày Tạo";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(102, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(200, 22);
            this.textBox1.TabIndex = 1;
            // 
            // lblInvoiceCode
            // 
            this.lblInvoiceCode.AutoSize = true;
            this.lblInvoiceCode.Location = new System.Drawing.Point(10, 22);
            this.lblInvoiceCode.Name = "lblInvoiceCode";
            this.lblInvoiceCode.Size = new System.Drawing.Size(49, 16);
            this.lblInvoiceCode.TabIndex = 0;
            this.lblInvoiceCode.Text = "Mã HD";
            // 
            // btnView
            // 
            this.btnView.AutoSize = true;
            this.btnView.Location = new System.Drawing.Point(426, 295);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(84, 26);
            this.btnView.TabIndex = 3;
            this.btnView.Text = "Xem chi tiết";
            this.btnView.UseVisualStyleBackColor = true;
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(522, 297);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(84, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = true;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(618, 297);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(84, 23);
            this.btnPrint.TabIndex = 3;
            this.btnPrint.Text = "In ";
            this.btnPrint.UseVisualStyleBackColor = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(714, 297);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(84, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(810, 297);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(84, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // FormInvoiceManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 634);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.dgvInvoiceManagement);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTitle);
            this.Text = "FormInvoiceManagement";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoiceManagement)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvInvoiceManagement;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DateTimePicker dtpToDate;
        private System.Windows.Forms.Label lblToDate;
        private System.Windows.Forms.DateTimePicker dtpFromDate;
        private System.Windows.Forms.Label lblFromDate;
        private System.Windows.Forms.Button btnView;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cmbSearchStatus;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.ComboBox cmbSearchStaff;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblSearchCustomer;
        private System.Windows.Forms.Label lblSearchStatus;
        private System.Windows.Forms.Label lblSearchStaff;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label lblStaff;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lblInvoiceDate;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblInvoiceCode;
        private System.Windows.Forms.TextBox txtCustomerSearch;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn InvoiceDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Staff;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewButtonColumn Column7;
        private System.Windows.Forms.DataGridViewButtonColumn Column10;
        private System.Windows.Forms.DataGridViewButtonColumn Column8;
        private System.Windows.Forms.DataGridViewButtonColumn Column9;
    }
}