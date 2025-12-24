namespace QuanLyNhaSach
{
    partial class FormUserManagement
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
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblUserName = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblComfirm = new System.Windows.Forms.Label();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.dgvUsers = new System.Windows.Forms.DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRoles = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.clbRoles = new System.Windows.Forms.CheckedListBox();
            this.lblRoleHint = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.Location = new System.Drawing.Point(567, 440);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(112, 31);
            this.btnRefresh.TabIndex = 8;
            this.btnRefresh.Text = "Tải Lại";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(410, 440);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(112, 31);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(255, 440);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(112, 31);
            this.btnUpdate.TabIndex = 10;
            this.btnUpdate.Text = "Sửa";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(103, 440);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(112, 31);
            this.btnAdd.TabIndex = 11;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(25, 31);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(102, 16);
            this.lblUserName.TabIndex = 12;
            this.lblUserName.Text = "Tên Đăng Nhập";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(28, 95);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(62, 16);
            this.lblPassword.TabIndex = 12;
            this.lblPassword.Text = "Mật Khẩu";
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(25, 63);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(105, 16);
            this.lblFullName.TabIndex = 12;
            this.lblFullName.Text = "Tên Người Dùng";
            // 
            // lblComfirm
            // 
            this.lblComfirm.AutoSize = true;
            this.lblComfirm.Location = new System.Drawing.Point(28, 127);
            this.lblComfirm.Name = "lblComfirm";
            this.lblComfirm.Size = new System.Drawing.Size(65, 16);
            this.lblComfirm.TabIndex = 12;
            this.lblComfirm.Text = "Xác Nhận";
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(136, 28);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(376, 22);
            this.txtUserName.TabIndex = 13;
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(136, 60);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(376, 22);
            this.txtFullName.TabIndex = 13;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(136, 92);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(376, 22);
            this.txtPassword.TabIndex = 13;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // dgvUsers
            // 
            this.dgvUsers.AllowUserToAddRows = false;
            this.dgvUsers.AllowUserToDeleteRows = false;
            this.dgvUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.Column1,
            this.Column2,
            this.colRoles,
            this.Column5});
            this.dgvUsers.Location = new System.Drawing.Point(68, 481);
            this.dgvUsers.Name = "dgvUsers";
            this.dgvUsers.ReadOnly = true;
            this.dgvUsers.RowHeadersWidth = 51;
            this.dgvUsers.RowTemplate.Height = 24;
            this.dgvUsers.Size = new System.Drawing.Size(665, 150);
            this.dgvUsers.TabIndex = 15;
            this.dgvUsers.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvUsers_CellClick);
            // 
            // Column4
            // 
            this.Column4.HeaderText = "UserID";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Visible = false;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Tên Người Dùng";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "Họ Tên";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // colRoles
            // 
            this.colRoles.HeaderText = "Quyền";
            this.colRoles.MinimumWidth = 6;
            this.colRoles.Name = "colRoles";
            this.colRoles.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.HeaderText = "Kích Hoạt";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkIsActive);
            this.groupBox1.Controls.Add(this.txtFullName);
            this.groupBox1.Controls.Add(this.txtConfirmPassword);
            this.groupBox1.Controls.Add(this.txtPassword);
            this.groupBox1.Controls.Add(this.txtUserName);
            this.groupBox1.Controls.Add(this.lblComfirm);
            this.groupBox1.Controls.Add(this.lblUserName);
            this.groupBox1.Controls.Add(this.lblPassword);
            this.groupBox1.Controls.Add(this.lblFullName);
            this.groupBox1.Location = new System.Drawing.Point(103, 62);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(542, 191);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin người dùng";
            // 
            // chkIsActive
            // 
            this.chkIsActive.AutoSize = true;
            this.chkIsActive.Location = new System.Drawing.Point(136, 157);
            this.chkIsActive.Name = "chkIsActive";
            this.chkIsActive.Size = new System.Drawing.Size(86, 20);
            this.chkIsActive.TabIndex = 14;
            this.chkIsActive.Text = "Kích Hoạt";
            this.chkIsActive.UseVisualStyleBackColor = true;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(136, 124);
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.Size = new System.Drawing.Size(376, 22);
            this.txtConfirmPassword.TabIndex = 13;
            this.txtConfirmPassword.UseSystemPasswordChar = true;
            // 
            // clbRoles
            // 
            this.clbRoles.FormattingEnabled = true;
            this.clbRoles.Location = new System.Drawing.Point(28, 31);
            this.clbRoles.Name = "clbRoles";
            this.clbRoles.Size = new System.Drawing.Size(484, 89);
            this.clbRoles.TabIndex = 17;
            // 
            // lblRoleHint
            // 
            this.lblRoleHint.AutoSize = true;
            this.lblRoleHint.Location = new System.Drawing.Point(25, 134);
            this.lblRoleHint.Name = "lblRoleHint";
            this.lblRoleHint.Size = new System.Drawing.Size(125, 16);
            this.lblRoleHint.TabIndex = 18;
            this.lblRoleHint.Text = "Chọn ít nhất 1 quyền";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clbRoles);
            this.groupBox2.Controls.Add(this.lblRoleHint);
            this.groupBox2.Location = new System.Drawing.Point(103, 259);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(542, 161);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách quyền";
            // 
            // FormUserManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 640);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dgvUsers);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormUserManagement";
            this.Text = "FormUserManagement";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormUserManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsers)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Label lblComfirm;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.DataGridView dgvUsers;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.CheckedListBox clbRoles;
        private System.Windows.Forms.Label lblRoleHint;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRoles;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Column5;
    }
}