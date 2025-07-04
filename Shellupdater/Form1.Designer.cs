namespace Shellupdater
    {
        partial class Form1
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
                this.txtConsoleOutput = new System.Windows.Forms.RichTextBox();
                this.btnGuncellemeleriKontrolEt = new System.Windows.Forms.Button();
                this.btnTumunuGuncelle = new System.Windows.Forms.Button();
                this.btnWingetYukle = new System.Windows.Forms.Button();
                this.progressBar1 = new System.Windows.Forms.ProgressBar();
                this.lblBaslik = new System.Windows.Forms.Label();
                this.SuspendLayout();
                // 
                // txtConsoleOutput
                // 
                this.txtConsoleOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
                this.txtConsoleOutput.BackColor = System.Drawing.Color.Black;
                this.txtConsoleOutput.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                this.txtConsoleOutput.ForeColor = System.Drawing.Color.Lime;
                this.txtConsoleOutput.Location = new System.Drawing.Point(12, 41);
                this.txtConsoleOutput.Name = "txtConsoleOutput";
                this.txtConsoleOutput.ReadOnly = true;
                this.txtConsoleOutput.Size = new System.Drawing.Size(776, 350);
                this.txtConsoleOutput.TabIndex = 0;
                this.txtConsoleOutput.Text = "";
                // 
                // btnGuncellemeleriKontrolEt
                // 
                this.btnGuncellemeleriKontrolEt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                this.btnGuncellemeleriKontrolEt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
                this.btnGuncellemeleriKontrolEt.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.btnGuncellemeleriKontrolEt.ForeColor = System.Drawing.Color.White;
                this.btnGuncellemeleriKontrolEt.Location = new System.Drawing.Point(12, 397);
                this.btnGuncellemeleriKontrolEt.Name = "btnGuncellemeleriKontrolEt";
                this.btnGuncellemeleriKontrolEt.Size = new System.Drawing.Size(180, 41);
                this.btnGuncellemeleriKontrolEt.TabIndex = 1;
                this.btnGuncellemeleriKontrolEt.Text = "Güncellemeleri Kontrol Et";
                this.btnGuncellemeleriKontrolEt.UseVisualStyleBackColor = false;
                this.btnGuncellemeleriKontrolEt.Click += new System.EventHandler(this.btnCheckUpdates_Click);
                // 
                // btnTumunuGuncelle
                // 
                this.btnTumunuGuncelle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
                this.btnTumunuGuncelle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
                this.btnTumunuGuncelle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.btnTumunuGuncelle.ForeColor = System.Drawing.Color.White;
                this.btnTumunuGuncelle.Location = new System.Drawing.Point(198, 397);
                this.btnTumunuGuncelle.Name = "btnTumunuGuncelle";
                this.btnTumunuGuncelle.Size = new System.Drawing.Size(180, 41);
                this.btnTumunuGuncelle.TabIndex = 2;
                this.btnTumunuGuncelle.Text = "Tümünü Güncelle";
                this.btnTumunuGuncelle.UseVisualStyleBackColor = false;
                this.btnTumunuGuncelle.Click += new System.EventHandler(this.btnInstallUpdates_Click);
                // 
                // btnWingetYukle
                // 
                this.btnWingetYukle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
                this.btnWingetYukle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
                this.btnWingetYukle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                this.btnWingetYukle.ForeColor = System.Drawing.Color.White;
                this.btnWingetYukle.Location = new System.Drawing.Point(608, 397);
                this.btnWingetYukle.Name = "btnWingetYukle";
                this.btnWingetYukle.Size = new System.Drawing.Size(180, 41);
                this.btnWingetYukle.TabIndex = 3;
                this.btnWingetYukle.Text = "Winget Yükle";
                this.btnWingetYukle.UseVisualStyleBackColor = false;
                this.btnWingetYukle.Click += new System.EventHandler(this.btnInstallWinget_Click);
                // 
                // progressBar1
                // 
                this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                | System.Windows.Forms.AnchorStyles.Right)));
                this.progressBar1.Location = new System.Drawing.Point(12, 444);
                this.progressBar1.Name = "progressBar1";
                this.progressBar1.Size = new System.Drawing.Size(776, 23);
                this.progressBar1.TabIndex = 4;
                // 
                // lblBaslik
                // 
                this.lblBaslik.AutoSize = true;
                this.lblBaslik.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                this.lblBaslik.ForeColor = System.Drawing.Color.White;
                this.lblBaslik.Location = new System.Drawing.Point(12, 9);
                this.lblBaslik.Name = "lblBaslik";
                this.lblBaslik.Size = new System.Drawing.Size(204, 21);
                this.lblBaslik.TabIndex = 5;
                this.lblBaslik.Text = "Winget Güncelleme Aracı";
                // 
                // Form1
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(48)))));
                this.ClientSize = new System.Drawing.Size(800, 479);
                this.Controls.Add(this.lblBaslik);
                this.Controls.Add(this.progressBar1);
                this.Controls.Add(this.btnWingetYukle);
                this.Controls.Add(this.btnTumunuGuncelle);
                this.Controls.Add(this.btnGuncellemeleriKontrolEt);
                this.Controls.Add(this.txtConsoleOutput);
                this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                this.MinimumSize = new System.Drawing.Size(600, 400);
                this.Name = "Form1";
                this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                this.Text = "Winget Güncelleme Aracı";
                this.Load += new System.EventHandler(this.Form1_Load);
                this.ResumeLayout(false);
                this.PerformLayout();

            }

            #endregion

            private System.Windows.Forms.RichTextBox txtConsoleOutput;
            private System.Windows.Forms.Button btnGuncellemeleriKontrolEt;
            private System.Windows.Forms.Button btnTumunuGuncelle;
            private System.Windows.Forms.Button btnWingetYukle;
            private System.Windows.Forms.ProgressBar progressBar1;
            private System.Windows.Forms.Label lblBaslik;
        }
    }