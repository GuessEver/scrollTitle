using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scrollTitle
{
    class Title : System.Windows.Forms.Form
    {
        public Title(string str, int fontSize, Color fontColor, Color fontBorderColor, int left, int top)
        {
            this.Paint += Title_Paint;
            return;
            this.AutoSize = true;
            this.Top = top;
            this.Left = left;
            this.Font = new Font("微软雅黑", fontSize, FontStyle.Bold);
            this.ForeColor = fontColor;
            this.BackColor = Color.Transparent;
            this.Text = str;
            
        }

        private void Title_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            string s = "宋体宋体宋体宋体宋体宋体宋体宋体宋体";
            RectangleF rect = new RectangleF(350, 0, 400, 200);
            Font font = this.Font;
            StringFormat format = StringFormat.GenericTypographic;
            float dpi = g.DpiY;
            using (GraphicsPath path = GetStringPath(s, dpi, rect, font, format))
            {
                //阴影代码
                //RectangleF off = rect;
                //off.Offset(5, 5);//阴影偏移
                //using (GraphicsPath offPath = GetStringPath(s, dpi, off, font, format))
                //{
                //    Brush b = new SolidBrush(Color.FromArgb(100, 0, 0, 0));
                //    g.FillPath(b, offPath);
                //    b.Dispose();
                //}
                g.SmoothingMode = SmoothingMode.AntiAlias;//设置字体质量
                g.DrawPath(Pens.Black, path);//绘制轮廓（描边）
                g.FillPath(Brushes.White, path);//填充轮廓（填充）
            }
        }
        GraphicsPath GetStringPath(string s, float dpi, RectangleF rect, Font font, StringFormat format)
        {
            GraphicsPath path = new GraphicsPath();
            // Convert font size into appropriate coordinates
            float emSize = dpi * font.SizeInPoints / 72;
            path.AddString(s, font.FontFamily, (int)font.Style, emSize, rect, format);

            return path;
        }
    }
}
