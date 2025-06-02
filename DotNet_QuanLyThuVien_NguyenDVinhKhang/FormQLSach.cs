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
    public partial class FormQLSach : Form
    {
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
                MessageBox.Show("Lỗi khi tải dữ liệu thể loại!.");
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

                pictureBoxHinhAnh.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBoxHinhAnh.Width = 320;
                pictureBoxHinhAnh.Height = 180;
                pictureBoxHinhAnh.Location = new Point(781, 52);
            }
            else
            {
                MessageBox.Show("Lỗi khi tải dữ liệu sách!.");
            }
        }

        private void CleanInput()
        {
            string newID = IDGenerator.GetNextIDFromDatabase(SqlServerConnection.ConnectionString, "Sach", "MaSach", "S");
            txtMaSach.Text = newID;
            cboMaTheLoai.SelectedIndex = -1;
            txtTieuDe.Text = "";
            txtTacGia.Text = "";
            txtNamXuatBan.Text = "";
            txtNhaXuatBan.Text = "";
            pictureBoxHinhAnh.Image = null;
            pictureBoxHinhAnh.ImageLocation = null;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cboMaTheLoai.SelectedIndex == -1 ||
            string.IsNullOrWhiteSpace(txtTieuDe.Text) ||
            string.IsNullOrWhiteSpace(txtTacGia.Text) ||
            string.IsNullOrWhiteSpace(txtNamXuatBan.Text) ||
            !int.TryParse(txtNamXuatBan.Text.Trim(), out int namXuatBan) ||
            string.IsNullOrWhiteSpace(txtNhaXuatBan.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ và đúng thông tin sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maSach = txtMaSach.Text.Trim();
            string maTheLoai = cboMaTheLoai.SelectedValue.ToString();
            string tieuDe = txtTieuDe.Text.Trim();
            string tacGia = txtTacGia.Text.Trim();
            string nhaXuatBan = txtNhaXuatBan.Text.Trim();
            string hinhAnh = (pictureBoxHinhAnh.ImageLocation != null) ? pictureBoxHinhAnh.ImageLocation : null;

            string query = $"INSERT INTO Sach (MaSach, MaTheLoai, TieuDe, TacGia, NamXuatBan, NhaXuatBan, HinhAnh) " +
                           $"VALUES ('{maSach}', '{maTheLoai}', N'{tieuDe}', N'{tacGia}', {namXuatBan}, N'{nhaXuatBan}', '{hinhAnh}')";

            SqlServerConnection db = new SqlServerConnection();
            int result = db.ExecuteNonQuery(query);

            if (result > 0)
            {
                MessageBox.Show("Thêm sách thành công!");
                LoadSach();
                CleanInput();
            }
            else
            {
                MessageBox.Show("Thêm sách thất bại!");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (cboMaTheLoai.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtTieuDe.Text) ||
                string.IsNullOrWhiteSpace(txtTacGia.Text) ||
                string.IsNullOrWhiteSpace(txtNamXuatBan.Text) ||
                !int.TryParse(txtNamXuatBan.Text.Trim(), out int namXuatBan) ||
                string.IsNullOrWhiteSpace(txtNhaXuatBan.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ và đúng thông tin sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maSach = txtMaSach.Text.Trim();
            string maTheLoai = cboMaTheLoai.SelectedValue.ToString();
            string tieuDe = txtTieuDe.Text.Trim();
            string tacGia = txtTacGia.Text.Trim();
            string nhaXuatBan = txtNhaXuatBan.Text.Trim();
            string hinhAnh = (pictureBoxHinhAnh.ImageLocation != null) ? pictureBoxHinhAnh.ImageLocation : null;

            string query = $"UPDATE Sach SET MaTheLoai='{maTheLoai}', TieuDe=N'{tieuDe}', TacGia=N'{tacGia}', NamXuatBan={namXuatBan}, NhaXuatBan=N'{nhaXuatBan}', HinhAnh='{hinhAnh}' WHERE MaSach='{maSach}'";

            SqlServerConnection db = new SqlServerConnection();
            int result = db.ExecuteNonQuery(query);

            if (result > 0)
            {
                MessageBox.Show("Sửa sách thành công!");
                LoadSach();
            }
            else
            {
                MessageBox.Show("Sửa sách thất bại!");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string maSach = txtMaSach.Text.Trim();
            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa sách này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) return;

            
            SqlServerConnection db = new SqlServerConnection();
            string query = $"DELETE FROM Sach WHERE MaSach='{maSach}'";
            int result = db.ExecuteNonQuery(query);

            if (result > 0)
            {
                MessageBox.Show("Xóa sách thành công!");
                LoadSach();
                CleanInput();
            }
            else
            {
                MessageBox.Show("Xóa sách thất bại!");
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

                txtMaSach.Text = row.Cells["MaSach"].Value.ToString();
                cboMaTheLoai.SelectedValue = row.Cells["MaTheLoai"].Value.ToString();
                txtTieuDe.Text = row.Cells["TieuDe"].Value.ToString();
                txtTacGia.Text = row.Cells["TacGia"].Value.ToString();
                txtNamXuatBan.Text = row.Cells["NamXuatBan"].Value.ToString();
                txtNhaXuatBan.Text = row.Cells["NhaXuatBan"].Value.ToString();
                string hinhAnh = row.Cells["HinhAnh"].Value.ToString();
                pictureBoxHinhAnh.ImageLocation = string.IsNullOrEmpty(hinhAnh) ? null : hinhAnh;
            }
        }

        private void pictureBoxHinhAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBoxHinhAnh.ImageLocation = ofd.FileName;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            CleanInput();
        }
    }
}
