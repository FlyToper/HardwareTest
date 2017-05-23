using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 硬件调试
{
    public partial class Form2 : Form
    {
        

        public SerialPort sp = null;//串口对象
        public bool isOpen = false;//是否打开串口

        public string comPort = "";//串口号
        public string baudRate = "";//波特率
        public string stopbits = "";//停止位
        public string parity = "";//奇偶校验

        private int btnType = 0;//按钮类型 1-连接 2-读取 3-设置
        //private int resend = 1;//当前重发次数
        public Form PF = null;

        //读取串口相关参数
        private Timer timer1 = null;
        private int k1 = 1;
        private int k2 = 0;
        private int k3 = 0;
        private int[] brate = { 4800, 9600, 14400, 19200, 28800, 38400, 57600, 115200 };
        private Parity[] p = { Parity.None, Parity.Odd, Parity.Even };
        private bool isReadOK = false;


        //数据接收使用的代理
        private delegate void myDelegate(byte[] readBuffer);


        public Form2()
        {
            InitializeComponent();
        }

        

        private void Form2_Load(object sender, EventArgs e)
        {
            cbxMacAddress.SelectedIndex = 0;//从机地址

            cbxSetAddress.SelectedIndex = 0;//设置的从机地址
            cbxSetBaudRate.SelectedIndex = 0;//设置的波特率
            cbxSetParity.SelectedIndex = 0;//设置的奇偶校验位

        }
        
        


        /// <summary>
        /// 【从机连接按钮点击事件】
        /// 20170501
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!isOpen)   
            {
                SetPortProperty();//执行设置串口
            }


            SendData(cbxMacAddress.Text.Trim(), "03", "00", "01");
            btnType = 1;
            //MessageBox.Show(comPort+"-"+baudRate+"-"+stopbits+"-"+parity);
            //frm = new Form1();//
            

            //Control cs = frm.Controls.Find("cbxCOMPort", true).First();
            //MessageBox.Show(cs.Text);
            

        }


        /// <summary>
        /// 【发送指令】
        /// 20170505
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
            catch (Exception e)
            {
                //ShowMessage(2, "系统错误！(可能原因：串口设置问题)");
                ShowMessage(2, "系统错误！(可能原因:" + e.Message + ")");
            }


        }


        /// <summary>
        /// 【端口检测】
        ///  20170505
        /// </summary>
        /// <returns></returns>
        private bool CheckPort()
        {
            try
            {
                SerialPort p = new SerialPort(comPort);

                p.Open();
                p.Close();
                return true;

            }
            catch (Exception e)
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
                if (!CheckPort()) 
                {
                    ShowMessage(2, "串口打开失败！");
                    return;
                }

                sp = new SerialPort();

                //0、设置委托
                sp.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);//设置委托

                //1、设置串口名称
                sp.PortName = comPort;

                //2、设置波特率
                sp.BaudRate = Convert.ToInt32(baudRate);

                //3、设置停止位
                float f = Convert.ToSingle(stopbits);
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
                string s = parity;
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
                ShowMessage(1, "串口设置成功");
                
            }
            catch
            {
                MessageBox.Show("串口设置出错，请仔细检查配置！", "系统提示");
                //lblState.Text = "串口打开失败！";
            }

        }

        /// <summary>
        /// 【数据接受委托】
        ///  201701505
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


                string[] data = rst.Split('-');
                int address = Convert.ToInt32(cbxMacAddress.Text.Trim());
                if (btnType == 1)//从机连接
                {
                   
                    if (string.Format("{0:00}", address) == data[0])
                    {
                        // Form1.m_io = Convert.ToInt32( data[4]);
                        //Form1.ChangIO(64);
                        //Form pf = this.MdiParent;
                        if (PF.GetType() == typeof(Form1))
                        {
                            ((Form1)PF).ChangIO(cbxMacAddress.Text.Trim(), 64);
                        }
                        ShowMessage(1, "从机连接成功");
                        MessageBox.Show("连接成功！", "系统提示");
                        this.Dispose();

                    }
                    else
                    {
                        ShowMessage(2, "从机连接失败");
                    }
                }
                else if(btnType == 2)//读取串口
                {
                    if ("6D" == data[3])
                    {
                        this.isReadOK = true;
                        ShowMessage(1, "读取串口成功！");
                        //测试成功。
                    }

                }
            }
            catch
            {
                ShowMessage(2, "系统错误！");
            }
        }


        /// <summary>
        /// 【在TextBox中展示发送接收数据】
        ///  20170505
        /// </summary>
        /// <param name="type">数据类型：1-发送 2-接收</param>
        /// <param name="data">数据</param>
        private void ShowDataByTBX(int type, string data)
        {
            if (type == 1)//Send
            {
                tbxShowData.Text += "[ Send:" + data + " ]\r\n";
                //[ Time:" + DateTime.Now + " ]
            }
            else//接收数据
            {
                tbxShowData.Text += "[ Recv:" + data + " ]\r\n\r\n";
            }
        }

        /// <summary>
        /// 【状态显示】
        /// 20170505
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

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(sp != null)
                sp.Dispose();
        }

        /// <summary>
        /// 【读取串口点击事件】
        ///  20170519
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, EventArgs e)
        {
            if (btnRead.Text == "读取串口")
            {

                if (!isOpen)
                    SetPortProperty();//设置串口

                if (timer1 == null)
                    timer1 = new Timer();
                timer1.Interval = 1000;
                timer1.Tick += RunAction;
                timer1.Start();
                btnType = 2;//设置按钮类型

                btnRead.Text = "停止读取";

                BanControllers(1);//禁用控件
            }
            else
            {
                timer1.Dispose();
                k1 = 1;
                k2 = k3 = 0;//初始化数据
                isReadOK = false;

                btnRead.Text = "读取串口";

                //开启被禁用的
                BanControllers(2);
            }
             
        }

        /// <summary>
        /// 【禁用和启用控件】
        ///  20170523
        /// </summary>
        /// <param name="type">禁用还是启用：1-禁用 2-启用</param>
        private void BanControllers(int type = 1)
        {
            if (type == 1)
            {
                //禁用按钮
                cbxMacAddress.Enabled = false;//【从机地址】
                btnSetting.Enabled = false;//【设置串口】
                button1.Enabled = false;//【连接】

                cbxSetAddress.Enabled = false;
                cbxSetBaudRate.Enabled = false;
                cbxSetParity.Enabled = false;
            }
            else
            {
                //禁用按钮
                cbxMacAddress.Enabled = true;//【从机地址】
                btnSetting.Enabled = true;//【设置串口】
                button1.Enabled = true;//【连接】

                cbxSetAddress.Enabled = true;
                cbxSetBaudRate.Enabled = true;
                cbxSetParity.Enabled = true;
            }
        }

        

        /// <summary>
        /// 【读取串口-线程循环执行方法】
        ///  20170522
        /// </summary>
        private void RunAction(object sender, EventArgs e)
        {
            if (k1 >= 63)
            {
                //暂停定时器
                timer1.Stop();
                timer1.Dispose();
                btnRead.Text = "读取串口";

                //判断是否成功
                if (!isReadOK)
                {
                    ShowMessage(2, "读取串口失败!");
                }
            }
            else
            {
                if (k2 >= brate.Length)//7
                {
                    k2 = 0;
                    k1++;//修改从机型号
                }
                else
                {
                    if (k3 >= p.Length )//2
                    {
                        k3 = 0;
                        k2++;//修改波特率
                    }
                    else
                    {
                        sp.BaudRate = brate[k2];//修改波特率
                        sp.Parity = p[k3];//修改奇偶校验
                        if (!isReadOK)
                        {
                            //发送数据
                            SendData("0" + k1, "03", "00", "01");
                            //tbxShowData.Text += "k1=" + k1 + ", k2=" + k2 + ",k3=" + k3 + "\r\n";
                            k3++;//修改奇偶校验位
                        }
                        else
                        {
                            string[] p2 = {"无校验", "奇校验","偶校验"};
                            timer1.Dispose();
                            k1 = 1;
                            k2 = k3 = 0;//初始化数据
                            btnRead.Text = "读取串口";
                            tbxShowData.Text = "串口读取成功\r\n" + "从机地址为：[" + k1 + "]\r\n波特率为：[" + brate[k2] + "]\r\n奇偶校验为：[" + p2[k3]+"]";
                        }
                    }
                }
            }

            //MessageBox.Show("k1="+k1+", k2="+k2+",k3="+k3);
            //MessageBox.Show(isOpen.ToString());
        }

        /// <summary>
        /// 【设置串口点击事件】
        ///  20170519
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetting_Click(object sender, EventArgs e)
        {
            Send13KLongFrame();
        }

        /// <summary>
        /// 【发送设置串口的长数据帧数据】
        ///  20170523
        /// </summary>
        private void Send13KLongFrame()
        {
            string setaddr = cbxSetAddress.Text.Trim();
            string setbaudrate = cbxSetBaudRate.Text.Trim();
            string setparity = cbxSetParity.Text.Trim();
            if (string.IsNullOrEmpty(setaddr) || string.IsNullOrEmpty(setbaudrate) || string.IsNullOrEmpty(setparity))
            {
                ShowMessage(2, "请设置左边的参数");
                return;
            }
            else
            {
                //奇偶校验位转换
                if (setparity == "无")
                    setparity = "00";
                else if (setparity == "奇校验")
                    setparity = "01";
                else
                    setparity = "10";

                //波特率转换
                string[] ss = {"000","001","010","011","100","101","110","111"};
                setbaudrate = ss[cbxSetBaudRate.SelectedIndex];
            }

            if (!isOpen)
                SetPortProperty();//设置并打开串口

            //01 10 00 08 00 02 04 xx xx xx  xx CRC
            //000+校验位2位+波特率3位
            byte[] data =  
            { 
                Convert.ToByte(setaddr,16),//从机地址
                Convert.ToByte("10",16),
                Convert.ToByte("00",16),
                Convert.ToByte("08",16),
                Convert.ToByte("00",16),
                Convert.ToByte("02",16),
                Convert.ToByte("04",16),
                Convert.ToByte("00",16),
                Convert.ToByte("0"+setparity.Substring(0,1),16),//0 + 奇偶校验位第一位
                Convert.ToByte(setparity.Substring(1,1)+setbaudrate.Substring(0,1),16),//奇偶校验位第二位 + 波特率第一位
                Convert.ToByte(setbaudrate.Substring(1,2),16),//波特率第二三位

            };

            //str = str.PadLeft(8, '0');
            //string.Format("{0:}", Convert.ToInt32(str, 2));

            //MessageBox.Show("你点击了" + string.Format("{0:X}", Convert.ToInt32(str, 2)).PadLeft(2,'0'));

            MyModbus modbus = new MyModbus();
            byte[] text = modbus.Get13kReadFrame(data);
            sp.Write(text, 0, 13);

            ShowDataByTBX(1, BitConverter.ToString(text));
        }

    }
}
