namespace lfglnet
{
    partial class Form_mb
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.cImgButton2 = new imgbutton.CImgButton();
            this.cImgButton1 = new imgbutton.CImgButton();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(144)))), ((int)(((byte)(103)))));
            this.panel2.Controls.Add(this.cImgButton2);
            this.panel2.Controls.Add(this.cImgButton1);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(604, 34);
            this.panel2.TabIndex = 1;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            this.panel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseDown);
            // 
            // cImgButton2
            // 
            this.cImgButton2.BackColor = System.Drawing.Color.White;
            this.cImgButton2.bkal = 100;
            this.cImgButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cImgButton2.fimage = global::lfglnet.Properties.Resources.btn2_1;
            this.cImgButton2.Location = new System.Drawing.Point(540, 3);
            this.cImgButton2.matrixB = 1F;
            this.cImgButton2.matrixen = true;
            this.cImgButton2.matrixG = 1F;
            this.cImgButton2.matrixR = 1F;
            this.cImgButton2.Name = "cImgButton2";
            this.cImgButton2.Size = new System.Drawing.Size(28, 28);
            this.cImgButton2.TabIndex = 30;
            this.cImgButton2.tiptxt = "";
            this.cImgButton2.Click += new System.EventHandler(this.label3_Click);
            // 
            // cImgButton1
            // 
            this.cImgButton1.BackColor = System.Drawing.Color.Red;
            this.cImgButton1.bkal = 200;
            this.cImgButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cImgButton1.fimage = global::lfglnet.Properties.Resources.btn1_1;
            this.cImgButton1.Location = new System.Drawing.Point(572, 3);
            this.cImgButton1.matrixB = 1F;
            this.cImgButton1.matrixen = true;
            this.cImgButton1.matrixG = 1F;
            this.cImgButton1.matrixR = 1F;
            this.cImgButton1.Name = "cImgButton1";
            this.cImgButton1.Size = new System.Drawing.Size(28, 28);
            this.cImgButton1.TabIndex = 29;
            this.cImgButton1.tiptxt = "";
            this.cImgButton1.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "标题";
            // 
            // Form_mb
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 478);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form_mb";
            this.Text = "Form2";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Form_mb_Paint);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        public System.Windows.Forms.Label label1;
        private imgbutton.CImgButton cImgButton2;
        private imgbutton.CImgButton cImgButton1;

    }
}