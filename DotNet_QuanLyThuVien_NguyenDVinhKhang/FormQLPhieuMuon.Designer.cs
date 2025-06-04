namespace DotNet_QuanLyThuVien_NguyenDVinhKhang
{
    partial class FormQLPhieuMuon
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
            this.dgvQLMuonTra = new System.Windows.Forms.DataGridView();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.dtpNgayMuon = new System.Windows.Forms.DateTimePicker();
            this.txtMaMuon = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpNgayHenTra = new System.Windows.Forms.DateTimePicker();
            this.dtpNgayTraThucTe = new System.Windows.Forms.DateTimePicker();
            this.cboMaSach = new System.Windows.Forms.ComboBox();
            this.cboMaDocGia = new System.Windows.Forms.ComboBox();
            this.pictureBoxHinhAnhDocGia = new System.Windows.Forms.PictureBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.pictureBoxHinhAnhSach = new System.Windows.Forms.PictureBox();
            this.btnTraSach = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvQLMuonTra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHinhAnhDocGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHinhAnhSach)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvQLMuonTra
            // 
            this.dgvQLMuonTra.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvQLMuonTra.Location = new System.Drawing.Point(12, 374);
            this.dgvQLMuonTra.Name = "dgvQLMuonTra";
            this.dgvQLMuonTra.RowHeadersWidth = 51;
            this.dgvQLMuonTra.Size = new System.Drawing.Size(1287, 273);
            this.dgvQLMuonTra.TabIndex = 18;
            this.dgvQLMuonTra.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvQLMuonTra_CellClick);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(443, 313);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(89, 27);
            this.btnXoa.TabIndex = 15;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(304, 313);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(89, 27);
            this.btnSua.TabIndex = 14;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(165, 313);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(89, 27);
            this.btnThem.TabIndex = 13;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // dtpNgayMuon
            // 
            this.dtpNgayMuon.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayMuon.Location = new System.Drawing.Point(265, 165);
            this.dtpNgayMuon.Name = "dtpNgayMuon";
            this.dtpNgayMuon.Size = new System.Drawing.Size(267, 22);
            this.dtpNgayMuon.TabIndex = 10;
            this.dtpNgayMuon.ValueChanged += new System.EventHandler(this.dtpNgayMuon_ValueChanged);
            // 
            // txtMaMuon
            // 
            this.txtMaMuon.Location = new System.Drawing.Point(265, 31);
            this.txtMaMuon.Name = "txtMaMuon";
            this.txtMaMuon.Size = new System.Drawing.Size(267, 22);
            this.txtMaMuon.TabIndex = 7;
            this.txtMaMuon.Text = "Mã phiếu mượn được tạo tự động";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(111, 261);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "Ngày Trả";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(111, 216);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Ngày Hẹn Trả";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 170);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ngày Mượn";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(111, 75);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "Tên Đọc Giả";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 123);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Tên Sách";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 34);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mã Phiếu Mượn";
            // 
            // dtpNgayHenTra
            // 
            this.dtpNgayHenTra.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayHenTra.Location = new System.Drawing.Point(265, 211);
            this.dtpNgayHenTra.Name = "dtpNgayHenTra";
            this.dtpNgayHenTra.Size = new System.Drawing.Size(267, 22);
            this.dtpNgayHenTra.TabIndex = 11;
            // 
            // dtpNgayTraThucTe
            // 
            this.dtpNgayTraThucTe.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayTraThucTe.Location = new System.Drawing.Point(265, 256);
            this.dtpNgayTraThucTe.Name = "dtpNgayTraThucTe";
            this.dtpNgayTraThucTe.Size = new System.Drawing.Size(267, 22);
            this.dtpNgayTraThucTe.TabIndex = 12;
            // 
            // cboMaSach
            // 
            this.cboMaSach.FormattingEnabled = true;
            this.cboMaSach.Location = new System.Drawing.Point(265, 120);
            this.cboMaSach.Name = "cboMaSach";
            this.cboMaSach.Size = new System.Drawing.Size(267, 24);
            this.cboMaSach.TabIndex = 9;
            this.cboMaSach.SelectedIndexChanged += new System.EventHandler(this.cboMaSach_SelectedIndexChanged);
            // 
            // cboMaDocGia
            // 
            this.cboMaDocGia.FormattingEnabled = true;
            this.cboMaDocGia.Location = new System.Drawing.Point(265, 72);
            this.cboMaDocGia.Name = "cboMaDocGia";
            this.cboMaDocGia.Size = new System.Drawing.Size(267, 24);
            this.cboMaDocGia.TabIndex = 8;
            this.cboMaDocGia.SelectedIndexChanged += new System.EventHandler(this.cboMaDocGia_SelectedIndexChanged);
            // 
            // pictureBoxHinhAnhDocGia
            // 
            this.pictureBoxHinhAnhDocGia.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxHinhAnhDocGia.Location = new System.Drawing.Point(1001, 34);
            this.pictureBoxHinhAnhDocGia.Name = "pictureBoxHinhAnhDocGia";
            this.pictureBoxHinhAnhDocGia.Size = new System.Drawing.Size(182, 199);
            this.pictureBoxHinhAnhDocGia.TabIndex = 41;
            this.pictureBoxHinhAnhDocGia.TabStop = false;
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(1036, 313);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(115, 27);
            this.btnReset.TabIndex = 17;
            this.btnReset.Text = "Reset Form";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(601, 34);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "Hình Ảnh";
            // 
            // pictureBoxHinhAnhSach
            // 
            this.pictureBoxHinhAnhSach.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxHinhAnhSach.Location = new System.Drawing.Point(730, 34);
            this.pictureBoxHinhAnhSach.Name = "pictureBoxHinhAnhSach";
            this.pictureBoxHinhAnhSach.Size = new System.Drawing.Size(182, 199);
            this.pictureBoxHinhAnhSach.TabIndex = 42;
            this.pictureBoxHinhAnhSach.TabStop = false;
            // 
            // btnTraSach
            // 
            this.btnTraSach.Location = new System.Drawing.Point(762, 313);
            this.btnTraSach.Name = "btnTraSach";
            this.btnTraSach.Size = new System.Drawing.Size(115, 27);
            this.btnTraSach.TabIndex = 16;
            this.btnTraSach.Text = "Trả Sách";
            this.btnTraSach.UseVisualStyleBackColor = true;
            this.btnTraSach.Click += new System.EventHandler(this.btnTraSach_Click);
            // 
            // FormQLPhieuMuon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1311, 659);
            this.Controls.Add(this.btnTraSach);
            this.Controls.Add(this.pictureBoxHinhAnhSach);
            this.Controls.Add(this.pictureBoxHinhAnhDocGia);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cboMaDocGia);
            this.Controls.Add(this.cboMaSach);
            this.Controls.Add(this.dtpNgayTraThucTe);
            this.Controls.Add(this.dtpNgayHenTra);
            this.Controls.Add(this.dgvQLMuonTra);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dtpNgayMuon);
            this.Controls.Add(this.txtMaMuon);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormQLPhieuMuon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Quản Lý Phiếu Mượn";
            this.Load += new System.EventHandler(this.FormQLMuonTra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvQLMuonTra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHinhAnhDocGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHinhAnhSach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvQLMuonTra;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DateTimePicker dtpNgayMuon;
        private System.Windows.Forms.TextBox txtMaMuon;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpNgayHenTra;
        private System.Windows.Forms.DateTimePicker dtpNgayTraThucTe;
        private System.Windows.Forms.ComboBox cboMaSach;
        private System.Windows.Forms.ComboBox cboMaDocGia;
        private System.Windows.Forms.PictureBox pictureBoxHinhAnhDocGia;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pictureBoxHinhAnhSach;
        private System.Windows.Forms.Button btnTraSach;
    }
}