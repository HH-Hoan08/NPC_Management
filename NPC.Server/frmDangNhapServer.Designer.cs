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
            picNen = new PictureBox();
            btnConfirm = new Button();
            ((System.ComponentModel.ISupportInitialize)picLoading).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picNen).BeginInit();
            SuspendLayout();
            // 
            // lblTen
            // 
            lblTen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblTen.AutoSize = true;
            lblTen.Font = new Font("Rockwell", 14.25F, FontStyle.Bold);
            lblTen.ForeColor = SystemColors.ControlLightLight;
            lblTen.Location = new Point(22, 460);
            lblTen.Name = "lblTen";
            lblTen.Size = new Size(188, 29);
            lblTen.TabIndex = 0;
            lblTen.Text = "Tên đăng nhập";
            lblTen.Click += lblTen_Click;
            // 
            // lblMatKhau
            // 
            lblMatKhau.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lblMatKhau.AutoSize = true;
            lblMatKhau.Font = new Font("Rockwell", 14.25F, FontStyle.Bold);
            lblMatKhau.ForeColor = SystemColors.ControlLightLight;
            lblMatKhau.Location = new Point(22, 509);
            lblMatKhau.Name = "lblMatKhau";
            lblMatKhau.Size = new Size(126, 29);
            lblMatKhau.TabIndex = 1;
            lblMatKhau.Text = "Mật khẩu";
            lblMatKhau.Click += lblMatKhau_Click;
            // 
            // txtTenDangNhap
            // 
            txtTenDangNhap.Font = new Font("Rockwell", 14.25F);
            txtTenDangNhap.Location = new Point(216, 458);
            txtTenDangNhap.Margin = new Padding(3, 4, 3, 4);
            txtTenDangNhap.Name = "txtTenDangNhap";
            txtTenDangNhap.Size = new Size(242, 35);
            txtTenDangNhap.TabIndex = 2;
            txtTenDangNhap.TextChanged += txtTenDangNhap_TextChanged;
            txtTenDangNhap.KeyDown += txtTenDangNhap_KeyDown;
            // 
            // txtMatKhau
            // 
            txtMatKhau.Font = new Font("Rockwell", 14.25F);
            txtMatKhau.Location = new Point(216, 503);
            txtMatKhau.Margin = new Padding(3, 4, 3, 4);
            txtMatKhau.Name = "txtMatKhau";
            txtMatKhau.Size = new Size(242, 35);
            txtMatKhau.TabIndex = 3;
            txtMatKhau.TextChanged += txtMatKhau_TextChanged;
            txtMatKhau.KeyDown += txtMatKhau_KeyDown;
            // 
            // picLoading
            // 
            picLoading.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            picLoading.BackColor = Color.Transparent;
            picLoading.Image = Properties.Resources.loading_1;
            picLoading.Location = new Point(766, 483);
            picLoading.Margin = new Padding(3, 4, 3, 4);
            picLoading.Name = "picLoading";
            picLoading.Size = new Size(111, 97);
            picLoading.SizeMode = PictureBoxSizeMode.Zoom;
            picLoading.TabIndex = 4;
            picLoading.TabStop = false;
            picLoading.Click += picLoading_Click;
            // 
            // picNen
            // 
            picNen.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            picNen.BackColor = Color.Transparent;
            picNen.Image = Properties.Resources.brte;
            picNen.Location = new Point(-3, 1);
            picNen.Margin = new Padding(3, 4, 3, 4);
            picNen.Name = "picNen";
            picNen.Size = new Size(917, 599);
            picNen.SizeMode = PictureBoxSizeMode.StretchImage;
            picNen.TabIndex = 5;
            picNen.TabStop = false;
            picNen.Click += picNen_Click;
            // 
            // btnConfirm
            // 
            btnConfirm.Location = new Point(486, 483);
            btnConfirm.Margin = new Padding(3, 4, 3, 4);
            btnConfirm.Name = "btnConfirm";
            btnConfirm.Size = new Size(98, 37);
            btnConfirm.TabIndex = 6;
            btnConfirm.Text = "Xác nhận";
            btnConfirm.UseVisualStyleBackColor = true;
            btnConfirm.Click += btnConfirm_Click;
            // 
            // frmDangNhapServer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlText;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(914, 600);
            Controls.Add(btnConfirm);
            Controls.Add(picLoading);
            Controls.Add(txtMatKhau);
            Controls.Add(txtTenDangNhap);
            Controls.Add(lblMatKhau);
            Controls.Add(lblTen);
            Controls.Add(picNen);
            DoubleBuffered = true;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmDangNhapServer";
            Text = "Server";
            Load += frmDangNhapServer_Load;
            Resize += frmDangNhapServer_Resize;
            ((System.ComponentModel.ISupportInitialize)picLoading).EndInit();
            ((System.ComponentModel.ISupportInitialize)picNen).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTen;
        private Label lblMatKhau;
        private TextBox txtTenDangNhap;
        private TextBox txtMatKhau;
        private PictureBox picLoading;
        private PictureBox picNen;
        private Button btnConfirm;
    }
}
