using System;
using System.Windows.Forms;

namespace QuanLyNhaSach
{
    public partial class FormUserGuide : Form
    {
        public FormUserGuide()
        {
            InitializeComponent();
        }

        private void FormUserGuide_Load(object sender, EventArgs e)
        {
            LoadUserGuide();
        }

        private void LoadUserGuide()
        {
            string guide = @"
════════════════════════════════════════════════════════════════
        HƯỚNG DẪN SỬ DỤNG PHẦN MỀM QUẢN LÝ NHÀ SÁCH
════════════════════════════════════════════════════════════════

I. GIỚI THIỆU CHUNG
------------------------------------------------
Phần mềm Quản Lý Nhà Sách được xây dựng nhằm hỗ trợ các nghiệp vụ:
- Quản lý danh mục sách
- Quản lý bán hàng – nhập hàng
- Theo dõi tồn kho
- Thống kê và báo cáo doanh thu

------------------------------------------------
II. ĐĂNG NHẬP HỆ THỐNG
------------------------------------------------
• Menu: Hệ Thống → Đăng Nhập  
• Người dùng nhập Tên đăng nhập và Mật khẩu  
• Hệ thống phân quyền theo 3 vai trò:
  - Admin: Toàn quyền hệ thống
  - Manager: Quản lý nghiệp vụ, báo cáo
  - Staff: Bán hàng, nhập hàng cơ bản

------------------------------------------------
III. QUẢN LÝ DANH MỤC
------------------------------------------------

1. Quản lý Sách  
   • Menu: Danh Mục → Sách  
   • Chức năng:
     - Thêm / Sửa / Xóa thông tin sách
     - Tìm kiếm theo tên, mã, thể loại
     - Xem danh sách sách hiện có

2. Quản lý Thể Loại  
   • Menu: Danh Mục → Thể Loại  
   • Quản lý các nhóm sách (Văn học, Kỹ năng, Giáo khoa, …)

3. Quản lý Tác Giả  
   • Menu: Danh Mục → Tác Giả  
   • Lưu thông tin tác giả phục vụ quản lý sách

4. Quản lý Nhà Xuất Bản  
   • Menu: Danh Mục → Nhà Xuất Bản  
   • Lưu thông tin đơn vị phát hành

5. Quản lý Khách Hàng  
   • Menu: Danh Mục → Khách Hàng  
   • Lưu thông tin khách hàng phục vụ lập hóa đơn

------------------------------------------------
IV. NGHIỆP VỤ BÁN HÀNG – KHO
------------------------------------------------

1. Lập Hóa Đơn Bán Hàng  
   • Menu: Nghiệp Vụ → Bán Hàng / Hóa Đơn  
   • Các bước:
     - Chọn khách hàng
     - Thêm sách và số lượng
     - Thanh toán
     - In hóa đơn
   • Sau khi thanh toán, tồn kho được tự động cập nhật

2. Quản Lý Hóa Đơn  
   • Menu: Nghiệp Vụ → Quản Lý Hóa Đơn  
   • Chức năng:
     - Xem danh sách hóa đơn
     - Lọc theo ngày, trạng thái
     - In lại hóa đơn

3. Nhập Hàng  
   • Menu: Nghiệp Vụ → Nhập Hàng  
   • Các bước:
     - Chọn nhà cung cấp
     - Chọn sách cần nhập
     - Nhập số lượng và giá nhập
     - Lưu phiếu nhập
   • Hệ thống tự động tăng số lượng tồn kho

4. Kiểm Kho  
   • Menu: Nghiệp Vụ → Kiểm Kho  
   • So sánh:
     - Số lượng tồn trên hệ thống
     - Số lượng thực tế
   • Ghi nhận chênh lệch để phục vụ quản lý kho

------------------------------------------------
V. THỐNG KÊ – BÁO CÁO
------------------------------------------------

1. Doanh Thu Theo Tháng  
   • Menu: Thống Kê & Báo Cáo → Doanh Thu Theo Tháng  
   • Hiển thị tổng doanh thu theo thời gian

2. Doanh Thu Theo Thể Loại  
   • Menu: Thống Kê & Báo Cáo → Doanh Thu Theo Thể Loại  
   • Phân tích doanh thu theo nhóm sách

3. Báo Cáo Tồn Kho  
   • Menu: Thống Kê & Báo Cáo → Báo Cáo Tồn Kho  
   • Theo dõi:
     - Sách còn nhiều
     - Sách sắp hết
     - Sách hết hàng

4. Xuất Báo Cáo  
   • Hỗ trợ xuất dữ liệu ra Excel
   • Phục vụ lưu trữ và in ấn

------------------------------------------------
VI. PHÍM TẮT HỖ TRỢ
------------------------------------------------
...

------------------------------------------------
VII. LƯU Ý KHI SỬ DỤNG
------------------------------------------------
• Đăng xuất sau khi kết thúc ca làm việc  
• Không chia sẻ tài khoản  
• Sao lưu dữ liệu định kỳ  
• Chỉ Admin được thay đổi cấu hình hệ thống

════════════════════════════════════════════════════════════════
";

            txtGuide.Text = guide;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}