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
            DateTime dt = new DateTime();
            //修改文件
            do
            {
                string time = getTime();
                write(time);
                submit(time);
                updateDatetime();

                Process p = new Process();
                p.StartInfo.FileName = "runme.bat";
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.Start();
                p.WaitForExit();

               // Thread.Sleep(10);
                dt = dateTimePicker1.Value;



            } while (dt < new DateTime(2019, 3, 1) );
           


        }

        private void updateDatetime()
        {
            
            Random rand = new Random();
        
            DateTime oldTime = dateTimePicker1.Value;

            int randResult = 0;
            double aDay = 0;
            randResult = rand.Next(30);
            if (randResult == 1) {
                aDay = 3600 * 24 * 10;
            }
            else if (oldTime.DayOfWeek == DayOfWeek.Saturday)
            {
                aDay = 3600 * 24 * 2.5;
            }
            else if (oldTime.DayOfWeek == DayOfWeek.Sunday)
            {
                aDay = 3600 * 24 * 1.5;
            }
            else {
                aDay = 3600 * 24 * 1.2;
            }

            randResult = rand.Next(Convert.ToInt32(aDay));
            DateTime newValue = oldTime.AddSeconds(randResult);

            dateTimePicker1.Value = newValue;

        }

        private void submit(string time)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("cd ../../../../");
            sb.AppendLine("git add .");
            sb.AppendLine("git commit --date=\"" + time + " + 0800 \" -am \"提交" + time + "\"");
            //sb.AppendLine("pause");
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
