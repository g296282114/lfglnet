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


namespace lfglnet
{


    public partial class Form1 : Form
    {
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

        private delegate void Updatelogoeq(tctbtn tb, Boolean bl);
        private delegate void Update2(String str);

        public Form1()
        {
            InitializeComponent();
            sendstr.Clear();

        }

        private void tctbtn_UserControlBtnDoubleClicked(object sender, EventArgs e)
        {
            tctbtn tb = (tctbtn)sender;
            Form3 frm3 = new Form3(tb.Name.Substring(tb.Name.IndexOf('_') + 1, tb.Name.Length - tb.Name.IndexOf('_') - 1));
            frm3.Show();
            
        }

        private delegate void _SafeCall(bool bl);
        private delegate void _SafereCall();

        private void Updatecon_1(tctbtn tb, Boolean bl)
        {
            if (bl)
                panel_con1.Controls.Add(tb);
            else
                panel_con1.Controls.Remove(tb);
        }

        public void Updatecon_2(String str)
        {
            listBox1.Items.Add(DateTime.Now.ToString() + " " + str);


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
                reJSON json = JSONHelper.Deserialize<reJSON>(PostWebRequest("http://localhost:6625/glf/getpost", JSONHelper.Serialize<JSEQdata>(jsq)));
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
            while(true)
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
                                    if(recnum > 0)                                   
                                        if (AnvizNew.CKT_ClearClockingRecord(teq.clientid, 1, 0) == 0)
                                            throw new Exception("Clear Error");
                                }
                                if (ret == 1) break;
                            }
                            if(recnum > 0)
                            this.BeginInvoke(new Update2(Updatecon_2), new object[] { teq.clientName + ":" + recnum.ToString() });
                        }

                    }
                    catch (Exception ex)
                    {
                        this.BeginInvoke(new Update2(Updatecon_2), new object[] { teq.clientName + ":"+ex.Message });
                    }
                }
                Thread.Sleep(10000);
            }
 
        }

        

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



            Updatecon_4();

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
                panel_con.Left = descx > panel_con.Left ? panel_con.Left + itep : panel_con.Left - itep;
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
                if (this.Visible)
                {
                    this.Hide();
                }
                else
                {
                    this.Show();
                }


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
            PostWebRequest("http://torsion.apphb.com/wechat/", "okok");
            notifyIcon1.Icon = Properties.Resources.tp1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            notifyIcon1.Icon = Properties.Resources.tp2;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            notifyIcon1.Icon = null;
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            Refresh();
        }

        private string PostWebRequest(string postUrl, string paramData)
        {
            string ret = string.Empty;
            Encoding dataEncode = Encoding.Default;
            try
            {
                byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
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
                MessageBox.Show(ex.Message);
            }
            return ret;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //readEq();
            new System.Threading.Thread(readEq).Start();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var tp = new Person()
            {
                FirstName = "gao",
                LastName = "longfei",
                tdata = new TData() {data1 = "d1",data2 = "d2"}
            };

            string json;
            json = JSONHelper.Serialize<Person>(tp);
            MessageBox.Show(json);

             Person tper =  JSONHelper.Deserialize<Person>(json);
        }



    }
}
