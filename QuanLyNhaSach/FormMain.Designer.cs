namespace QuanLyNhaSach
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.mnuSystem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRoleManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUserManagement = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDirectory = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCategories = new System.Windows.Forms.ToolStripMenuItem();
            this.tácGiảToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPublishers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDistributor = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCustomers = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOperations = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSalesInvoices = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýHóaĐơnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPurchasing = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuManagePurchaseOrders = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStocktaking = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStatisticsReports = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMonthlySales = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSalesByGenre = new System.Windows.Forms.ToolStripMenuItem();
            this.StockReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExportToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuUserGuide = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tslUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslRole = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslDb = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mniBook = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.directoryEntry1 = new System.DirectoryServices.DirectoryEntry();
            this.menuStrip2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip2
            // 
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSystem,
            this.mnuDirectory,
            this.mnuOperations,
            this.mnuStatisticsReports,
            this.mnuHelp});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1054, 28);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // mnuSystem
            // 
            this.mnuSystem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLogin,
            this.mnuLogout,
            this.mnuRoleManagement,
            this.mnuUserManagement});
            this.mnuSystem.Name = "mnuSystem";
            this.mnuSystem.Size = new System.Drawing.Size(88, 24);
            this.mnuSystem.Text = "Hệ Thống";
            // 
            // mnuLogin
            // 
            this.mnuLogin.Name = "mnuLogin";
            this.mnuLogin.Size = new System.Drawing.Size(223, 26);
            this.mnuLogin.Text = "Đăng Nhập";
            this.mnuLogin.Click += new System.EventHandler(this.mnuLogin_Click);
            // 
            // mnuLogout
            // 
            this.mnuLogout.Name = "mnuLogout";
            this.mnuLogout.Size = new System.Drawing.Size(223, 26);
            this.mnuLogout.Text = "Đăng Xuất";
            this.mnuLogout.Click += new System.EventHandler(this.mnuLogout_Click);
            // 
            // mnuRoleManagement
            // 
            this.mnuRoleManagement.Name = "mnuRoleManagement";
            this.mnuRoleManagement.Size = new System.Drawing.Size(223, 26);
            this.mnuRoleManagement.Text = "Quản lý quyền";
            this.mnuRoleManagement.Click += new System.EventHandler(this.mnuRoleManagement_Click);
            // 
            // mnuUserManagement
            // 
            this.mnuUserManagement.Name = "mnuUserManagement";
            this.mnuUserManagement.Size = new System.Drawing.Size(223, 26);
            this.mnuUserManagement.Text = "Quản lý người dùng";
            this.mnuUserManagement.Click += new System.EventHandler(this.mnuUserManagement_Click);
            // 
            // mnuDirectory
            // 
            this.mnuDirectory.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuBooks,
            this.mnuCategories,
            this.tácGiảToolStripMenuItem,
            this.mnuPublishers,
            this.mnuDistributor,
            this.mnuCustomers});
            this.mnuDirectory.Name = "mnuDirectory";
            this.mnuDirectory.Size = new System.Drawing.Size(90, 24);
            this.mnuDirectory.Text = "Danh Mục";
            // 
            // mnuBooks
            // 
            this.mnuBooks.Name = "mnuBooks";
            this.mnuBooks.Size = new System.Drawing.Size(191, 26);
            this.mnuBooks.Text = "Sách";
            this.mnuBooks.Click += new System.EventHandler(this.mnuBooks_Click);
            // 
            // mnuCategories
            // 
            this.mnuCategories.Name = "mnuCategories";
            this.mnuCategories.Size = new System.Drawing.Size(191, 26);
            this.mnuCategories.Text = "Thể Loại";
            this.mnuCategories.Click += new System.EventHandler(this.mnuCategories_Click);
            // 
            // tácGiảToolStripMenuItem
            // 
            this.tácGiảToolStripMenuItem.Name = "tácGiảToolStripMenuItem";
            this.tácGiảToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.tácGiảToolStripMenuItem.Text = "Tác Giả";
            this.tácGiảToolStripMenuItem.Click += new System.EventHandler(this.tácGiảToolStripMenuItem_Click);
            // 
            // mnuPublishers
            // 
            this.mnuPublishers.Name = "mnuPublishers";
            this.mnuPublishers.Size = new System.Drawing.Size(191, 26);
            this.mnuPublishers.Text = "Nhà Xuất Bản";
            this.mnuPublishers.Click += new System.EventHandler(this.mnuPublishers_Click);
            // 
            // mnuDistributor
            // 
            this.mnuDistributor.Name = "mnuDistributor";
            this.mnuDistributor.Size = new System.Drawing.Size(191, 26);
            this.mnuDistributor.Text = "Nhà Phát Hành";
            this.mnuDistributor.Click += new System.EventHandler(this.mnuDistributor_Click);
            // 
            // mnuCustomers
            // 
            this.mnuCustomers.Name = "mnuCustomers";
            this.mnuCustomers.Size = new System.Drawing.Size(191, 26);
            this.mnuCustomers.Text = "Khách Hàng";
            this.mnuCustomers.Click += new System.EventHandler(this.mnuCustomers_Click);
            // 
            // mnuOperations
            // 
            this.mnuOperations.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSalesInvoices,
            this.quảnLýHóaĐơnToolStripMenuItem,
            this.mnuPurchasing,
            this.mnuManagePurchaseOrders,
            this.mnuStocktaking});
            this.mnuOperations.Name = "mnuOperations";
            this.mnuOperations.Size = new System.Drawing.Size(93, 24);
            this.mnuOperations.Text = "Nghiệp Vụ";
            // 
            // mnuSalesInvoices
            // 
            this.mnuSalesInvoices.Name = "mnuSalesInvoices";
            this.mnuSalesInvoices.Size = new System.Drawing.Size(231, 26);
            this.mnuSalesInvoices.Text = "Bán Hàng / Hóa Đơn";
            this.mnuSalesInvoices.Click += new System.EventHandler(this.mnuSalesInvoices_Click);
            // 
            // quảnLýHóaĐơnToolStripMenuItem
            // 
            this.quảnLýHóaĐơnToolStripMenuItem.Name = "quảnLýHóaĐơnToolStripMenuItem";
            this.quảnLýHóaĐơnToolStripMenuItem.Size = new System.Drawing.Size(231, 26);
            this.quảnLýHóaĐơnToolStripMenuItem.Text = "Quản Lý Hóa Đơn";
            this.quảnLýHóaĐơnToolStripMenuItem.Click += new System.EventHandler(this.quảnLýHóaĐơnToolStripMenuItem_Click);
            // 
            // mnuPurchasing
            // 
            this.mnuPurchasing.Name = "mnuPurchasing";
            this.mnuPurchasing.Size = new System.Drawing.Size(231, 26);
            this.mnuPurchasing.Text = "Nhập Hàng";
            this.mnuPurchasing.Click += new System.EventHandler(this.mnuPurchasing_Click);
            // 
            // mnuManagePurchaseOrders
            // 
            this.mnuManagePurchaseOrders.Name = "mnuManagePurchaseOrders";
            this.mnuManagePurchaseOrders.Size = new System.Drawing.Size(231, 26);
            this.mnuManagePurchaseOrders.Text = "Quản Lý Phiếu Nhập";
            this.mnuManagePurchaseOrders.Click += new System.EventHandler(this.mnuManagePurchaseOrders_Click);
            // 
            // mnuStocktaking
            // 
            this.mnuStocktaking.Name = "mnuStocktaking";
            this.mnuStocktaking.Size = new System.Drawing.Size(231, 26);
            this.mnuStocktaking.Text = "Kiểm Kho";
            this.mnuStocktaking.Click += new System.EventHandler(this.mnuStocktaking_Click);
            // 
            // mnuStatisticsReports
            // 
            this.mnuStatisticsReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMonthlySales,
            this.mnuSalesByGenre,
            this.StockReport,
            this.mnuExportToExcel});
            this.mnuStatisticsReports.Name = "mnuStatisticsReports";
            this.mnuStatisticsReports.Size = new System.Drawing.Size(150, 24);
            this.mnuStatisticsReports.Text = "Thống Kê & Báo Cáo";
            // 
            // mnuMonthlySales
            // 
            this.mnuMonthlySales.Name = "mnuMonthlySales";
            this.mnuMonthlySales.Size = new System.Drawing.Size(261, 26);
            this.mnuMonthlySales.Text = "Doanh Thu Theo Tháng";
            this.mnuMonthlySales.Click += new System.EventHandler(this.mnuMonthlySales_Click);
            // 
            // mnuSalesByGenre
            // 
            this.mnuSalesByGenre.Name = "mnuSalesByGenre";
            this.mnuSalesByGenre.Size = new System.Drawing.Size(261, 26);
            this.mnuSalesByGenre.Text = "Doanh Thu Theo Thể Loại";
            this.mnuSalesByGenre.Click += new System.EventHandler(this.mnuSalesByGenre_Click);
            // 
            // StockReport
            // 
            this.StockReport.Name = "StockReport";
            this.StockReport.Size = new System.Drawing.Size(261, 26);
            this.StockReport.Text = "Báo Cáo Tồn Kho";
            // 
            // mnuExportToExcel
            // 
            this.mnuExportToExcel.Name = "mnuExportToExcel";
            this.mnuExportToExcel.Size = new System.Drawing.Size(261, 26);
            this.mnuExportToExcel.Text = "Xuất Excel";
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuUserGuide,
            this.mnuAbout});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(79, 24);
            this.mnuHelp.Text = "Trợ Giúp";
            // 
            // mnuUserGuide
            // 
            this.mnuUserGuide.Name = "mnuUserGuide";
            this.mnuUserGuide.Size = new System.Drawing.Size(173, 26);
            this.mnuUserGuide.Text = "Hướng Dẫn ";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(173, 26);
            this.mnuAbout.Text = "Giới Thiệu";
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslUser,
            this.tslRole,
            this.tslTime,
            this.tslDb});
            this.statusStrip1.Location = new System.Drawing.Point(0, 711);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1054, 26);
            this.statusStrip1.TabIndex = 4;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tslUser
            // 
            this.tslUser.Name = "tslUser";
            this.tslUser.Size = new System.Drawing.Size(38, 20);
            this.tslUser.Text = "User";
            // 
            // tslRole
            // 
            this.tslRole.Name = "tslRole";
            this.tslRole.Size = new System.Drawing.Size(39, 20);
            this.tslRole.Text = "Role";
            // 
            // tslTime
            // 
            this.tslTime.Name = "tslTime";
            this.tslTime.Size = new System.Drawing.Size(42, 20);
            this.tslTime.Text = "Time";
            // 
            // tslDb
            // 
            this.tslDb.Name = "tslDb";
            this.tslDb.Size = new System.Drawing.Size(29, 20);
            this.tslDb.Text = "Db";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniBook,
            this.toolStripButton1,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripButton6});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1054, 27);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // mniBook
            // 
            this.mniBook.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mniBook.Image = ((System.Drawing.Image)(resources.GetObject("mniBook.Image")));
            this.mniBook.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mniBook.Name = "mniBook";
            this.mniBook.Size = new System.Drawing.Size(29, 24);
            this.mniBook.Text = "Thêm Sách";
            this.mniBook.Click += new System.EventHandler(this.mnuBooks_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton1.Text = "Bán Hàng";
            this.toolStripButton1.Click += new System.EventHandler(this.mnuSalesInvoices_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton3.Text = "Hóa Đơn";
            this.toolStripButton3.Click += new System.EventHandler(this.quảnLýHóaĐơnToolStripMenuItem_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton4.Text = "Nhập Hàng";
            this.toolStripButton4.Click += new System.EventHandler(this.mnuPurchasing_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton5.Text = "Tải Lại";
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton6.Text = "Thoát";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1054, 737);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip2);
            this.IsMdiContainer = true;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem mnuSystem;
        private System.Windows.Forms.ToolStripMenuItem mnuLogin;
        private System.Windows.Forms.ToolStripMenuItem mnuDirectory;
        private System.Windows.Forms.ToolStripMenuItem mnuBooks;
        private System.Windows.Forms.ToolStripMenuItem mnuCategories;
        private System.Windows.Forms.ToolStripMenuItem mnuPublishers;
        private System.Windows.Forms.ToolStripMenuItem mnuOperations;
        private System.Windows.Forms.ToolStripMenuItem mnuSalesInvoices;
        private System.Windows.Forms.ToolStripMenuItem quảnLýHóaĐơnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuPurchasing;
        private System.Windows.Forms.ToolStripMenuItem mnuManagePurchaseOrders;
        private System.Windows.Forms.ToolStripMenuItem mnuStocktaking;
        private System.Windows.Forms.ToolStripMenuItem mnuStatisticsReports;
        private System.Windows.Forms.ToolStripMenuItem mnuMonthlySales;
        private System.Windows.Forms.ToolStripMenuItem mnuSalesByGenre;
        private System.Windows.Forms.ToolStripMenuItem StockReport;
        private System.Windows.Forms.ToolStripMenuItem mnuExportToExcel;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuUserGuide;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuLogout;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tslUser;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton mniBook;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripStatusLabel tslRole;
        private System.Windows.Forms.ToolStripStatusLabel tslTime;
        private System.Windows.Forms.ToolStripStatusLabel tslDb;
        private System.Windows.Forms.ToolStripMenuItem tácGiảToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuRoleManagement;
        private System.Windows.Forms.ToolStripMenuItem mnuUserManagement;
        private System.Windows.Forms.ToolStripMenuItem mnuDistributor;
        private System.Windows.Forms.ToolStripMenuItem mnuCustomers;
        private System.DirectoryServices.DirectoryEntry directoryEntry1;
    }
}