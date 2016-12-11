using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scrollTitle
{
    public partial class Screen : Form
    {
        public Screen()
        {
            InitializeComponent();
        }

        private void Screen_Load(object sender, EventArgs e)
        {
        }
        

        private void bringToFront()
        {
            this.TopMost = true;
        }
        private void resizeToFullScreen()
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;
        }
        private void initScreen()
        {
            this.TransparencyKey = this.BackColor;
            bringToFront();
            resizeToFullScreen();
            this.tipLabel.Dispose();
        }

        /**
         * 从服务器获取数据并存入队列
         */
        private void fetchData(object source, EventArgs e)
        {
            fetchDataTimer.Enabled = false;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(api);
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
            fetchDataTimer.Enabled = true;
        }

        /**
         * 发射弹幕
         */
        Random random = new Random();
        private void shoot(string str)
        {
            Label currentLabel = new Label();
            //currentLabel.Height = 100;
            //currentLabel.Width = 9999;
            currentLabel.AutoSize = true;
            currentLabel.Top = random.Next(0, this.Height / 2);
            currentLabel.Left = this.Width - 100;
            currentLabel.Font = new Font("微软雅黑", fontSize, FontStyle.Bold);
            currentLabel.ForeColor = fontColor;
            currentLabel.BackColor = Color.Transparent;
            currentLabel.Tag = random.Next(3, 9);
            currentLabel.Text = str;
            this.Controls.Add(currentLabel);
        }
        private void shootData(object source, EventArgs e)
        {
            if (this.Controls.Count > 20) return;
            try
            {
                string content = data.Dequeue();
                shoot(content);
            }
            catch
            {
            }
        }

        /**
         * 移动弹幕
         * 若移除边界则销毁
         */
        private void moveData(Object source, EventArgs e)
        {
            foreach (Control item in this.Controls)
            {
                if (item is Label)
                {
                    item.Left = item.Left - (int)item.Tag;
                    if (item.Left + item.Width < 0)
                    {
                        this.Controls.Remove(item);
                    }
                }
            }
        }

        /**
         * 初始化数据
         */
        Timer fetchDataTimer;
        Timer shootDataTimer;
        Timer moveDataTimer;
        Queue<string> data;
        private void initData()
        {
            data = new Queue<string>();
            
            fetchDataTimer = new Timer();
            fetchDataTimer.Interval = 3000;
            fetchDataTimer.Tick += new EventHandler(fetchData);
            fetchDataTimer.Enabled = true;
            
            shootDataTimer = new Timer();
            shootDataTimer.Interval = 500;
            shootDataTimer.Tick += new EventHandler(shootData);
            shootDataTimer.Enabled = true;

            moveDataTimer = new Timer();
            moveDataTimer.Interval = 10;
            moveDataTimer.Tick += new EventHandler(moveData);
            moveDataTimer.Enabled = true;
        }

        private string api = "";
        private int fontSize;
        private Color fontColor;
        public void init(string url, int size, Color color)
        {
            initScreen();
            api = url; fontSize = size; fontColor = color;
            initData();
        }

        /**
         * 清屏
         */
        public void clearScreen()
        {
            foreach (Control item in this.Controls)
            {
                if (item is Label)
                {
                    this.Controls.Remove(item);
                }
            }
        }
    }
}
