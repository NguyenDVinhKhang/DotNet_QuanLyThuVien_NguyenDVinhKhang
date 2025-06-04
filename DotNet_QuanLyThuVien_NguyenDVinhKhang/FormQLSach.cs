using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageMagick;

namespace DotNet_QuanLyThuVien_NguyenDVinhKhang
{
    public partial class FormQLSach : Form
    {
        private bool isEditing = false;

        public FormQLSach()
        {
            InitializeComponent();
            dgvQLSach.CellClick += dgvQLSach_CellClick;
        }

        private void FormQLSach_Load(object sender, EventArgs e)
        {
            txtMaSach.ReadOnly = true;
            LoadTheLoai();
            LoadSach();
            CleanInput();

            int width = 180;
            int height = (int)(width * 4.0 / 3.0);
            pictureBoxHinhAnhSach.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxHinhAnhSach.Width = width;
            pictureBoxHinhAnhSach.Height = height;
            pictureBoxHinhAnhSach.Location = new Point(781, 52);
        }

        private void LoadTheLoai()
        {
            SqlServerConnection db = new SqlServerConnection();
            string query = "SELECT * FROM TheLoai";
            DataTable dt = db.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                cboMaTheLoai.DataSource = dt;
                cboMaTheLoai.DisplayMember = "TenTheLoai";
                cboMaTheLoai.ValueMember = "MaTheLoai";
                cboMaTheLoai.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Lỗi khi tải dữ liệu thể loại!.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSach()
        {
            SqlServerConnection db = new SqlServerConnection();
            string query = "SELECT * FROM Sach";
            DataTable dt = db.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                dgvQLSach.DataSource = dt;
                dgvQLSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                MessageBox.Show("Lỗi khi tải dữ liệu sách!.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CleanInput()
        {
            txtMaSach.Text = "Mã sách được tạo tự động";

            cboMaTheLoai.SelectedIndex = -1;
            txtTieuDe.Text = "";
            txtTacGia.Text = "";
            txtNamXuatBan.Text = "";
            txtNhaXuatBan.Text = "";
            pictureBoxHinhAnhSach.Image = null;
            pictureBoxHinhAnhSach.ImageLocation = null;
            pictureBoxHinhAnhSach.Tag = null;

            isEditing = false;
        }

        private bool ValidateInput()
        {
            if (cboMaTheLoai.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn thể loại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTieuDe.Text))
            {
                MessageBox.Show("Vui lòng nhập tiêu đề!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTacGia.Text))
            {
                MessageBox.Show("Vui lòng nhập tác giả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNamXuatBan.Text))
            {
                MessageBox.Show("Vui lòng nhập năm xuất bản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (!int.TryParse(txtNamXuatBan.Text.Trim(), out int namXuatBan))
            {
                MessageBox.Show("Năm xuất bản phải là số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNhaXuatBan.Text))
            {
                MessageBox.Show("Vui lòng nhập nhà xuất bản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (isEditing)
            {
                MessageBox.Show("Bạn đang trong chế độ chỉnh sửa. Vui lòng reset form trước khi thêm mới!",
                              "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput())
                return;

            string maSach = IDGenerator.GetNextIDFromDatabase(SqlServerConnection.ConnectionString, "Sach", "MaSach", "S");
            string maTheLoai = cboMaTheLoai.SelectedValue.ToString();
            string tieuDe = txtTieuDe.Text.Trim();
            string tacGia = txtTacGia.Text.Trim();
            int namXuatBan = int.Parse(txtNamXuatBan.Text.Trim());
            string nhaXuatBan = txtNhaXuatBan.Text.Trim();
            string hinhAnh = (pictureBoxHinhAnhSach.Tag != null) ? pictureBoxHinhAnhSach.Tag.ToString() : null;

            string query = $"INSERT INTO Sach (MaSach, MaTheLoai, TieuDe, TacGia, NamXuatBan, NhaXuatBan, HinhAnh) " +
                           $"VALUES ('{maSach}', '{maTheLoai}', N'{tieuDe}', N'{tacGia}', {namXuatBan}, N'{nhaXuatBan}', '{hinhAnh}')";

            SqlServerConnection db = new SqlServerConnection();
            int result = db.ExecuteNonQuery(query);

            if (result > 0)
            {
                MessageBox.Show("Thêm sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSach();
                CleanInput();
            }
            else
            {
                MessageBox.Show("Thêm sách thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaSach.Text == "Mã sách được tạo tự động" || string.IsNullOrWhiteSpace(txtMaSach.Text))
            {
                MessageBox.Show("Vui lòng chọn sách cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput())
                return;

            string maSach = txtMaSach.Text.Trim();
            string maTheLoai = cboMaTheLoai.SelectedValue.ToString();
            string tieuDe = txtTieuDe.Text.Trim();
            string tacGia = txtTacGia.Text.Trim();
            int namXuatBan = int.Parse(txtNamXuatBan.Text.Trim());
            string nhaXuatBan = txtNhaXuatBan.Text.Trim();
            string hinhAnh = (pictureBoxHinhAnhSach.Tag != null) ? pictureBoxHinhAnhSach.Tag.ToString() : null;

            // Kiểm tra xem sách đã được mượn chưa
            SqlServerConnection db = new SqlServerConnection();
            string checkQuery = $"SELECT COUNT(*) FROM PhieuMuon WHERE MaSach = '{maSach}' AND NgayTraThucTe IS NULL";
            int count = db.ExecuteScalar(checkQuery);

            if (count > 0)
            {
                MessageBox.Show("Không thể sửa thông tin sách này vì đang được mượn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = $"UPDATE Sach SET MaTheLoai='{maTheLoai}', TieuDe=N'{tieuDe}', TacGia=N'{tacGia}', NamXuatBan={namXuatBan}, NhaXuatBan=N'{nhaXuatBan}', HinhAnh='{hinhAnh}' WHERE MaSach='{maSach}'";
            int result = db.ExecuteNonQuery(query);

            if (result > 0)
            {
                MessageBox.Show("Sửa sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSach();
                CleanInput();
            }
            else
            {
                MessageBox.Show("Sửa sách thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaSach.Text == "Mã sách được tạo tự động" || string.IsNullOrWhiteSpace(txtMaSach.Text))
            {
                MessageBox.Show("Vui lòng chọn sách cần xóa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maSach = txtMaSach.Text.Trim();

            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa sách này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) return;

            // Kiểm tra xem sách đã được mượn chưa
            SqlServerConnection db = new SqlServerConnection();
            string checkQuery = $"SELECT COUNT(*) FROM PhieuMuon WHERE MaSach = '{maSach}' AND NgayTraThucTe IS NULL";
            int count = db.ExecuteScalar(checkQuery);

            if (count > 0)
            {
                MessageBox.Show("Không thể xóa sách này vì đang được mượn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = $"DELETE FROM Sach WHERE MaSach='{maSach}'";
            int result = db.ExecuteNonQuery(query);

            if (result > 0)
            {
                MessageBox.Show("Xóa sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadSach();
                CleanInput();
            }
            else
            {
                MessageBox.Show("Xóa sách thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvQLSach_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvQLSach.Rows[e.RowIndex];

                bool isEmptyRow = true;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null && !string.IsNullOrWhiteSpace(cell.Value.ToString()))
                    {
                        isEmptyRow = false;
                        break;
                    }
                }

                if (isEmptyRow)
                {
                    CleanInput();
                    return;
                }

                isEditing = true;

                txtMaSach.Text = row.Cells["MaSach"].Value.ToString();

                cboMaTheLoai.SelectedValue = row.Cells["MaTheLoai"].Value.ToString();
                txtTieuDe.Text = row.Cells["TieuDe"].Value.ToString();
                txtTacGia.Text = row.Cells["TacGia"].Value.ToString();
                txtNamXuatBan.Text = row.Cells["NamXuatBan"].Value.ToString();
                txtNhaXuatBan.Text = row.Cells["NhaXuatBan"].Value.ToString();

                if (row.Cells["HinhAnh"].Value != DBNull.Value && row.Cells["HinhAnh"].Value != null)
                {
                    string hinhAnh = row.Cells["HinhAnh"].Value.ToString();
                    if (!string.IsNullOrEmpty(hinhAnh))
                    {
                        string imagePath = System.IO.Path.Combine(Application.StartupPath, hinhAnh);
                        pictureBoxHinhAnhSach.ImageLocation = imagePath;
                        pictureBoxHinhAnhSach.Tag = hinhAnh;
                    }
                    else
                    {
                        pictureBoxHinhAnhSach.ImageLocation = null;
                        pictureBoxHinhAnhSach.Tag = null;
                    }
                }
                else
                {
                    pictureBoxHinhAnhSach.ImageLocation = null;
                    pictureBoxHinhAnhSach.Tag = null;
                }
            }
        }

        private void pictureBoxHinhAnhSach_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.webp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string imagesFolder = System.IO.Path.Combine(Application.StartupPath, "Images", "Books");
                    if (!System.IO.Directory.Exists(imagesFolder))
                    {
                        System.IO.Directory.CreateDirectory(imagesFolder);
                    }

                    string fileName = System.IO.Path.GetFileName(ofd.FileName);
                    string extension = System.IO.Path.GetExtension(fileName).ToLower();
                    string fileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(fileName);
                    string destPath = System.IO.Path.Combine(imagesFolder, fileName);
                    string relativePath = System.IO.Path.Combine("Images", "Books", fileName);

                    // Copy file gốc
                    if (!System.IO.File.Exists(destPath))
                    {
                        System.IO.File.Copy(ofd.FileName, destPath);
                    }

                    // Nếu là định dạng WebP, xử lý đặc biệt
                    if (extension == ".webp")
                    {
                        try
                        {
                            // Chuyển đổi WebP sang PNG
                            string pngFileName = fileNameWithoutExt + ".png";
                            string pngPath = System.IO.Path.Combine(imagesFolder, pngFileName);
                            string pngRelativePath = System.IO.Path.Combine("Images", "Books", pngFileName);

                            // Sử dụng Magick.NET để chuyển đổi
                            using (var image = new MagickImage(destPath))
                            {
                                image.Write(pngPath);
                            }

                            // Hiển thị ảnh PNG đã chuyển đổi
                            pictureBoxHinhAnhSach.ImageLocation = System.IO.Path.Combine(Application.StartupPath, pngRelativePath);
                            pictureBoxHinhAnhSach.Tag = pngRelativePath; // Lưu đường dẫn PNG để lưu vào DB
                        }
                        catch (Exception webpEx)
                        {
                            MessageBox.Show($"Lỗi khi chuyển đổi ảnh WebP: {webpEx.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            // Nếu chuyển đổi thất bại, thông báo cho người dùng
                            MessageBox.Show("Không thể xử lý ảnh WebP. Vui lòng chọn định dạng khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            // Xóa file WebP đã copy nếu có lỗi
                            if (System.IO.File.Exists(destPath))
                            {
                                try { System.IO.File.Delete(destPath); } catch { }
                            }

                            // Reset lại ảnh và Tag
                            pictureBoxHinhAnhSach.ImageLocation = null;
                            pictureBoxHinhAnhSach.Image = null;
                            pictureBoxHinhAnhSach.Tag = null;
                        }
                    }
                    else
                    {
                        // Các định dạng thông thường (JPG, PNG)
                        try
                        {
                            using (System.Drawing.Image testImage = System.Drawing.Image.FromFile(destPath))
                            {
                                // Nếu tải được ảnh, tiếp tục
                                pictureBoxHinhAnhSach.ImageLocation = System.IO.Path.Combine(Application.StartupPath, relativePath);
                                pictureBoxHinhAnhSach.Tag = relativePath; // Lưu lại đường dẫn tương đối để lưu vào DB
                            }
                        }
                        catch (OutOfMemoryException)
                        {
                            // Lỗi này thường xảy ra khi định dạng không được hỗ trợ
                            MessageBox.Show("Định dạng ảnh không được hỗ trợ hoặc ảnh bị hỏng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            // Xóa file đã copy nếu không hỗ trợ
                            if (System.IO.File.Exists(destPath))
                            {
                                try { System.IO.File.Delete(destPath); } catch { }
                            }

                            // Reset lại ảnh và Tag
                            pictureBoxHinhAnhSach.ImageLocation = null;
                            pictureBoxHinhAnhSach.Image = null;
                            pictureBoxHinhAnhSach.Tag = null;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi khi tải ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                            // Reset lại ảnh và Tag
                            pictureBoxHinhAnhSach.ImageLocation = null;
                            pictureBoxHinhAnhSach.Image = null;
                            pictureBoxHinhAnhSach.Tag = null;

                            // Xóa file đã copy nếu có lỗi
                            if (System.IO.File.Exists(destPath))
                            {
                                try { System.IO.File.Delete(destPath); } catch { }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            CleanInput();
        }
    }
}
