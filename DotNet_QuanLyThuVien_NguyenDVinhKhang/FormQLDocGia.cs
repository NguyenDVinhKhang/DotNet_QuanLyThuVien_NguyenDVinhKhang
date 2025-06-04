using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNet_QuanLyThuVien_NguyenDVinhKhang
{
    public partial class FormQLDocGia : Form
    {
        private bool isEditing = false;

        public FormQLDocGia()
        {
            InitializeComponent();
            dgvQLDocGia.CellClick += dgvQLDocGia_CellClick;
        }

        private void FormQLDocGia_Load(object sender, EventArgs e)
        {
            txtMaDocGia.ReadOnly = true;
            LoadDocGia();
            CleanInput();

            int width = 180;
            int height = (int)(width * 4.0 / 3.0);
            pictureBoxHinhAnhDocGia.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxHinhAnhDocGia.Width = width;
            pictureBoxHinhAnhDocGia.Height = height;
            pictureBoxHinhAnhDocGia.Location = new Point(815, 34);
        }

        private void LoadDocGia()
        {
            SqlServerConnection db = new SqlServerConnection();
            string query = "SELECT * FROM DocGia";
            DataTable dt = db.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                dgvQLDocGia.DataSource = dt;
                dgvQLDocGia.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                MessageBox.Show("Lỗi khi tải dữ liệu đọc giả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CleanInput()
        {
            txtMaDocGia.Text = "Mã đọc giả được tạo tự động";

            txtHoTen.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
            txtDiaChi.Text = "";

            pictureBoxHinhAnhDocGia.Image = null;
            pictureBoxHinhAnhDocGia.ImageLocation = null;
            pictureBoxHinhAnhDocGia.Tag = null;

            isEditing = false;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            // Kiểm tra SDT chỉ chứa số và tối đa 10 ký tự
            string sdt = txtSDT.Text.Trim();
            if (!System.Text.RegularExpressions.Regex.IsMatch(sdt, @"^\d{1,10}$"))
            {
                MessageBox.Show("Số điện thoại chỉ được chứa số và tối đa 10 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDiaChi.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            string maDocGia = IDGenerator.GetNextIDFromDatabase(SqlServerConnection.ConnectionString, "DocGia", "MaDocGia", "DG");
            string hoTen = txtHoTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string hinhAnh = (pictureBoxHinhAnhDocGia.Tag != null) ? pictureBoxHinhAnhDocGia.Tag.ToString() : null;

            string query = $"INSERT INTO DocGia (MaDocGia, HoTen, Email, SDT, DiaChi, HinhAnh) " +
                           $"VALUES ('{maDocGia}', N'{hoTen}', '{email}', '{sdt}', N'{diaChi}', '{hinhAnh}')";

            SqlServerConnection db = new SqlServerConnection();
            int result = db.ExecuteNonQuery(query);

            if (result > 0)
            {
                MessageBox.Show("Thêm đọc giả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDocGia();
                CleanInput();
            }
            else
            {
                MessageBox.Show("Thêm đọc giả thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaDocGia.Text == "Mã đọc giả được tạo tự động" || string.IsNullOrWhiteSpace(txtMaDocGia.Text))
            {
                MessageBox.Show("Vui lòng chọn đọc giả cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput())
                return;

            string maDocGia = txtMaDocGia.Text.Trim();
            string hoTen = txtHoTen.Text.Trim();
            string email = txtEmail.Text.Trim();
            string sdt = txtSDT.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();
            string hinhAnh = (pictureBoxHinhAnhDocGia.Tag != null) ? pictureBoxHinhAnhDocGia.Tag.ToString() : null;

            // Kiểm tra xem đọc giả có đang mượn sách không
            SqlServerConnection db = new SqlServerConnection();
            string checkQuery = $"SELECT COUNT(*) FROM PhieuMuon WHERE MaDocGia = '{maDocGia}' AND NgayTraThucTe IS NULL";
            int count = db.ExecuteScalar(checkQuery);

            if (count > 0)
            {
                // Vẫn cho phép sửa thông tin, nhưng hiển thị thông báo cho người dùng biết
                DialogResult dr = MessageBox.Show("Đọc giả này đang mượn sách. Bạn vẫn muốn sửa thông tin?",
                                                "Cảnh báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.No)
                    return;
            }

            string query = $"UPDATE DocGia SET HoTen=N'{hoTen}', Email='{email}', SDT='{sdt}', DiaChi=N'{diaChi}', HinhAnh='{hinhAnh}' WHERE MaDocGia='{maDocGia}'";
            int result = db.ExecuteNonQuery(query);

            if (result > 0)
            {
                MessageBox.Show("Sửa thông tin đọc giả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDocGia();
                CleanInput();
            }
            else
            {
                MessageBox.Show("Sửa thông tin đọc giả thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaDocGia.Text == "Mã đọc giả được tạo tự động" || string.IsNullOrWhiteSpace(txtMaDocGia.Text))
            {
                MessageBox.Show("Vui lòng chọn đọc giả cần xóa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maDocGia = txtMaDocGia.Text.Trim();

            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa đọc giả này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) return;

            // Kiểm tra xem đọc giả có đang mượn sách không
            SqlServerConnection db = new SqlServerConnection();
            string checkQuery = $"SELECT COUNT(*) FROM PhieuMuon WHERE MaDocGia = '{maDocGia}' AND NgayTraThucTe IS NULL";
            int count = db.ExecuteScalar(checkQuery);

            if (count > 0)
            {
                MessageBox.Show("Không thể xóa đọc giả này vì đang mượn sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = $"DELETE FROM DocGia WHERE MaDocGia='{maDocGia}'";
            int result = db.ExecuteNonQuery(query);

            if (result > 0)
            {
                MessageBox.Show("Xóa đọc giả thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDocGia();
                CleanInput();
            }
            else
            {
                MessageBox.Show("Xóa đọc giả thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvQLDocGia_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvQLDocGia.Rows[e.RowIndex];

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

                txtMaDocGia.Text = row.Cells["MaDocGia"].Value.ToString();
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();
                txtEmail.Text = row.Cells["Email"].Value.ToString();
                txtSDT.Text = row.Cells["SDT"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();

                if (row.Cells["HinhAnh"].Value != DBNull.Value && row.Cells["HinhAnh"].Value != null)
                {
                    string hinhAnh = row.Cells["HinhAnh"].Value.ToString();
                    if (!string.IsNullOrEmpty(hinhAnh))
                    {
                        string imagePath = System.IO.Path.Combine(Application.StartupPath, hinhAnh);
                        pictureBoxHinhAnhDocGia.ImageLocation = imagePath;
                        pictureBoxHinhAnhDocGia.Tag = hinhAnh;
                    }
                    else
                    {
                        pictureBoxHinhAnhDocGia.ImageLocation = null;
                        pictureBoxHinhAnhDocGia.Tag = null;
                    }
                }
                else
                {
                    pictureBoxHinhAnhDocGia.ImageLocation = null;
                    pictureBoxHinhAnhDocGia.Tag = null;
                }
            }
        }

        private void pictureBoxHinhAnhDocGia_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string imagesFolder = System.IO.Path.Combine(Application.StartupPath, "Images", "Members");
                    if (!System.IO.Directory.Exists(imagesFolder))
                    {
                        System.IO.Directory.CreateDirectory(imagesFolder);
                    }

                    string fileName = System.IO.Path.GetFileName(ofd.FileName);
                    string destPath = System.IO.Path.Combine(imagesFolder, fileName);

                    // Copy file nếu chưa tồn tại
                    if (!System.IO.File.Exists(destPath))
                    {
                        System.IO.File.Copy(ofd.FileName, destPath);
                    }

                    // Lưu đường dẫn tương đối
                    string relativePath = System.IO.Path.Combine("Images", "Members", fileName);

                    // Kiểm tra xem ảnh có tải được không
                    try
                    {
                        using (System.Drawing.Image testImage = System.Drawing.Image.FromFile(destPath))
                        {
                            // Nếu tải được ảnh, tiếp tục
                            pictureBoxHinhAnhDocGia.ImageLocation = System.IO.Path.Combine(Application.StartupPath, relativePath);
                            pictureBoxHinhAnhDocGia.Tag = relativePath; // Lưu lại đường dẫn tương đối để lưu vào DB
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi tải ảnh: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        // Xóa file đã copy nếu có lỗi
                        if (System.IO.File.Exists(destPath))
                        {
                            try { System.IO.File.Delete(destPath); } catch { }
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
