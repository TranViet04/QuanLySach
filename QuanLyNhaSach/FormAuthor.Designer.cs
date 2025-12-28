namespace QuanLyNhaSach
{
    partial class FormAuthor
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
            this.optFemale = new System.Windows.Forms.RadioButton();
            this.optMale = new System.Windows.Forms.RadioButton();
            this.txtID = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtBio = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblBio = new System.Windows.Forms.Label();
            this.lblSex = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblAuthorId = new System.Windows.Forms.Label();
            this.dgvAuthor = new System.Windows.Forms.DataGridView();
            this.colAuthorId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colGender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPhone = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEmail = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnRefesh = new System.Windows.Forms.Button();
            this.errWarning = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuthor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errWarning)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(279, 44);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(422, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "MÀN HÌNH QUẢN LÝ TÁC GIẢ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.optFemale);
            this.groupBox1.Controls.Add(this.optMale);
            this.groupBox1.Controls.Add(this.txtID);
            this.groupBox1.Controls.Add(this.txtName);
            this.groupBox1.Controls.Add(this.txtBio);
            this.groupBox1.Controls.Add(this.txtPhone);
            this.groupBox1.Controls.Add(this.txtEmail);
            this.groupBox1.Controls.Add(this.lblEmail);
            this.groupBox1.Controls.Add(this.lblPhone);
            this.groupBox1.Controls.Add(this.lblBio);
            this.groupBox1.Controls.Add(this.lblSex);
            this.groupBox1.Controls.Add(this.lblName);
            this.groupBox1.Controls.Add(this.lblAuthorId);
            this.groupBox1.Location = new System.Drawing.Point(87, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(563, 225);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Quản lý thông tin tác giả";
            // 
            // optFemale
            // 
            this.optFemale.AutoSize = true;
            this.optFemale.Location = new System.Drawing.Point(263, 96);
            this.optFemale.Name = "optFemale";
            this.optFemale.Size = new System.Drawing.Size(45, 20);
            this.optFemale.TabIndex = 3;
            this.optFemale.TabStop = true;
            this.optFemale.Text = "Nữ";
            this.optFemale.UseVisualStyleBackColor = true;
            // 
            // optMale
            // 
            this.optMale.AutoSize = true;
            this.optMale.Location = new System.Drawing.Point(141, 96);
            this.optMale.Name = "optMale";
            this.optMale.Size = new System.Drawing.Size(57, 20);
            this.optMale.TabIndex = 2;
            this.optMale.TabStop = true;
            this.optMale.Text = "Nam";
            this.optMale.UseVisualStyleBackColor = true;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(141, 31);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(124, 22);
            this.txtID.TabIndex = 1;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(141, 63);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(288, 22);
            this.txtName.TabIndex = 1;
            // 
            // txtBio
            // 
            this.txtBio.Location = new System.Drawing.Point(141, 127);
            this.txtBio.Name = "txtBio";
            this.txtBio.Size = new System.Drawing.Size(288, 22);
            this.txtBio.TabIndex = 1;
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(141, 159);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(288, 22);
            this.txtPhone.TabIndex = 1;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(141, 191);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(288, 22);
            this.txtEmail.TabIndex = 1;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(27, 194);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(41, 16);
            this.lblEmail.TabIndex = 0;
            this.lblEmail.Text = "Email";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(27, 162);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(85, 16);
            this.lblPhone.TabIndex = 0;
            this.lblPhone.Text = "Số điện thoại";
            // 
            // lblBio
            // 
            this.lblBio.AutoSize = true;
            this.lblBio.Location = new System.Drawing.Point(27, 130);
            this.lblBio.Name = "lblBio";
            this.lblBio.Size = new System.Drawing.Size(47, 16);
            this.lblBio.TabIndex = 0;
            this.lblBio.Text = "Địa chỉ";
            // 
            // lblSex
            // 
            this.lblSex.AutoSize = true;
            this.lblSex.Location = new System.Drawing.Point(27, 98);
            this.lblSex.Name = "lblSex";
            this.lblSex.Size = new System.Drawing.Size(54, 16);
            this.lblSex.TabIndex = 0;
            this.lblSex.Text = "Giới tính";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(27, 66);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(74, 16);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Tên tác giả";
            // 
            // lblAuthorId
            // 
            this.lblAuthorId.AutoSize = true;
            this.lblAuthorId.Location = new System.Drawing.Point(27, 34);
            this.lblAuthorId.Name = "lblAuthorId";
            this.lblAuthorId.Size = new System.Drawing.Size(69, 16);
            this.lblAuthorId.TabIndex = 0;
            this.lblAuthorId.Text = "Mã tác giả";
            // 
            // dgvAuthor
            // 
            this.dgvAuthor.AllowUserToAddRows = false;
            this.dgvAuthor.AllowUserToDeleteRows = false;
            this.dgvAuthor.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAuthor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAuthor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colAuthorId,
            this.colName,
            this.colGender,
            this.colBio,
            this.colPhone,
            this.colEmail});
            this.dgvAuthor.Location = new System.Drawing.Point(91, 340);
            this.dgvAuthor.Name = "dgvAuthor";
            this.dgvAuthor.ReadOnly = true;
            this.dgvAuthor.RowHeadersWidth = 51;
            this.dgvAuthor.RowTemplate.Height = 24;
            this.dgvAuthor.Size = new System.Drawing.Size(799, 181);
            this.dgvAuthor.TabIndex = 2;
            this.dgvAuthor.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAuthor_CellClick);
            // 
            // colAuthorId
            // 
            this.colAuthorId.HeaderText = "ID";
            this.colAuthorId.MinimumWidth = 6;
            this.colAuthorId.Name = "colAuthorId";
            this.colAuthorId.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.HeaderText = "Tên Tác Giả";
            this.colName.MinimumWidth = 6;
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colGender
            // 
            this.colGender.HeaderText = "Giới Tính";
            this.colGender.MinimumWidth = 6;
            this.colGender.Name = "colGender";
            this.colGender.ReadOnly = true;
            // 
            // colBio
            // 
            this.colBio.HeaderText = "Địa Chỉ";
            this.colBio.MinimumWidth = 6;
            this.colBio.Name = "colBio";
            this.colBio.ReadOnly = true;
            // 
            // colPhone
            // 
            this.colPhone.HeaderText = "Số Điện Thoại";
            this.colPhone.MinimumWidth = 6;
            this.colPhone.Name = "colPhone";
            this.colPhone.ReadOnly = true;
            // 
            // colEmail
            // 
            this.colEmail.HeaderText = "Email";
            this.colEmail.MinimumWidth = 6;
            this.colEmail.Name = "colEmail";
            this.colEmail.ReadOnly = true;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(719, 98);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(719, 138);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(75, 23);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "Sửa";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(719, 178);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(75, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnRefesh
            // 
            this.btnRefesh.Location = new System.Drawing.Point(719, 218);
            this.btnRefesh.Name = "btnRefesh";
            this.btnRefesh.Size = new System.Drawing.Size(75, 23);
            this.btnRefesh.TabIndex = 3;
            this.btnRefesh.Text = "Tải Lại";
            this.btnRefesh.UseVisualStyleBackColor = true;
            this.btnRefesh.Click += new System.EventHandler(this.btnRefesh_Click);
            // 
            // errWarning
            // 
            this.errWarning.ContainerControl = this;
            // 
            // FormAuthor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 634);
            this.Controls.Add(this.btnRefesh);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvAuthor);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblTitle);
            this.Name = "FormAuthor";
            this.Text = "FormAuthor";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormCustomer_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuthor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errWarning)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblBio;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtBio;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.RadioButton optFemale;
        private System.Windows.Forms.RadioButton optMale;
        private System.Windows.Forms.Label lblSex;
        private System.Windows.Forms.Label lblAuthorId;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnRefesh;
        private System.Windows.Forms.DataGridView dgvAuthor;
        private System.Windows.Forms.ErrorProvider errWarning;
        private System.Windows.Forms.DataGridViewTextBoxColumn colAuthorId;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colGender;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBio;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPhone;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEmail;
    }
}