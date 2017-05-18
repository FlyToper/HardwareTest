namespace 硬件调试
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.cbxCOMPort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxBaudRate = new System.Windows.Forms.ComboBox();
            this.cbxParity = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblComState = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblState = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.cbxStopBits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbxMacAddress = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnReadMac = new System.Windows.Forms.Button();
            this.tbxMacVersion = new System.Windows.Forms.TextBox();
            this.tbxHWVersion = new System.Windows.Forms.TextBox();
            this.tbxSofeVersion = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tbxShowData = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口号：";
            // 
            // cbxCOMPort
            // 
            this.cbxCOMPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCOMPort.FormattingEnabled = true;
            this.cbxCOMPort.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10"});
            this.cbxCOMPort.Location = new System.Drawing.Point(79, 32);
            this.cbxCOMPort.Name = "cbxCOMPort";
            this.cbxCOMPort.Size = new System.Drawing.Size(89, 22);
            this.cbxCOMPort.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(186, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "波特率：";
            // 
            // cbxBaudRate
            // 
            this.cbxBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxBaudRate.FormattingEnabled = true;
            this.cbxBaudRate.Items.AddRange(new object[] {
            "4800",
            "9600",
            "14400",
            "19200",
            "28800",
            "38400",
            "57600",
            "115200"});
            this.cbxBaudRate.Location = new System.Drawing.Point(264, 29);
            this.cbxBaudRate.Name = "cbxBaudRate";
            this.cbxBaudRate.Size = new System.Drawing.Size(89, 22);
            this.cbxBaudRate.TabIndex = 3;
            // 
            // cbxParity
            // 
            this.cbxParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxParity.FormattingEnabled = true;
            this.cbxParity.Items.AddRange(new object[] {
            "无",
            "奇校验",
            "偶校验"});
            this.cbxParity.Location = new System.Drawing.Point(264, 69);
            this.cbxParity.Name = "cbxParity";
            this.cbxParity.Size = new System.Drawing.Size(89, 22);
            this.cbxParity.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(186, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "校验位：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblComState);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lblState);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.cbxStopBits);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cbxParity);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbxCOMPort);
            this.groupBox1.Controls.Add(this.cbxBaudRate);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(15, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(478, 183);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "【串口设置】";
            // 
            // lblComState
            // 
            this.lblComState.AutoSize = true;
            this.lblComState.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblComState.Location = new System.Drawing.Point(86, 114);
            this.lblComState.Name = "lblComState";
            this.lblComState.Size = new System.Drawing.Size(24, 16);
            this.lblComState.TabIndex = 15;
            this.lblComState.Text = "无";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(6, 114);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 16);
            this.label10.TabIndex = 14;
            this.label10.Text = "可用串口：";
            // 
            // lblState
            // 
            this.lblState.AutoSize = true;
            this.lblState.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblState.Location = new System.Drawing.Point(86, 148);
            this.lblState.Name = "lblState";
            this.lblState.Size = new System.Drawing.Size(24, 16);
            this.lblState.TabIndex = 13;
            this.lblState.Text = "无";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(6, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "状  态：";
            // 
            // btnConnect
            // 
            this.btnConnect.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConnect.Location = new System.Drawing.Point(378, 69);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(82, 31);
            this.btnConnect.TabIndex = 11;
            this.btnConnect.Text = "连接从机";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpen.Location = new System.Drawing.Point(378, 24);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(82, 31);
            this.btnOpen.TabIndex = 10;
            this.btnOpen.Text = "打开串口";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // cbxStopBits
            // 
            this.cbxStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxStopBits.FormattingEnabled = true;
            this.cbxStopBits.Items.AddRange(new object[] {
            "1"});
            this.cbxStopBits.Location = new System.Drawing.Point(79, 69);
            this.cbxStopBits.Name = "cbxStopBits";
            this.cbxStopBits.Size = new System.Drawing.Size(89, 22);
            this.cbxStopBits.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(6, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 16);
            this.label4.TabIndex = 6;
            this.label4.Text = "停止位：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbxMacAddress);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.btnReadMac);
            this.groupBox2.Controls.Add(this.tbxMacVersion);
            this.groupBox2.Controls.Add(this.tbxHWVersion);
            this.groupBox2.Controls.Add(this.tbxSofeVersion);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.Location = new System.Drawing.Point(15, 212);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(478, 125);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "【版本读取】";
            // 
            // cbxMacAddress
            // 
            this.cbxMacAddress.FormattingEnabled = true;
            this.cbxMacAddress.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39",
            "40",
            "41",
            "42",
            "43",
            "44",
            "45",
            "46",
            "47",
            "48",
            "49",
            "50",
            "51",
            "52",
            "53",
            "54",
            "55",
            "56",
            "57",
            "58",
            "59",
            "60",
            "61",
            "62",
            "63",
            ""});
            this.cbxMacAddress.Location = new System.Drawing.Point(285, 79);
            this.cbxMacAddress.Name = "cbxMacAddress";
            this.cbxMacAddress.Size = new System.Drawing.Size(68, 22);
            this.cbxMacAddress.TabIndex = 27;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(186, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 16);
            this.label9.TabIndex = 26;
            this.label9.Text = "从机地址：";
            // 
            // btnReadMac
            // 
            this.btnReadMac.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReadMac.Location = new System.Drawing.Point(380, 27);
            this.btnReadMac.Name = "btnReadMac";
            this.btnReadMac.Size = new System.Drawing.Size(80, 31);
            this.btnReadMac.TabIndex = 25;
            this.btnReadMac.Text = "读取";
            this.btnReadMac.UseVisualStyleBackColor = true;
            this.btnReadMac.Click += new System.EventHandler(this.btnReadMac_Click);
            // 
            // tbxMacVersion
            // 
            this.tbxMacVersion.Location = new System.Drawing.Point(107, 79);
            this.tbxMacVersion.Name = "tbxMacVersion";
            this.tbxMacVersion.ReadOnly = true;
            this.tbxMacVersion.Size = new System.Drawing.Size(61, 23);
            this.tbxMacVersion.TabIndex = 24;
            // 
            // tbxHWVersion
            // 
            this.tbxHWVersion.Location = new System.Drawing.Point(285, 29);
            this.tbxHWVersion.Name = "tbxHWVersion";
            this.tbxHWVersion.ReadOnly = true;
            this.tbxHWVersion.Size = new System.Drawing.Size(68, 23);
            this.tbxHWVersion.TabIndex = 23;
            // 
            // tbxSofeVersion
            // 
            this.tbxSofeVersion.Location = new System.Drawing.Point(107, 30);
            this.tbxSofeVersion.Name = "tbxSofeVersion";
            this.tbxSofeVersion.ReadOnly = true;
            this.tbxSofeVersion.Size = new System.Drawing.Size(61, 23);
            this.tbxSofeVersion.TabIndex = 22;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(13, 83);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 16);
            this.label8.TabIndex = 21;
            this.label8.Text = "从机型号：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(186, 35);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 16);
            this.label7.TabIndex = 20;
            this.label7.Text = "硬件版本：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("黑体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(13, 34);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 16);
            this.label6.TabIndex = 19;
            this.label6.Text = "软件版本：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tbxShowData);
            this.groupBox3.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.Location = new System.Drawing.Point(19, 343);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(478, 114);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "【收发数据】";
            // 
            // tbxShowData
            // 
            this.tbxShowData.BackColor = System.Drawing.SystemColors.Info;
            this.tbxShowData.Location = new System.Drawing.Point(6, 20);
            this.tbxShowData.Multiline = true;
            this.tbxShowData.Name = "tbxShowData";
            this.tbxShowData.ReadOnly = true;
            this.tbxShowData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbxShowData.Size = new System.Drawing.Size(466, 88);
            this.tbxShowData.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox4.Location = new System.Drawing.Point(509, 23);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(254, 314);
            this.groupBox4.TabIndex = 21;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "【通道状态】";
            this.groupBox4.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 473);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "硬件调试";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbxCOMPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxBaudRate;
        private System.Windows.Forms.ComboBox cbxParity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.ComboBox cbxStopBits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblState;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblComState;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbxMacAddress;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnReadMac;
        private System.Windows.Forms.TextBox tbxMacVersion;
        private System.Windows.Forms.TextBox tbxHWVersion;
        private System.Windows.Forms.TextBox tbxSofeVersion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbxShowData;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}

