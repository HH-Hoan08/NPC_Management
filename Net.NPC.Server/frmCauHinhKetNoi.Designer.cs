namespace Net.NPC.Server
{
    partial class frmCauHinhKetNoi
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblTenServer = new System.Windows.Forms.Label();
            this.txtTenServer = new System.Windows.Forms.TextBox();
            this.lblTenCSDL = new System.Windows.Forms.Label();
            this.txtTenCSDL = new System.Windows.Forms.TextBox();
            this.lblXacThuc = new System.Windows.Forms.Label();
            this.cboXacThuc = new System.Windows.Forms.ComboBox();
            this.lblUserSQL = new System.Windows.Forms.Label();
            this.txtUserSQL = new System.Windows.Forms.TextBox();
            this.lblPassSQL = new System.Windows.Forms.Label();
            this.txtPassSQL = new System.Windows.Forms.TextBox();
            this.btnKiemTraKetNoi = new System.Windows.Forms.Button();
            this.btnLuuCauHinh = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTieuDe
            // 
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.ForeColor = System.Drawing.Color.Navy;
            this.lblTieuDe.Location = new System.Drawing.Point(12, 18);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(460, 35);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "CẤU HÌNH KẾT NỐI CƠ SỞ DỮ LIỆU";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTenServer
            // 
            this.lblTenServer.AutoSize = true;
            this.lblTenServer.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenServer.Location = new System.Drawing.Point(35, 75);
            this.lblTenServer.Name = "lblTenServer";
            this.lblTenServer.Size = new System.Drawing.Size(84, 21);
            this.lblTenServer.TabIndex = 1;
            this.lblTenServer.Text = "Tên Server:";
            // 
            // txtTenServer
            // 
            this.txtTenServer.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenServer.Location = new System.Drawing.Point(165, 72);
            this.txtTenServer.Name = "txtTenServer";
            this.txtTenServer.Size = new System.Drawing.Size(280, 29);
            this.txtTenServer.TabIndex = 2;
            // 
            // lblTenCSDL
            // 
            this.lblTenCSDL.AutoSize = true;
            this.lblTenCSDL.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenCSDL.Location = new System.Drawing.Point(35, 118);
            this.lblTenCSDL.Name = "lblTenCSDL";
            this.lblTenCSDL.Size = new System.Drawing.Size(77, 21);
            this.lblTenCSDL.TabIndex = 3;
            this.lblTenCSDL.Text = "Tên CSDL:";
            // 
            // txtTenCSDL
            // 
            this.txtTenCSDL.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenCSDL.Location = new System.Drawing.Point(165, 115);
            this.txtTenCSDL.Name = "txtTenCSDL";
            this.txtTenCSDL.Size = new System.Drawing.Size(280, 29);
            this.txtTenCSDL.TabIndex = 4;
            // 
            // lblXacThuc
            // 
            this.lblXacThuc.AutoSize = true;
            this.lblXacThuc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblXacThuc.Location = new System.Drawing.Point(35, 161);
            this.lblXacThuc.Name = "lblXacThuc";
            this.lblXacThuc.Size = new System.Drawing.Size(74, 21);
            this.lblXacThuc.TabIndex = 5;
            this.lblXacThuc.Text = "Xác thực:";
            // 
            // cboXacThuc
            // 
            this.cboXacThuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboXacThuc.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboXacThuc.FormattingEnabled = true;
            this.cboXacThuc.Location = new System.Drawing.Point(165, 158);
            this.cboXacThuc.Name = "cboXacThuc";
            this.cboXacThuc.Size = new System.Drawing.Size(280, 29);
            this.cboXacThuc.TabIndex = 6;
            this.cboXacThuc.SelectedIndexChanged += new System.EventHandler(this.cboXacThuc_SelectedIndexChanged);
            // 
            // lblUserSQL
            // 
            this.lblUserSQL.AutoSize = true;
            this.lblUserSQL.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserSQL.Location = new System.Drawing.Point(35, 204);
            this.lblUserSQL.Name = "lblUserSQL";
            this.lblUserSQL.Size = new System.Drawing.Size(114, 21);
            this.lblUserSQL.TabIndex = 7;
            this.lblUserSQL.Text = "Tài khoản (User):";
            // 
            // txtUserSQL
            // 
            this.txtUserSQL.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUserSQL.Location = new System.Drawing.Point(165, 201);
            this.txtUserSQL.Name = "txtUserSQL";
            this.txtUserSQL.Size = new System.Drawing.Size(280, 29);
            this.txtUserSQL.TabIndex = 8;
            // 
            // lblPassSQL
            // 
            this.lblPassSQL.AutoSize = true;
            this.lblPassSQL.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassSQL.Location = new System.Drawing.Point(35, 247);
            this.lblPassSQL.Name = "lblPassSQL";
            this.lblPassSQL.Size = new System.Drawing.Size(78, 21);
            this.lblPassSQL.TabIndex = 9;
            this.lblPassSQL.Text = "Mật khẩu:";
            // 
            // txtPassSQL
            // 
            this.txtPassSQL.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassSQL.Location = new System.Drawing.Point(165, 244);
            this.txtPassSQL.Name = "txtPassSQL";
            this.txtPassSQL.Size = new System.Drawing.Size(280, 29);
            this.txtPassSQL.TabIndex = 10;
            this.txtPassSQL.UseSystemPasswordChar = true;
            // 
            // btnKiemTraKetNoi
            // 
            this.btnKiemTraKetNoi.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnKiemTraKetNoi.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKiemTraKetNoi.Location = new System.Drawing.Point(165, 295);
            this.btnKiemTraKetNoi.Name = "btnKiemTraKetNoi";
            this.btnKiemTraKetNoi.Size = new System.Drawing.Size(135, 38);
            this.btnKiemTraKetNoi.TabIndex = 11;
            this.btnKiemTraKetNoi.Text = "Kiểm tra kết nối";
            this.btnKiemTraKetNoi.UseVisualStyleBackColor = false;
            this.btnKiemTraKetNoi.Click += new System.EventHandler(this.btnKiemTraKetNoi_Click);
            // 
            // btnLuuCauHinh
            // 
            this.btnLuuCauHinh.BackColor = System.Drawing.Color.Navy;
            this.btnLuuCauHinh.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLuuCauHinh.ForeColor = System.Drawing.Color.White;
            this.btnLuuCauHinh.Location = new System.Drawing.Point(310, 295);
            this.btnLuuCauHinh.Name = "btnLuuCauHinh";
            this.btnLuuCauHinh.Size = new System.Drawing.Size(135, 38);
            this.btnLuuCauHinh.TabIndex = 12;
            this.btnLuuCauHinh.Text = "Lưu cấu hình";
            this.btnLuuCauHinh.UseVisualStyleBackColor = false;
            this.btnLuuCauHinh.Click += new System.EventHandler(this.btnLuuCauHinh_Click);
            // 
            // frmCauHinhKetNoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(484, 360);
            this.Controls.Add(this.btnLuuCauHinh);
            this.Controls.Add(this.btnKiemTraKetNoi);
            this.Controls.Add(this.txtPassSQL);
            this.Controls.Add(this.lblPassSQL);
            this.Controls.Add(this.txtUserSQL);
            this.Controls.Add(this.lblUserSQL);
            this.Controls.Add(this.cboXacThuc);
            this.Controls.Add(this.lblXacThuc);
            this.Controls.Add(this.txtTenCSDL);
            this.Controls.Add(this.lblTenCSDL);
            this.Controls.Add(this.txtTenServer);
            this.Controls.Add(this.lblTenServer);
            this.Controls.Add(this.lblTieuDe);
            this.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCauHinhKetNoi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cấu Hình Kết Nối CSDL";
            this.Load += new System.EventHandler(this.frmCauHinhKetNoi_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblTenServer;
        private System.Windows.Forms.TextBox txtTenServer;
        private System.Windows.Forms.Label lblTenCSDL;
        private System.Windows.Forms.TextBox txtTenCSDL;
        private System.Windows.Forms.Label lblXacThuc;
        private System.Windows.Forms.ComboBox cboXacThuc;
        private System.Windows.Forms.Label lblUserSQL;
        private System.Windows.Forms.TextBox txtUserSQL;
        private System.Windows.Forms.Label lblPassSQL;
        private System.Windows.Forms.TextBox txtPassSQL;
        private System.Windows.Forms.Button btnKiemTraKetNoi;
        private System.Windows.Forms.Button btnLuuCauHinh;
    }
}