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
            readInfoFromFile();
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
        private void hostInput_TextChanged(object sender, EventArgs e)
        {
            inputHostname = ((Control)sender).Text;
        }
        private void tokenInput_TextChanged(object sender, EventArgs e)
        {
            inputToken = ((Control)sender).Text;
        }
        private void fontSizeInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputFontSize = ((Control)sender).Text;
        }
        private void fontColorInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            inputFontColor = ((Control)sender).Text;
        }

        /**
         * 转换输入为正确格式
         */
        private string url = "";
        private int fontSize = 30;
        private Color fontColor = Color.Blue;
        private string getUrl()
        {
            if (inputHostname.Length < 8)
            {
                MessageBox.Show("请填写完整主机名！");
                return "";
            }
            if (inputHostname.Substring(0, 7) != "http://" && inputHostname.Substring(0, 8) != "https://")
            {
                MessageBox.Show("主机名请以http://或者https://开头！");
                return "";
            }
            if (inputHostname.Substring(inputHostname.Length - 1, 1) != "/") inputHostname += "/";
            return inputHostname + "getData.php?token=" + inputToken;
        }
        private int getFontSize()
        {
            if (inputFontSize == "大") return 40;
            else if (inputFontSize == "中") return 30;
            else if (inputFontSize == "小") return 20;
            return 30;
        }
        private Color getFontColor()
        {
            if (inputFontColor == "蓝色") return Color.Blue;
            else if (inputFontColor == "黄色") return Color.Yellow;
            else if (inputFontColor == "黑色") return Color.Black;
            else if (inputFontColor == "白色") return Color.White;
            return Color.Blue;
        }

        /**
         * 保存数据
         */
        private void saveDataButton_Click(object sender, EventArgs e)
        {
            url = getUrl();
            if (url == "") return;
            fontSize = getFontSize();
            fontColor = getFontColor();
            MessageBox.Show("保存成功！");
            initScreen();
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
            Screen.init(url, fontSize, fontColor);
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
