namespace lfglnet
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_con = new System.Windows.Forms.Panel();
            this.panel_con3 = new System.Windows.Forms.Panel();
            this.panel_con2 = new System.Windows.Forms.Panel();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel_con1 = new System.Windows.Forms.Panel();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel_con_v = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.cImgButton3 = new imgbutton.CImgButton();
            this.cImgButton2 = new imgbutton.CImgButton();
            this.cImgButton1 = new imgbutton.CImgButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.cImgButton6 = new imgbutton.CImgButton();
            this.cImgButton5 = new imgbutton.CImgButton();
            this.cImgButton4 = new imgbutton.CImgButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.panel_con.SuspendLayout();
            this.panel_con2.SuspendLayout();
            this.panel_con1.SuspendLayout();
            this.panel_con_v.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 100000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "控费管理";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem1,
            this.toolStripSeparator2});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 60);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(97, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuItem2.Text = "打开";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(100, 22);
            this.toolStripMenuItem1.Text = "退出";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(97, 6);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(219)))), ((int)(((byte)(218)))));
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(1, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(279, 2);
            this.panel2.TabIndex = 12;
            // 
            // panel_con
            // 
            this.panel_con.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(235)))), ((int)(((byte)(233)))));
            this.panel_con.Controls.Add(this.panel_con3);
            this.panel_con.Controls.Add(this.panel_con2);
            this.panel_con.Controls.Add(this.panel_con1);
            this.panel_con.Location = new System.Drawing.Point(0, 2);
            this.panel_con.Name = "panel_con";
            this.panel_con.Size = new System.Drawing.Size(843, 412);
            this.panel_con.TabIndex = 18;
            // 
            // panel_con3
            // 
            this.panel_con3.AutoScroll = true;
            this.panel_con3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.panel_con3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_con3.Location = new System.Drawing.Point(562, 0);
            this.panel_con3.Name = "panel_con3";
            this.panel_con3.Size = new System.Drawing.Size(281, 412);
            this.panel_con3.TabIndex = 18;
            // 
            // panel_con2
            // 
            this.panel_con2.AutoScroll = true;
            this.panel_con2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.panel_con2.Controls.Add(this.listBox1);
            this.panel_con2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_con2.Location = new System.Drawing.Point(281, 0);
            this.panel_con2.Name = "panel_con2";
            this.panel_con2.Size = new System.Drawing.Size(281, 412);
            this.panel_con2.TabIndex = 17;
            // 
            // listBox1
            // 
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(0, 0);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(281, 412);
            this.listBox1.TabIndex = 0;
            // 
            // panel_con1
            // 
            this.panel_con1.AutoScroll = true;
            this.panel_con1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(246)))), ((int)(((byte)(242)))));
            this.panel_con1.Controls.Add(this.button3);
            this.panel_con1.Controls.Add(this.button2);
            this.panel_con1.Controls.Add(this.button1);
            this.panel_con1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_con1.Location = new System.Drawing.Point(0, 0);
            this.panel_con1.Name = "panel_con1";
            this.panel_con1.Size = new System.Drawing.Size(281, 412);
            this.panel_con1.TabIndex = 0;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(67, 22);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(160, 54);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(63, 54);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel_con_v
            // 
            this.panel_con_v.BackColor = System.Drawing.Color.Khaki;
            this.panel_con_v.Controls.Add(this.panel_con);
            this.panel_con_v.Controls.Add(this.panel2);
            this.panel_con_v.Location = new System.Drawing.Point(1, 162);
            this.panel_con_v.Name = "panel_con_v";
            this.panel_con_v.Padding = new System.Windows.Forms.Padding(1);
            this.panel_con_v.Size = new System.Drawing.Size(281, 413);
            this.panel_con_v.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.ForeColor = System.Drawing.Color.OliveDrab;
            this.label1.Location = new System.Drawing.Point(109, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 19);
            this.label1.TabIndex = 28;
            this.label1.Text = "安威士设备管理";
            // 
            // cImgButton3
            // 
            this.cImgButton3.bkal = 0;
            this.cImgButton3.Cursor = System.Windows.Forms.Cursors.Default;
            this.cImgButton3.fimage = ((System.Drawing.Bitmap)(resources.GetObject("cImgButton3.fimage")));
            this.cImgButton3.Location = new System.Drawing.Point(9, 10);
            this.cImgButton3.matrixB = 1F;
            this.cImgButton3.matrixen = false;
            this.cImgButton3.matrixG = 1F;
            this.cImgButton3.matrixR = 1F;
            this.cImgButton3.Name = "cImgButton3";
            this.cImgButton3.Size = new System.Drawing.Size(86, 20);
            this.cImgButton3.TabIndex = 27;
            this.cImgButton3.tiptxt = "";
            // 
            // cImgButton2
            // 
            this.cImgButton2.BackColor = System.Drawing.Color.White;
            this.cImgButton2.bkal = 100;
            this.cImgButton2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cImgButton2.fimage = global::lfglnet.Properties.Resources.btn2_1;
            this.cImgButton2.Location = new System.Drawing.Point(233, 2);
            this.cImgButton2.matrixB = 1F;
            this.cImgButton2.matrixen = true;
            this.cImgButton2.matrixG = 1F;
            this.cImgButton2.matrixR = 1F;
            this.cImgButton2.Name = "cImgButton2";
            this.cImgButton2.Size = new System.Drawing.Size(24, 22);
            this.cImgButton2.TabIndex = 26;
            this.cImgButton2.tiptxt = "";
            this.cImgButton2.Click += new System.EventHandler(this.cImgButton2_Click);
            // 
            // cImgButton1
            // 
            this.cImgButton1.BackColor = System.Drawing.Color.Red;
            this.cImgButton1.bkal = 200;
            this.cImgButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cImgButton1.fimage = global::lfglnet.Properties.Resources.btn1_1;
            this.cImgButton1.Location = new System.Drawing.Point(257, 2);
            this.cImgButton1.matrixB = 1F;
            this.cImgButton1.matrixen = true;
            this.cImgButton1.matrixG = 1F;
            this.cImgButton1.matrixR = 1F;
            this.cImgButton1.Name = "cImgButton1";
            this.cImgButton1.Size = new System.Drawing.Size(24, 22);
            this.cImgButton1.TabIndex = 25;
            this.cImgButton1.tiptxt = "";
            this.cImgButton1.Click += new System.EventHandler(this.cImgButton1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = global::lfglnet.Properties.Resources.bg1;
            this.pictureBox1.Location = new System.Drawing.Point(1, 1);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(281, 123);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(238)))), ((int)(((byte)(232)))));
            this.panel4.Controls.Add(this.cImgButton6);
            this.panel4.Controls.Add(this.cImgButton5);
            this.panel4.Controls.Add(this.cImgButton4);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(1, 124);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(281, 40);
            this.panel4.TabIndex = 29;
            // 
            // cImgButton6
            // 
            this.cImgButton6.bkal = 0;
            this.cImgButton6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cImgButton6.fimage = global::lfglnet.Properties.Resources.ti3;
            this.cImgButton6.Location = new System.Drawing.Point(210, 7);
            this.cImgButton6.matrixB = 0.9F;
            this.cImgButton6.matrixen = true;
            this.cImgButton6.matrixG = 1.4F;
            this.cImgButton6.matrixR = 3.2F;
            this.cImgButton6.Name = "cImgButton6";
            this.cImgButton6.Size = new System.Drawing.Size(25, 25);
            this.cImgButton6.TabIndex = 5;
            this.cImgButton6.tiptxt = "";
            this.cImgButton6.Click += new System.EventHandler(this.userControl_Click);
            // 
            // cImgButton5
            // 
            this.cImgButton5.bkal = 0;
            this.cImgButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cImgButton5.fimage = global::lfglnet.Properties.Resources.ti2;
            this.cImgButton5.Location = new System.Drawing.Point(122, 7);
            this.cImgButton5.matrixB = 0.9F;
            this.cImgButton5.matrixen = true;
            this.cImgButton5.matrixG = 1.4F;
            this.cImgButton5.matrixR = 3.2F;
            this.cImgButton5.Name = "cImgButton5";
            this.cImgButton5.Size = new System.Drawing.Size(25, 25);
            this.cImgButton5.TabIndex = 4;
            this.cImgButton5.tiptxt = "";
            this.cImgButton5.Click += new System.EventHandler(this.userControl_Click);
            // 
            // cImgButton4
            // 
            this.cImgButton4.bkal = 0;
            this.cImgButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cImgButton4.fimage = global::lfglnet.Properties.Resources.ti1;
            this.cImgButton4.Location = new System.Drawing.Point(40, 7);
            this.cImgButton4.matrixB = 1.5F;
            this.cImgButton4.matrixen = true;
            this.cImgButton4.matrixG = 1.5F;
            this.cImgButton4.matrixR = 1.5F;
            this.cImgButton4.Name = "cImgButton4";
            this.cImgButton4.Size = new System.Drawing.Size(25, 25);
            this.cImgButton4.TabIndex = 3;
            this.cImgButton4.tiptxt = "";
            this.cImgButton4.Click += new System.EventHandler(this.userControl_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(235)))), ((int)(((byte)(227)))));
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(1, 574);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(281, 45);
            this.panel3.TabIndex = 30;
            // 
            // label2
            // 
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(178, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 0;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(90)))), ((int)(((byte)(69)))));
            this.ClientSize = new System.Drawing.Size(283, 620);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cImgButton3);
            this.Controls.Add(this.cImgButton2);
            this.Controls.Add(this.cImgButton1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel_con_v);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.TopMost = true;
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel_con.ResumeLayout(false);
            this.panel_con2.ResumeLayout(false);
            this.panel_con1.ResumeLayout(false);
            this.panel_con_v.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel_con;
        private System.Windows.Forms.Panel panel_con3;
        private System.Windows.Forms.Panel panel_con2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel_con1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel_con_v;
        private System.Windows.Forms.Label label1;
        private imgbutton.CImgButton cImgButton3;
        private imgbutton.CImgButton cImgButton2;
        private imgbutton.CImgButton cImgButton1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel4;
        private imgbutton.CImgButton cImgButton6;
        private imgbutton.CImgButton cImgButton5;
        private imgbutton.CImgButton cImgButton4;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
    }
}

