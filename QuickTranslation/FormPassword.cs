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
    public partial class FormPassword : Form
    {
        public string Caption
        {
            get => txtCaption.Text;
            set => txtCaption.Text = value;
        }

        public string Password
        {
            get => txtPassword.Text;
            set => txtPassword.Text = value;
        }

        public FormPassword()
        {
            InitializeComponent();
        }
    }
}
