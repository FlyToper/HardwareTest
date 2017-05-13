using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 硬件调试
{
    class Basic
    {
        //private SerialPort sp = null;


        ////数据接收使用的代理
        //private delegate void myDelegate(byte[] readBuffer);


        ///// <summary>
        ///// 【端口检测】
        /////  20170501
        ///// </summary>
        ///// <returns></returns>
        //private bool CheckPort(string port)
        //{
        //    try
        //    {
        //        //string port = cbxCOMPort.Text.Trim();//获取当前选中的端口号
        //        sp = new SerialPort(port);


        //        sp.Open();
        //        sp.Close();
        //        return true;

        //    }
        //    catch (Exception e)
        //    {
        //        //MessageBox.Show(e.Message);
        //        return false;
        //    }

        //}


        ///// <summary>
        ///// 【设置端口属性】
        /////  20170501
        ///// </summary>
        //private SerialPort SetPortProperty(string portName, string baudRate, string stopBits, string parity)
        //{
        //    try
        //    {

        //        sp = new SerialPort();

        //        //0、设置委托
        //        sp.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);//设置委托

        //        //1、设置串口名称
        //        sp.PortName = portName.Trim();

        //        //2、设置波特率
        //        sp.BaudRate = Convert.ToInt32(baudRate.Trim());

        //        //3、设置停止位
        //        float f = Convert.ToSingle(stopBits.Trim());
        //        #region 停止位
        //        if (f == 0)
        //        {
        //            sp.StopBits = StopBits.None;//表示不使用停止位
        //        }
        //        else if (f == 1.5)
        //        {
        //            sp.StopBits = StopBits.OnePointFive;//使用1.5个停止位
        //        }
        //        else if (f == 2)
        //        {
        //            sp.StopBits = StopBits.Two;//表示使用两个停止位
        //        }
        //        else
        //        {
        //            sp.StopBits = StopBits.One;//默认使用一个停止位
        //        }
        //        #endregion

        //        //4、设置奇偶校验位
        //        string s = parity.Trim();
        //        #region 奇偶校验
        //        if (s.CompareTo("无") == 0)
        //        {
        //            sp.Parity = Parity.None;//不发生奇偶校验检查
        //        }
        //        else if (s.CompareTo("奇校验") == 0)
        //        {
        //            sp.Parity = Parity.Odd;//设置奇校验
        //        }
        //        else if (s.CompareTo("偶校验") == 0)
        //        {
        //            sp.Parity = Parity.Even;//设置偶检验
        //        }
        //        else
        //        {
        //            sp.Parity = Parity.None;
        //        }
        //        #endregion

        //        //5、设置数据读取超时
        //        sp.ReadTimeout = -1;

        //        //6、打开串口
        //        sp.Open();
        //        isOpen = true;
        //        lblState.Text = "串口打开成功！";
        //    }
        //    catch
        //    {
        //        MessageBox.Show("串口设置出错，请仔细检查配置！", "系统提示");
        //        lblState.Text = "串口打开失败！";
        //    }

        //}

        ///// <summary>
        ///// 【接收数据的委托】
        /////  20170501
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        //{
        //    myDelegate md = new myDelegate(ShowRst);
        //    try
        //    {
        //        if (sp.IsOpen)
        //        {
        //            int count = sp.BytesToRead;
        //            if (count > 0)
        //            {
        //                byte[] readBuffer = new byte[count];
        //                sp.Read(readBuffer, 0, count);//读取串口数据
        //                //     serialPort1.Write(readBuffer, 0, count);

        //                if (System.Windows.Forms.Application.OpenForms.Count > 0)
        //                {
        //                    Form frm = System.Windows.Forms.Application.OpenForms.Cast<System.Windows.Forms.Form>().First();
        //                    frm.Invoke(md, readBuffer);
        //                }

                        
        //            }
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        throw err;
        //    }

        //}

        ///// <summary>
        ///// 【显示接收返回的数据】
        /////  20170501
        ///// </summary>
        ///// <param name="resbuffer"></param>
        //public void ShowRst(byte[] resbuffer)
        //{
        //    try
        //    {
        //        MyModbus modbus = new MyModbus();
        //        //tbxRecvData.Text += "Recv:" + modbus.SetText(resbuffer) + "\r\n";
        //        //tbxRecvData.Text += "\r\n";
        //        if (resbuffer.Length <= 0)
        //        {
        //            //tbxShowData.Text += "[ Recv:" + "无可读数据" + " ]\r\n\r\n";
        //            ShowDataByTBX(2, "无可读数据");
        //            return;
        //        }

        //        string str = modbus.SetText(resbuffer);
        //        string rst = "";
        //        for (int i = 0; i < str.Length; i++)
        //        {
        //            if ((i + 1) % 2 == 0 && i < str.Length - 1)
        //                rst += str[i] + "-";
        //            else
        //                rst += str[i];
        //        }

        //        //MessageBox.Show(rst);
        //        //tbxShowData.Text += "[ Recv:" + rst + " ] [ Time:" + DateTime.Now + " ]\r\n\r\n";
        //        ShowDataByTBX(2, rst);//展示数据
        //        ShowMessage(1, "读取成功");

        //        if (btnType == 1)//读取版本型号
        //        {
        //            string[] data = rst.Split('-');
        //            tbxSofeVersion.Text = data[5] + " " + data[6];//软件版本
        //            tbxMacVersion.Text = data[3] + " " + data[4];//从机型号
        //            tbxHWVersion.Text = data[7] + " " + data[8];//硬件版本
        //        }
        //    }
        //    catch
        //    {
        //        ShowMessage(2, "系统错误！");
        //    }
        //}
    }
}
