using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using ctbtn;

namespace lfglnet
{
   
    
    public struct Tsqluser
    {
        public string staffcode;
        public string staffname;
        public string sectname;
        public string emercnt;
    }
    public struct Tsqlitem
    {
        public string itemcode;
        public string itemname;
        public string sectname;
    }
    class TEQinfo
    {
        public int clientid;
        public string clientName;
        public int linkMode;
        public string ipAddress;
        public int clientNumber;
        public tctbtn ctbtn;
    }
    class lfcon
    {
        private static string _stastr = "";
        private static Form1 _frm1 = new Form1();
        private static SqlConnection conn;
        public static String _constring;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);


        static lfcon()
        {
            _constring = "";
            try
            {

                try
                {
                    StringBuilder temp = new StringBuilder(1024);
                    string strFilePath = Application.StartupPath + "\\Config.ini";
                    GetPrivateProfileString("database", "server", "", temp, 1024, strFilePath);
                    _constring += "server=" + temp.ToString();

                    GetPrivateProfileString("database", "uid", "", temp, 1024, strFilePath);
                    _constring += ";uid=" + temp.ToString();

                    GetPrivateProfileString("database", "pwd", "", temp, 1024, strFilePath);
                    _constring += ";pwd=" + temp.ToString();

                    GetPrivateProfileString("database", "database", "", temp, 1024, strFilePath);
                    _constring += ";database=" + temp.ToString();


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());

                }



            }
            catch (System.Exception ex)
            {
                MessageBox.Show("读取配置失败：" + ex.Message);
                Environment.Exit(0);
            }

            //try
            //{
            //    conn = new SqlConnection();
            //    conn.ConnectionString = _constring;// "server=192.168.70.101;uid=sa;pwd=sa;database=lfgl";
            //    conn.Open();

            //}
            //catch (System.Exception ex)
            //{
            //    conn.Close();
            //    MessageBox.Show("数据库连接失败" + ex.Message);
            //    Environment.Exit(0);
            //}

        }


        public static SqlConnection con
        {
            get
            {
                return conn;
            }
            set
            {
                conn = value;
            }
        }
        public static SqlDataReader sqlselect(string sqlstr)
        {
            using (conn = new SqlConnection(_constring))
            {
                try
                {
                    SqlCommand command = new SqlCommand(sqlstr, conn);
                    command.Connection.Open();
                    return command.ExecuteReader();

                }
                catch (SqlException ex)
                {
                    return null;
                    // throw ex;
                }
            }
        }

        public static Form1 frm1
        {
            get
            {
                return _frm1;
            }

        }
        public static string stastr
        {
            get
            {
                return _stastr;
            }
            set
            {
                _stastr = value;
            }
        }


    }

    class Cliinf
    {
        public bool exist = false;
        public bool used = false;
        public string name = "";
        public string eqname = "";
        public string eqid = "";
        public string lstip = "";
        public string createdate = "";
        public string logodate = "";
        public string beizhu = "";
        public Cliinf()
        {


        }

        public void addxml(string streqid)
        {
            XmlDocument dom = new XmlDocument();
            dom.Load(System.AppDomain.CurrentDomain.BaseDirectory + "client.xml");
            XmlNode root = dom.SelectSingleNode("client");//查找<bookstore>
            if (root.SelectSingleNode(streqid) != null)
                root.RemoveChild(root.SelectSingleNode(streqid));
            XmlElement xeqid = dom.CreateElement(streqid);//添加一个名字为title的子节点

            XmlElement xname = dom.CreateElement("name");
            xname.InnerText = name;
            xeqid.AppendChild(xname);

            XmlElement xeqname = dom.CreateElement("eqname");
            xeqname.InnerText = eqname;
            xeqid.AppendChild(xeqname);

            XmlElement xlstip = dom.CreateElement("lstip");
            xlstip.InnerText = lstip;
            xeqid.AppendChild(xlstip);

            XmlElement xcreatedate = dom.CreateElement("createdate");
            xcreatedate.InnerText = createdate;
            xeqid.AppendChild(xcreatedate);

            XmlElement xlogodate = dom.CreateElement("logodate");
            xlogodate.InnerText = logodate;
            xeqid.AppendChild(xlogodate);

            XmlElement xbeizhu = dom.CreateElement("beizhu");
            xbeizhu.InnerText = beizhu;
            xeqid.AppendChild(xbeizhu);

            XmlElement xused = dom.CreateElement("used");
            xused.InnerText = used.ToString();
            xeqid.AppendChild(xused);

            root.AppendChild(xeqid);

            dom.Save(System.AppDomain.CurrentDomain.BaseDirectory + "client.xml");
        }

        public Cliinf(string streqid)
        {
            XmlDocument dom = new XmlDocument();
            dom.Load(System.AppDomain.CurrentDomain.BaseDirectory + "client.xml");
            foreach (XmlElement neqid in dom.DocumentElement.ChildNodes)
            {
                if (streqid == neqid.Name)
                {
                    exist = true;
                    eqid = streqid;
                    foreach (XmlElement tname in neqid.ChildNodes)
                    {
                        switch (tname.Name)
                        {
                            case "name": name = tname.InnerText;
                                break;
                            case "eqname": eqname = tname.InnerText;
                                break;
                            case "lstip": lstip = tname.InnerText;
                                break;
                            case "createdate": createdate = tname.InnerText;
                                break;
                            case "logodate": logodate = tname.InnerText;
                                break;
                            case "beizhu": beizhu = tname.InnerText;
                                break;
                            case "used": used = tname.InnerText.ToLower() == "true" ? true : false;
                                break;

                        }
                    }

                }
            }
        }



    }
    class Client
    {
        #region 字段
        private System.Threading.Thread clthread;
        private System.Net.EndPoint endpoint;
        private string name;
        private System.Net.Sockets.Socket sock;
        #endregion


        #region 构造
        public Client(string _name, System.Net.EndPoint _endpoint, System.Threading.Thread _thread, System.Net.Sockets.Socket _sock)
        {
            clthread = _thread;
            endpoint = _endpoint;
            name = _name;
            sock = _sock;
        }
        #endregion
        #region 方法
        public override string ToString()
        {
            return endpoint.ToString() + ":" + name;
        }
        #endregion

        #region 属性
        public System.Threading.Thread CLThread
        {
            get { return clthread; }
            set { clthread = value; }
        }
        public System.Net.EndPoint Host
        {
            get { return endpoint; }
            set { endpoint = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public System.Net.Sockets.Socket Sock
        {
            get { return sock; }
            set { sock = value; }
        }
        #endregion
    }

}
