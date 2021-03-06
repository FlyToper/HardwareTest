﻿using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 硬件调试
{
    class MyModbus
    {

        #region CRC高位表 byte[] _auchCRCHi
        private static readonly byte[] _auchCRCHi = new byte[]//crc高位表
        {
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 
            0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 
            0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 
            0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 
            0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 
            0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
            0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
            0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 
            0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 
            0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 
            0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 
            0x80, 0x41, 0x00, 0xC1, 0x81, 0x40
        };
        #endregion

        #region CRC低位表 byte[] _auchCRCLo
        private static readonly byte[] _auchCRCLo = new byte[]//crc低位表
        {
            0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 
            0x07, 0xC7, 0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD, 
            0x0F, 0xCF, 0xCE, 0x0E, 0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09, 
            0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9, 0x1B, 0xDB, 0xDA, 0x1A, 
            0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC, 0x14, 0xD4, 
            0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3, 
            0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 
            0xF2, 0x32, 0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4, 
            0x3C, 0xFC, 0xFD, 0x3D, 0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A, 
            0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38, 0x28, 0xE8, 0xE9, 0x29, 
            0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF, 0x2D, 0xED, 
            0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26, 
            0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 
            0x61, 0xA1, 0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67, 
            0xA5, 0x65, 0x64, 0xA4, 0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F, 
            0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB, 0x69, 0xA9, 0xA8, 0x68, 
            0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA, 0xBE, 0x7E, 
            0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5, 
            0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 
            0x70, 0xB0, 0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92, 
            0x96, 0x56, 0x57, 0x97, 0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C, 
            0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E, 0x5A, 0x9A, 0x9B, 0x5B, 
            0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89, 0x4B, 0x8B, 
            0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C, 
            0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 
            0x43, 0x83, 0x41, 0x81, 0x80, 0x40
        };
        #endregion

        /// <summary>
        /// 【获取读数据命令，返回命令帧】
        /// </summary>
        /// <param name="mdaddr">地址码</param>
        /// <param name="R_CMD">功能码</param>
        /// <param name="min_reg">寄存器地址</param>
        /// <param name="data_len">寄存器个数</param>
        /// <param name="R_CMD_LEN">命令长度</param>
        /// <returns></returns>
        public byte[] GetReadFrame(byte mdaddr, byte R_CMD, ushort min_reg, ushort data_len, int R_CMD_LEN,int type = 1)
        {
            //主机命令帧格式
            //  字节    功能描述            例子
            //
            //  1         地址码             0x01
            //  2         功能码             0x03
            //  3         寄存器地址高       0x00
            //  4         寄存器地址低       0x00
            //  5         寄存器个数高       0x00
            //  6         寄存器个数低       0x02
            //  7         CRC检验码低        0xC4
            //  8         CRC校验码高        0x0B

            ushort crc;
            byte[] message = new byte[8];

            //设置模块号
            message[0] = mdaddr;
            //设置命令字
            message[1] = R_CMD;

            //设置开始寄存器
            message[2] = WORD_HI(min_reg);
            message[3] = WORD_LO(min_reg);
            

            //设置数据长度
            if (type == 1)
            {
                message[4] = WORD_HI(data_len);
                message[5] = WORD_LO(data_len);
            }
            else//处理 发送写入单个输出
            {
                message[4] = WORD_LO(data_len);
                message[5] = WORD_HI(data_len);
            }
            

            //设置 CRC
            crc = CRC16(message, 0, R_CMD_LEN - 3);

            message[6] = WORD_HI(crc);//CRC校验码高位
            message[7] = WORD_LO(crc);//CRC校验码低位


            return message;
        }

        //public byte[] GetReadFrame

        /// <summary>
        /// 【获取长字节数据帧】
        ///  20170518
        /// </summary>
        /// <param name="sdata"></param>
        /// <returns>10 字节的数据帧</returns>
        public byte[] GetLongReadFrame(byte[] sdata)
        {
            //从机地址，
            //功能码，
            //（线圈的）起始地址高，
            //（线圈的）起始地址低，
            //线圈数量高，
            //线圈数量低，
            //（线圈的）字节计数（1-8个线圈是1个字节，2-16个线圈是2字节，byteCount=线圈数/8），
            //线圈的输出状态，
            //CRC高，
            //CRC低
            ushort crc;
    
            byte[] message = new byte[10];
            message[0] = sdata[0];
            message[1] = sdata[1];
            message[2] = WORD_HI(sdata[2]);//线圈地址
            message[3] = WORD_LO(sdata[2]);
            message[4] = WORD_HI(sdata[3]);//线圈数量
            message[5] = WORD_LO(sdata[3]);
            message[6] = sdata[4];//字节数
            message[7] = sdata[5];//线圈输出状态
            





            //设置 CRC
            crc = CRC16(message, 0, 7);

            message[8] = WORD_HI(crc);//CRC校验码高位
            message[9] = WORD_LO(crc);//CRC校验码低位

            return message;
        }

        public byte[] Get13kReadFrame(byte[] sdata)
        {
            //01 10 00 08 00 02 04 xx xx xx  xx CRC
            ushort crc;

            byte[] message = new byte[13];
            message[0] = sdata[0];
            message[1] = sdata[1];
            message[2] = sdata[2];
            message[3] = sdata[3];
            message[4] = sdata[4];
            message[5] = sdata[5];
            message[6] = sdata[6];
            message[7] = sdata[7];
            message[8] = sdata[8];
            message[9] = sdata[9];
            message[10] = sdata[10];
           


            //设置 CRC
            crc = CRC16(message, 0, 10);

            message[11] = WORD_HI(crc);//CRC校验码高位
            message[12] = WORD_LO(crc);//CRC校验码低位

            return message;
        }


        /// <summary>
        /// 【格式化输出，校验读取的数据】
        /// </summary>
        /// <param name="readBuffer"></param>
        /// <returns></returns>
        public string SetText(byte[] readBuffer)
        {
            //将byte 转换成string 用于显示
            //string readstr = string.Empty;
            if (readBuffer != null)
            {
                ushort crc = CRC16(readBuffer, 0, readBuffer.Length - 3);
                if (readBuffer[readBuffer.Length - 2] == WORD_HI(crc) && readBuffer[readBuffer.Length - 1] == WORD_LO(crc))//crc校验
                {
                    return ToHexString(readBuffer);
                }
                else
                {
                    return "CRC校验错误";
                }
            }

            return "程序出错";
        }


        /// <summary>
        /// 【CRC校验】
        /// </summary>
        /// <param name="buffer">命令帧合适前6字节</param>
        /// <param name="Sset">开始位</param>
        /// <param name="Eset">结束位</param>
        /// <returns>CRC校验码</returns>
        public static ushort CRC16(Byte[] buffer, int Sset, int Eset)
        {
            byte crcHi = 0xff;  // 高位初始化

            byte crcLo = 0xff;  // 低位初始化

            for (int i = Sset; i <= Eset; i++)
            {
                int crcIndex = crcHi ^ buffer[i]; //查找crc表值

                crcHi = (byte)(crcLo ^ _auchCRCHi[crcIndex]);
                crcLo = _auchCRCLo[crcIndex];
            }

            return (ushort)(crcHi << 8 | crcLo);
        }

        /// <summary>
        /// 【获取大写字母】
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToHexString(byte[] bytes) // 0xae00cf => "AE00CF "
        {
            string hexString = string.Empty;

            if (bytes != null)
            {

                StringBuilder strB = new StringBuilder();

                for (int i = 0; i < bytes.Length; i++)
                {

                    strB.Append(bytes[i].ToString("X2"));

                }

                hexString = strB.ToString();

            } return hexString;

        }

        //取Word变量的高位字节、低位字节
        /// <summary>
        /// 【获取低位字节】
        /// </summary>
        /// <param name="crcCLo"></param>
        /// <returns></returns>
        public static byte WORD_LO(ushort crcCLo)
        {
            crcCLo = (ushort)(crcCLo & 0X00FF);
            return (byte)crcCLo;
        }

        /// <summary>
        /// 【获取高位字节】
        /// </summary>
        /// <param name="crcHI"></param>
        /// <returns></returns>
        public static byte WORD_HI(ushort crcHI)
        {
            crcHI = (ushort)(crcHI >> 8 & 0X00FF);
            return (byte)crcHI;
        }
    }
}
