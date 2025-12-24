namespace QuanLyNhaSach
{
    partial class FormRoleManagement
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
            this.lblRoleName = new System.Windows.Forms.Label();
            this.txtRoleName = new System.Windows.Forms.TextBox();
            this.dgvRoles = new System.Windows.Forms.DataGridView();
            this.btnRefesh = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDecription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblRoleName
            // 
            this.lblRoleName.AutoSize = true;
            this.lblRoleName.Location = new System.Drawing.Point(150, 136);
            this.lblRoleName.Name = "lblRoleName";
            this.lblRoleName.Size = new System.Drawing.Size(74, 16);
            this.lblRoleName.TabIndex = 0;
            this.lblRoleName.Text = "Tên quyền:";
            // 
            // txtRoleName
            // 
            this.txtRoleName.Location = new System.Drawing.Point(230, 130);
            this.txtRoleName.Name = "txtRoleName";
            this.txtRoleName.Size = new System.Drawing.Size(372, 22);
            this.txtRoleName.TabIndex = 1;
            // 
            // dgvRoles
            // 
            this.dgvRoles.AllowUserToAddRows = false;
            this.dgvRoles.AllowUserToDeleteRows = false;
            this.dgvRoles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRoles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRoles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colID,
            this.colName,
            this.colDecription});
            this.dgvRoles.Location = new System.Drawing.Point(96, 317);
            this.dgvRoles.MultiSelect = false;
            this.dgvRoles.Name = "dgvRoles";
            this.dgvRoles.ReadOnly = true;
            this.dgvRoles.RowHeadersWidth = 51;
            this.dgvRoles.RowTemplate.Height = 24;
            this.dgvRoles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRoles.Size = new System.Drawing.Size(609, 150);
            this.dgvRoles.TabIndex = 2;
            this.dgvRoles.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRoles_CellClick);
            // 
            // btnRefesh
            // 
            this.btnRefesh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefesh.Location = new System.Drawing.Point(503, 255);
            this.btnRefesh.Margin = new System.Windows.Forms.Padding(4);
            this.btnRefesh.Name = "btnRefesh";
            this.btnRefesh.Size = new System.Drawing.Size(112, 31);
            this.btnRefesh.TabIndex = 4;
            this.btnRefesh.Text = "Tải Lại";
            this.btnRefesh.UseVisualStyleBackColor = true;
            this.btnRefesh.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(383, 255);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(112, 31);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdate.Location = new System.Drawing.Point(263, 255);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(112, 31);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Sửa";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(143, 255);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(112, 31);
            this.btnAdd.TabIndex = 7;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 22.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(120, 52);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(554, 42);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "QUẢN LÝ QUYỀN HỆ THỐNG";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(150, 188);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(43, 16);
            this.lblDescription.TabIndex = 0;
            this.lblDescription.Text = "Mô tả:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(230, 185);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(372, 22);
            this.txtDescription.TabIndex = 1;
            // 
            // colID
            // 
            this.colID.DataPropertyName = "RoleId";
            this.colID.HeaderText = "ID";
            this.colID.MinimumWidth = 6;
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Tên Quyền ";
            this.colName.MinimumWidth = 6;
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colDecription
            // 
            this.colDecription.DataPropertyName = "Description";
            this.colDecription.HeaderText = "Mô tả";
            this.colDecription.MinimumWidth = 6;
            this.colDecription.Name = "colDecription";
            this.colDecription.ReadOnly = true;
            // 
            // FormRoleManagement
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 513);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnRefesh);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.dgvRoles);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtRoleName);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblRoleName);
            this.Name = "FormRoleManagement";
            this.Text = "FormRoleManagement";
            this.Load += new System.EventHandler(this.FormRoleManagement_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRoleName;
        private System.Windows.Forms.TextBox txtRoleName;
        private System.Windows.Forms.DataGridView dgvRoles;
        private System.Windows.Forms.Button btnRefesh;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDecription;
    }
}