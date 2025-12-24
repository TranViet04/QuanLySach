namespace QuanLyNhaSach
{
    partial class FormDistributor
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
            this.components = new System.ComponentModel.Container();
            this.lblTitle = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtDistributorName = new System.Windows.Forms.TextBox();
            this.txtDistributorID = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.lblDistributorName = new System.Windows.Forms.Label();
            this.lblDistributorID = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dgvDistributer = new System.Windows.Forms.DataGridView();
            this.errWarning = new System.Windows.Forms.ErrorProvider(this.components);
            this.dgvDistributorID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvDistributorName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvAddress = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistributer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(229, 68);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(555, 46);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ NHÀ PHÁT HÀNH";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.txtPhone);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.txtDistributorName);
            this.groupBox1.Controls.Add(this.txtDistributorID);
            this.groupBox1.Controls.Add(this.lblEmail);
            this.groupBox1.Controls.Add(this.lblPhone);
            this.groupBox1.Controls.Add(this.lblAddress);
            this.groupBox1.Controls.Add(this.lblDistributorName);
            this.groupBox1.Controls.Add(this.lblDistributorID);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(161, 147);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(563, 237);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin Nhà phát hành";
            // 
            // txtEmail
            // 
            this.txtEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEmail.Location = new System.Drawing.Point(180, 189);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(366, 27);
            this.txtEmail.TabIndex = 5;
            // 
            // txtPhone
            // 
            this.txtPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPhone.Location = new System.Drawing.Point(180, 149);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(366, 27);
            this.txtPhone.TabIndex = 5;
            // 
            // txtAddress
            // 
            this.txtAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAddress.Location = new System.Drawing.Point(180, 112);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(366, 27);
            this.txtAddress.TabIndex = 5;
            // 
            // txtDistributorName
            // 
            this.txtDistributorName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDistributorName.Location = new System.Drawing.Point(180, 69);
            this.txtDistributorName.Name = "txtDistributorName";
            this.txtDistributorName.Size = new System.Drawing.Size(366, 27);
            this.txtDistributorName.TabIndex = 5;
            // 
            // txtDistributorID
            // 
            this.txtDistributorID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDistributorID.Location = new System.Drawing.Point(180, 32);
            this.txtDistributorID.Name = "txtDistributorID";
            this.txtDistributorID.ReadOnly = true;
            this.txtDistributorID.Size = new System.Drawing.Size(366, 27);
            this.txtDistributorID.TabIndex = 5;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.Location = new System.Drawing.Point(32, 195);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(51, 20);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "Email";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhone.Location = new System.Drawing.Point(32, 155);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(84, 20);
            this.lblPhone.TabIndex = 3;
            this.lblPhone.Text = "Điện thoại";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddress.Location = new System.Drawing.Point(32, 115);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(61, 20);
            this.lblAddress.TabIndex = 2;
            this.lblAddress.Text = "Địa chỉ";
            // 
            // lblDistributorName
            // 
            this.lblDistributorName.AutoSize = true;
            this.lblDistributorName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistributorName.Location = new System.Drawing.Point(32, 75);
            this.lblDistributorName.Name = "lblDistributorName";
            this.lblDistributorName.Size = new System.Drawing.Size(140, 20);
            this.lblDistributorName.TabIndex = 1;
            this.lblDistributorName.Text = "Tên Nhà xuất bản";
            // 
            // lblDistributorID
            // 
            this.lblDistributorID.AutoSize = true;
            this.lblDistributorID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistributorID.Location = new System.Drawing.Point(32, 35);
            this.lblDistributorID.Name = "lblDistributorID";
            this.lblDistributorID.Size = new System.Drawing.Size(145, 20);
            this.lblDistributorID.TabIndex = 0;
            this.lblDistributorID.Text = "Mã Nhà phát hành";
            // 
            // btnAdd
            // 
            this.btnAdd.AutoSize = true;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(744, 212);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 30);
            this.btnAdd.TabIndex = 2;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.AutoSize = true;
            this.btnEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(744, 252);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 30);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.AutoSize = true;
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(744, 292);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 30);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoSize = true;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(744, 336);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 30);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dgvDistributer
            // 
            this.dgvDistributer.AllowUserToAddRows = false;
            this.dgvDistributer.AllowUserToDeleteRows = false;
            this.dgvDistributer.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDistributer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDistributer.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvDistributorID,
            this.dgvDistributorName,
            this.dgvAddress,
            this.dgvPhone,
            this.dgvEmail});
            this.dgvDistributer.Location = new System.Drawing.Point(161, 400);
            this.dgvDistributer.Name = "dgvDistributer";
            this.dgvDistributer.ReadOnly = true;
            this.dgvDistributer.RowHeadersWidth = 51;
            this.dgvDistributer.RowTemplate.Height = 24;
            this.dgvDistributer.Size = new System.Drawing.Size(658, 150);
            this.dgvDistributer.TabIndex = 3;
            this.dgvDistributer.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDistributor_CellClick);
            this.dgvDistributer.SelectionChanged += new System.EventHandler(this.dgvDistributor_SelectionChanged);
            // 
            // errWarning
            // 
            this.errWarning.ContainerControl = this;
            // 
            // dgvDistributorID
            // 
            this.dgvDistributorID.HeaderText = "Mã NPH";
            this.dgvDistributorID.MinimumWidth = 6;
            this.dgvDistributorID.Name = "dgvDistributorID";
            this.dgvDistributorID.ReadOnly = true;
            // 
            // dgvDistributorName
            // 
            this.dgvDistributorName.HeaderText = "Tên NPH";
            this.dgvDistributorName.MinimumWidth = 6;
            this.dgvDistributorName.Name = "dgvDistributorName";
            this.dgvDistributorName.ReadOnly = true;
            // 
            // dgvAddress
            // 
            this.dgvAddress.HeaderText = "Địa Chỉ";
            this.dgvAddress.MinimumWidth = 6;
            this.dgvAddress.Name = "dgvAddress";
            this.dgvAddress.ReadOnly = true;
            // 
            // dgvPhone
            // 
            this.dgvPhone.HeaderText = "Điện Thoại";
            this.dgvPhone.MinimumWidth = 6;
            this.dgvPhone.Name = "dgvPhone";
            this.dgvPhone.ReadOnly = true;
            // 
            // dgvEmail
            // 
            this.dgvEmail.HeaderText = "Email";
            this.dgvEmail.MinimumWidth = 6;
            this.dgvEmail.Name = "dgvEmail";
            this.dgvEmail.ReadOnly = true;
            // 
            // FormDistributor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(981, 634);
            this.Controls.Add(this.dgvDistributer);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTitle);
            this.Name = "FormDistributor";
            this.Text = "FormDistributor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormDistributor_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDistributer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errWarning)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Label lblDistributorName;
        private System.Windows.Forms.Label lblDistributorID;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtDistributorName;
        private System.Windows.Forms.TextBox txtDistributorID;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridView dgvDistributer;
        private System.Windows.Forms.ErrorProvider errWarning;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDistributorID;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvDistributorName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvAddress;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvEmail;
    }
}