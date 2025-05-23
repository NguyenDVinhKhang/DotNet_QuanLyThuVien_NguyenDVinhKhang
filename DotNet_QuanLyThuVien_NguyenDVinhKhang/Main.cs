using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DotNet_QuanLyThuVien_NguyenDVinhKhang
{
    public partial class Main : Form
    {
        private bool isLoginSuccess = false;
        private FormLogin formLogin;
        private FormQLSach formQLSach;
        private FormQLDocGia formQLDocGia;
        private FormQLMuonTra formQLMuonTra;
        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            LoadLoginForm();
        }
        
        private void LoadLoginForm()
        {
            this.menuStrip1.Enabled = false;
            isLoginSuccess = false;
            formLogin = new FormLogin();
            formLogin.MdiParent = this;
            formLogin.LoginSuccess += FormLogin_LoginSuccess;
            formLogin.FormClosed += FormLogin_FormClosed;
            formLogin.Show();
        }
        
        private void FormLogin_LoginSuccess(object sender, EventArgs e)
        {
            this.menuStrip1.Enabled = true;
            isLoginSuccess = true;
            formLogin.Close();
        }

        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isLoginSuccess)
            {
                Application.Exit();
            }
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formQLSach != null && !formQLSach.IsDisposed)
                formQLSach.Close();
            if (formQLDocGia != null && !formQLDocGia.IsDisposed)
                formQLDocGia.Close();
            if (formQLMuonTra != null && !formQLMuonTra.IsDisposed)
                formQLMuonTra.Close();

            LoadLoginForm();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mởFormQLSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formQLSach == null || formQLSach.IsDisposed)
            {
                formQLSach = new FormQLSach();
                formQLSach.MdiParent = this;
                formQLSach.Show();
            }
            else
            {
                formQLSach.Activate();
            }
        }

        private void đóngFormQLSáchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formQLSach.Close();
        }

        private void mởFormQLĐộcGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formQLDocGia == null || formQLDocGia.IsDisposed)
            {
                formQLDocGia = new FormQLDocGia();
                formQLDocGia.MdiParent = this;
                formQLDocGia.Show();
            }
            else
            {
                formQLDocGia.Activate();
            }
        }

        private void đóngFormQLĐộcGiảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formQLDocGia.Close();
        }

        private void mởFormQLMượnTrảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formQLMuonTra == null || formQLMuonTra.IsDisposed)
            {
                formQLMuonTra = new FormQLMuonTra();
                formQLMuonTra.MdiParent = this;
                formQLMuonTra.Show();
            }
            else
            {
                formQLMuonTra.Activate();
            }
        }

        private void đóngFormQLMượnTrảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            formQLMuonTra.Close();
        }
    }
}
