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
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
        }
        
        private void Screen_Load(object sender, EventArgs e)
        {
            this.Paint += Screen_Paint;
        }

        Bitmap bmp;// = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
        void Screen_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                //Graphics g = this.CreateGraphics();
                Graphics g = Graphics.FromImage(bmp);
                g.Clear(this.BackColor);
                if (this.textRenderAntiAliasGridFit) // 画质优先
                {
                    // 抗锯齿效果，占用更多cpu资源
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                }
                SolidBrush fontBrush = new SolidBrush(this.fontColor);
                SolidBrush fontBorderBrush = new SolidBrush(this.fontBorderColor);
                //foreach (Title title in titles)
                for (int i = 0; i < titles.Count; i++)
                {
                    Title title = (Title)titles[i];
                    PointF point = new PointF(title.left, title.top);

                    point.X -= 1; // 绘制左背景文字
                    g.DrawString(title.text, title.font, fontBorderBrush, point);

                    point.X += 2; // 绘制右背景文字
                    g.DrawString(title.text, title.font, fontBorderBrush, point);

                    point.X -= 1; point.Y -= 1; // 绘制上背景文字
                    g.DrawString(title.text, title.font, fontBorderBrush, point);

                    point.Y += 2; // 绘制下背景文字
                    g.DrawString(title.text, title.font, fontBorderBrush, point);

                    point.Y -= 1; // 绘制前景文字
                    g.DrawString(title.text, title.font, fontBrush, point);

                }
                e.Graphics.DrawImage(bmp, 0, 0);
                g.Dispose();
            }
            catch { }
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
        private void BringToFrontTimer_Tick(object sender, EventArgs e)
        {
            this.TopMost = true;
        }

        /**
         * 从服务器获取数据并存入队列
         */
        private void FetchDataTimer_Tick(object sender, EventArgs e)
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
            Title title = new Title(str, this.fontSize, this.fontColor, this.fontBorderColor, this.Width, random.Next(0, this.Height * 3 / 5), random.Next(this.speed, 10 + this.speed));
            titles.Add(title);
        }
        private void ShootDataTimer_Tick(object sender, EventArgs e)
        {
            if (this.titles.Count > this.maxAmount) return;
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
        private void MoveDataTimer_Tick(object sender, EventArgs e)
        {
            //foreach (Title title in titles)
            for (int i = 0; i < titles.Count; i++)
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
            this.fetchDataTimer.Tick += FetchDataTimer_Tick;
            this.fetchDataTimer.Enabled = true;

            this.shootDataTimer = new Timer();
            this.shootDataTimer.Interval = 500;
            this.shootDataTimer.Tick += ShootDataTimer_Tick; ;
            this.shootDataTimer.Enabled = true;

            this.moveDataTimer = new Timer();
            this.moveDataTimer.Interval = 20;
            this.moveDataTimer.Tick += MoveDataTimer_Tick;
            this.moveDataTimer.Enabled = true;
        }

        private string api = "";
        private int fontSize;
        private Color fontColor;
        private Color fontBorderColor;
        private int speed;
        private int maxAmount;
        private bool textRenderAntiAliasGridFit;
        public void init(string url, int size, Color color, Color borderColor, int speed, int maxAmount, bool textRenderAntiAliasGridFit)
        {
            initScreen();
            bmp = new Bitmap(this.Width, this.Height);
            this.api = url;
            this.fontSize = size;
            this.fontColor = color;
            this.fontBorderColor = borderColor;
            this.speed = speed;
            this.maxAmount = maxAmount;
            this.textRenderAntiAliasGridFit = textRenderAntiAliasGridFit;
            initData();
        }

        /**
         * 清屏
         */
        public void clearScreen()
        {
            titles.Clear();
            this.Invalidate();
        }
    }
}
