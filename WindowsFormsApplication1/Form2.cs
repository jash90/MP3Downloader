using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public String strona = "";
        public Form2(String s)
        {
            InitializeComponent();
            strona = s;
            textBox1.Text = s;
            webBrowser1.Navigate(s);
            webBrowser1.Refresh();
        }

    }
}
