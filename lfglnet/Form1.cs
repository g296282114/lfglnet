using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Runtime.InteropServices;
using imgbtn;
using System.Xml;
using System.Collections;
using ctbtn;
using System.Data.SqlClient;
using System.Data.OleDb;
using imgbutton;
using System.IO;
using Newtonsoft.Json;


namespace lfglnet
{

    public partial class Form1 : Form
    {
        static Mutex DeviceUsing = new Mutex(false, "gl_DeviceUsing");
        static Mutex CallCmdUsing = new Mutex(false, "gl_CallCmdUsing");
        torsion.Model.SoftInfo gl_si = new torsion.Model.SoftInfo();
        List<SoftModel.TDeviceList> gl_lst = new List<SoftModel.TDeviceList>();


        string secretKey = "";
        //public string post_url = "http://localhost:6625/glf/InAttendanceSetInfo";
        //public string post_url = "http://localhost:6625/glf/getpost";
        //public string post_url = "http://torsion.apphb.com";
        public string post_url;

        List<torsion.Model.DeviceInfo.DeviceSearch> eqsearch = new List<torsion.Model.DeviceInfo.DeviceSearch>();
        List<TEQinfo> eqinfos = new List<TEQinfo>();
        List<Tsqluser> sqluser = new List<Tsqluser>();
        List<Tsqlitem> sqlitem = new List<Tsqlitem>();
        List<String> sendstr = new List<String>();
        System.Net.Sockets.Socket clientsocket;
        System.Collections.ArrayList clients;
        System.Threading.Thread clientservice;
        System.Threading.Thread threadListen;
        System.Net.Sockets.TcpListener listener;
        System.Net.IPAddress ipAddress;
        public System.Net.Sockets.Socket addclient;
        Int32 listenport;
        public delegate void MyInvoke(string s);

        [DllImport("Kernel32.dll")]
        public static extern bool RtlMoveMemory(ref AnvizNew.CLOCKINGRECORD Destination, int Source, int Length);
        [DllImport("Kernel32.dll")]
        public static extern bool RtlMoveMemory(ref AnvizNew.PERSONINFO Destination, int Source, int Length);
        [DllImport("Kernel32.dll")]
        public static extern bool RtlMoveMemory(ref AnvizNew.PERSONINFOEX Destination, int Source, int Length);
        [DllImport("Kernel32.dll")]
        public static extern bool RtlMoveMemory(ref int Destination, int Source, int Length);

        [DllImport("Kernel32.dll")]
        public static extern bool RtlMoveMemory(byte[] Destination, int Source, int Length);
        [DllImport("Kernel32.dll")]
        public static extern void GetLocalTime(ref AnvizNew.SYSTEMTIME lpSystemTime);
        [DllImport("user32.dll")] //需添加using System.Runtime.InteropServices;         
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;
        public const int WMSZ_BOTTOM = 0xF006;

        int descx = 0;

        public string[] strtitle = new string[4] { "lfgl", "院长", "信息", "提示" };
        public string[] fhstr = new string[5];
        Boolean thclient = true;

        private delegate void _SafeCall_bl(Boolean bl);
        private delegate void _SafeCall();
        private delegate void _SafeCall_int(int var1);
        private delegate void DEnopar();
        private delegate void Updatelogoeq(tctbtn tb, Boolean bl);
        private delegate void Update2(String str);

        public Form1()
        {
            InitializeComponent();
            sendstr.Clear();


        }
        private void changeNotifyIco()
        {
            notifyIcon1.Icon = Properties.Resources.tp2;
            //if (label2.InvokeRequired)
            //{
            //    // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
            //    Action<string> actionDelegate = (x) => { this.label2.Text = x.ToString(); };
            //    // 或者
            //    // Action<string> actionDelegate = delegate(string txt) { this.label2.Text = txt; };
            //    this.label2.Invoke(actionDelegate, str);
            //}
            //else
            //{
            //    this.label2.Text = str.ToString();
            //}

        }
        private void tctbtn_UserControlBtnDoubleClicked(object sender, EventArgs e)
        {
            tctbtn tb = (tctbtn)sender;
            Form3 frm3 = new Form3(tb.Name.Substring(tb.Name.IndexOf('_') + 1, tb.Name.Length - tb.Name.IndexOf('_') - 1));
            frm3.Show();

        }

        private void Updatecon_1(tctbtn tb, Boolean bl)
        {
            if (bl)
                panel_con1.Controls.Add(tb);
            else
                panel_con1.Controls.Remove(tb);
        }

        public void Updatecon_2(String str)
        {
            if (str[str.Length - 1] == '%')
            {
                str = str.PadLeft(5);
                string str1 = textBox1.Text;
                if (str1[str1.Length - 1] == '%')
                {
                    str1 = str1.Substring(0, str1.Length - 5);
                }
                str1 += str;
                textBox1.Text = str1;
            }
            else
            {
                textBox1.AppendText(System.Environment.NewLine + DateTime.Now.ToString("MM/dd HH:mm:ss") + System.Environment.NewLine + "    " + str);
            }


        }

        public void UpLabel2()
        {
            label2.Text = DateTime.Now.ToString();
        }

        public void Updatecon_3()
        {
            panel_con3.Controls.Clear();
            XmlDocument dom = new XmlDocument();
            dom.Load(System.AppDomain.CurrentDomain.BaseDirectory + "client.xml");
            int i = 0;
            foreach (XmlElement neqid in dom.DocumentElement.ChildNodes)
            {
                tctbtn ttct = new tctbtn();
                ttct.Name = "tct_" + neqid.Name;
                ttct.Dock = DockStyle.Top;
                ttct.UserControlBtnDoubleClicked += new tctbtn.BtnDoubleClickHandle(tctbtn_UserControlBtnDoubleClicked);

                foreach (XmlElement tname in neqid.ChildNodes)
                {
                    switch (tname.Name)
                    {

                        case "eqname": ttct.eqname = tname.InnerText;
                            break;

                        case "createdate": ttct.eqinfo = tname.InnerText;
                            break;


                    }
                }
                ttct.eqstate = 0;
                panel_con3.Controls.Add(ttct);
            }
        }
        //public TEQinfo getEq(int clientid)
        //{
        //    TEQinfo req = null;
        //    foreach (TEQinfo teq in eqinfos)
        //    {
        //        if (teq.clientid == clientid)
        //        {
        //            req = teq;
        //        }

        //    }
        //    return req;
        //}





        public void Updatecon_4()
        {
            using (SqlConnection sqlcon = new SqlConnection(lfcon._constring))
            {
                try
                {
                    SqlCommand command = new SqlCommand("select * from fingerclient;", sqlcon);
                    command.Connection.Open();
                    SqlDataReader sqlreader = command.ExecuteReader();
                    eqinfos.Clear();
                    while (sqlreader.Read())
                    {
                        //public int clientid;
                        //public string clientName;
                        //public int linkMode;
                        //public string ipAddress;
                        //public int clientNumber;
                        tctbtn ttct = new tctbtn();
                        ttct.Name = "tct_" + sqlreader["clientid"].ToString();
                        ttct.Dock = DockStyle.Top;
                        ttct.UserControlBtnDoubleClicked += new tctbtn.BtnDoubleClickHandle(tctbtn_UserControlBtnDoubleClicked);
                        ttct.eqname = sqlreader["clientname"].ToString();
                        ttct.eqinfo = sqlreader["linkmode"].ToString();
                        ttct.eqstate = 0;

                        TEQinfo teq = new TEQinfo();
                        teq.clientid = Convert.ToInt32(sqlreader["clientid"]);
                        teq.clientName = sqlreader["clientname"].ToString();
                        teq.linkMode = Convert.ToInt32(sqlreader["linkMode"]);
                        teq.ipAddress = sqlreader["ipAddress"].ToString();
                        teq.clientNumber = Convert.ToInt32(sqlreader["clientNumber"]);
                        teq.ctbtn = ttct;
                        eqinfos.Add(teq);




                        panel_con1.Controls.Add(ttct);
                    }
                    sqlreader.Close();
                    sqlcon.Close();

                }
                catch (SqlException ex)
                {

                    // throw ex;
                }
            }


        }

        public void chgstastr(string str)
        {

        }

        private void StartListening()
        {
            try
            {
                listener = new System.Net.Sockets.TcpListener(ipAddress, listenport);

                listener.Start();

            }
            catch (Exception e)
            {
                MessageBox.Show("listening Error:" + e.Message);
            }
            while (thclient)
            {
                try
                {
                    clientsocket = listener.AcceptSocket();
                    clientservice = new System.Threading.Thread(new System.Threading.ThreadStart(ServiceClient));
                    clientservice.Start();
                }
                catch (Exception)
                {

                }
            }

        }
        private Boolean clientadd(Client cclient)
        {
            foreach (Client tc in clients)
            {
                if (tc.Name.CompareTo(cclient.Name) == 0)
                {

                }
            }
            clients.Add(cclient);
            Cliinf ci = new Cliinf(cclient.Name);
            ci.lstip = cclient.Host.ToString();
            ci.logodate = DateTime.Now.ToString();
            ci.addxml(cclient.Name);
            tctbtn ttb = new tctbtn();
            ttb.Name = "tct_" + ci.eqid;
            ttb.Dock = DockStyle.Top;
            ttb.eqname = ci.eqname;
            ttb.eqinfo = ci.logodate;
            ttb.UserControlBtnDoubleClicked += new tctbtn.BtnDoubleClickHandle(tctbtn_UserControlBtnDoubleClicked);
            this.BeginInvoke(new Updatelogoeq(Updatecon_1), new object[] { ttb, true });
            return false;

        }

        private Boolean clientdel(Client cclient)
        {

            foreach (Client tc in clients)
            {
                if (tc.Name.CompareTo(cclient.Name) == 0)
                {
                    foreach (Control c in panel_con1.Controls)
                    {
                        if (c is tctbtn)
                        {
                            if (c.Name == "tct_" + tc.Name)
                            {
                                clients.Remove(tc);
                                this.BeginInvoke(new Updatelogoeq(Updatecon_1), new object[] { c, false });
                                return true;
                            }
                        }
                    }


                }
            }
            return false;

        }

        private void ServiceClient()
        {
            System.Net.Sockets.Socket client = clientsocket;
            Cliinf clf = null;
            bool keepalive = true;
            bool keepjoin = false;
            if (client == null) keepalive = false;
            else keepalive = true;
            while (keepalive)
            {
                Byte[] buffer = new Byte[1024];
                int bufLen = 0;
                try
                {
                    bufLen = client.Available;
                    client.Receive(buffer, 0, bufLen, System.Net.Sockets.SocketFlags.None);
                    if (bufLen == 0) continue;
                    string clientcommand = System.Text.Encoding.UTF8.GetString(buffer).Substring(2, bufLen);
                    string[] tokens = ((clientcommand.Replace("\0", "").Replace("\f", "")).Trim()).Split(new Char[] { '|' });

                    switch (tokens[0].Trim())
                    {
                        case "10":
                            chgstastr(tokens[1].Trim());

                            addclient = client;
                            SendToClient(addclient, "10|等待授权");
                            break;
                        case "20":
                            try
                            {
                                Boolean secc = false;
                                foreach (Client tc in clients)
                                {
                                    if (tc.Name.CompareTo(tokens[1]) == 0)
                                    {
                                        secc = true;

                                    }
                                }
                                if (!secc)
                                {
                                    XmlDocument dom = new XmlDocument();
                                    dom.Load(Application.StartupPath + "\\client.xml");
                                    Boolean secl = false;
                                    foreach (XmlElement eqid in dom.DocumentElement.ChildNodes)
                                    {
                                        if (tokens[1].CompareTo(eqid.Name) == 0)
                                        {
                                            secl = true;

                                        }
                                    }

                                    if (secl)
                                    {
                                        System.Net.EndPoint ep = client.RemoteEndPoint;
                                        Client c = new Client(tokens[1], ep, clientservice, client);
                                        SendToClient(c.Sock, "20|success");
                                        clientadd(c);
                                        clf = new Cliinf(tokens[1]);
                                        this.BeginInvoke(new Update2(Updatecon_2), new object[] { clf.eqname + ":连接" });

                                    }
                                    else
                                    {
                                        SendToClient(client, "20|未添加设备");
                                    }
                                }
                                else
                                {
                                    SendToClient(client, "20|success");
                                }



                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message.ToString());

                            }
                            break;
                        case "12":
                            int remove = 0;

                            bool found = false;

                            int ccli = clients.Count;

                            string clientname = "";

                            for (int n = 0; n < clients.Count; n++)
                            {

                                Client cl = (Client)clients[n];

                                if (cl.Name.CompareTo(tokens[1]) != 0)

                                    SendToClient(cl.Sock, clientcommand);
                                else
                                {

                                    SendToClient(cl.Sock, "QUIT|");

                                    clientname = cl.Name + "|" + cl.Host.ToString();

                                    remove = n;

                                    found = true;
                                }
                            }

                            if (found)
                            {



                                clients.RemoveAt(remove);

                                keepalive = false;
                            }
                            break;
                        case "40":
                            int useri = 0;
                            try
                            {
                                if (tokens[1].CompareTo("user") == 0)
                                {
                                    udsqluser();
                                    useri = sqluser.Count - 1;
                                }
                                else
                                {
                                    useri = int.Parse(tokens[1]) - 1;

                                }


                                if (useri >= 0)
                                {
                                    SendToClient(client, "40|" + useri + "," + sqluser[useri].staffcode + "," + sqluser[useri].staffname + "," + sqluser[useri].sectname + "," + sqluser[useri].emercnt);
                                    if (useri == 0)
                                        sqluser.Clear();
                                }

                            }
                            catch (System.Exception ex)
                            {

                            }

                            break;
                        case "50":
                            int userj = 0;
                            try
                            {
                                if (tokens[1].CompareTo("user") == 0)
                                {
                                    udsqlitem();
                                    userj = sqlitem.Count - 1;
                                }
                                else
                                {
                                    userj = int.Parse(tokens[1]) - 1;

                                }


                                if (userj >= 0)
                                {
                                    SendToClient(client, "50|" + userj + "," + sqlitem[userj].itemcode + "," + sqlitem[userj].itemname + "," + sqlitem[userj].sectname);
                                    if (userj == 0)
                                        sqlitem.Clear();

                                }

                            }
                            catch (System.Exception ex)
                            {

                            }

                            break;
                        case "60":
                            int userk = 0;
                            try
                            {
                                if (tokens[1].CompareTo("user") == 0)
                                {
                                    udsqluser();
                                    userk = sqluser.Count - 1;
                                }
                                else
                                {
                                    userk = int.Parse(tokens[1]) - 1;

                                }


                                if (userk >= 0)
                                {
                                    SendToClient(client, "60|" + userk + "," + sqluser[userk].staffcode + "," + sqluser[userk].emercnt);
                                    if (userk == 0)
                                        sqluser.Clear();
                                }
                            }
                            catch (System.Exception ex)
                            {

                            }

                            break;
                        case "70":
                            try
                            {

                                string[] tmp = tokens[1].Split(',');
                                string query = "update lk_usercfg set emercnt =  " + tmp[1] + " where staffcode = '" + tmp[0] + "';";
                                SqlCommand objSqlCommand = new SqlCommand(query, lfcon.con);
                                SendToClient(client, "70|" + tmp[0] + "," + tmp[1]);
                                this.BeginInvoke(new Update2(Updatecon_2), new object[] { tmp[2] + "_" + tmp[0] + "急诊数更新为" + tmp[1] });


                            }
                            catch (System.Exception ex)
                            {
                                SendToClient(client, "70|" + ex.Message);
                            }

                            break;
                        case "80":


                            string[] tmp1 = tokens[1].Split(',');
                            fhstr[0] = tmp1[0];
                            fhstr[1] = tmp1[1];
                            fhstr[2] = tmp1[2];
                            fhstr[3] = tmp1[3];
                            fhstr[4] = tmp1[4];

                            ydsqlfh(client, int.Parse(fhstr[2]));
                            if (clf != null)
                                this.BeginInvoke(new Update2(Updatecon_2), new object[] { clf.eqname + ":放号" + fhstr[0] + "_" + fhstr[1] + "_" + fhstr[2] + "_" + fhstr[3] + "_" + fhstr[4] });


                            break;
                        case "90":

                            ydsqlfh(client, int.Parse(tokens[1]));


                            break;
                        default:
                            break;
                    }
                    timer2.Enabled = false;
                    timer2.Enabled = true;

                }
                catch (Exception)
                {
                    keepalive = false;
                    foreach (Client tc in clients)
                    {
                        if (tc.Sock == client)
                        {
                            clientdel(tc);
                            break;
                        }
                    }

                }
            }
        }

        private string GetChatterList()
        {
            string result = "";
            for (int i = 0; i < clients.Count; i++)
            {
                result += ((Client)clients[i]).Name + "|";
            }
            return result;
        }

        public Boolean SendToClient(Socket sk, string clientCommand)
        {
            try
            {
                sendstr.Add(clientCommand);
                do
                {
                    Thread.Sleep(10);
                    Byte[] message = System.Text.Encoding.UTF8.GetBytes(sendstr[0]);

                    System.Net.Sockets.Socket s = sk;

                    if (s.Connected)
                    {
                        s.Send(message, message.Length, 0);
                        sendstr.RemoveAt(0);
                        return true;
                    }
                    else
                        return false;
                } while (sendstr.Count > 0);


            }
            catch (System.Exception ex)
            {
                return false;
            }


        }




        private void Form1_Load(object sender, EventArgs e)
        {
            clients = new System.Collections.ArrayList();
            this.Left = Screen.PrimaryScreen.Bounds.Width - 350;
            this.Top = 80;

            label1.Parent = pictureBox1;
            comboBox1.SelectedIndex = 0;
            //DeviceUsing.ReleaseMutex();
            //CallCmdUsing.ReleaseMutex();

            //   Updatecon_4();

            //startls();


        }

        private void startls()
        {
            ipAddress = System.Net.IPAddress.Any;// ("192.168.104.110"); 
            listenport = 5000;

            threadListen = new System.Threading.Thread(StartListening);
            threadListen.Start();
            timer2.Enabled = true;
            label2.Text = "正在监听...";

        }



        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, WM_SYSCOMMAND, SC_MOVE + HTCAPTION, 0);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {

            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
            }
            else
            {
                //panel_con_v.Height = this.Height - 1;
                //panel_con.Height = panel_con_v.Height - 238;
            }
        }


        private void userControl_Click(object sender, EventArgs e)
        {
            CImgButton ut1;
            ut1 = (CImgButton)sender;
            CImgButton[] uta = { cImgButton4, cImgButton5, cImgButton6 };
            for (int i = 0; i < 3; i++)
            {
                if (uta[i].Name == ut1.Name)
                {
                    descx = i * -281;
                    uta[i].matrixR = 1.5F;
                    uta[i].matrixG = 1.5F;
                    uta[i].matrixB = 1.5F;
                }
                else
                {
                    uta[i].matrixR = 3.2F;
                    uta[i].matrixG = 1.4F;
                    uta[i].matrixB = 0.9F;
                }
                uta[i].Refresh();
            }

            //if (cImgButton4.Name == ut1.Name)
            //{
            //    descx = 0;
            //}
            //if (cImgButton5.Name == ut1.Name)
            //{
            //    descx = -281;
            //}
            //if (cImgButton6.Name == ut1.Name)
            //{
            //    descx = -562;
            //}



            timer1.Enabled = true;


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int itep = 50;
            if (panel_con.Left > descx - itep && panel_con.Left < descx + itep)
            {

                panel_con.Left = descx;
                timer1.Enabled = false;
            }
            else
            {
                panel_con.Left = descx > panel_con.Left ? panel_con.Left + itep : panel_con.Left - itep;
            }

            label5.Left = 45 + Convert.ToInt32(-panel_con.Left / 3.3);
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            if (clients == null)
                return;
            foreach (Client tc in clients)
            {
                if (!SendToClient(tc.Sock, "4|ok"))
                {
                    clientdel(tc);
                    break;
                }



            }


        }

        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            MouseEventArgs Mouse_e = (MouseEventArgs)e;


            //点鼠标右键,return  
            if (Mouse_e.Button == MouseButtons.Right)
            {
                this.contextMenuStrip1.Show(MousePosition.X, MousePosition.Y - contextMenuStrip1.Height);

            }
            else
            {
                this.TopMost = true;
                this.Show();
                this.TopMost = false;
            }

        }



        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.Show();
        }



        public void udsqluser()
        {
            try
            {
                sqluser.Clear();
                string query = "select staffcode,staffname,sectname,emercnt from lk_user  ";
                SqlCommand objSqlCommand = new SqlCommand(query, lfcon.con);
                SqlDataReader objSqlReader = objSqlCommand.ExecuteReader();
                while (objSqlReader.Read())
                {
                    Tsqluser tsqlu = new Tsqluser();
                    tsqlu.staffcode = objSqlReader["staffcode"].ToString();
                    tsqlu.staffname = objSqlReader["staffname"].ToString();
                    tsqlu.sectname = objSqlReader["sectname"].ToString();
                    tsqlu.emercnt = objSqlReader["emercnt"].ToString();
                    sqluser.Add(tsqlu);
                }



                objSqlReader.Close();
            }
            finally
            {

            }

        }
        public void udsqlitem()
        {
            try
            {
                sqlitem.Clear();
                string query = "select itemcode,itemname,sectname from lk_itemcfg  ";
                SqlCommand objSqlCommand = new SqlCommand(query, lfcon.con);
                SqlDataReader objSqlReader = objSqlCommand.ExecuteReader();
                while (objSqlReader.Read())
                {
                    Tsqlitem tsqli = new Tsqlitem();
                    tsqli.itemcode = objSqlReader["itemcode"].ToString();
                    tsqli.itemname = objSqlReader["itemname"].ToString();
                    tsqli.sectname = objSqlReader["sectname"].ToString();
                    sqlitem.Add(tsqli);
                }



                objSqlReader.Close();
            }
            finally
            {

            }
        }
        public void ydsqlfh(Socket client, int ni)
        {
            try
            {

                if (ni > 0)
                {
                    Random ran = new Random();


                    SqlCommand objSqlCommand;
                    SqlDataReader objSqlReader;
                    string query;
                    int patcode;
                    Boolean bl = true;
                    do
                    {

                        patcode = ran.Next(10000, 99999);
                        query = "select invioces from lk_paymentinfo where invioces = '" + patcode + "'";
                        objSqlCommand = new SqlCommand(query, lfcon.con);
                        objSqlReader = objSqlCommand.ExecuteReader();
                        bl = objSqlReader.Read();
                        objSqlReader.Close();

                    } while (bl);

                    string[] titem = fhstr[4].Split('`');
                    foreach (string stritem in titem)
                    {
                        if (stritem != "")
                        {
                            query = "INSERT INTO LK_PAYMENTINFO (PATIENTNAME,PATIENTCODE,ITEMCODE,CHKSTATE,CHKCNT,SECTTYPE,STAFF,AMOUNT,PAYDATE,EXTSERIES,CODEUSE,INVIOCES)  VALUES (@PATIENTNAME,@PATIENTCODE,@ITEMCODE,0,@CHKCNT,@SECTTYPE,@STAFF,0,getdate(),@EXTSERIES,@CODEUSE,@INVIOCES)";
                            objSqlCommand = new SqlCommand(query, lfcon.con);

                            objSqlCommand.Parameters.AddWithValue("@PATIENTNAME", fhstr[1]);
                            objSqlCommand.Parameters.AddWithValue("@PATIENTCODE", patcode);
                            objSqlCommand.Parameters.AddWithValue("@ITEMCODE", stritem);
                            objSqlCommand.Parameters.AddWithValue("@CHKCNT", fhstr[3]);


                            objSqlCommand.Parameters.AddWithValue("@STAFF", "");
                            objSqlCommand.Parameters.AddWithValue("@CODEUSE", 1);
                            objSqlCommand.Parameters.AddWithValue("@INVIOCES", patcode);


                            switch (fhstr[0])
                            {
                                case "1":
                                    objSqlCommand.Parameters.AddWithValue("@EXTSERIES", "TP" + patcode);
                                    objSqlCommand.Parameters.AddWithValue("@SECTTYPE", 4);
                                    break;
                                case "2":
                                    objSqlCommand.Parameters.AddWithValue("@EXTSERIES", "TJFHDW" + patcode);
                                    objSqlCommand.Parameters.AddWithValue("@SECTTYPE", 3);
                                    break;
                                case "3":
                                    objSqlCommand.Parameters.AddWithValue("@EXTSERIES", "TJFHGR" + patcode);
                                    objSqlCommand.Parameters.AddWithValue("@SECTTYPE", 3);
                                    break;
                            }




                            objSqlCommand.ExecuteNonQuery();
                        }

                    }

                    ni--;
                    SendToClient(client, "90|" + ni + "," + patcode);

                }

                else
                {
                    SendToClient(client, "90|0");

                }


            }

            catch (System.Exception ex)
            {

                SendToClient(client, "80|" + ex.Message);

            }

        }

        private void cImgButton1_Click(object sender, EventArgs e)
        {
            notifyIcon1.Icon = null;
            thclient = false;
            foreach (Client ct in clients)
            {

                ct.Sock.Close();
                ct.CLThread.Abort();
            }
            if (listener != null)
                listener.Stop();
            Close();
        }

        private void cImgButton2_Click(object sender, EventArgs e)
        {
            Hide();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            PostWebRequest("http://localhost:6625/glf/coneq", "okok");
            notifyIcon1.Icon = Properties.Resources.tp1;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            changeNotifyIco();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            notifyIcon1.Icon = null;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Refresh();
        }

        private string PostWebRequest(string postUrl, string paramData, int timeout = 100000)
        {
            string ret = string.Empty;
            Encoding dataEncode = Encoding.UTF8;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Timeout = timeout;
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";

                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
            }
            catch (Exception ex)
            {
                torsion.Model.GlfGloFun.Write_Err(ex.Message);
            }

            return ret;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //readEq();
            // getAllPerson();
            new Thread(getAllPerson).Start();
            // getAllPerson();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var tp = new Person()
            {
                FirstName = "gao",
                LastName = "longfei",
                tdata = new TData() { data1 = "d1", data2 = "d2" }
            };

            string json;
            json = JSONHelper.Serialize<Person>(tp);
            MessageBox.Show(json);

            Person tper = JSONHelper.Deserialize<Person>(json);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AttendanceSet.ClassesInfo ac = new AttendanceSet.ClassesInfo();
            ac.id = 0;
            ac.t_name = "test";
            ac.time_e = -1;
            ac.time_s = 1;
            ac.valid_e = 2;
            ac.valid_s = -2;
            ac.work_s = 3;
            ac.work_e = 4;
            ac.timeinfo = new AttendanceSet.TimeInfo[2];

            ac.timeinfo[0] = new AttendanceSet.TimeInfo();
            ac.timeinfo[0].cid = 0;
            ac.timeinfo[0].id = 0;
            ac.timeinfo[0].rate = 1.5f;
            ac.timeinfo[0].t_name = "t1";
            ac.timeinfo[0].t_type = 1;
            ac.timeinfo[0].work_s = 100;
            ac.timeinfo[0].work_e = -100;

            ac.timeinfo[1] = new AttendanceSet.TimeInfo();
            ac.timeinfo[1].cid = 0;
            ac.timeinfo[1].id = 1;
            ac.timeinfo[1].rate = 2.5f;
            ac.timeinfo[1].t_name = "t2";
            ac.timeinfo[1].t_type = 2;
            ac.timeinfo[1].work_s = 200;
            ac.timeinfo[1].work_e = -200;


            reJSON json = JSONHelper.Deserialize<reJSON>(PostWebRequest(post_url, JSONHelper.Serialize<AttendanceSet.ClassesInfo>(ac)));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            post_url = comboBox1.Text;
            if (ConDevice() == torsion.Model.GlfGloVar.RE_SUCCESS)
            {
                gl_si.cmd = torsion.Model.GlfGloVar.CMD_HEARTBEAT;
                new System.Threading.Thread(ComDevice).Start();
                //new Thread(EQcmd).Start();
                this.BeginInvoke(new Update2(Updatecon_2), new object[] { "Connected " + post_url });
            }

        }

        public void addDevice()
        {
            panel_con1.Controls.Clear();

            foreach (torsion.Model.DeviceInfo di in gl_si.di)
            {
                SoftModel.TDeviceList smtdl = new SoftModel.TDeviceList();
                smtdl.di = di;
                tctbtn ttct = new tctbtn();
                ttct.Name = "tct_" + di.deviceSet.DeviceId;
                ttct.Dock = DockStyle.Top;
                ttct.UserControlBtnDoubleClicked += new tctbtn.BtnDoubleClickHandle(tctbtn_UserControlBtnDoubleClicked);
                ttct.eqname = di.deviceSet.DeviceName;
                ttct.eqinfo = di.deviceSet.IP;
                ttct.eqstate = 0;
                ttct.Cursor = Cursors.Hand;
                panel_con1.Controls.Add(ttct);

                smtdl.tb = ttct;
                gl_lst.Add(smtdl);

            }

        }
        public void clearDevice()
        {
            while (gl_lst.Count != 0)
            {
                panel_con1.Controls.Remove(gl_lst[0].tb);
                gl_lst.Remove(gl_lst[0]);
            }

        }

        public torsion.Model.JsonModel.RecData SendData(torsion.Model.JsonModel.RecData sjmrd)
        {
            torsion.Model.JsonModel.RecData rjmrd = new torsion.Model.JsonModel.RecData();
            try
            {
                string ComDevice_url = post_url + "/SoftInfo/RecSoft" + "?access_token=" + gl_si.assess_token;
                string recstr = PostWebRequest(ComDevice_url, JsonConvert.SerializeObject(sjmrd), torsion.Model.GlfGloVar.SENDTIMEOUT);
                rjmrd = JsonConvert.DeserializeObject<torsion.Model.JsonModel.RecData>(recstr);
            }
            catch (Exception e)
            {
                torsion.Model.GlfGloFun.Write_Err(e.Message);
            }

            return rjmrd;

        }

        public void upAllPerson()
        {
            //string dir = System.Environment.CurrentDirectory + "\\tmp\\0\\";
            //Directory.CreateDirectory(dir);
            //using (StreamWriter sw = new StreamWriter(dir + fileint.ToString() + ".tmp"))
            //{
            //    sw.Write(jmrd.cdata);
            //}
            //fileint++;
        }

        public void getAllPerson()
        {

            torsion.Model.JsonModel.RecData jmrd = new torsion.Model.JsonModel.RecData();
            torsion.Model.JsonModel.RecData rjmrd = new torsion.Model.JsonModel.RecData();
            jmrd.cmd = torsion.Model.GlfGloVar.CMD_S_SETALLPERSON;
            this.BeginInvoke(new Update2(Updatecon_2), new object[] { "Start getAllPerson" });
            int RecordCount = new int();
            int RetCount = new int();
            int pPersons = new int();
            int pLongRun = new int();
            AnvizNew.CKT_SetNetTimeouts(20000);
            int ret = AnvizNew.CKT_RegisterNetWithPort(0, 6011, "192.168.19.213");
            // AnvizNew.CKT_RegisterUSB(0, 0);
            AnvizNew.PERSONINFO person = new AnvizNew.PERSONINFO();
            int ptemp;
            int tempptr = 0;
            if (AnvizNew.CKT_ListPersonInfoEx(0, ref pLongRun) == 1)
            {
                torsion.Model.JsonModel.MulJson jmmj = new torsion.Model.JsonModel.MulJson();
                List<torsion.Model.UserInfo.BaseInfo> luibi = new List<torsion.Model.UserInfo.BaseInfo>();
                while (true)
                {

                    if (jmmj.jstat == 2) break;
                    ret = AnvizNew.CKT_ListPersonProgress(pLongRun, ref RecordCount, ref RetCount, ref pPersons);
                    if (ret != 0 && RetCount > 0)
                    {
                        jmmj.jall = RecordCount;
                        ptemp = Marshal.SizeOf(person);
                        tempptr = pPersons;

                        for (int i = 0; i < RetCount; i++)
                        {
                            RtlMoveMemory(ref person, pPersons, ptemp);
                            pPersons = pPersons + ptemp;
                            torsion.Model.UserInfo.BaseInfo uibi = new torsion.Model.UserInfo.BaseInfo();
                            uibi.UserCode = person.PersonID;
                            uibi.Name = Encoding.Default.GetString(person.Name);
                            if(uibi.Name.IndexOf('\0') >= 0)
                            uibi.Name = uibi.Name.Substring(0,uibi.Name.IndexOf('\0'));
                            uibi.Pwd = Encoding.Default.GetString(person.Password);
                            if (uibi.Pwd.IndexOf('\0') >= 0)
                                uibi.Pwd = uibi.Pwd.Substring(0, uibi.Pwd.IndexOf('\0'));
                            uibi.CardNo = person.CardNo.ToString();
                            uibi.Deptid = person.Dept;
                            uibi.Groupid = person.Group;
                            uibi.UserType = person.Other;
                            uibi.CompareType = person.KQOption;
                            luibi.Add(uibi);
                            jmmj.jalr += 1;
                        }
                        //modified by wanhaiping
                        if (tempptr != 0) AnvizNew.CKT_FreeMemory(tempptr);
                        jmmj.jstat = 1;
                        jmmj.jdata = Newtonsoft.Json.JsonConvert.SerializeObject(luibi);
                        luibi.Clear();

                        jmrd.cdata = JsonConvert.SerializeObject(jmmj);

                        //string dir = System.Environment.CurrentDirectory + "\\tmp\\0\\";
                        //Directory.CreateDirectory(dir);
                        //using (StreamWriter sw = new StreamWriter(dir + fileint.ToString()+".tmp"))
                        //{
                        //    sw.Write(jmrd.cdata);
                        //}
                        //fileint++;
                        rjmrd = SendData(jmrd);

                        this.BeginInvoke(new Update2(Updatecon_2), new object[] { ((jmmj.jalr * 100) / jmmj.jall).ToString() + "%" });
                        if (ret == 1) break;
                        //AnvizNew.CKT_FreeMemory(ref ptemp);
                        //if (tempptr != 0) AnvizNew.CKT_FreeMemory(tempptr);
                    };
                    if (ret != 2)
                    {
                        break;
                    }

                }
                this.BeginInvoke(new Update2(Updatecon_2), new object[] { "Stop getAllPerson:" + jmmj.jalr.ToString() });
            }

        }

        public void getAllRecord()
        {
            torsion.Model.JsonModel.RecData jmrd = new torsion.Model.JsonModel.RecData();
            torsion.Model.JsonModel.RecData rjmrd = new torsion.Model.JsonModel.RecData();
            jmrd.cmd = torsion.Model.GlfGloVar.CMD_S_SETALLRECORD;

            int i;
            int RecordCount = new int();
            int RetCount = new int();
            int pClockings = new int();
            int pLongRun = new int();
            int pTemprun = new int();
            int ptemp;
            int tempptr;
            int ret;
            AnvizNew.CLOCKINGRECORD clocking = new AnvizNew.CLOCKINGRECORD();
            torsion.Model.Attendance.JsonAttendance aja = new torsion.Model.Attendance.JsonAttendance();
            aja.sendnum = 0;


            DeviceUsing.WaitOne();
            AnvizNew.CKT_RegisterUSB(0, 0);
            if (AnvizNew.CKT_GetClockingRecordEx(0, ref pLongRun) == 1)
            {

                pTemprun = pLongRun;
                while (true)
                {

                    aja.aai.Clear();
                    //ret == 2??? wanhaiping
                    // ret = AnvizNew.CKT_GetClockingRecordProgress(pLongRun, ref RecordCount, ref RetCount, ref pClockings);
                    ret = AnvizNew.CKT_GetClockingRecordProgress(pTemprun, ref RecordCount, ref RetCount, ref pClockings);
                    aja.allnum = RecordCount;

                    if (ret != 0)
                    {

                        ptemp = Marshal.SizeOf(clocking);
                        tempptr = pClockings;
                        for (i = 0; i < RetCount; i++)
                        {
                            RtlMoveMemory(ref clocking, pClockings, ptemp);
                            pClockings = pClockings + ptemp;

                            torsion.Model.Attendance.AttendanceInfo taai = new torsion.Model.Attendance.AttendanceInfo();
                            taai.Userid = clocking.PersonID;
                            string stime = Encoding.Default.GetString(clocking.Time).TrimEnd('\0');
                            try
                            {
                                taai.CheckTime = Convert.ToDateTime(stime);
                            }
                            catch
                            {
                                taai.CheckTime = Convert.ToDateTime("2010-01-01 00:00:01");
                            }

                            taai.CheckType = clocking.Stat;
                            taai.Sensorid = clocking.ID;
                            taai.WorkType = clocking.WorkType;
                            taai.AttFlag = clocking.BackupCode;
                            taai.OpenDoorFlag = 0;
                            aja.aai.Add(taai);
                            //wanhaiping33
                            aja.sendnum++;

                        }
                        //modified by wanhaiping
                        //if (ptemp != 0) AnvizNew.CKT_FreeMemory(ref ptemp);
                        if (tempptr != 0) AnvizNew.CKT_FreeMemory(tempptr);

                        jmrd.cdata = JsonConvert.SerializeObject(aja);
                        rjmrd = SendData(jmrd);
                        if (ret == 1) break;
                    }

                }


            }
            AnvizNew.CKT_Disconnect();
            DeviceUsing.ReleaseMutex();


        }
        public void ComDevice()
        {

            torsion.Model.JsonModel.RecData rjmrd = new torsion.Model.JsonModel.RecData();
            torsion.Model.JsonModel.RecData jmrd = new torsion.Model.JsonModel.RecData();
            string ComDevice_url = post_url + "/SoftInfo/ComSoft" + "?access_token=";
            int errcnt = 0;
            while (true)
            {
                try
                {
                    jmrd.cmd = gl_si.cmd;
                    if (gl_si.cmd == torsion.Model.GlfGloVar.CMD_HEARTBEAT)
                    {
                        reDeviceStat();
                    }

                    jmrd.cdata = gl_si.sendStr;

                    gl_si.cmd = torsion.Model.GlfGloVar.CMD_HEARTBEAT;
                    rjmrd = JsonConvert.DeserializeObject<torsion.Model.JsonModel.RecData>(PostWebRequest(ComDevice_url + gl_si.assess_token, JsonConvert.SerializeObject(jmrd), torsion.Model.GlfGloVar.CLIENT_POST_TIMEOUT));
                    if (rjmrd == null)
                    {
                        errcnt++;
                        continue;
                    }

                    gl_si.cmd = rjmrd.cmd;
                    switch (rjmrd.cmd)
                    {
                        case torsion.Model.GlfGloVar.CMD_HEARTBEAT:
                            gl_si.cmd = torsion.Model.GlfGloVar.CMD_HEARTBEAT;
                            break;
                        case torsion.Model.GlfGloVar.CMD_NEEDCONNECT:
                            ConDevice();
                            break;
                        case torsion.Model.GlfGloVar.CMD_C_OPENLOCK:
                            forceOpenLock(rjmrd);
                            continue;
                        // break;
                        case torsion.Model.GlfGloVar.CMD_C_REDEVICESTAT:
                            reDeviceStat();
                            continue;
                        case torsion.Model.GlfGloVar.CMD_C_GETALLRECORD:
                            gl_si.sendStr = "ok";
                            new Thread(getAllRecord).Start();
                            continue;
                        case torsion.Model.GlfGloVar.CMD_C_GETALLPERSON:
                            gl_si.sendStr = "start";
                            gl_si.jmmj = JsonConvert.DeserializeObject<torsion.Model.JsonModel.MulJson>(jmrd.cdata);
                            new Thread(getAllPerson).Start();
                            continue;
                        case torsion.Model.GlfGloVar.CMD_C_SEARCHDEVICE:
                            SearchDevice();
                            gl_si.sendStr = Newtonsoft.Json.JsonConvert.SerializeObject(eqsearch);
                            continue;

                        default:
                            //gl_si.recStr = rjmrd.cdata;
                            //gl_si.cmd = rjmrd.cmd;
                            break;
                    }
                    gl_si.cmd = torsion.Model.GlfGloVar.CMD_HEARTBEAT;
                    this.BeginInvoke(new _SafeCall(UpLabel2), new object[] { });

                }
                catch (Exception e)
                {
                    errcnt++;
                    this.BeginInvoke(new Update2(Updatecon_2), new object[] { e.Message });
                    torsion.Model.GlfGloFun.Write_Err(e.Message);
                }
                System.Threading.Thread.Sleep(torsion.Model.GlfGloVar.CLIENT_SLEEP_TIME);
            }

        }

        public int ConDevice()
        {

            string ConDevice_url = post_url + "/SoftInfo/ConSoft";
            gl_si.assess_token = PostWebRequest(ConDevice_url, "secretKey");
            if (torsion.Model.GlfGloVar.ERRSTR_UNREGISTERED == gl_si.assess_token)
            {
                return 2;
            }

            if (gl_si.assess_token.Length != torsion.Model.GlfGloVar.ACCESS_TOKEN_LEN)
            {
                return 3;
            }

            getDeviceList();
            return torsion.Model.GlfGloVar.RE_SUCCESS;
        }

        private void button8_Click(object sender, EventArgs e)
        {

            new Thread(readNewRecord).Start();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
        public void sendDataOver(int stat)
        {
            gl_si.conStat = stat;
            for (int i = 0; i < torsion.Model.GlfGloVar.CLIENT_POST_TIMEOUT / torsion.Model.GlfGloVar.CLIENT_SLEEP_TIME; i++)
            {
                if (gl_si.conStat >= 4)
                    return;
                System.Threading.Thread.Sleep(torsion.Model.GlfGloVar.CLIENT_SLEEP_TIME);
            }
        }
        // CKT_ForceOpenLock

        public void getDeviceType(torsion.Model.JsonModel.RecData rjmrd)
        {
            try
            {

                DeviceUsing.WaitOne();
                AnvizNew.CKT_RegisterUSB(0, 0);
                AnvizNew.CKT_ForceOpenLock(0);
                AnvizNew.CKT_Disconnect();
                gl_si.sendStr = "Success";
                DeviceUsing.ReleaseMutex();
                this.BeginInvoke(new Update2(Updatecon_2), new object[] { "forceOpenLock" });
            }
            catch (Exception e)
            {
                gl_si.sendStr = e.Message;
                torsion.Model.GlfGloFun.Write_Err(e.Message);
            }
        }
        public void forceOpenLock(torsion.Model.JsonModel.RecData rjmrd)
        {
            try
            {
                torsion.Model.JsonModel.MulJson jmmj = Newtonsoft.Json.JsonConvert.DeserializeObject<torsion.Model.JsonModel.MulJson>(rjmrd.cdata);
                DeviceUsing.WaitOne();
                AnvizNew.CKT_RegisterUSB(0, 0);
                AnvizNew.CKT_ForceOpenLock(0);
                AnvizNew.CKT_Disconnect();
                gl_si.sendStr = "Success";
                DeviceUsing.ReleaseMutex();
                this.BeginInvoke(new Update2(Updatecon_2), new object[] { "forceOpenLock" });
            }
            catch (Exception e)
            {
                gl_si.sendStr = e.Message;
                torsion.Model.GlfGloFun.Write_Err(e.Message);
            }

        }
        public void reDeviceStat()
        {
            try
            {
                List<torsion.Model.DeviceInfo> cs = new List<torsion.Model.DeviceInfo>();

                foreach (SoftModel.TDeviceList smtdl in gl_lst)
                {
                    torsion.Model.DeviceInfo tdi = new torsion.Model.DeviceInfo();
                    tdi.deviceSet = smtdl.di.deviceSet;
                    tdi.conStat = smtdl.di.conStat;
                    cs.Add(tdi);
                }
                gl_si.sendStr = JsonConvert.SerializeObject(cs);
            }
            catch (Exception e)
            {
                torsion.Model.GlfGloFun.Write_Err(e.Message);
            }

        }
        public void getDeviceList()
        {
            torsion.Model.JsonModel.RecData jmrd = new torsion.Model.JsonModel.RecData();
            torsion.Model.JsonModel.RecData rjmrd = new torsion.Model.JsonModel.RecData();
            jmrd.cmd = torsion.Model.GlfGloVar.CMD_S_DEVICELIST;
            try
            {
                rjmrd = SendData(jmrd);
                if (gl_si.di == null) gl_si.di = new List<torsion.Model.DeviceInfo>();
                gl_si.di = JsonConvert.DeserializeObject<List<torsion.Model.DeviceInfo>>(rjmrd.cdata);
                this.BeginInvoke(new DEnopar(clearDevice));
                this.BeginInvoke(new DEnopar(addDevice));
            }
            catch (Exception e)
            {
                torsion.Model.GlfGloFun.Write_Err(e.Message);
            }

        }

        public void ChangeConStat(SoftModel.TDeviceList tdl, int stat = 1)
        {
            tdl.tb.eqstate = stat;
            if (tdl.di.conStat == null)
                tdl.di.conStat = new torsion.Model.DeviceInfo.ConStat();
            tdl.di.conStat.Stat = stat;
            tdl.di.conStat.LstTime = DateTime.Now.ToString(torsion.Model.GlfGloVar.DATATIME_FORMAT);
        }
        public void EQcmd()
        {
            int ret = 0;

            while (true)
            {
                try
                {
                    foreach (SoftModel.TDeviceList smtdl in gl_lst)
                    {

                        DeviceUsing.WaitOne();
                        ret = 0;
                        switch (smtdl.di.deviceSet.LinkMode)
                        {
                            case 2:
                                break;
                            case 3:
                                ret = AnvizNew.CKT_RegisterUSB(smtdl.di.deviceSet.DeviceId, 0);

                                break;
                            case 4:
                                break;
                            default:
                                ret = AnvizNew.CKT_RegisterNet(smtdl.di.deviceSet.DeviceId, smtdl.di.deviceSet.IP);
                                break;
                        }
                        ChangeConStat(smtdl, ret);
                        if (ret == 0)
                        {
                            DeviceUsing.ReleaseMutex();
                            continue;
                        };
                    }
                }
                catch
                {
                }
                Thread.Sleep(10000);
            }
        }
        public void readNewRecord()
        {
            AnvizNew.CLOCKINGRECORD ta = new AnvizNew.CLOCKINGRECORD();
            int RecordCount = new int();
            int pLongRun = new int();
            int RetCount = new int();
            int pClockings = new int();
            int pTemprun = new int();
            int ret = 0;

            while (true)
            {
                try
                {
                    foreach (SoftModel.TDeviceList smtdl in gl_lst)
                    {

                        DeviceUsing.WaitOne();
                        ret = 0;
                        switch (smtdl.di.deviceSet.LinkMode)
                        {
                            case 2:
                                break;
                            case 3:
                                ret = AnvizNew.CKT_RegisterUSB(smtdl.di.deviceSet.DeviceId, 0);

                                break;
                            case 4:
                                break;
                            default:
                                ret = AnvizNew.CKT_RegisterNet(smtdl.di.deviceSet.DeviceId, smtdl.di.deviceSet.IP);
                                break;
                        }
                        ChangeConStat(smtdl, ret);
                        if (ret == 0)
                        {
                            DeviceUsing.ReleaseMutex();
                            continue;
                        };
                        try
                        {
                            if (AnvizNew.CKT_GetClockingNewRecordEx(smtdl.di.deviceSet.DeviceId, ref pLongRun) == 1)
                            {
                                int recnum = 0;
                                pTemprun = pLongRun;
                                while (true)
                                {
                                    //ret = AnvizNew.CKT_GetClockingRecordProgress(pLongRun, ref RecordCount, ref RetCount, ref pClockings);
                                    ret = AnvizNew.CKT_GetClockingRecordProgress(pTemprun, ref RecordCount, ref RetCount, ref pClockings);// pClockings);
                                    if (ret != 0)
                                    {
                                        List<torsion.Model.Attendance.AttendanceInfo> aai = new List<torsion.Model.Attendance.AttendanceInfo>();
                                        if (RetCount > 0)
                                        {
                                            for (int i = 0; i < RetCount; i++)
                                            {

                                                recnum++;
                                                RtlMoveMemory(ref ta, pClockings, Marshal.SizeOf(ta));
                                                pClockings = pClockings + Marshal.SizeOf(ta);

                                                torsion.Model.Attendance.AttendanceInfo taai = new torsion.Model.Attendance.AttendanceInfo();
                                                taai.Userid = ta.PersonID;
                                                taai.CheckTime = Convert.ToDateTime(Encoding.Default.GetString(ta.Time).TrimEnd('\0'));
                                                taai.CheckType = ta.Stat;
                                                taai.Sensorid = ta.ID;
                                                taai.WorkType = ta.WorkType;
                                                taai.AttFlag = ta.BackupCode;
                                                taai.OpenDoorFlag = 0;
                                                aai.Add(taai);

                                            }
                                            AnvizNew.CKT_FreeMemory(pClockings);
                                            torsion.Model.JsonModel.RecData rjmrd = new torsion.Model.JsonModel.RecData();
                                            torsion.Model.JsonModel.RecData sjmrd = new torsion.Model.JsonModel.RecData();
                                            sjmrd.cmd = torsion.Model.GlfGloVar.CMD_S_NEWRECORD;
                                            torsion.Model.Attendance.JsonAttendance aja = new torsion.Model.Attendance.JsonAttendance();
                                            aja.allnum = RetCount;
                                            aja.sendnum = RetCount;
                                            aja.aai = aai;
                                            sjmrd.cdata = JsonConvert.SerializeObject(aja);
                                            rjmrd = SendData(sjmrd);
                                            if (rjmrd.stat == 4)
                                            {

                                                if (AnvizNew.CKT_ClearClockingRecord(smtdl.di.deviceSet.DeviceId, 1, 0) == 0)
                                                    throw new Exception("Clear Error");
                                                this.BeginInvoke(new Update2(Updatecon_2), new object[] { smtdl.di.deviceSet.DeviceName + ":" + recnum + " new record" + Environment.NewLine + rjmrd.cdata });
                                            }


                                        }

                                    }
                                    if (ret == 1) break;
                                }

                            }
                        }
                        catch (Exception e)
                        {

                            torsion.Model.GlfGloFun.Write_Err(e.Message, 2);
                        }
                        AnvizNew.CKT_Disconnect();
                        DeviceUsing.ReleaseMutex();



                    }

                }
                catch (Exception ex)
                {
                    torsion.Model.GlfGloFun.Write_Err(ex.Message, 1);

                }
                Thread.Sleep(10000);
            }

        }


        public int sendEQdata(JSEQdata jsq)
        {
            if (false)
            {
                using (SqlConnection sqlcon = new SqlConnection(lfcon._constring))
                {

                    SqlCommand command = new SqlCommand("Insert into Checkinout(Userid,Checktime,Checktype,Sensorid,WorkType,AttFlag,OpenDoorFlag) values(@Userid,@Checktime,@Checktype,@Sensorid,@WorkType,@AttFlag,@OpenDoorFlag);", sqlcon);
                    command.Connection.Open();
                    command.Parameters.Add("@Userid", SqlDbType.Int).Value = jsq.Userid;
                    command.Parameters.Add("@Checktime", SqlDbType.DateTime).Value = jsq.Checktime;
                    command.Parameters.Add("@Checktype", SqlDbType.Int).Value = jsq.Checktype;
                    command.Parameters.Add("@Sensorid", SqlDbType.Int).Value = jsq.Sensorid;
                    command.Parameters.Add("@WorkType", SqlDbType.Int).Value = jsq.WorkType;
                    command.Parameters.Add("@AttFlag", SqlDbType.Int).Value = jsq.AttFlag;
                    command.Parameters.Add("@OpenDoorFlag", SqlDbType.Int).Value = jsq.OpenDoorFlag;
                    command.ExecuteNonQuery();

                    sqlcon.Close();

                }
            }
            if (true)
            {
                reJSON json = JSONHelper.Deserialize<reJSON>(PostWebRequest(post_url, JSONHelper.Serialize<JSEQdata>(jsq)));
            }
            return 0;
        }
        public void readEq()
        {
            AnvizNew.CLOCKINGRECORD ta = new AnvizNew.CLOCKINGRECORD();
            int RecordCount = new int();
            int pLongRun = new int();
            int RetCount = new int();
            int pClockings = new int();
            int pTemprun = new int();
            while (true)
            {
                foreach (TEQinfo teq in eqinfos)
                {
                    int ret = 0;
                    try
                    {
                        switch (teq.linkMode)
                        {
                            case 3:
                                ret = AnvizNew.CKT_RegisterUSB(teq.clientid, 0);

                                break;
                            case 4:
                                break;
                            default:
                                break;
                        }
                        if (ret == 0)
                        {
                            teq.ctbtn.eqstate = 0;
                            continue;
                        }
                        else
                        {
                            teq.ctbtn.eqstate = 1;
                        }

                        if (AnvizNew.CKT_GetClockingNewRecordEx(teq.clientid, ref pLongRun) == 1)
                        {
                            int recnum = 0;
                            pTemprun = pLongRun;
                            while (true)
                            {
                                //ret = AnvizNew.CKT_GetClockingRecordProgress(pLongRun, ref RecordCount, ref RetCount, ref pClockings);
                                ret = AnvizNew.CKT_GetClockingRecordProgress(pTemprun, ref RecordCount, ref RetCount, ref pClockings);// pClockings);
                                if (ret != 0)
                                {
                                    for (int i = 0; i < RetCount; i++)
                                    {
                                        recnum++;
                                        RtlMoveMemory(ref ta, pClockings, Marshal.SizeOf(ta));
                                        pClockings = pClockings + Marshal.SizeOf(ta);
                                        if (ta.PersonID < 0)
                                        {
                                            continue;
                                        }
                                        JSEQdata tjsq = new JSEQdata();
                                        tjsq.Userid = ta.PersonID;
                                        tjsq.Checktime = Encoding.Default.GetString(ta.Time).TrimEnd('\0');
                                        tjsq.Checktype = ta.Stat;
                                        tjsq.Sensorid = ta.ID;
                                        tjsq.WorkType = ta.WorkType;
                                        tjsq.AttFlag = ta.BackupCode;
                                        tjsq.OpenDoorFlag = 0;
                                        sendEQdata(tjsq);

                                    }

                                    AnvizNew.CKT_FreeMemory(pClockings);
                                    if (recnum > 0)
                                        if (AnvizNew.CKT_ClearClockingRecord(teq.clientid, 1, 0) == 0)
                                            throw new Exception("Clear Error");
                                }
                                if (ret == 1) break;
                            }
                            if (recnum > 0)
                                this.BeginInvoke(new Update2(Updatecon_2), new object[] { teq.clientName + ":" + recnum.ToString() });
                        }

                    }
                    catch (Exception ex)
                    {
                        this.BeginInvoke(new Update2(Updatecon_2), new object[] { teq.clientName + ":" + ex.Message });
                    }
                }
                Thread.Sleep(10000);
            }

        }

        private void button9_Click(object sender, EventArgs e)
        {
            getDeviceList();
            //torsion.Model.JsonModel.RecData jmrd = new torsion.Model.JsonModel.RecData();
            //jmrd.cmd = torsion.Model.GlfGloVar.CMD_DEVICELIST;
            //SendData(jmrd);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //torsion.Model.JsonModel.RecData jmrd = new torsion.Model.JsonModel.RecData();
            //getAllRecord(jmrd);
            getAllRecord();

            //forceOpenLock();
        }


        private void SearchDevice()
        {

            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //255.255.255.255 
            IPEndPoint iep1 = new IPEndPoint(IPAddress.Broadcast, 5050);
            string hostname = Dns.GetHostName();
            byte[] data = new byte[10];
            data[0] = 0xA5;
            data[1] = 0x00;
            data[2] = 0x00;
            data[3] = 0x00;
            data[4] = 0x00;
            data[5] = 0x02;
            data[6] = 0x00;
            data[7] = 0x00;
            data[8] = 0x47;
            data[9] = 0x23;
            EndPoint remoteIP = new IPEndPoint(IPAddress.Any, 5060);
            sock.Bind(remoteIP);
            sock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);
            sock.SendTo(data, iep1);
            // sock.SendTo(data, iep2);
            byte[] bdata = new byte[1024];
            EndPoint bremoteIP = new IPEndPoint(IPAddress.Any, 0);

            eqsearch.Clear();
            while (true)
            {
                int recv = 0;
                string stringData = "";
                sock.ReceiveTimeout = 1000;
                try
                {
                    recv = sock.ReceiveFrom(bdata, ref bremoteIP);
                }
                catch
                {
                    break;
                }

                for (int i = 0; i < recv; i++)
                {
                    stringData += Convert.ToString(bdata[i], 16) + " ";
                }
                if (recv < 11) continue;
                if (bdata[5] != 0x82) continue;
                torsion.Model.DeviceInfo.DeviceSearch dd = new torsion.Model.DeviceInfo.DeviceSearch();
                dd.DeviceNum = hextoint(bdata, 1, 4);
                dd.DeviceType = hextostr(bdata, 9, 10);
                dd.DeviceSerial = hextostr(bdata, 19, 16);
                dd.IP = hextostr(bdata, 35, 4, 1);
                dd.SubMask = hextostr(bdata, 39, 4, 1);
                dd.Geteway = hextostr(bdata, 43, 4, 1);
                dd.MAC = hextostr(bdata, 47, 6, 2);
                dd.ServerIP = hextostr(bdata, 53, 4, 1);
                dd.Port = hextoint(bdata, 57, 2);
                dd.WorkMode = bdata[59];
                dd.SysVersion = hextostr(bdata, 60, 8);
                dd.Keep = hextoint(bdata, 68, 2);
                eqsearch.Add(dd);
                this.BeginInvoke(new Update2(Updatecon_2), new object[] { stringData + Environment.NewLine });
            }

            sock.Close();
        }

        public string hextostr(byte[] da, int start, int len, int btype = 0)
        {
            string restr = "";
            switch (btype)
            {
                case 1:
                    for (int i = 0; i < len; i++)
                    {
                        if (restr != "") restr += ".";
                        restr += da[i + start].ToString();

                    }
                    break;
                case 2:
                    for (int i = 0; i < len; i++)
                    {
                        if (restr != "") restr += "-";

                        restr += String.Format("{0:X2}", da[i + start]);
                    }
                    break;
                default:
                    restr = System.Text.Encoding.Default.GetString(da, start, len);
                    break;
            }
            return restr.TrimEnd('\0');
        }

        public int hextoint(byte[] da, int start, int len)
        {
            int ri = 0;

            for (int i = 0; i < len; i++)
            {
                ri += da[i + start] << ((len - 1 - i) * 8);
            }
            return ri;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            SearchDevice();
            //  new Thread(StartListener).Start();
        }



    }
}
