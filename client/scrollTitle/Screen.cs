using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
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
            this.Paint += Screen_Paint;
        }
        
        void Screen_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            //foreach (Title title in titles)
            for (int i = 0; i < titles.Count; i++)
            {
                Title title = (Title)titles[i];
                SolidBrush brush = new SolidBrush(title.fontColor);
                g.DrawString(title.text, title.font, brush, new PointF(title.left, title.top));
            }
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
            this.bringToFront();
            this.resizeToFullScreen();
            this.tipLabel.Dispose();
        }

        /**
         * 从服务器获取数据并存入队列
         */
        private void fetchData(object source, EventArgs e)
        {
            this.fetchDataTimer.Stop();
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
                    this.data.Enqueue(item);
                }
            }
            catch { }
            this.fetchDataTimer.Start();
        }

        /**
         * 发射弹幕
         */
        Random random = new Random();
        ArrayList titles = new ArrayList();
        private void shoot(string str)
        {
            Title title = new Title(str, this.fontSize, this.fontColor, this.fontBorderColor, this.Width - 100, random.Next(0, this.Height * 2 / 5), random.Next(3, 9));
            titles.Add(title);
        }
        private void shootData(object source, EventArgs e)
        {
            if (this.Controls.Count > 10) return;
            try
            {
                string content = this.data.Dequeue();
                this.shoot(content);
            }
            catch { }
        }

        /**
         * 移动弹幕
         * 若移除边界则销毁
         */
        private void moveData(Object source, EventArgs e)
        {
            //foreach (Title title in titles)
            for(int i = 0; i < titles.Count; i++)
            {
                Title title = (Title)titles[i];
                title.left -= title.speed;
                if (title.left + title.width < 0)
                {
                    titles.Remove(title);
                }
            }
            this.Invalidate();
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

            this.fetchDataTimer = new Timer();
            this.fetchDataTimer.Interval = 3000;
            this.fetchDataTimer.Tick += new EventHandler(fetchData);
            this.fetchDataTimer.Enabled = true;

            this.shootDataTimer = new Timer();
            this.shootDataTimer.Interval = 500;
            this.shootDataTimer.Tick += new EventHandler(shootData);
            this.shootDataTimer.Enabled = true;

            this.moveDataTimer = new Timer();
            this.moveDataTimer.Interval = 50;
            this.moveDataTimer.Tick += new EventHandler(moveData);
            this.moveDataTimer.Enabled = true;
        }

        private string api = "";
        private int fontSize;
        private Color fontColor;
        private Color fontBorderColor;
        public void init(string url, int size, Color color, Color borderColor)
        {
            initScreen();
            this.api = url;
            this.fontSize = size;
            this.fontColor = color;
            this.fontBorderColor = borderColor;
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
