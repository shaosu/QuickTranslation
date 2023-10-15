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
            chkOutputOriginal.Checked = AppMain.Config.OutputOriginal;
        }

        private void FormConfigApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AppMain.Config.OutputOriginal != chkOutputOriginal.Checked)
            {
                AppMain.Config.OutputOriginal = chkOutputOriginal.Checked;
                AppMain.SaveConfig();
            }
            
        }
    }
}
