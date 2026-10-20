namespace NPC.Server
{
    partial class frmDangNhapServer
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblTen = new Label();
            lblMatKhau = new Label();
            txtTenDangNhap = new TextBox();
            txtMatKhau = new TextBox();
            picLoading = new PictureBox();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picLoading).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblTen
            // 
            lblTen.AutoSize = true;
            lblTen.Font = new Font("Rockwell", 14.25F, FontStyle.Bold);
            lblTen.ForeColor = SystemColors.ControlLightLight;
            lblTen.Location = new Point(19, 345);
            lblTen.Name = "lblTen";
            lblTen.Size = new Size(143, 23);
            lblTen.TabIndex = 0;
            lblTen.Text = "Tên đăng nhập";
            lblTen.Click += lblTen_Click;
            // 
            // lblMatKhau
            // 
            lblMatKhau.AutoSize = true;
            lblMatKhau.Font = new Font("Rockwell", 14.25F, FontStyle.Bold);
            lblMatKhau.ForeColor = SystemColors.ControlLightLight;
            lblMatKhau.Location = new Point(19, 382);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(96, 23);
            lblMatKhau.TabIndex = 1;
            lblMatKhau.Text = "Mật khẩu";
            lblMatKhau.Click += lblMatKhau_Click;
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Font = new Font("Rockwell", 14.25F);
            txtTenDangNhap.Location = new Point(168, 342);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(212, 30);
            txtTenDangNhap.TabIndex = 2;
            txtTenDangNhap.TextChanged += txtTenDangNhap_TextChanged;
            // 
            // txtMatKhau
            // 
            txtMatKhau.Font = new Font("Rockwell", 14.25F);
            txtMatKhau.Location = new Point(168, 378);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(212, 30);
            txtMatKhau.TabIndex = 3;
            txtMatKhau.TextChanged += txtMatKhau_TextChanged;
            // 
            // picLoading
            // 
            picLoading.BackColor = Color.Transparent;
            picLoading.Image = Properties.Resources.loading_1;
            picLoading.Location = new Point(680, 302);
            picLoading.Name = "picLoading";
            picLoading.Size = new Size(35, 34);
            picLoading.SizeMode = PictureBoxSizeMode.Zoom;
            picLoading.TabIndex = 4;
            picLoading.TabStop = false;
            picLoading.Click += picLoading_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Transparent;
            pictureBox1.Image = Properties.Resources.brte;
            pictureBox1.Location = new Point(-1, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(802, 451);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // frmDangNhapServer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlText;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(800, 450);
            Controls.Add(picLoading);
            Controls.Add(txtMatKhau);
            Controls.Add(txtTenDangNhap);
            Controls.Add(lblMatKhau);
            Controls.Add(lblTen);
            Controls.Add(pictureBox1);
            DoubleBuffered = true;
            Name = "frmDangNhapServer";
            Text = "Server";
            Load += frmDangNhapServer_Load;
            ((System.ComponentModel.ISupportInitialize)picLoading).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTen;
        private Label lblMatKhau;
        private TextBox txtTenDangNhap;
        private TextBox txtMatKhau;
        private PictureBox picLoading;
        private PictureBox pictureBox1;
    }
}
