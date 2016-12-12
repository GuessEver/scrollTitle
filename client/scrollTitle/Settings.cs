﻿using System;
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
        private string inputMaxAmount = "";
        private string inputTextRenderPriority = "";
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
        private void speedInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.inputSpeed = ((Control)sender).Text;
        }
        private void maxAmountInput_TextChanged(object sender, EventArgs e)
        {
            this.inputMaxAmount = ((Control)sender).Text;
        }
        private void textRenderPriorityInput_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.inputTextRenderPriority = ((Control)sender).Text;
        }

        /**
         * 转换输入为正确格式
         */
        private string url = "";
        private int fontSize = 30;
        private Color fontColor = Color.Blue;
        private Color fontBorderColor = Color.White;
        private int speed = 7;
        private int maxAmount = 15;
        private bool textRenderAntiAliasGridFit = true;
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
            switch (this.inputFontSize)
            {
                case "大": return 40;
                case "小": return 20;
                case "中": default: return 30;
            }
        }
        private Color getFontColor()
        {
            switch (this.inputFontColor)
            {
                case "黄色": return Color.Yellow;
                case "黑色": return Color.Black;
                case "白色": return Color.White;
                case "蓝色": default: return Color.Blue;
            }
        }
        private Color getFontBorderColor()
        {
            switch (this.inputFontColor)
            {
                case "黄色": return Color.Red;
                case "黑色": return Color.White;
                case "白色": return Color.Black;
                case "蓝色": default: return Color.White;
            }
        }
        private int getSpeed()
        {
            switch (this.inputSpeed)
            {
                case "慢速": return 3;
                case "快速": return 11;
                case "中速": default: return 7;
            }
        }
        private int getMaxAmount()
        {
            try
            {
                return Int32.Parse(this.inputMaxAmount);
            }
            catch
            {
                return 15;
            }
        }
        private bool getTextRenderPriority()
        {
            if (this.inputTextRenderPriority == "画质优先") return true;
            return false;
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
            this.fontBorderColor = this.getFontBorderColor();
            this.speed = this.getSpeed();
            this.maxAmount = this.getMaxAmount();
            this.textRenderAntiAliasGridFit = this.getTextRenderPriority();
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
            Screen.init(this.url, this.fontSize, this.fontColor, this.fontBorderColor, this.speed, this.maxAmount, this.textRenderAntiAliasGridFit);
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
