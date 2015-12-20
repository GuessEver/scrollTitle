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
        string url = "";
        string hostname = "";
        string token = "";
        // 从服务器获取数据
        public void getData()
        {
            this.timer2.Enabled = false;
            try
            {
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
            }
            catch
            {
            }

            this.timer2.Interval = 3000;
            this.timer2.Enabled = true;
        }
        private void saveInfo(object sender, EventArgs e)
        {
            foreach (Control item in controlForm.Controls)
            { // 原谅我太傻比，只能这么写
                if (item.Name == "input_hostname")
                {
                    hostname = item.Text;
                }
                if (item.Name == "input_token")
                {
                    token = item.Text;
                }
            }
            if (hostname.Length < 8)
            {
                MessageBox.Show("请填写完整主机名！");
                return;
            }
            if (hostname.Substring(0, 7) != "http://" && hostname.Substring(0, 8) != "https://")
            {
                MessageBox.Show("主机名请以http://或者https://开头！");
                return;
            }
            if (hostname.Substring(hostname.Length - 1, 1) != "/") hostname += "/";
            url = hostname + "getData.php?token=" + token;
            MessageBox.Show("保存成功！");
        }
        // 初始化弹幕
        private void Form1_ready_to_shoot(object sender, EventArgs e)
        {
            if (url == "")
            {
                MessageBox.Show("请先保存数据");
                return;
            }
            // this.timer2.Interval = 500;
            // this.timer2.Enabled = true;
            getData();

            this.timer3.Interval = 500;
            this.timer3.Enabled = true;

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
            this.TransparencyKey = this.BackColor;
        }
        private void Form1_clearScreen(object sender, EventArgs e)
        {
            foreach (System.Windows.Forms.Control item in this.Controls)
            {
                if (item is System.Windows.Forms.Label)
                {
                    this.Controls.Remove(item);
                }
            }
        }
        private void Form1_finish(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.TransparencyKey = Color.White;
        }

        Form controlForm = new Form();
        // 创建一个控制窗口
        public void create_control_Form()
        {
            // Form1_ready_to_shoot();
            controlForm.Width = 350;
            controlForm.Show();
            controlForm.Height = 220;
            controlForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            controlForm.MaximizeBox = false;

            Label info = new Label();
            info.Width = controlForm.Width;
            info.Top = 10;
            info.Left = 10;
            info.AutoSize = true;
            info.Text = "如果您是扩展屏幕，请先把弹幕窗口拖到要放映的屏幕上\n\n输入数据 -> 保存数据 -> 初始化弹幕";
            controlForm.Controls.Add(info);

            Label label_hostname = new Label();
            label_hostname.Text = "主机名（如http://baidu.com/）：";
            label_hostname.Top = 60;
            label_hostname.Left = 100;
            label_hostname.AutoSize = true;
            controlForm.Controls.Add(label_hostname);
            TextBox input_hostname = new TextBox();
            input_hostname.Top = 80;
            input_hostname.Left = 100;
            input_hostname.Width = 200;
            input_hostname.Name = "input_hostname";
            input_hostname.Text = "";
            controlForm.Controls.Add(input_hostname);

            Label label_token = new Label();
            label_token.Text = "Token：";
            label_token.Top = 120;
            label_token.Left = 100;
            label_token.AutoSize = true;
            controlForm.Controls.Add(label_token);
            TextBox input_token = new TextBox();
            input_token.Top = 140;
            input_token.Left = 100;
            input_token.Width = 200;
            input_token.Name = "input_token";
            input_token.Text = "";
            controlForm.Controls.Add(input_token);


            Button infoBtn = new Button();
            infoBtn.Text = "保存数据";
            infoBtn.Top = 60;
            infoBtn.Left = 10;
            infoBtn.Click += new System.EventHandler(this.saveInfo);
            controlForm.Controls.Add(infoBtn);

            Button readyBtn = new Button();
            readyBtn.Text = "初始化弹幕";
            readyBtn.Top = 90;
            readyBtn.Left = 10;
            readyBtn.Click += new System.EventHandler(this.Form1_ready_to_shoot);
            controlForm.Controls.Add(readyBtn);

            Button clearScreenBtn = new Button();
            clearScreenBtn.Text = "清屏";
            clearScreenBtn.Top = 120;
            clearScreenBtn.Left = 10;
            clearScreenBtn.Click += new System.EventHandler(this.Form1_clearScreen);
            controlForm.Controls.Add(clearScreenBtn);

            Button closeBtn = new Button();
            closeBtn.Text = "关闭弹幕";
            closeBtn.Top = 150;
            closeBtn.Left = 10;
            closeBtn.Click += new System.EventHandler(this.Form1_finish);
            controlForm.Controls.Add(closeBtn);
            
            //controlForm.FormClosing += new FormClosingEventHandler(this.Form1_finish);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SetWindowPos(this.Handle, -1, 0, 0, 0, 0, 1 | 2); //最后参数也有用1 | 4　
            //SetPenetrate();
            this.timer1.Interval = 10;
            this.timer1.Enabled = true;

            create_control_Form();
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
