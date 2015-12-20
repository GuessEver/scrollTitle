using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;


namespace scrollTitle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const uint WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int GWL_STYLE = (-16);
        private const int GWL_EXSTYLE = (-20);
        private const int LWA_ALPHA = 0;

        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(
        IntPtr hwnd,
        int nIndex,
        uint dwNewLong
        );

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(
        IntPtr hwnd,
        int nIndex
        );

        [DllImport("user32", EntryPoint = "SetLayeredWindowAttributes")]
        private static extern int SetLayeredWindowAttributes(
        IntPtr hwnd,
        int crKey,
        int bAlpha,
        int dwFlags
        );

        /// <summary> 
        /// 设置窗体具有鼠标穿透效果 
        /// </summary> 
        public void SetPenetrate()
        {
            GetWindowLong(this.Handle, GWL_EXSTYLE);
            SetWindowLong(this.Handle, GWL_EXSTYLE, WS_EX_TRANSPARENT | WS_EX_LAYERED);
            SetLayeredWindowAttributes(this.Handle, 0, 100, LWA_ALPHA);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int y, int Width, int Height, int flags);
        /// <summary>
        /// 得到当前活动的窗口
        /// </summary>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern System.IntPtr GetForegroundWindow();


        Random random = new Random();
        // 输出文字
        public void shoot(string str)
        {
            Label currentLabel = new Label();
            //currentLabel.Height = 100;
            //currentLabel.Width = 9999;
            currentLabel.AutoSize = true;
            currentLabel.Top = random.Next(0, 200);
            currentLabel.Left = this.Width - 100;
            currentLabel.Font = new Font("微软雅黑", 20, FontStyle.Bold);
            currentLabel.ForeColor = Color.Blue;
            //currentLabel.Image = Image.FromFile("transparentBackground.png");
            currentLabel.BackColor = Color.Transparent;
            currentLabel.Tag = random.Next(3, 10);
            currentLabel.Text = str;
            this.Controls.Add(currentLabel);
        }
        
        Queue<string> data = new Queue<string>();
        // 从队列发射弹幕
        public void shootData()
        {
            try
            {
                string content = data.Dequeue();
                shoot(content);
            }
            catch
            {
            }
        }

        // 从服务器获取数据
        public void getData()
        {
            this.timer2.Enabled = false;

            string url = "http://192.168.1.108/newyear/data.txt";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream ResStream = response.GetResponseStream();
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            StreamReader streamReader = new StreamReader(ResStream, encoding);
            string dataStr = streamReader.ReadToEnd();
            string[] items = dataStr.Split('\n');
            foreach (string item in items)
            {
                data.Enqueue(item);
            }

            this.timer2.Interval = 3000;
            this.timer2.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetWindowPos(this.Handle, -1, 0, 0, 0, 0, 1 | 2); //最后参数也有用1 | 4　
            //SetPenetrate();
            this.timer1.Interval = 10;
            this.timer1.Enabled = true;
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TransparencyKey = this.BackColor;

            getData();
            this.timer3.Interval = 500;
            this.timer3.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.Control item in this.Controls)
            {
                if (item is System.Windows.Forms.Label)
                {
                    item.Left = item.Left - (int)item.Tag;
                    if (item.Left + item.Width < 0)
                    {
                        this.Controls.Remove(item);
                    }
                }
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            getData();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            shootData();
        }
        
    }
}
