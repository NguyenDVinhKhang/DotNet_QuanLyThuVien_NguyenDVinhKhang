using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNet_QuanLyThuVien_NguyenDVinhKhang
{
    public partial class FormQLPhieuMuon : Form
    {
        private bool isEditing = false;

        public FormQLPhieuMuon()
        {
            InitializeComponent();
            dgvQLMuonTra.CellClick += dgvQLMuonTra_CellClick;
        }

        private void FormQLMuonTra_Load(object sender, EventArgs e)
        {
            txtMaMuon.ReadOnly = true;
            dtpNgayTraThucTe.Enabled = false;
            LoadSach();
            LoadDocGia();
            LoadPhieuMuon();
            CleanInput();

            int width = 180;
            int height = (int)(width * 4.0 / 3.0);

            pictureBoxHinhAnhDocGia.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxHinhAnhDocGia.Width = width;
            pictureBoxHinhAnhDocGia.Height = height;
            pictureBoxHinhAnhDocGia.Location = new Point(731, 34);

            pictureBoxHinhAnhSach.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxHinhAnhSach.Width = width;
            pictureBoxHinhAnhSach.Height = height;
            pictureBoxHinhAnhSach.Location = new Point(1001, 34);

            
        }

        private void LoadSach()
        {
            SqlServerConnection db = new SqlServerConnection();
            string query = "SELECT MaSach, TieuDe FROM Sach";
            DataTable dt = db.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                cboMaSach.DataSource = dt;
                cboMaSach.DisplayMember = "TieuDe";
                cboMaSach.ValueMember = "MaSach";
                cboMaSach.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Lỗi khi tải dữ liệu sách!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDocGia()
        {
            SqlServerConnection db = new SqlServerConnection();
            string query = "SELECT MaDocGia, HoTen FROM DocGia";
            DataTable dt = db.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0)
            {
                cboMaDocGia.DataSource = dt;
                cboMaDocGia.DisplayMember = "HoTen";
                cboMaDocGia.ValueMember = "MaDocGia";
                cboMaDocGia.SelectedIndex = -1;
            }
            else
            {
                MessageBox.Show("Lỗi khi tải dữ liệu đọc giả!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadPhieuMuon()
        {
            SqlServerConnection db = new SqlServerConnection();
            string query = $"SELECT * FROM PhieuMuon ORDER BY NgayMuon DESC";
            DataTable dt = db.ExecuteQuery(query);

            if (dt != null)
            {
                dgvQLMuonTra.DataSource = dt;
                dgvQLMuonTra.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            else
            {
                MessageBox.Show("Lỗi khi tải dữ liệu phiếu mượn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CleanInput()
        {
            txtMaMuon.Text = "Mã phiếu mượn được tạo tự động";

            cboMaSach.SelectedIndex = -1;
            cboMaDocGia.SelectedIndex = -1;

            dtpNgayMuon.Value = DateTime.Today;
            dtpNgayHenTra.Value = DateTime.Today.AddDays(7);

            dtpNgayTraThucTe.Value = DateTime.Today;
            dtpNgayTraThucTe.Enabled = false;

            pictureBoxHinhAnhDocGia.Image = null;
            pictureBoxHinhAnhDocGia.ImageLocation = null;
            pictureBoxHinhAnhDocGia.Tag = null;

            pictureBoxHinhAnhSach.Image = null;
            pictureBoxHinhAnhSach.ImageLocation = null;
            pictureBoxHinhAnhSach.Tag = null;

            isEditing = false;
        }

        private bool ValidateInput()
        {
            if (cboMaSach.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cboMaDocGia.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn đọc giả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (dtpNgayMuon.Value.Date != DateTime.Today)
            {
                MessageBox.Show("Ngày mượn chỉ có thể bắt đầu từ hôm nay!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            string maPhieuMuon = IDGenerator.GetNextIDFromDatabase(SqlServerConnection.ConnectionString, "PhieuMuon", "MaPhieuMuon", "PM");
            string maSach = cboMaSach.SelectedValue.ToString();
            string maDocGia = cboMaDocGia.SelectedValue.ToString();

            string ngayMuon = dtpNgayMuon.Value.ToString("yyyy-MM-dd");
            string ngayHenTra = dtpNgayHenTra.Value.ToString("yyyy-MM-dd");

            // Kiểm tra xem sách đã được mượn chưa
            SqlServerConnection db = new SqlServerConnection();
            string checkQuery = $"SELECT COUNT(*) FROM PhieuMuon WHERE MaSach = '{maSach}' AND NgayTraThucTe IS NULL";
            int count = db.ExecuteScalar(checkQuery);

            if (count > 0)
            {
                MessageBox.Show("Sách này đang được mượn bởi người khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thêm phiếu mượn mới
            string query = $"INSERT INTO PhieuMuon (MaPhieuMuon, MaDocGia, MaSach, NgayMuon, NgayHenTra) " +
                          $"VALUES ('{maPhieuMuon}', '{maDocGia}', '{maSach}', '{ngayMuon}', '{ngayHenTra}')";

            int result = db.ExecuteNonQuery(query);

            if (result > 0)
            {
                MessageBox.Show("Thêm phiếu mượn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPhieuMuon();
                CleanInput();
            }
            else
            {
                MessageBox.Show("Thêm phiếu mượn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (txtMaMuon.Text == "Mã phiếu mượn được tạo tự động" || string.IsNullOrWhiteSpace(txtMaMuon.Text))
            {
                MessageBox.Show("Vui lòng chọn phiếu mượn cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput())
                return;

            string maPhieuMuon = txtMaMuon.Text.Trim();
            string maSach = cboMaSach.SelectedValue.ToString();
            string maDocGia = cboMaDocGia.SelectedValue.ToString();

            string ngayMuon = dtpNgayMuon.Value.ToString("yyyy-MM-dd");
            string ngayHenTra = dtpNgayHenTra.Value.ToString("yyyy-MM-dd");

            string ngayTraThucTe = "NULL";
            if (dtpNgayTraThucTe.Enabled && dtpNgayTraThucTe.Value != null)
            {
                // Kiểm tra ngày trả không được sớm hơn ngày mượn
                if (dtpNgayTraThucTe.Value < dtpNgayMuon.Value)
                {
                    MessageBox.Show("Ngày trả không thể sớm hơn ngày mượn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ngayTraThucTe = $"'{dtpNgayTraThucTe.Value.ToString("yyyy-MM-dd")}'";
            }

            // Kiểm tra xem sách mới có đang được mượn bởi người khác không
            SqlServerConnection db = new SqlServerConnection();
            if (maSach != cboMaSach.SelectedValue.ToString())
            {
                string checkQuery = $"SELECT COUNT(*) FROM PhieuMuon WHERE MaSach = '{maSach}' AND NgayTraThucTe IS NULL AND MaPhieuMuon <> '{maPhieuMuon}'";
                int count = db.ExecuteScalar(checkQuery);

                if (count > 0)
                {
                    MessageBox.Show("Sách này đang được mượn bởi người khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            string query = $"UPDATE PhieuMuon SET " +
                          $"MaDocGia = '{maDocGia}', " +
                          $"MaSach = '{maSach}', " +
                          $"NgayMuon = '{ngayMuon}', " +
                          $"NgayHenTra = '{ngayHenTra}', " +
                          $"NgayTraThucTe = {ngayTraThucTe} " +
                          $"WHERE MaPhieuMuon = '{maPhieuMuon}'";

            int result = db.ExecuteNonQuery(query);

            if (result > 0)
            {
                MessageBox.Show("Cập nhật phiếu mượn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPhieuMuon();
            }
            else
            {
                MessageBox.Show("Cập nhật phiếu mượn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaMuon.Text == "Mã phiếu mượn được tạo tự động" || string.IsNullOrWhiteSpace(txtMaMuon.Text))
            {
                MessageBox.Show("Vui lòng chọn phiếu mượn cần xóa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPhieuMuon = txtMaMuon.Text.Trim();

            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa phiếu mượn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) return;

            // Kiểm tra xem phiếu mượn đã được trả sách chưa
            SqlServerConnection db = new SqlServerConnection();
            string checkQuery = $"SELECT NgayTraThucTe FROM PhieuMuon WHERE MaPhieuMuon = '{maPhieuMuon}'";
            DataTable checkResult = db.ExecuteQuery(checkQuery);
            if (checkResult != null && checkResult.Rows.Count > 0 && checkResult.Rows[0]["NgayTraThucTe"] == DBNull.Value)
            {
                MessageBox.Show("Không thể xóa phiếu mượn vì sách chưa được trả!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = $"DELETE FROM PhieuMuon WHERE MaPhieuMuon = '{maPhieuMuon}'";
            int result = db.ExecuteNonQuery(query);

            if (result > 0)
            {
                MessageBox.Show("Xóa phiếu mượn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPhieuMuon();
                CleanInput();
            }
            else
            {
                MessageBox.Show("Xóa phiếu mượn thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTraSach_Click(object sender, EventArgs e)
        {
            if (txtMaMuon.Text == "Mã phiếu mượn được tạo tự động" || string.IsNullOrWhiteSpace(txtMaMuon.Text))
            {
                MessageBox.Show("Vui lòng chọn phiếu mượn cần trả sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maPhieuMuon = txtMaMuon.Text.Trim();

            // Kiểm tra xem sách đã được trả chưa
            SqlServerConnection db = new SqlServerConnection();
            string checkQuery = $"SELECT NgayTraThucTe FROM PhieuMuon WHERE MaPhieuMuon = '{maPhieuMuon}'";
            DataTable checkResult = db.ExecuteQuery(checkQuery);

            if (checkResult != null && checkResult.Rows.Count > 0)
            {
                if (checkResult.Rows[0]["NgayTraThucTe"] != DBNull.Value)
                {
                    MessageBox.Show("Sách này đã được trả rồi!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            // Enable DateTimePicker và set ngày hiện tại
            dtpNgayTraThucTe.Enabled = true;
            dtpNgayTraThucTe.Value = DateTime.Today;

            // Xác nhận với người dùng về ngày trả
            DialogResult dr = MessageBox.Show($"Xác nhận trả sách vào ngày {dtpNgayTraThucTe.Value.ToString("dd/MM/yyyy")}?",
                                             "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No)
            {
                // Nếu người dùng không đồng ý, disable lại control
                dtpNgayTraThucTe.Enabled = false;
                return;
            }

            // Kiểm tra ngày trả không được sớm hơn ngày mượn
            if (dtpNgayTraThucTe.Value < dtpNgayMuon.Value)
            {
                MessageBox.Show("Ngày trả không thể sớm hơn ngày mượn!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpNgayTraThucTe.Enabled = false;
                return;
            }

            // Cập nhật ngày trả
            string ngayTraThucTe = dtpNgayTraThucTe.Value.ToString("yyyy-MM-dd");
            string query = $"UPDATE PhieuMuon SET NgayTraThucTe = '{ngayTraThucTe}' WHERE MaPhieuMuon = '{maPhieuMuon}'";

            int result = db.ExecuteNonQuery(query);

            if (result > 0)
            {
                MessageBox.Show("Trả sách thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadPhieuMuon();
                CleanInput();
            }
            else
            {
                MessageBox.Show("Trả sách thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Disable lại nếu thất bại
                dtpNgayTraThucTe.Enabled = false;
            }
        }

        private void dgvQLMuonTra_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvQLMuonTra.Rows[e.RowIndex];

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

                txtMaMuon.Text = row.Cells["MaPhieuMuon"].Value.ToString();

                cboMaSach.SelectedValue = row.Cells["MaSach"].Value.ToString();
                cboMaDocGia.SelectedValue = row.Cells["MaDocGia"].Value.ToString();

                dtpNgayMuon.Value = Convert.ToDateTime(row.Cells["NgayMuon"].Value);
                dtpNgayHenTra.Value = Convert.ToDateTime(row.Cells["NgayHenTra"].Value);

                // Xử lý ngày trả thực tế
                if (row.Cells["NgayTraThucTe"].Value != DBNull.Value &&
                    row.Cells["NgayTraThucTe"].Value != null)
                {
                    dtpNgayTraThucTe.Enabled = true; // Đã trả, enable để hiển thị
                    dtpNgayTraThucTe.Value = Convert.ToDateTime(row.Cells["NgayTraThucTe"].Value);
                }
                else
                {
                    dtpNgayTraThucTe.Enabled = false; // Chưa trả, disable
                    dtpNgayTraThucTe.Value = DateTime.Today; // Giá trị mặc định
                }

                // Sử dụng các phương thức có sẵn để tải hình ảnh
                LoadBookImage(row.Cells["MaSach"].Value.ToString());
                LoadReaderImage(row.Cells["MaDocGia"].Value.ToString());
            }
        }

        private void LoadBookImage(string maSach)
        {
            if (string.IsNullOrEmpty(maSach))
            {
                pictureBoxHinhAnhSach.ImageLocation = null;
                pictureBoxHinhAnhSach.Tag = null;
                return;
            }

            SqlServerConnection db = new SqlServerConnection();
            string query = $"SELECT HinhAnh FROM Sach WHERE MaSach = '{maSach}'";
            DataTable dt = db.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["HinhAnh"] != DBNull.Value)
            {
                string hinhAnh = dt.Rows[0]["HinhAnh"].ToString();
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

        private void LoadReaderImage(string maDocGia)
        {
            if (string.IsNullOrEmpty(maDocGia))
            {
                pictureBoxHinhAnhDocGia.ImageLocation = null;
                pictureBoxHinhAnhDocGia.Tag = null;
                return;
            }

            SqlServerConnection db = new SqlServerConnection();
            string query = $"SELECT HinhAnh FROM DocGia WHERE MaDocGia = '{maDocGia}'";
            DataTable dt = db.ExecuteQuery(query);

            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0]["HinhAnh"] != DBNull.Value)
            {
                string hinhAnh = dt.Rows[0]["HinhAnh"].ToString();
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

        private void cboMaSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaSach.SelectedIndex != -1 && cboMaSach.SelectedValue != null)
            {
                string maSach = cboMaSach.SelectedValue.ToString();
                LoadBookImage(maSach);
            }
        }

        private void cboMaDocGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMaDocGia.SelectedIndex != -1 && cboMaDocGia.SelectedValue != null)
            {
                string maDocGia = cboMaDocGia.SelectedValue.ToString();
                LoadReaderImage(maDocGia);
            }
        }

        private void dtpNgayMuon_ValueChanged(object sender, EventArgs e)
        {
            dtpNgayHenTra.Value = dtpNgayMuon.Value.AddDays(7);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            CleanInput();
        }

    }
}
