using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace lfglnet
{
    public partial class Form_mb : Form
    {
        [DllImport("user32.dll")] //需添加using System.Runtime.InteropServices;         
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        


        public Form_mb()
        {
            InitializeComponent();
           
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
 
    
        }



        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0); 
        }

       

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form_mb_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics,
               panel2.ClientRectangle,
               Color.FromArgb(123, 93, 67),
               1,
               ButtonBorderStyle.Solid,
               Color.FromArgb(123, 93, 67),
               1,
               ButtonBorderStyle.Solid,
               Color.FromArgb(123, 93, 67),
               1,
               ButtonBorderStyle.Solid,
               Color.FromArgb(123, 93, 67),
               1,
               ButtonBorderStyle.Solid); 
        }


    }
}
