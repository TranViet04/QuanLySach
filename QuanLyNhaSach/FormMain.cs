using QuanLyNhaSach.Data;
using QuanLyNhaSach.Models;
using System;
using System.Windows.Forms;

namespace QuanLyNhaSach
{
    public partial class FormMain : Form
    {
        //===========================================
        // CONSTRUCTOR
        //===========================================
        public FormMain()
        {
            InitializeComponent();
            IsMdiContainer = true;
        }

        //===========================================
        // FORM EVENTS
        //===========================================
        private void FormMain_Load(object sender, EventArgs e)
        {
            timer1.Interval = 1000;
            timer1.Start();
            UpdateStatusBar();
            ApplyAuthorization();
            OpenLoginForm();
        }

        //===========================================
        // AUTHORIZATION & SESSION
        //===========================================
        public void ApplyAuthorization()
        {
            // ===============================
            // 1. KHÓA TOÀN BỘ (DEFAULT)
            // ===============================
            foreach (ToolStripMenuItem menu in menuStrip2.Items)
                menu.Visible = true;

            // Toolstrip menus
            mniBook.Visible = false;
            mniInvoice.Visible = false;
            mniInvoiceManagement.Visible = false;
            mniPurchaseOrder.Visible = false;
            mniRefresh.Visible = true;
            mniExit.Visible = true;

            // System
            mnuLogin.Visible = true;
            mnuLogout.Visible = false;
            mnuRoleManagement.Visible = false;
            mnuUserManagement.Visible = false;

            // Directory
            mnuDirectory.Visible = false;
            mnuBooks.Visible = false;
            mnuCategories.Visible = false;
            tácGiảToolStripMenuItem.Visible = false;
            mnuPublishers.Visible = false;
            mnuDistributor.Visible = false;
            mnuCustomers.Visible = false;

            // Operations
            mnuOperations.Visible = false;
            mnuSalesInvoices.Visible = false;
            quảnLýHóaĐơnToolStripMenuItem.Visible = false;
            mnuPurchasing.Visible = false;
            mnuManagePurchaseOrders.Visible = false;
            mnuStocktaking.Visible = false;

            // Reports
            mnuStatisticsReports.Visible = false;
            mnuMonthlySales.Visible = false;
            mnuSalesByGenre.Visible = false;
            StockReport.Visible = false;

            // Help
            mnuHelp.Visible = true;
            mnuUserGuide.Visible = true;
            mnuAbout.Visible = true;

            // ===============================
            // 2. CHƯA ĐĂNG NHẬP → DỪNG
            // ===============================
            if (!CurrentUser.IsAuthenticated)
                return;

            // Đã login
            mnuLogout.Visible = true;

            bool isAdmin = AuthorizationHelper.IsAdmin();
            bool isManager = AuthorizationHelper.IsManager();
            bool isStaff = AuthorizationHelper.IsStaff();

            // ===============================
            // 3. ADMIN
            // ===============================
            if (isAdmin)
            {
                // Toolstrip menus
                mniBook.Visible = true;
                mniInvoice.Visible = true;
                mniInvoiceManagement.Visible = true;
                mniPurchaseOrder.Visible = true;   

                // System
                mnuSystem.Visible = true;
                mnuRoleManagement.Visible = true;
                mnuUserManagement.Visible = true;

                // Directory
                mnuDirectory.Visible = true;
                mnuBooks.Visible = true;
                mnuCategories.Visible = true;
                tácGiảToolStripMenuItem.Visible = true;
                mnuPublishers.Visible = true;
                mnuDistributor.Visible = true;
                mnuCustomers.Visible = true;

                // Operations
                mnuOperations.Visible = true;
                mnuSalesInvoices.Visible = true;
                quảnLýHóaĐơnToolStripMenuItem.Visible = true;
                mnuPurchasing.Visible = true;
                mnuManagePurchaseOrders.Visible = true;
                mnuStocktaking.Visible = true;

                // Reports
                mnuStatisticsReports.Visible = true;
                mnuMonthlySales.Visible = true;
                mnuSalesByGenre.Visible = true;
                StockReport.Visible = true;

                return;
            }

            // ===============================
            // 4. MANAGER
            // ===============================
            if (isManager)
            {
                mnuSystem.Visible = true;

                // Directory
                mnuDirectory.Visible = true;
                mnuBooks.Visible = true;
                mnuCategories.Visible = true;
                tácGiảToolStripMenuItem.Visible = true;
                mnuPublishers.Visible = true;
                mnuDistributor.Visible = true;
                mnuCustomers.Visible = true;

                // Operations
                mnuOperations.Visible = true;
                mnuSalesInvoices.Visible = true;
                quảnLýHóaĐơnToolStripMenuItem.Visible = true;
                mnuPurchasing.Visible = true;
                mnuManagePurchaseOrders.Visible = true;
                mnuStocktaking.Visible = true;

                // Reports
                mnuStatisticsReports.Visible = true;
                mnuMonthlySales.Visible = true;
                mnuSalesByGenre.Visible = true;
                StockReport.Visible = true;

                return;
            }

            // ===============================
            // 5. STAFF
            // ===============================
            if (isStaff)
            {
                mnuSystem.Visible = true;

                // Directory
                mnuDirectory.Visible = true;
                mnuBooks.Visible = true;
                mnuCustomers.Visible = true;

                // Operations
                mnuOperations.Visible = true;
                mnuSalesInvoices.Visible = true;

                return;
            }
        }


        //===========================================
        // MDI NAVIGATION
        //===========================================
        public void OpenLoginForm()
        {
            CloseAllMdiChildren();

            var login = new FormLogin
            {
                MdiParent = this
            };
            login.Show();
        }

        public void OpenRegisterForm(FormLogin fromLogin)
        {
            CloseAllMdiChildren();

            var register = new FormRegister
            {
                MdiParent = this,
                LoginForm = fromLogin
            };
            register.Show();
        }

        private void CloseAllMdiChildren()
        {
            foreach (Form f in MdiChildren)
                f.Close();
        }

        //===========================================
        // STATUS BAR
        //===========================================
        public void UpdateStatusBar()
        {
            tslUser.Text = "User: " + CurrentUser.Username;
            tslRole.Text = "Role: " + CurrentUser.RoleDisplay;
            tslDb.Text = CheckConnection();
        }

        private string CheckConnection()
        {
            try
            {
                using (var db = new BookStoreContext())
                {
                    return db.Database.Exists()
                        ? "DB Connected"
                        : "DB Not Found";
                }
            }
            catch
            {
                return "DB Error";
            }
        }

        //===========================================
        // MENU AUTH
        //===========================================
        private void mnuLogin_Click(object sender, EventArgs e)
        {
            OpenLoginForm();
        }

        private void mnuRegister_Click(object sender, EventArgs e)
        {
            OpenRegisterForm(null);
        }

        private void mnuLogout_Click(object sender, EventArgs e)
        {
            CurrentUser.Reset();
            ApplyAuthorization();
            UpdateStatusBar();
            OpenLoginForm();
        }

        //===========================================
        // TIMER
        //===========================================
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            tslTime.Text = "Time: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        //===========================================
        // BUSINESS FORMS
        //===========================================
        private void mnuBooks_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormBook());
        }

        private void mnuCategories_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormCategory());
        }

        private void mnuCustomers_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormCustomer());
        }

        private void mnuPublishers_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormPublisher());
        }

        private void mnuDistributor_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormDistributor());
        }

        private void mnuRoleManagement_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormRoleManagement());
        }

        private void mnuUserManagement_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormUserManagement());
        }

        //===========================================
        // COMMON HELPER
        //===========================================
        private void OpenChildForm(Form form)
        {
            CloseAllMdiChildren();
            form.MdiParent = this;
            form.Show();
        }

        private void tácGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormAuthor());
        }

        private void mnuSalesInvoices_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormInvoice());
        }

        private void quảnLýHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormInvoiceManagement());
        }

        private void mnuPurchasing_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormPurchaseOrder());
        }

        private void mnuManagePurchaseOrders_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormPurchaseOrderList());
        }

        private void mnuStocktaking_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormStockTaking());
        }

        private void mnuMonthlySales_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmThongKeTheoThang());
        }

        private void mnuSalesByGenre_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmThongKeTheoTheLoai());
        }

        private void StockReport_Click(object sender, EventArgs e)
        {
            var form = new FormReport("Inventory");
            form.ShowDialog();
        }

        private void menuBaoCaoDoanhThu_Click(object sender, EventArgs e)
        {
            // Mặc định: Tháng hiện tại
            var param = new
            {
                FromDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                ToDate = DateTime.Now
            };

            var form = new FormReport("Revenue", param);
            form.ShowDialog();
        }

        private void mnuUserGuide_Click(object sender, EventArgs e)
        {
            var form = new FormUserGuide();
            form.ShowDialog();
        }

        private void mnuAbout_Click(object sender, EventArgs e)
        {
            var form = new FormAbout();
            form.ShowDialog();
        }

        private void mniRefresh_Click(object sender, EventArgs e)
        {
            FormMain_Load(sender, e);
            // Refresh the main form
            ApplyAuthorization();
            UpdateStatusBar();
            
        }

        private void mniExit_Click(object sender, EventArgs e)
        {
            CloseAllMdiChildren();
            Application.Exit();

        }
    }
}
