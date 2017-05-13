using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 硬件调试
{
    public partial class Form1 : Form
    {
        public SerialPort sp = null;//串口对象
        bool isOpen = false;//是否打开串口
        int btnType = 0;//1---读取从机信息 2-读取输入IO 3-自动读取输入
        

        //数据接收使用的代理
        private delegate void myDelegate(byte[] readBuffer);

        private Timer t1 = null;//时间器

        //连接从机
        private int xinput = 0;//输入
        private int youtput = 0;//输出
        private string macaddr = "01";//当前连接的从机地址

        //public static int m_io = 0;//io点

        public Form1()
        {
            InitializeComponent();
            //this.IsMdiContainer = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblState.Text = "1";
            //MessageBox.Show("Load");
            
            //1、设置窗口大小固定
            this.MaximumSize = this.Size;
            this.MinimumSize = this.Size;
            cbxStopBits.SelectedIndex = 0;//停止位
            cbxBaudRate.SelectedIndex = 0;
            cbxParity.SelectedIndex = 1;

            cbxMacAddress.SelectedIndex = 0;//从机地址


            #region 检查是否有可用串口
            SerialPort p = null;
            string usefulcom = "";
            for (int i = 1; i <= 10; i++)
            {
                try
                {
                    p = new SerialPort("COM" + i);
                    p.Open();
                    p.Close();

                    usefulcom += " 【COM" + i+"】";
                    cbxCOMPort.SelectedIndex = i - 1;
                }
                catch
                {
                    continue;   
                }
            }

            if (usefulcom != "")
            {
                lblComState.ForeColor = Color.Green;
                lblComState.Text = usefulcom;
            }
            else
            {
                lblComState.ForeColor = Color.Red;
                lblComState.Text = "无可用串口！";
            }

            #endregion

        }


        /// <summary>
        /// 【端口检测】
        ///  20170428
        /// </summary>
        /// <returns></returns>
        private bool CheckPort()
        {
            try
            {
                string port = cbxCOMPort.Text.Trim();//获取当前选中的端口号
                sp = new SerialPort(port);

                
                sp.Open();
                sp.Close();
                return true;

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }

        }

        /// <summary>
        /// 【设置端口属性】
        ///  20170428
        /// </summary>
        private void SetPortProperty()
        {
            try
            {
                if (isOpen)
                    return;//如果已经开启，则不执行

                sp = new SerialPort();

                //0、设置委托
                sp.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);//设置委托

                //1、设置串口名称
                sp.PortName = cbxCOMPort.Text.Trim();

                //2、设置波特率
                sp.BaudRate = Convert.ToInt32(cbxBaudRate.Text.Trim());

                //3、设置停止位
                float f = Convert.ToSingle(cbxStopBits.Text.Trim());
                #region 停止位
                if (f == 0)
                {
                    sp.StopBits = StopBits.None;//表示不使用停止位
                }
                else if (f == 1.5)
                {
                    sp.StopBits = StopBits.OnePointFive;//使用1.5个停止位
                }
                else if (f == 2)
                {
                    sp.StopBits = StopBits.Two;//表示使用两个停止位
                }
                else
                {
                    sp.StopBits = StopBits.One;//默认使用一个停止位
                }
                #endregion

                //4、设置奇偶校验位
                string s = cbxParity.Text.Trim();
                #region 奇偶校验
                if (s.CompareTo("无") == 0)
                {
                    sp.Parity = Parity.None;//不发生奇偶校验检查
                }
                else if (s.CompareTo("奇校验") == 0)
                {
                    sp.Parity = Parity.Odd;//设置奇校验
                }
                else if (s.CompareTo("偶校验") == 0)
                {
                    sp.Parity = Parity.Even;//设置偶检验
                }
                else
                {
                    sp.Parity = Parity.None;
                }
                #endregion

                //5、设置数据读取超时
                sp.ReadTimeout = -1;

                //6、打开串口
                sp.Open();
                isOpen = true;
                lblState.Text = "串口打开成功！";
            }
            catch
            {
                MessageBox.Show("串口设置出错，请仔细检查配置！", "系统提示");
                lblState.Text = "串口打开失败！";
            }
            
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show("你好啊","系统提示");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            Form2 frm2 = new Form2();
            if (sp != null)
            {
                isOpen = false;//是否开启
                sp.Dispose();
            }

            frm2.comPort = cbxCOMPort.Text.Trim();//串口号
            frm2.baudRate = cbxBaudRate.Text.Trim();//波特率
            frm2.stopbits = cbxStopBits.Text.Trim();//停止位
            frm2.parity = cbxParity.Text.Trim();//奇偶校验
            frm2.isOpen = isOpen;//是否开启
            frm2.sp = sp;//串口对象
            //frm2.MdiParent = this;
            frm2.PF = this;

            frm2.StartPosition = FormStartPosition.CenterParent;
            

           frm2.ShowDialog();
            //frm2.Show();
           
        }

        /// <summary>
        /// 【打开串口按钮事件】
        ///  20170428
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            
            //1、检查串口和设置串口
            if (!isOpen)
            {
                if (!this.CheckPort())
                {
                    ShowMessage(2, "串口打开失败！");
                    return;
                }
                this.SetPortProperty();
                BanController();//禁用
                btnOpen.Text = "关闭串口";
            }
            else
            {
                if(sp != null)
                    sp.Dispose();
               
                btnOpen.Text = "打开串口";
                isOpen = false;
                BanController();

            }
            
        }


        /// <summary>
        /// 【读取从机型号和硬件版本】
        ///  20170428
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadMac_Click(object sender, EventArgs e)
        {
            BanController();
            //if (!this.CheckPort())
            //{
            //    ShowMessage(2, "串口打开失败！");
            //    return;
            //}
            
            int macaddress = Convert.ToInt32( cbxMacAddress.Text.Trim());
            if (macaddress < 1 || macaddress > 63)
            {
                MessageBox.Show("从机地址错误，请选择1-63", "系统提示");
                return;
            }


            btnType = 1;//设置标识
            SendData(macaddress.ToString(),"03","00","03");

        }

        /// <summary>
        /// 【发送指令】
        /// 20170428
        /// </summary>
        /// <param name="address">地址码</param>
        /// <param name="cmd">功能码</param>
        /// <param name="regAddr">地址码</param>
        /// <param name="regNum"></param>
        private void SendData(string address, string cmd, string regAddr, string regNum)
        {
            try
            {
                if (!isOpen)
                    SetPortProperty();//设置并打开串口

                byte address1 = Convert.ToByte(address.Trim(), 16);//地址码
                byte cmd1 = Convert.ToByte(cmd.Trim(), 16);//命令帧
                byte regAddr1 = Convert.ToByte(regAddr.Trim(), 16);//寄存器地址
                byte regNum1 = Convert.ToByte(regNum.Trim(), 16);//寄存器数量

                //Modbus相关处理对象
                MyModbus modbus = new MyModbus();
                byte[] text = modbus.GetReadFrame(address1, cmd1, regAddr1, regNum1, 8);
                sp.Write(text, 0, 8);



                //tbxShowData.Text += "[ Send:" + BitConverter.ToString(text) + " ] [ Time:"+DateTime.Now +" ]\r\n";
                ShowDataByTBX(1, BitConverter.ToString(text));
            }
            catch(Exception e)
            {
                //ShowMessage(2, "系统错误！(可能原因：串口设置问题)");
                ShowMessage(2, "系统错误！(可能原因:"+e.Message+")");
            }

            
        }



        public void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            myDelegate md = new myDelegate(ShowRst);
            try
            {
                if (sp.IsOpen)
                {
                    int count = sp.BytesToRead;
                    if (count > 0)
                    {
                        byte[] readBuffer = new byte[count];
                        sp.Read(readBuffer, 0, count);//读取串口数据
                        //     serialPort1.Write(readBuffer, 0, count);
                        Invoke(md, readBuffer);
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

        }

        /// <summary>
        /// 【显示接收返回的数据】
        /// </summary>
        /// <param name="resbuffer"></param>
        public void ShowRst(byte[] resbuffer)
        {
            try
            {
                MyModbus modbus = new MyModbus();
                //tbxRecvData.Text += "Recv:" + modbus.SetText(resbuffer) + "\r\n";
                //tbxRecvData.Text += "\r\n";
                if (resbuffer.Length <= 0)
                {
                    //tbxShowData.Text += "[ Recv:" + "无可读数据" + " ]\r\n\r\n";
                    ShowDataByTBX(2, "无可读数据");
                    return;
                }

                string str = modbus.SetText(resbuffer);
                if (str == "CRC校验错误")
                {
                    ShowMessage(2, "CRC校验错误");
                    return;
                }
                if (str == "程序出错")
                {
                    ShowMessage(2, "程序错误");
                    return;
                }

                string rst = "";
                for (int i = 0; i < str.Length; i++)
                {
                    if ((i + 1) % 2 == 0 && i < str.Length - 1)
                        rst += str[i] + "-";
                    else
                        rst += str[i];
                }

                //MessageBox.Show(rst);
                //tbxShowData.Text += "[ Recv:" + rst + " ] [ Time:" + DateTime.Now + " ]\r\n\r\n";
                ShowDataByTBX(2, rst);//展示数据
                ShowMessage(1, "读取成功");


                string[] data = rst.Split('-');//{ "01", "02","03" }
                if (btnType == 1)//读取版本型号ye
                {
                    tbxSofeVersion.Text = data[5] + " " + data[6];//软件版本
                    tbxMacVersion.Text = data[3] + " " + data[4];//从机型号
                    tbxHWVersion.Text = data[7] + " " + data[8];//硬件版本
                }
                else if (btnType == 2)//读取输入IO
                {
                    int address = Convert.ToInt32(macaddr);

                    if (string.Format("{0:00}", address) == data[0])
                    {
                        ShowMessage(1, "读取成功");    
                    }
                    else
                    {
                        ShowMessage(2, "读取失败");
                    }
                }
                else if (btnType == 3)//自动读取IO
                {
 
                }
            }
            catch
            {
                ShowMessage(2, "系统错误！");
            }
        }

        /// <summary>
        /// 【状态显示】
        /// 20170429
        /// </summary>
        /// <param name="type"></param>
        /// <param name="msg"></param>
        public void ShowMessage(int type, string msg) 
        {
            if (type == 1)//成功的状态
            {
                lblState.Text = msg;
                lblState.ForeColor = Color.Green;
            }
            else//失败的状态显示
            {
                lblState.Text = msg;
                lblState.ForeColor = Color.Red;
            }
        }

        /// <summary>
        /// 【设置控件的禁用】
        ///  20170430
        /// </summary>
        private void BanController()
        {
            if (isOpen)
            {
                cbxCOMPort.Enabled = false;//串口号
                cbxBaudRate.Enabled = false;//波特率
                cbxParity.Enabled = false;//奇偶校验
                cbxStopBits.Enabled = false;//停止位
            }
            else
            {
                cbxCOMPort.Enabled = true;//串口号
                cbxBaudRate.Enabled = true;//波特率
                cbxParity.Enabled = true;//奇偶校验
                cbxStopBits.Enabled = true;//停止位
            }
        }

        /// <summary>
        /// 【在TextBox中展示发送接收数据】
        ///  20170430
        /// </summary>
        /// <param name="type">数据类型：1-发送 2-接收</param>
        /// <param name="data">数据</param>
        private void ShowDataByTBX(int type, string data)
        {
            if (type == 1)//Send
            {
                tbxShowData.Text += "[ Send:" + data + " ] [ Time:" + DateTime.Now + " ]\r\n";
            }
            else//接收数据
            {
                tbxShowData.Text += "[ Recv:" + data + " ] [ Time:" + DateTime.Now + " ]\r\n\r\n";
            }
        }

        /// <summary>
        /// 【动态添加新按钮】
        ///  20170506
        /// </summary>
        /// <param name="m_io"></param>
        public void ChangIO(string maddr,int m_io)
        {
            Font font = new Font("黑体", 12);
            //1、容器出初始化
            this.MaximumSize = new Size(792,512);
            this.MinimumSize = new Size(792,512);
            groupBox4.Visible = true;
            groupBox4.Controls.Clear();

            //2、
            string str = string.Format("{0:00}", m_io);
            int x = Convert.ToInt32( str.Substring(0,1));
            int y = Convert.ToInt32(str.Substring(1,1));
            

            //赋值，后面调用
            xinput = x;
            youtput = y;
            macaddr = maddr;

            for (int i = 0; i < x; i++)
            {
                //输入 X0 ●
                //Label x1 = GetLabel("X"+i,"X"+i,24,16,23,24+(24*i),font);
                //Label x2 = GetLabel("lbl" + (i + 100), "●", 24, 16, 47, 24 + (24 * i),font);

                groupBox4.Controls.Add(GetLabel("X" + i, "X" + i, 24, 16, 23, 24 + (24 * i), font));
                groupBox4.Controls.Add(GetLabel("lbl" + (i + 100), "●", 24, 16, 47, 24 + (24 * i), font));
                
            }


            int ybx = 150;
            if (x <= 0)
                ybx = 23;
            for (int i = 0; i < y; i++)
            {
                CheckBox ck = new CheckBox();

                ck.Name = "Y" + i;
                ck.Text = "Y" + i;
                ck.Font = font;
                ck.Size = new Size(46, 20);
                ck.Location = new Point(ybx, 23 + (24 * i));

                groupBox4.Controls.Add(ck);
 
            }

            //3、添加按钮
            
            int btnHeight = x>y?(x+1)*24:(y+1)*24;
            //3-1 输入按钮
            if (x > 0)
            {


                Button btn1 = GetButton("btnReadIO", "读取", 80, 31, 23, btnHeight,font);
                btn1.Click += new EventHandler(btnReadIO_Click);//点击事件
               
                groupBox4.Controls.Add(btn1);

                TextBox tb1 = new TextBox();
                tb1.Name = "tbxAutoReadTime";
                tb1.Text = "100";
                tb1.Font = font;
                tb1.Size = new Size(50, 23);
                tb1.Location = new Point(23, btnHeight + 43);
                groupBox4.Controls.Add(tb1);

                Label lb1 = GetLabel("lb1", "(ms)", 60, 16, 70, btnHeight + 47, font);
                groupBox4.Controls.Add(lb1);



                Button btn2 = GetButton("btnAutoReadIO", "自动读取", 80, 31, 23, btnHeight + 78, font);
                btn2.Click += new EventHandler(btnAutoReadIO_Click);

                groupBox4.Controls.Add(btn2);
            }
            //3-2 输出按钮
            if (y > 0)
            {
                Button btn3 = GetButton("btnCloseAll","关闭所有",80,31,ybx,btnHeight,font);
                btn3.Click += new EventHandler(btnCloseAll_Click);//点击事件

                groupBox4.Controls.Add(btn3);

                Button btn4 = GetButton("btnOpenAll", "打开所有", 80, 31, ybx, btnHeight + 43, font);
                btn4.Click += new EventHandler(btnOpenAll_Click);//绑定事件
                groupBox4.Controls.Add(btn4);
            }

        }

        /// <summary>
        /// 【读取-点击事件】
        ///  20170508
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReadIO_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("读取IO");
            btnType = 2;//设置按钮类型
            SendData(macaddr, "02", "00", string.Format("{0:00}", xinput));
        }

        /// <summary>
        /// 【自动读取点击事件】
        ///  20170508
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAutoReadIO_Click(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)groupBox4.Controls.Find("tbxAutoReadTime", false).First();
            int time = Convert.ToInt32( tb.Text.Trim());

            if(t1 == null)
                t1 = new Timer();
            t1.Interval = time;
            
            t1.Tick += RunAutoReadIO;
            t1.Start();


            //MessageBox.Show("自动读取IO");
        }

        /// <summary>
        /// 【自动读取-定时器执行的事件】
        ///  20170508
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunAutoReadIO(object sender, EventArgs e)
        {
            //MessageBox.Show("Run");
            //return;
            
            //lblState.Text = (Convert.ToInt32(lblState.Text.Trim()) + 1).ToString();

            btnType = 2;//设置按钮类型
            SendData(macaddr, "02", "00", string.Format("{0:00}", xinput));
        }

        /// <summary>
        /// 【关闭所有点击事件】
        ///  20170508
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseAll_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < youtput; i++)
            {
                try
                {
                    CheckBox ck = groupBox4.Controls.Find("Y" + i, false).First() as CheckBox;

                    ck.Checked = false;      
                }
                catch
                {
                    continue;
                }  
            }
        }

        /// <summary>
        /// 【打开所有点击事件】
        ///  20170508
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < youtput; i++)
            {
                try
                {
                    CheckBox ck = groupBox4.Controls.Find("Y" + i, false).First() as CheckBox;

                    ck.Checked = true;
                }
                catch
                {
                    continue;
                }
            }
        }


        /// <summary>
        /// 【获取Label】
        ///  20170507
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="text">Text</param>
        /// <param name="sx">Size的第一个参数</param>
        /// <param name="sy">Size的第二个参数</param>
        /// <param name="px">Point的第一个参数</param>
        /// <param name="py">Point的第二个参数</param>
        /// <param name="font">字体</param>
        /// <returns></returns>
        private Label GetLabel(string name, string text, int sx, int sy, int px, int py, Font font)
        {
            Label lbl = new Label();
            lbl.Text = text;
            lbl.Name = name;
            lbl.Font = font;
            lbl.Size = new Size(sx, sy);
            lbl.Location = new Point(px, py);
            if (text == "●")
                lbl.ForeColor = Color.Red;

            return lbl;
        }

        /// <summary>
        /// 【获取Button】
        /// 20170507
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="text">Text</param>
        /// <param name="sx">Size的第一个参数</param>
        /// <param name="sy">Size的第二个参数</param>
        /// <param name="px">Point的第一个参数</param>
        /// <param name="py">Point的第二个参数</param>
        /// <param name="font">字体</param>
        /// <returns></returns>
        private Button GetButton(string name, string text, int sx, int sy, int px, int py, Font font) 
        {
            Button btn = new Button();
            btn.Name = name;
            btn.Text = text;
            btn.Font = font;
            btn.Size = new Size(sx, sy);
            btn.Location = new Point(px, py);

            return btn;
        }
     

       
    }
}
