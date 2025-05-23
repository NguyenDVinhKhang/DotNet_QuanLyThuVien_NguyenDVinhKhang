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
    public partial class FormLogin : Form
    {
        public event EventHandler LoginSuccess;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();
            string query = $"SELECT COUNT(*) FROM [USER] WHERE Username='{user}' AND Password='{pass}'";

            SqlServerConnection db = new SqlServerConnection();
            int count = db.ExecuteScalar(query);

            if (count > 0)
            {
                LoginSuccess?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu! Vui lòng thử lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
