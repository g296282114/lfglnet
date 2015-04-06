using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace lfglnet
{
    public partial class Form3 : Form_mb
    {

        String eqid;
        Cliinf clinf;
  
        public Form3(String teqid="")
        {
            InitializeComponent();
            eqid = teqid;


        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            
            label1.Text = "添加/修改信息";
            if (eqid == "")
            {
                label1.Text = "添加手机";
               clinf = new Cliinf();

            }
            else
            {
                label1.Text = "修改手机";
                button2.Enabled = true;
                clinf = new Cliinf(eqid);
                textBox1.Text = clinf.name;
                textBox5.Text = clinf.eqid;
                checkBox1.Checked = clinf.used;
                textBox7.Text = clinf.createdate;
                textBox4.Text = clinf.eqname;
                textBox2.Text = clinf.beizhu;
                textBox3.Text = clinf.lstip;
                textBox8.Text = clinf.logodate;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (eqid == "")
            {
                if (lfcon.stastr == "" && label9.Text != "未连接设备")
                {
                    label9.Text = "未连接设备";
                    label9.ForeColor = Color.Red;
                }

                if (lfcon.stastr != "" && label9.Text != "已连接设备")
                {
                    label9.Text = "已连接设备";
                    label9.ForeColor = Color.Green;
                    addeqid();
                }

            }

         
        }
        private void addeqid()
        {
            XmlDocument dom = new XmlDocument();
            dom.Load(System.AppDomain.CurrentDomain.BaseDirectory + "client.xml");
            System.Random aa = new Random();
            bool congfu = false;
            string teqid = "";
            do
            {
                teqid = "c" + aa.Next(100000, 999999).ToString();
                foreach (XmlElement neqid in dom.DocumentElement.ChildNodes)
                {
                    if (neqid.Name == teqid)
                        congfu = true;
                }

            }
            while (congfu);
            textBox5.Text = teqid;
            textBox4.Text = lfcon.stastr;
            textBox7.Text = DateTime.Now.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("持设备人不能为空");
                return;
            }
            if (textBox5.Text.Trim() == "")
            {
                MessageBox.Show("无设备请求连接");
                return;
            }

            clinf.name = textBox1.Text;
            clinf.eqid = textBox5.Text;
            clinf.used = checkBox1.Checked;
            clinf.createdate = textBox7.Text;
            clinf.eqname = textBox4.Text;
            clinf.beizhu = textBox2.Text;
            clinf.addxml(textBox5.Text);
            if (eqid == "")
            {
                lfcon.frm1.SendToClient(lfcon.frm1.addclient, "11|" + clinf.eqid);

                MessageBox.Show("添加成功");
            }
            else
            {
                MessageBox.Show("保存成功");
            }

            lfcon.frm1.Updatecon_3();
            Close();
            
            

            
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            lfcon.frm1.chgstastr("");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            XmlDocument dom = new XmlDocument();
            dom.Load(System.AppDomain.CurrentDomain.BaseDirectory + "client.xml");
            XmlNode root = dom.SelectSingleNode("client");//查找<bookstore>
            if (root.SelectSingleNode(textBox5.Text) != null)
            {
                root.RemoveChild(root.SelectSingleNode(textBox5.Text));
                dom.Save(System.AppDomain.CurrentDomain.BaseDirectory + "client.xml");
            }

            lfcon.frm1.Updatecon_3();
            this.Close();
        }
    }
}
