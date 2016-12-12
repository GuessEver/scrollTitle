using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace scrollTitle
{
    class Title
    {
        public string text;
        public int speed, width, top, left;
        public Font font;
        public Color fontColor, fontBorderColor;
        public Title(string str, int fontSize, Color fontColor, Color fontBorderColor, int left, int top, int speed)
        {
            this.text = str;
            this.left = left;
            this.top = top;
            this.speed = speed;
            this.font = new Font("微软雅黑", fontSize, FontStyle.Bold);
            this.fontColor = fontColor;
            this.fontBorderColor = fontBorderColor;

            Label currentLabel = new Label();
            Graphics graphics = currentLabel.CreateGraphics();
            this.width = (int)graphics.MeasureString(str, this.font).Width;
        }
    }
}
