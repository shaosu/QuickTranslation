using Himesyo.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickTranslation
{
    public partial class FormConfigApp : Form
    {
        public FormConfigApp()
        {
            InitializeComponent();
        }

        private void FormConfigApp_Load(object sender, EventArgs e)
        {
            chkShowOriginal.Checked = AppMain.Config.ShowOriginal;
        }

        private void FormConfigApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AppMain.Config.ShowOriginal != chkShowOriginal.Checked)
            {
                AppMain.Config.ShowOriginal = chkShowOriginal.Checked;
                AppMain.SaveConfig();
            }
            
        }
    }
}
