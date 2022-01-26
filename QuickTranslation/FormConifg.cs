using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickTranslation
{
    public partial class FormConifg : Form
    {
        public BaiduTranslator Translator { get; set; }

        public FormConifg()
        {
            InitializeComponent();
        }

        private void FormConifg_Load(object sender, EventArgs e)
        {
            if (Translator == null)
            {
                Translator = new BaiduTranslator();
            }
            txtAppid.Text = Translator.AppID;
            txtSecretKey.Text = string.Empty;
            txtInterval.Text = Translator.Interval.ToString();
            if (string.IsNullOrWhiteSpace(Translator.AppID))
            {
                button1.Text = "清空";
            }
            else
            {
                txtAppid.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "清空";
            txtAppid.Enabled = true;
            txtAppid.Text = string.Empty;
            txtSecretKey.Text = string.Empty;
            txtInterval.Text = "1200";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BaiduTranslator translator = new BaiduTranslator();
            translator.AppID = txtAppid.Text?.Trim();
            translator.SecretKey = txtSecretKey.Text?.Trim();
            translator.Interval = int.TryParse(txtInterval.Text?.Trim(), out int result) ? result : 1200;

            if (!txtAppid.Enabled
                && !string.IsNullOrWhiteSpace(Translator.Password)
                && translator.SecretKey != Translator.SecretKey)
            {
                FormPassword form = new FormPassword();
                form.Caption = $"验证当前密码可使用原 SecretKey ：\r\n\r\nAppID : {translator.AppID}";
                if (form.ShowDialog() == DialogResult.OK
                    && Translator.VerifyPassword(form.Password))
                {
                    translator.SecretKey = Translator.SecretKey;
                    translator.Save("baidu.translator", form.Password);
                    Translator = translator;
                    return;
                }
            }
            if (string.IsNullOrWhiteSpace(translator.AppID))
            {
                MessageBox.Show("AppID 不能为空。");
                txtAppid.Focus();
            }
            else
            {
                FormPassword form = new FormPassword();
                form.Caption = $"为其设置一个保护的密码：\r\n\r\n可为空，如果为空则不设密码。\r\n\r\nAppID : {translator.AppID}";
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string password = form.Password?.Trim();
                    translator.Save("baidu.translator", password);
                    Translator = translator;
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Rectangle rect = panel1.ClientRectangle;
            e.Graphics.DrawString(
                FormMain.GetPaintString(),
                new Font("楷体", 13.5f, FontStyle.Italic),
                Brushes.Fuchsia,
                rect);
        }

        private void link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sender is Control control
                && toolLink.GetToolTip(control) is string link
                && !string.IsNullOrWhiteSpace(link))
            {
                Process.Start(link);
            }
        }
    }
}
