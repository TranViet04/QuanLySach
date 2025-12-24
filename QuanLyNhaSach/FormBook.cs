using QuanLyNhaSach.Data; // for BookStoreContext
using QuanLyNhaSach.Models; // for Book, Category, Author, Publisher
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

/*
 
 */

namespace QuanLyNhaSach
{
    public partial class FormBook : Form
    {
        private BindingSource categoryBinding = new BindingSource(); //Để liên kết danh mục với ComboBox
        private BindingSource authorBinding = new BindingSource();
        private BindingSource publisherBinding = new BindingSource(); //Để liên kết nhà xuất bản với ComboBox
        private BindingSource distributorBinding = new BindingSource(); //Để liên kết nhà phân phối với ComboBox
        private string currentCoverPath = null;

        private CultureInfo vnCulture = new CultureInfo("vi-VN");    //Để định dạng tiền Việt Nam

        public FormBook()
        {
            InitializeComponent();


        // start with edit/delete disabled
        IsEnableEditDelete(false);
        }
        private void IsEnableEditDelete(bool isEnabled) // enable or disable edit/delete buttons
        {
            btnUpdate.Enabled = isEnabled;
            btnDelete.Enabled = isEnabled;
            btnAdd.Enabled = !isEnabled;
        }

        private void FormBook_Load(object sender, EventArgs e)
        {
            LoadCategories();
            LoadAuthors();
            LoadPublishers();
            LoadBooks();

            // ensure buttons state after load
            IsEnableEditDelete(false);
        }

        public class CategoryVM
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }



        public void ReloadCategories() // dùng để tải lại danh mục từ FormCategory và cập nhật cmbCategory
        {
            if (this.IsDisposed || this.Disposing) return; // tránh lỗi khi form đã bị đóng
            // kiểm tra nếu gọi từ luồng khác thì sử dụng BeginInvoke để gọi lại trên luồng giao diện người dùng
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => { try { LoadCategories(); } catch { } }));
            }
            else { try { LoadCategories(); } catch { } }
        }


        private void LoadCategories()
        {
            try
            {
                using (var db = new BookStoreContext())
                {
                    var categories = db.Categories
                        .OrderBy(c => c.Name)
                        .Select(c => new CategoryVM
                        {
                            Id = c.CategoryId,
                            Name = c.Name
                        })
                        .ToList();

                    // 👉 ITEM GIẢ - đúng wireframe
                    categories.Insert(0, new CategoryVM
                    {
                        Id = 0,
                        Name = "-- Chọn Thể loại --"
                    });

                    cmbCategory.DataSource = categories;
                    cmbCategory.DisplayMember = "Name";
                    cmbCategory.ValueMember = "Id";
                    cmbCategory.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh mục: " + ex.Message);
            }
        }


        private void LoadAuthors() // load authors into cmbAuthor with autocomplete
        {
            try
            {
                using (var db = new BookStoreContext())
                {
                    var authors = db.Authors.OrderBy(a => a.Name).ToList();
                    authorBinding.DataSource = authors;
                    cmbAuthor.DataSource = authorBinding;
                    cmbAuthor.DisplayMember = "Name";
                    cmbAuthor.ValueMember = "AuthorId";

                    if (authors.Count > 0) cmbAuthor.SelectedIndex = 0;
                    else cmbAuthor.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải tác giả: " + ex.Message);
            }

        }

        private void LoadPublishers() // load publishers into cmbPublisher with autocomplete
        {
            try
            {
                using (var db = new BookStoreContext())
                {
                    var publishers = db.Publishers.OrderBy(p => p.Name).ToList();
                    publisherBinding.DataSource = publishers;
                    cmbPublisher.DataSource = publisherBinding;
                    cmbPublisher.DisplayMember = "Name";
                    cmbPublisher.ValueMember = "PublisherId";

                    if (publishers.Count > 0) cmbPublisher.SelectedIndex = 0;
                    else cmbPublisher.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải nhà xuất bản: " + ex.Message);
            }
        }

        private void LoadBooks() // load books into dataGridView1
        {
            try
            {
                dgvBook.Rows.Clear();
                using (var db = new BookStoreContext())
                {
                    // dùng var để lấy danh sách sách cùng với thông tin liên quan
                    var list = db.Books
                        .Include("Author")
                        .Include("Category")
                        .Include("Publisher")
                        .Include("Distributor")
                        .Select(b => new
                        {
                            b.BookId,
                            b.Title,
                            Author = b.Author != null ? b.Author.Name : "",
                            Category = b.Category != null ? b.Category.Name : "",
                            Publisher = b.Publisher != null ? b.Publisher.Name : "",
                            Distributor = b.Distributor != null ? b.Distributor.DistributorName : "",
                            b.PublishYear,
                            b.Price,
                            b.CoverImagePath

                        })
                        .ToList();

                    foreach (var b in list) // add rows to DataGridView 
                    {
                        // include Distributor and CoverImagePath
                        int rowIndex = dgvBook.Rows.Add(b.BookId, b.Title, b.Author, b.Category, b.Publisher, b.Distributor, b.PublishYear, b.Price.ToString("N0", vnCulture), b.CoverImagePath);
                        dgvBook.Rows[rowIndex].Tag = b.BookId;
                    }

                    // Ẩn cột BookId khỏi DataGridView nếu nó tồn tại
                    try
                    {
                        if (dgvBook.Columns.Contains("BookId"))
                            dgvBook.Columns["BookId"].Visible = false;
                    }
                    catch { }
                }

                // clear selection to match FormCategory behaviour
                dgvBook.ClearSelection();

                // ensure buttons disabled after loading fresh list
                IsEnableEditDelete(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải sách: " + ex.Message);
            }
        }

        private void LoadCoverImage(string path)
        {
            string defaultImage = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Images",
                "book-placeholder.png"
            );

            string imageToLoad =
                !string.IsNullOrWhiteSpace(path) && File.Exists(path)
                ? path
                : defaultImage;

            try
            {
                using (var fs = new FileStream(imageToLoad, FileMode.Open, FileAccess.Read))
                {
                    pictureBoxCover.Image = Image.FromStream(fs);
                }
            }
            catch
            {
                pictureBoxCover.Image = null;
            }
        }


        private void ClearForm() //Dùng để xóa trắng các trường nhập liệu
        {
            // do not clear txtBookId here; keep it showing selected/generated id when appropriate
            txtBookName.Clear();
            // cmbAuthor was used instead of a txtAuthor TextBox; set text to empty
            cmbAuthor.Text = string.Empty;
            cmbPublisher.SelectedIndex = -1;
            txtYear.Clear();
            txtPrice.Clear();
            currentCoverPath = null;
            LoadCoverImage(null);
            cmbCategory.SelectedIndex = cmbCategory.Items.Count > 0 ? 0 : -1;

            // clear previous error provider messages and reset backcolors
            try
            {
                errWarning.SetError(txtBookName, "");
                errWarning.SetError(cmbAuthor, "");
                errWarning.SetError(cmbPublisher, "");
                errWarning.SetError(cmbCategory, "");
                errWarning.SetError(txtYear, "");
                errWarning.SetError(txtPrice, "");

                txtBookName.BackColor = System.Drawing.Color.White;
                txtYear.BackColor = System.Drawing.Color.White;
                txtPrice.BackColor = System.Drawing.Color.White;
                cmbAuthor.BackColor = System.Drawing.Color.White;
                cmbPublisher.BackColor = System.Drawing.Color.White;
                cmbCategory.BackColor = System.Drawing.Color.White;
            }
            catch { }

            // disable edit/delete when form is cleared
            IsEnableEditDelete(false);
        }

        private void dgvListBooks_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBook.SelectedRows.Count <= 0)
            {
                ClearForm();
                txtBookId.Text = string.Empty;
                return;
            }
            
            var row = dgvBook.SelectedRows[0];
            var idObj = row.Cells["BookId"].Value ?? row.Tag; // fallback to Tag if cell is null
            txtBookId.Text = idObj?.ToString() ?? string.Empty;
            txtBookName.Text = row.Cells["Title"].Value?.ToString() ?? string.Empty;

            // load cover image
            string imgPath = row.Cells["CoverImagePath"].Value?.ToString();
            currentCoverPath = string.IsNullOrWhiteSpace(imgPath) ? null : imgPath;
            LoadCoverImage(currentCoverPath);


            cmbAuthor.Text = row.Cells["Author"].Value?.ToString() ?? string.Empty;
            
            SelectPublisherByName(row.Cells["Publisher"].Value?.ToString() ?? string.Empty);
   
            txtYear.Text = row.Cells["PublishYear"].Value?.ToString() ?? string.Empty;
            var priceStr = row.Cells["Price"].Value?.ToString() ?? string.Empty;
            txtPrice.Text = priceStr.Replace("\u00A0", "").Replace(",", ""); 
            cmbCategory.Text = row.Cells["Category"].Value?.ToString() ?? string.Empty;

            // enable edit/delete when a valid row is selected
            IsEnableEditDelete(true);
        }

        private void dgvBook_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                dgvBook.Rows[e.RowIndex].Selected = true;
            }
        }

        // =============================
        // Button event handlers 
        // =============================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // clear previous errors
            try
            {
                errWarning.SetError(txtBookName, "");
                errWarning.SetError(cmbAuthor, "");
                errWarning.SetError(cmbPublisher, "");
                errWarning.SetError(cmbCategory, "");
                errWarning.SetError(txtYear, "");
                errWarning.SetError(txtPrice, "");

                txtBookName.BackColor = System.Drawing.Color.White;
                txtYear.BackColor = System.Drawing.Color.White;
                txtPrice.BackColor = System.Drawing.Color.White;
                cmbAuthor.BackColor = System.Drawing.Color.White;
                cmbPublisher.BackColor = System.Drawing.Color.White;
                cmbCategory.BackColor = System.Drawing.Color.White;
            }
            catch { }

            // check book name not exist in database before adding case sensitive
            using (var db = new BookStoreContext())
            {
                var existingBook = db.Books.FirstOrDefault(b => b.Title == txtBookName.Text.Trim());
                if (existingBook != null)
                {
                    MessageBox.Show("Tên sách đã tồn tại trong cơ sở dữ liệu.");
                    txtBookName.BackColor = System.Drawing.Color.MistyRose;
                    errWarning.SetError(txtBookName, "Tên sách đã tồn tại.");
                    return;
                }
            }

            // validate basic fields with error provider
            if (string.IsNullOrWhiteSpace(txtBookName.Text))
            {
                txtBookName.BackColor = System.Drawing.Color.MistyRose;
                errWarning.SetError(txtBookName, "Vui lòng nhập tên sách.");
                MessageBox.Show("Vui lòng nhập tên sách.");
                return;
            }

            if (!int.TryParse(txtYear.Text, out int year))
            {
                txtYear.BackColor = System.Drawing.Color.MistyRose;
                errWarning.SetError(txtYear, "Năm xuất bản không hợp lệ.");
                MessageBox.Show("Năm xuất bản không hợp lệ.");
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowDecimalPoint, vnCulture, out decimal price))
            {
                txtPrice.BackColor = System.Drawing.Color.MistyRose;
                errWarning.SetError(txtPrice, "Giá tiền không hợp lệ.");
                MessageBox.Show("Giá tiền không hợp lệ.");
                return;
            }

            var authorName = (cmbAuthor.Text ?? string.Empty).Trim();
            
            var publisherName = string.Empty;
            if (cmbPublisher.SelectedItem is Publisher selectedPublisher)
            {
                publisherName = selectedPublisher.Name;
            }
            else
            {
                publisherName = (cmbPublisher.Text ?? string.Empty).Trim();
            }

            var categoryText = cmbCategory.Text?.Trim();

            using (var db = new BookStoreContext())
            {
                try
                {
                    // 1️. Chưa chọn / chưa nhập
                    if (string.IsNullOrWhiteSpace(categoryText)
                        || categoryText == "-- Chọn Thể loại --")
                    {
                        MessageBox.Show("Vui lòng chọn hoặc nhập thể loại.");
                        return;
                    }


                    // 2️. Tìm theo tên
                    var category = db.Categories
                        .FirstOrDefault(c => c.Name.ToLower() == categoryText.ToLower());

                    // 3️. Nếu chưa có → tạo mới
                    if (category == null)
                    {
                        category = new Category
                        {
                            Name = categoryText,
                            Description = string.Empty
                        };
                        db.Categories.Add(category);
                        db.SaveChanges();
                    }

                    if (category == null && cmbCategory.SelectedValue != null)
                    {
                        int selId = Convert.ToInt32(cmbCategory.SelectedValue);
                        category = db.Categories.Find(selId);
                    }

                    if (category == null)
                    {
                        MessageBox.Show("Vui lòng chọn hoặc nhập một thể loại hợp lệ.");
                        return;
                    }

                    // find or create author
                    Author author = null;
                    if (!string.IsNullOrWhiteSpace(authorName))
                    {
                        author = db.Authors.FirstOrDefault(a => a.Name == authorName);
                        if (author == null)
                        {
                            author = new Author { Name = authorName };
                            db.Authors.Add(author);
                            db.SaveChanges();
                            // reload author autocomplete so new author appears
                            try { LoadAuthors(); } catch { }
                        }
                    }

                    // find or create publisher
                    Publisher publisher = null;
                    if (!string.IsNullOrWhiteSpace(publisherName))
                    {
                        publisher = db.Publishers.FirstOrDefault(p => p.Name == publisherName);
                        if (publisher == null)
                        {
                            publisher = new Publisher { Name = publisherName };
                            db.Publishers.Add(publisher);
                            db.SaveChanges();
                            // reload publishers into combobox and select the newly created one
                            try { LoadPublishers(); cmbPublisher.SelectedValue = publisher.PublisherId; } catch { }
                        }
                    }


                    var book = new Book
                    {
                        Title = txtBookName.Text.Trim(),
                        CategoryId = category.CategoryId,
                        AuthorId = author != null ? author.AuthorId : 0, 
                        PublisherId = publisher != null ? publisher.PublisherId : 0,
                        PublishYear = year,
                        Price = price,
                        Description = string.Empty,
                        CoverImagePath = string.IsNullOrWhiteSpace(currentCoverPath) ? null : currentCoverPath
                    };

                    if (book.AuthorId == 0)
                    {
                        MessageBox.Show("Vui lòng nhập/ chọn tác giả (Author).\nTác giả không thể để trống.");
                        errWarning.SetError(cmbAuthor, "Vui lòng chọn tác giả");
                        return;
                    }

                    if (book.PublisherId == 0)
                    {
                        MessageBox.Show("Vui lòng nhập/ chọn nhà xuất bản (Publisher).\nNhà xuất bản không thể để trống.");
                        errWarning.SetError(cmbPublisher, "Vui lòng chọn nhà xuất bản");
                        return;
                    }

                    db.Books.Add(book);
                    db.SaveChanges();
                    DebugLogger.Log($"Book added: {book.Title} Id={book.BookId}");

                    // show generated id
                    txtBookId.Text = book.BookId.ToString();

                    // reload and select the new row
                    LoadBooks();
                    for (int i = 0; i < dgvBook.Rows.Count; i++)
                    {
                        var tag = dgvBook.Rows[i].Tag;
                        if (tag != null && tag.ToString() == book.BookId.ToString())
                        {
                            dgvBook.ClearSelection();
                            dgvBook.Rows[i].Selected = true;
                            dgvBook.FirstDisplayedScrollingRowIndex = i;
                            break;
                        }
                    }

                    // clear inputs (except BookId which shows the new id)
                    txtBookName.Clear();
                    // cmbAuthor is a ComboBox; clear text
                    cmbAuthor.Text = string.Empty;
                    cmbPublisher.SelectedIndex = -1;
                    txtYear.Clear();
                    txtPrice.Clear();
                    currentCoverPath = null;
                    LoadCoverImage(null);

                }
                catch (Exception ex)
                {
                    DebugLogger.Log("btnAdd_Click error: " + ex.ToString());
                    MessageBox.Show("Lỗi khi thêm sách: " + ex.Message);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // clear previous errors
            try
            {
                errWarning.SetError(txtBookName, "");
                errWarning.SetError(cmbAuthor, "");
                errWarning.SetError(cmbPublisher, "");
                errWarning.SetError(cmbCategory, "");
                errWarning.SetError(txtYear, "");
                errWarning.SetError(txtPrice, "");

                txtBookName.BackColor = System.Drawing.Color.White;
                txtYear.BackColor = System.Drawing.Color.White;
                txtPrice.BackColor = System.Drawing.Color.White;
                cmbAuthor.BackColor = System.Drawing.Color.White;
                cmbPublisher.BackColor = System.Drawing.Color.White;
                cmbCategory.BackColor = System.Drawing.Color.White;
            }
            catch { }

            // validate selected book id
            if (!int.TryParse(txtBookId.Text, out int id))
            {
                MessageBox.Show("Chưa chọn sách để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // basic validation with error provider
            if (string.IsNullOrWhiteSpace(txtBookName.Text))
            {
                txtBookName.BackColor = System.Drawing.Color.MistyRose;
                errWarning.SetError(txtBookName, "Vui lòng nhập tên sách.");
                MessageBox.Show("Vui lòng nhập tên sách.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtYear.Text, out int year))
            {
                txtYear.BackColor = System.Drawing.Color.MistyRose;
                errWarning.SetError(txtYear, "Năm xuất bản không hợp lệ.");
                MessageBox.Show("Năm xuất bản không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text, System.Globalization.NumberStyles.AllowThousands | System.Globalization.NumberStyles.AllowDecimalPoint, vnCulture, out decimal price))
            {
                txtPrice.BackColor = System.Drawing.Color.MistyRose;
                errWarning.SetError(txtPrice, "Giá tiền không hợp lệ.");
                MessageBox.Show("Giá tiền không hợp lệ.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var authorName = (cmbAuthor.Text ?? string.Empty).Trim();
            
            var publisherName = string.Empty;
            if (cmbPublisher.SelectedItem is Publisher selectedPublisher)
            {
                publisherName = selectedPublisher.Name;
            }
            else
            {
                publisherName = (cmbPublisher.Text ?? string.Empty).Trim();
            }

            var categoryText = cmbCategory.Text?.Trim();



            using (var db = new BookStoreContext())
            {
                try
                {
                    var book = db.Books.FirstOrDefault(b => b.BookId == id);
                    if (book == null)
                    {
                        MessageBox.Show("Không tìm thấy sách để sửa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // category
                    // 1️. Chưa chọn / chưa nhập
                    if (string.IsNullOrWhiteSpace(categoryText) || categoryText == "Thể loại")
                    {
                        MessageBox.Show("Vui lòng chọn hoặc nhập thể loại.");
                        return;
                    }

                    // 2️. Tìm theo tên
                    var category = db.Categories
                        .FirstOrDefault(c => c.Name.ToLower() == categoryText.ToLower());

                    // 3️. Nếu chưa có → tạo mới
                    if (category == null)
                    {
                        category = new Category
                        {
                            Name = categoryText,
                            Description = string.Empty
                        };
                        db.Categories.Add(category);
                        db.SaveChanges();
                    }

                    if (category == null && cmbCategory.SelectedValue != null)
                    {
                        int selId = Convert.ToInt32(cmbCategory.SelectedValue);
                        category = db.Categories.Find(selId);
                    }

                    // author
                    Author author = null;
                    if (!string.IsNullOrWhiteSpace(authorName))
                    {
                        author = db.Authors.FirstOrDefault(a => a.Name == authorName);
                        if (author == null)
                        {
                            author = new Author { Name = authorName };
                            db.Authors.Add(author);
                            db.SaveChanges();
                            try { LoadAuthors(); } catch { }
                        }
                    }

                    // publisher
                    Publisher publisher = null;
                    if (!string.IsNullOrWhiteSpace(publisherName))
                    {
                        publisher = db.Publishers.FirstOrDefault(p => p.Name == publisherName);
                        if (publisher == null)
                        {
                            publisher = new Publisher { Name = publisherName };
                            db.Publishers.Add(publisher);
                            db.SaveChanges();
                            try { LoadPublishers(); cmbPublisher.SelectedValue = publisher.PublisherId; } catch { }
                        }
                    }

                    // apply changes
                    book.Title = txtBookName.Text.Trim();
                    book.CategoryId = category.CategoryId;
                    if (author != null) book.AuthorId = author.AuthorId;
                    if (publisher != null) book.PublisherId = publisher.PublisherId;
                    book.PublishYear = year;
                    book.Price = price;


                    if (!string.IsNullOrWhiteSpace(currentCoverPath))
                    {
                        book.CoverImagePath = currentCoverPath;
                    }



                    db.SaveChanges();
                    DebugLogger.Log($"Book updated: {book.Title} Id={book.BookId}");

                    LoadBooks();
                    ClearForm();
                    txtBookId.Text = string.Empty;

                    MessageBox.Show("Cập nhật sách thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    DebugLogger.Log("btnUpdate_Click error: " + ex.ToString());
                    MessageBox.Show("Lỗi khi cập nhật sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtBookId.Text, out int id))
            {
                MessageBox.Show("Chưa chọn sách để xóa.");
                return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa sách này?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            using (var db = new BookStoreContext())
            {
                try
                {
                    var book = db.Books.Find(id);
                    if (book != null)
                    {
                        db.Books.Remove(book);
                        db.SaveChanges();
                        DebugLogger.Log($"Book deleted: {book.Title} Id={book.BookId}");
                    }

                    LoadBooks();
                    ClearForm();
                    txtBookId.Text = string.Empty; // clear id after delete
                }
                catch (Exception ex)
                {
                    DebugLogger.Log("btnDelete_Click error: " + ex.ToString());
                    MessageBox.Show("Lỗi khi xóa sách: " + ex.Message);
                }
            }
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            LoadBooks();
            ClearForm();
            txtBookId.Text = string.Empty;
        }

        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Chọn ảnh bìa sách";
                dlg.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                dlg.Multiselect = false;

                if (dlg.ShowDialog() != DialogResult.OK) return;

                var src = dlg.FileName;
                try
                {
                    var imagesDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images", "Covers");
                    Directory.CreateDirectory(imagesDir);

                    var destFileName = Guid.NewGuid().ToString() + Path.GetExtension(src);
                    var destPath = Path.Combine(imagesDir, destFileName);

                    // copy file (if same file already in target, overwrite to ensure latest)
                    File.Copy(src, destPath, true);

                    currentCoverPath = destPath;

                    // load into picture box safely
                    try
                    {
                        using (var fs = new FileStream(destPath, FileMode.Open, FileAccess.Read))
                        {
                            pictureBoxCover.Image = Image.FromStream(fs);
                        }
                    }
                    catch
                    {
                        pictureBoxCover.Image = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi chọn ảnh: " + ex.Message);
                }
            }
        }

        // Helper to select item in BindingSource ComboBox by object
        private void SelectPublisherByName(string publisherName)
        {
            if (string.IsNullOrEmpty(publisherName))
            {
                cmbPublisher.SelectedIndex = -1;
                return;
            }

            foreach (Publisher p in publisherBinding.List)
            {
                if (p.Name == publisherName)
                {
                    cmbPublisher.SelectedItem = p;
                    return;
                }
            }
            cmbPublisher.SelectedIndex = -1;
        }

    }
}
