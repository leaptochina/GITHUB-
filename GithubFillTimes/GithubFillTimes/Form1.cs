using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GithubFillTimes
{
    public partial class Form1 : Form
    {
        public Form1()
        {
       
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //修改文件
            do
            {
                string time = getTime();
                write(time);
                submit(time);
                updateDatetime();

                Process p = new Process();
                p.StartInfo.FileName = "runme.bat";
                p.Start();
                p.WaitForExit();

                Thread.Sleep(300);
                break;


            } while (true);
           


        }

        private void updateDatetime()
        {
            int aDay = 3600 * 12 * 3;
            Random rand = new Random();
            int randResult = rand.Next(aDay);
            DateTime oldTime = dateTimePicker1.Value;
            DateTime newValue = oldTime.AddSeconds(randResult);

            dateTimePicker1.Value = newValue;

        }

        private void submit(string time)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("cd ../../../../");
            sb.AppendLine("git add .");
            sb.AppendLine("git commit --date=\"" + time + " + 0800 \" -am \"提交" + time + "\"");
            sb.AppendLine("pause");
            writeBat(sb.ToString());
        }

        public string getTime() {
            
            string time = dateTimePicker1.Value.ToString("MMM dd HH:mm:ss yyyy");

            return time;

        }

        public void writeBat(string detail)
        {
            FileStream fs = new FileStream("runme.bat", FileMode.Create);
            //获得字节数组
            byte[] data = System.Text.Encoding.Default.GetBytes(detail + "\r\n");
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }

        public void write(string detail)
        {
            FileStream fs = new FileStream("被修改的文件.txt", FileMode.Append);
            //获得字节数组
            byte[] data = System.Text.Encoding.Default.GetBytes(detail + "\r\n");
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
