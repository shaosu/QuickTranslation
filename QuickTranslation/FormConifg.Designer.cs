
namespace QuickTranslation
{
    partial class FormConifg
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.linkAbout = new System.Windows.Forms.LinkLabel();
            this.linkFindXml = new System.Windows.Forms.LinkLabel();
            this.linkSee = new System.Windows.Forms.LinkLabel();
            this.linkOpen = new System.Windows.Forms.LinkLabel();
            this.linkRegister = new System.Windows.Forms.LinkLabel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.txtSecretKey = new System.Windows.Forms.TextBox();
            this.txtAppid = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInterval = new System.Windows.Forms.Label();
            this.lblSecretKey = new System.Windows.Forms.Label();
            this.lblAppid = new System.Windows.Forms.Label();
            this.toolLink = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.linkAbout);
            this.groupBox1.Controls.Add(this.linkFindXml);
            this.groupBox1.Controls.Add(this.linkSee);
            this.groupBox1.Controls.Add(this.linkOpen);
            this.groupBox1.Controls.Add(this.linkRegister);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtInterval);
            this.groupBox1.Controls.Add(this.txtSecretKey);
            this.groupBox1.Controls.Add(this.txtAppid);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.lblInterval);
            this.groupBox1.Controls.Add(this.lblSecretKey);
            this.groupBox1.Controls.Add(this.lblAppid);
            this.groupBox1.Location = new System.Drawing.Point(16, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(727, 473);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "翻译器";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(297, 368);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(417, 87);
            this.panel1.TabIndex = 11;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(16, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(698, 93);
            this.label2.TabIndex = 10;
            this.label2.Text = "此软件使用 百度翻译API 来进行翻译\r\n需要自行注册 百度翻译API 帐号.\r\n此软件为开源软件，且对保存的密钥加密保存。有关具体实现，请参阅源代码。\r\n声明：" +
    "此软件完全开源免费，\r\n如果您遇到任何人、网站、团队等收费才能使用，请举报。";
            // 
            // linkAbout
            // 
            this.linkAbout.AutoSize = true;
            this.linkAbout.Location = new System.Drawing.Point(27, 344);
            this.linkAbout.Name = "linkAbout";
            this.linkAbout.Size = new System.Drawing.Size(176, 16);
            this.linkAbout.TabIndex = 9;
            this.linkAbout.TabStop = true;
            this.linkAbout.Tag = "";
            this.linkAbout.Text = "关于 百度翻译开放平台";
            this.toolLink.SetToolTip(this.linkAbout, "https://fanyi-api.baidu.com/doc/11");
            this.linkAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_LinkClicked);
            // 
            // linkFindXml
            // 
            this.linkFindXml.AutoSize = true;
            this.linkFindXml.Location = new System.Drawing.Point(450, 344);
            this.linkFindXml.Name = "linkFindXml";
            this.linkFindXml.Size = new System.Drawing.Size(264, 16);
            this.linkFindXml.TabIndex = 9;
            this.linkFindXml.TabStop = true;
            this.linkFindXml.Tag = "";
            this.linkFindXml.Text = "从哪里可以找到注释文档的XML文件?";
            this.linkFindXml.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_LinkClicked);
            // 
            // linkSee
            // 
            this.linkSee.AutoSize = true;
            this.linkSee.Location = new System.Drawing.Point(27, 437);
            this.linkSee.Name = "linkSee";
            this.linkSee.Size = new System.Drawing.Size(224, 16);
            this.linkSee.TabIndex = 9;
            this.linkSee.TabStop = true;
            this.linkSee.Tag = "";
            this.linkSee.Text = "查看 百度翻译API 帐号和密钥";
            this.toolLink.SetToolTip(this.linkSee, "https://fanyi-api.baidu.com/manage/developer");
            this.linkSee.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_LinkClicked);
            // 
            // linkOpen
            // 
            this.linkOpen.AutoSize = true;
            this.linkOpen.Location = new System.Drawing.Point(27, 375);
            this.linkOpen.Name = "linkOpen";
            this.linkOpen.Size = new System.Drawing.Size(136, 16);
            this.linkOpen.TabIndex = 9;
            this.linkOpen.TabStop = true;
            this.linkOpen.Tag = "";
            this.linkOpen.Text = "开通通用翻译 API";
            this.toolLink.SetToolTip(this.linkOpen, "https://fanyi-api.baidu.com/product/11");
            this.linkOpen.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_LinkClicked);
            // 
            // linkRegister
            // 
            this.linkRegister.AutoSize = true;
            this.linkRegister.Location = new System.Drawing.Point(27, 406);
            this.linkRegister.Name = "linkRegister";
            this.linkRegister.Size = new System.Drawing.Size(208, 16);
            this.linkRegister.TabIndex = 9;
            this.linkRegister.TabStop = true;
            this.linkRegister.Tag = "";
            this.linkRegister.Text = "注册 百度翻译API 帐号文档";
            this.toolLink.SetToolTip(this.linkRegister, "https://fanyi-api.baidu.com/doc/12");
            this.linkRegister.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.link_LinkClicked);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(313, 276);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(109, 46);
            this.button2.TabIndex = 8;
            this.button2.Text = "保存";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(176, 276);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 46);
            this.button1.TabIndex = 7;
            this.button1.Text = "切换用户";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(123, 222);
            this.txtInterval.Margin = new System.Windows.Forms.Padding(4);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(115, 26);
            this.txtInterval.TabIndex = 5;
            this.txtInterval.Text = "1200";
            // 
            // txtSecretKey
            // 
            this.txtSecretKey.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSecretKey.Location = new System.Drawing.Point(123, 177);
            this.txtSecretKey.Margin = new System.Windows.Forms.Padding(4);
            this.txtSecretKey.Name = "txtSecretKey";
            this.txtSecretKey.Size = new System.Drawing.Size(591, 26);
            this.txtSecretKey.TabIndex = 3;
            this.txtSecretKey.UseSystemPasswordChar = true;
            // 
            // txtAppid
            // 
            this.txtAppid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAppid.Location = new System.Drawing.Point(123, 132);
            this.txtAppid.Margin = new System.Windows.Forms.Padding(4);
            this.txtAppid.Name = "txtAppid";
            this.txtAppid.Size = new System.Drawing.Size(591, 26);
            this.txtAppid.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(246, 225);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(384, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "*标准版的 QPS（每秒请求量）= 1，建议设为 1200。";
            // 
            // lblInterval
            // 
            this.lblInterval.AutoSize = true;
            this.lblInterval.Location = new System.Drawing.Point(27, 225);
            this.lblInterval.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblInterval.Name = "lblInterval";
            this.lblInterval.Size = new System.Drawing.Size(88, 16);
            this.lblInterval.TabIndex = 4;
            this.lblInterval.Text = "间隔(毫秒)";
            // 
            // lblSecretKey
            // 
            this.lblSecretKey.AutoSize = true;
            this.lblSecretKey.Location = new System.Drawing.Point(36, 181);
            this.lblSecretKey.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSecretKey.Name = "lblSecretKey";
            this.lblSecretKey.Size = new System.Drawing.Size(80, 16);
            this.lblSecretKey.TabIndex = 2;
            this.lblSecretKey.Text = "SecretKey";
            // 
            // lblAppid
            // 
            this.lblAppid.AutoSize = true;
            this.lblAppid.Location = new System.Drawing.Point(68, 136);
            this.lblAppid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAppid.Name = "lblAppid";
            this.lblAppid.Size = new System.Drawing.Size(48, 16);
            this.lblAppid.TabIndex = 0;
            this.lblAppid.Text = "AppID";
            // 
            // FormConifg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 502);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConifg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "配置";
            this.Load += new System.EventHandler(this.FormConifg_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblAppid;
        private System.Windows.Forms.TextBox txtAppid;
        private System.Windows.Forms.TextBox txtSecretKey;
        private System.Windows.Forms.Label lblSecretKey;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label lblInterval;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkRegister;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkAbout;
        private System.Windows.Forms.LinkLabel linkSee;
        private System.Windows.Forms.LinkLabel linkFindXml;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.LinkLabel linkOpen;
        private System.Windows.Forms.ToolTip toolLink;
    }
}