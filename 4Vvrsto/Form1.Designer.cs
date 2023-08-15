namespace _4Vvrsto
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.gbZacni = new System.Windows.Forms.Button();
            this.napisSVV = new System.Windows.Forms.Label();
            this.gameBoardPanel = new System.Windows.Forms.Panel();
            this.gbTezka = new System.Windows.Forms.Button();
            this.gbLahki = new System.Windows.Forms.Button();
            this.napisTezavnost = new System.Windows.Forms.Label();
            this.gbReset = new System.Windows.Forms.Button();
            this.gbRobot = new System.Windows.Forms.Button();
            this.napisZmage = new System.Windows.Forms.Label();
            this.zmageRdec = new System.Windows.Forms.Label();
            this.zmageRumeni = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.gameBoardPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbZacni
            // 
            this.gbZacni.AutoSize = true;
            this.gbZacni.Location = new System.Drawing.Point(0, 270);
            this.gbZacni.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbZacni.Name = "gbZacni";
            this.gbZacni.Size = new System.Drawing.Size(163, 121);
            this.gbZacni.TabIndex = 0;
            this.gbZacni.Text = "Ti in še en igralec";
            this.gbZacni.UseVisualStyleBackColor = true;
            this.gbZacni.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gbZacni_Click);
            // 
            // napisSVV
            // 
            this.napisSVV.AutoSize = true;
            this.napisSVV.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.napisSVV.ForeColor = System.Drawing.Color.Red;
            this.napisSVV.Location = new System.Drawing.Point(171, 38);
            this.napisSVV.Name = "napisSVV";
            this.napisSVV.Size = new System.Drawing.Size(131, 162);
            this.napisSVV.TabIndex = 1;
            this.napisSVV.Text = "Štiri\r\n   v\r\nvrsto";
            // 
            // gameBoardPanel
            // 
            this.gameBoardPanel.Controls.Add(this.gbTezka);
            this.gameBoardPanel.Controls.Add(this.gbLahki);
            this.gameBoardPanel.Controls.Add(this.napisTezavnost);
            this.gameBoardPanel.Controls.Add(this.gbReset);
            this.gameBoardPanel.Controls.Add(this.napisSVV);
            this.gameBoardPanel.Controls.Add(this.gbRobot);
            this.gameBoardPanel.Controls.Add(this.gbZacni);
            this.gameBoardPanel.Location = new System.Drawing.Point(187, 12);
            this.gameBoardPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gameBoardPanel.Name = "gameBoardPanel";
            this.gameBoardPanel.Size = new System.Drawing.Size(603, 430);
            this.gameBoardPanel.TabIndex = 2;
            this.gameBoardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.Narisi);
            this.gameBoardPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gameBoardPanel_MouseClick);
            // 
            // gbTezka
            // 
            this.gbTezka.Location = new System.Drawing.Point(299, 302);
            this.gbTezka.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbTezka.Name = "gbTezka";
            this.gbTezka.Size = new System.Drawing.Size(115, 59);
            this.gbTezka.TabIndex = 8;
            this.gbTezka.Text = "Težka";
            this.gbTezka.UseVisualStyleBackColor = true;
            this.gbTezka.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gbTezka_MouseClick);
            // 
            // gbLahki
            // 
            this.gbLahki.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbLahki.Location = new System.Drawing.Point(36, 299);
            this.gbLahki.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbLahki.Name = "gbLahki";
            this.gbLahki.Size = new System.Drawing.Size(97, 59);
            this.gbLahki.TabIndex = 8;
            this.gbLahki.Text = "Lahka";
            this.gbLahki.UseVisualStyleBackColor = true;
            this.gbLahki.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gbLahki_MouseClick);
            // 
            // napisTezavnost
            // 
            this.napisTezavnost.AutoSize = true;
            this.napisTezavnost.Font = new System.Drawing.Font("Perpetua Titling MT", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.napisTezavnost.ForeColor = System.Drawing.Color.White;
            this.napisTezavnost.Location = new System.Drawing.Point(3, 164);
            this.napisTezavnost.Name = "napisTezavnost";
            this.napisTezavnost.Size = new System.Drawing.Size(483, 36);
            this.napisTezavnost.TabIndex = 7;
            this.napisTezavnost.Text = "Izberi težavnost robota:";
            // 
            // gbReset
            // 
            this.gbReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbReset.Location = new System.Drawing.Point(507, 177);
            this.gbReset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbReset.Name = "gbReset";
            this.gbReset.Size = new System.Drawing.Size(75, 75);
            this.gbReset.TabIndex = 6;
            this.gbReset.Text = "Nova igra";
            this.gbReset.UseVisualStyleBackColor = true;
            this.gbReset.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gbReset_Click);
            // 
            // gbRobot
            // 
            this.gbRobot.AutoSize = true;
            this.gbRobot.Location = new System.Drawing.Point(279, 270);
            this.gbRobot.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gbRobot.Name = "gbRobot";
            this.gbRobot.Size = new System.Drawing.Size(149, 121);
            this.gbRobot.TabIndex = 0;
            this.gbRobot.Text = "Ti in robot";
            this.gbRobot.UseVisualStyleBackColor = true;
            this.gbRobot.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gbRobot_Click);
            // 
            // napisZmage
            // 
            this.napisZmage.AutoSize = true;
            this.napisZmage.BackColor = System.Drawing.Color.Blue;
            this.napisZmage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.napisZmage.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.napisZmage.ForeColor = System.Drawing.Color.Transparent;
            this.napisZmage.Location = new System.Drawing.Point(-1, 81);
            this.napisZmage.Name = "napisZmage";
            this.napisZmage.Size = new System.Drawing.Size(182, 27);
            this.napisZmage.TabIndex = 3;
            this.napisZmage.Text = "ŠTEVILO ZMAG:";
            // 
            // zmageRdec
            // 
            this.zmageRdec.AutoSize = true;
            this.zmageRdec.BackColor = System.Drawing.Color.Red;
            this.zmageRdec.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zmageRdec.ForeColor = System.Drawing.Color.Black;
            this.zmageRdec.Location = new System.Drawing.Point(12, 188);
            this.zmageRdec.Name = "zmageRdec";
            this.zmageRdec.Size = new System.Drawing.Size(64, 69);
            this.zmageRdec.TabIndex = 4;
            this.zmageRdec.Text = "0";
            // 
            // zmageRumeni
            // 
            this.zmageRumeni.AutoSize = true;
            this.zmageRumeni.BackColor = System.Drawing.Color.Yellow;
            this.zmageRumeni.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zmageRumeni.ForeColor = System.Drawing.Color.Black;
            this.zmageRumeni.Location = new System.Drawing.Point(105, 188);
            this.zmageRumeni.Name = "zmageRumeni";
            this.zmageRumeni.Size = new System.Drawing.Size(64, 69);
            this.zmageRumeni.TabIndex = 5;
            this.zmageRumeni.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.zmageRumeni);
            this.Controls.Add(this.zmageRdec);
            this.Controls.Add(this.napisZmage);
            this.Controls.Add(this.gameBoardPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Štiri v vrsto";
            this.gameBoardPanel.ResumeLayout(false);
            this.gameBoardPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button gbZacni;
        private System.Windows.Forms.Label napisSVV;
        private System.Windows.Forms.Panel gameBoardPanel;
        private System.Windows.Forms.Label napisZmage;
        private System.Windows.Forms.Label zmageRdec;
        private System.Windows.Forms.Label zmageRumeni;
        private System.Windows.Forms.Button gbReset;
        private System.Windows.Forms.Button gbRobot;
        private System.Windows.Forms.Button gbTezka;
        private System.Windows.Forms.Button gbLahki;
        private System.Windows.Forms.Label napisTezavnost;
        private System.Windows.Forms.Timer timer1;
    }
}

