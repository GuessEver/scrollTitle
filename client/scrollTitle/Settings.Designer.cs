namespace scrollTitle
{
    partial class Settings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.saveDataButton = new System.Windows.Forms.Button();
            this.initScreenButton = new System.Windows.Forms.Button();
            this.clearScreenButton = new System.Windows.Forms.Button();
            this.closeScreenButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.hostInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tokenInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.fontSizeInput = new System.Windows.Forms.ComboBox();
            this.fontColorInput = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(305, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "如果您是扩展屏幕，请把弹幕窗口拖到需要放映的屏幕上";
            // 
            // saveDataButton
            // 
            this.saveDataButton.Location = new System.Drawing.Point(12, 46);
            this.saveDataButton.Name = "saveDataButton";
            this.saveDataButton.Size = new System.Drawing.Size(75, 23);
            this.saveDataButton.TabIndex = 1;
            this.saveDataButton.Text = "保存数据";
            this.saveDataButton.UseVisualStyleBackColor = true;
            this.saveDataButton.Click += new System.EventHandler(this.saveDataButton_Click);
            // 
            // initScreenButton
            // 
            this.initScreenButton.Location = new System.Drawing.Point(12, 75);
            this.initScreenButton.Name = "initScreenButton";
            this.initScreenButton.Size = new System.Drawing.Size(75, 23);
            this.initScreenButton.TabIndex = 2;
            this.initScreenButton.Text = "初始化弹幕";
            this.initScreenButton.UseVisualStyleBackColor = true;
            this.initScreenButton.Click += new System.EventHandler(this.initScreenButton_Click);
            // 
            // clearScreenButton
            // 
            this.clearScreenButton.Location = new System.Drawing.Point(12, 104);
            this.clearScreenButton.Name = "clearScreenButton";
            this.clearScreenButton.Size = new System.Drawing.Size(75, 23);
            this.clearScreenButton.TabIndex = 3;
            this.clearScreenButton.Text = "弹幕清屏";
            this.clearScreenButton.UseVisualStyleBackColor = true;
            this.clearScreenButton.Click += new System.EventHandler(this.clearScreenButton_Click);
            // 
            // closeScreenButton
            // 
            this.closeScreenButton.Location = new System.Drawing.Point(13, 134);
            this.closeScreenButton.Name = "closeScreenButton";
            this.closeScreenButton.Size = new System.Drawing.Size(75, 23);
            this.closeScreenButton.TabIndex = 4;
            this.closeScreenButton.Text = "关闭弹幕";
            this.closeScreenButton.UseVisualStyleBackColor = true;
            this.closeScreenButton.Click += new System.EventHandler(this.closeScreenButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "主机名（如http://baidu.com）";
            // 
            // hostInput
            // 
            this.hostInput.Location = new System.Drawing.Point(108, 61);
            this.hostInput.Name = "hostInput";
            this.hostInput.Size = new System.Drawing.Size(210, 21);
            this.hostInput.TabIndex = 6;
            this.hostInput.TextChanged += new System.EventHandler(this.hostInput_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(106, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "Token 密钥";
            // 
            // tokenInput
            // 
            this.tokenInput.Location = new System.Drawing.Point(108, 106);
            this.tokenInput.Name = "tokenInput";
            this.tokenInput.Size = new System.Drawing.Size(210, 21);
            this.tokenInput.TabIndex = 8;
            this.tokenInput.TextChanged += new System.EventHandler(this.tokenInput_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(106, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "字体";
            // 
            // fontSizeInput
            // 
            this.fontSizeInput.FormattingEnabled = true;
            this.fontSizeInput.Items.AddRange(new object[] {
            "大",
            "中",
            "小"});
            this.fontSizeInput.Location = new System.Drawing.Point(141, 136);
            this.fontSizeInput.Name = "fontSizeInput";
            this.fontSizeInput.Size = new System.Drawing.Size(57, 20);
            this.fontSizeInput.TabIndex = 10;
            this.fontSizeInput.Tag = "";
            this.fontSizeInput.Text = "中";
            this.fontSizeInput.SelectedIndexChanged += new System.EventHandler(this.fontSizeInput_SelectedIndexChanged);
            // 
            // fontColorInput
            // 
            this.fontColorInput.FormattingEnabled = true;
            this.fontColorInput.Items.AddRange(new object[] {
            "蓝色",
            "黄色",
            "白色",
            "黑色"});
            this.fontColorInput.Location = new System.Drawing.Point(204, 137);
            this.fontColorInput.Name = "fontColorInput";
            this.fontColorInput.Size = new System.Drawing.Size(59, 20);
            this.fontColorInput.TabIndex = 11;
            this.fontColorInput.Text = "蓝色";
            this.fontColorInput.SelectedIndexChanged += new System.EventHandler(this.fontColorInput_SelectedIndexChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "慢速",
            "中速",
            "快速"});
            this.comboBox1.Location = new System.Drawing.Point(270, 136);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(48, 20);
            this.comboBox1.TabIndex = 12;
            this.comboBox1.Text = "中速";
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 169);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.fontColorInput);
            this.Controls.Add(this.fontSizeInput);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tokenInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hostInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.closeScreenButton);
            this.Controls.Add(this.clearScreenButton);
            this.Controls.Add(this.initScreenButton);
            this.Controls.Add(this.saveDataButton);
            this.Controls.Add(this.label1);
            this.Name = "Settings";
            this.Text = "Win桌面弹幕客户端";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button saveDataButton;
        private System.Windows.Forms.Button initScreenButton;
        private System.Windows.Forms.Button clearScreenButton;
        private System.Windows.Forms.Button closeScreenButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox hostInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tokenInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox fontSizeInput;
        private System.Windows.Forms.ComboBox fontColorInput;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}