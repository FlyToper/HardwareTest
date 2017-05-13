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
    public partial class Form2 : Form
    {
        public SerialPort sp = null;//串口对象
        public bool isOpen = false;//是否打开串口

        public string comPort = "";//串口号
        public string baudRate = "";//波特率
        public string stopbits = "";//停止位
        public string parity = "";//奇偶校验

        private int btnType = 0;//按钮类型 1-连接 2-其他
        //private int resend = 1;//当前重发次数
        public Form PF = null;

        //数据接收使用的代理
        private delegate void myDelegate(byte[] readBuffer);


        public Form2()
        {
            InitializeComponent();
        }

        

        private void Form2_Load(object sender, EventArgs e)
        {
            cbxMacAddress.SelectedIndex = 0;//从机地址
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

                if (btnType == 1)//从机连接
                {
                    string[] data = rst.Split('-');
                    int address = Convert.ToInt32(cbxMacAddress.Text.Trim());

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


    }
}
