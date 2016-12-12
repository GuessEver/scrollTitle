using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace scrollTitle
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        /**
         * 程序启动，先从配置文件（info.txt）中读入配置
         */
        private void Settings_Load(object sender, EventArgs e)
        {
            this.readInfoFromFile();
        }

        /**
         * 从 info.txt 读取 hostname 和 token 
         */
        private void readInfoFromFile()
        {
            string filename = "info.txt";
            string fileConfig_hostname = "", fileConfig_token = "";
            try
            {
                StreamReader sr = new StreamReader(filename, Encoding.Default);
                fileConfig_hostname = sr.ReadLine();
                fileConfig_token = sr.ReadLine();
                this.hostInput.Text = fileConfig_hostname;
                this.tokenInput.Text = fileConfig_token;
            }
            catch
            {
                MessageBox.Show("配置文件读取错误，请手动输入");
            }
        }

        /**
         * 从输入框获取输入
         */
        private string inputHostname = "";
        private string inputToken = "";
        private string inputFontSize = "";
        private string inputFontColor = "";
        private string inputSpeed = "";
        private void hostInput_TextChanged(object sender, EventArgs e)
        {
            this.inputHostname = ((Control)sender).Text;
        }
        private void tokenInput_TextChanged(object sender, EventArgs e)
        {
            this.inputToken = ((Control)sender).Text;
        }
        private void fontSizeInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.inputFontSize = ((Control)sender).Text;
        }
        private void fontColorInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.inputFontColor = ((Control)sender).Text;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.inputSpeed = ((Control)sender).Text;
        }

        /**
         * 转换输入为正确格式
         */
        private string url = "";
        private int fontSize = 30;
        private Color fontColor = Color.Blue;
        private Color fontBorderColor = Color.White;
        private int speed = 7;
        private string getUrl()
        {
            if (this.inputHostname.Length < 8)
            {
                MessageBox.Show("请填写完整主机名！");
                return "";
            }
            if (this.inputHostname.Substring(0, 7) != "http://" && this.inputHostname.Substring(0, 8) != "https://")
            {
                MessageBox.Show("主机名请以http://或者https://开头！");
                return "";
            }
            if (this.inputHostname.Substring(this.inputHostname.Length - 1, 1) != "/") this.inputHostname += "/";
            return this.inputHostname + "getData.php?token=" + this.inputToken;
        }
        private int getFontSize()
        {
            if (this.inputFontSize == "大") return 40;
            else if (this.inputFontSize == "中") return 30;
            else if (this.inputFontSize == "小") return 20;
            return 30;
        }
        private Color getFontColor()
        {
            if (this.inputFontColor == "蓝色") return Color.Blue;
            else if (this.inputFontColor == "黄色") return Color.Yellow;
            else if (this.inputFontColor == "黑色") return Color.Black;
            else if (this.inputFontColor == "白色") return Color.White;
            return Color.Blue;
        }
        private int getSpeed()
        {
            if (this.inputSpeed == "慢速") return 3;
            else if (this.inputSpeed == "中速") return 7;
            else if (this.inputSpeed == "快速") return 11;
            return 7;
        }

        /**
         * 保存数据
         */
        private void saveDataButton_Click(object sender, EventArgs e)
        {
            url = getUrl();
            if (url == "") return;
            this.fontSize = this.getFontSize();
            this.fontColor = this.getFontColor();
            if (this.fontColor == Color.White)
            {
                this.fontBorderColor = Color.Black;
            }
            else
            {
                this.fontBorderColor = Color.White;
            }
            this.speed = this.getSpeed();
            MessageBox.Show("保存成功！");
            this.initScreen();
        }

        private Screen Screen;
        private void initScreen()
        {
            try
            {
                Screen.Close();
                Screen.Dispose();
            }
            catch
            {
            }
            Screen = new Screen();
            Screen.Show();
        }

        /**
         * 初始化弹幕
         */
        private void initScreenButton_Click(object sender, EventArgs e)
        {
            if (url == "")
            {
                MessageBox.Show("请先保存数据！");
                return;
            }
            Screen.init(this.url, this.fontSize, this.fontColor, this.fontBorderColor, this.speed);
        }

        /**
         * 弹幕清屏
         */
        private void clearScreenButton_Click(object sender, EventArgs e)
        {
            Screen.clearScreen();
        }

        /**
         * 关闭弹幕
         */
        private void closeScreenButton_Click(object sender, EventArgs e)
        {
            Screen.Close();
        }
    }
}
