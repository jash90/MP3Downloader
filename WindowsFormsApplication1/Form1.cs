using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string p = "http://mp3.teledyski.info/index.html?text=";
        string k = "&file=global_test&name=eplik&&dalej=Szukaj";
        List<ListBox> listbox = new List<ListBox>();
        List<List<String>> linki = new List<List<string>>();
        List<int> index = new List<int>();
        List<int> imax = new List<int>();
        int indexlist = -1;
        private void button1_Click(object sender, EventArgs e)
        {
           indexlist++;
           TabPage tabpage1 = new TabPage();
           tabpage1.Text = textBox1.Text;
           tabControl1.TabPages.Add(tabpage1);
           listbox.Add(new ListBox());
           listbox[indexlist].Dock = DockStyle.Fill;
           listbox[indexlist].ScrollAlwaysVisible = true;
           tabpage1.Controls.Add(listbox[indexlist]);
           linki.Add(new List<String>());
           index.Add(new int());
           index[index.Count - 1]=0;
           tabControl1.SelectedTab = tabpage1;
           listbox[indexlist].SelectionMode = SelectionMode.One;
           this.listbox[indexlist].Click += new System.EventHandler(this.listbox_Click);
           tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
           Wyszukaniestrony(textBox1.Text,"");
           
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TabControl t = (TabControl)sender;
                if (t.SelectedIndex > -1)
                {
                    indexlist = t.SelectedIndex;
                    //indexlist--;
                    label1.Text = "Ilość piosenek wynosi = " + listbox[indexlist].Items.Count.ToString();
                }
                else
                {
                    label1.Text = "";
                    button4.Visible = false;
                    label2.Text = "";
                    toolStripStatusLabel1.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void Wyszukaniestrony(String wysz,String dodatki)
        {
            try
            {
                
                WebClient w = new WebClient();
                string page = w.DownloadString(p + zamiananaHTML(wysz) + k + dodatki);
                string name = "href=\"http://h+(.*?)\">+(.*?).mp3";
                foreach (Match match in Regex.Matches(page, name))
                {
                    String link ="http://www.h" + match.Groups[1].Value;
                    String nazwa =match.Groups[2].Value.Replace("<b style='color:black;background-color:#FFCC66'>", "").ToString().Replace("</b>", "").ToString();
                    listbox[indexlist].Items.Add(nazwa);
                    linki[indexlist].Add(link);
                }
                string name2 = "href=\"index.html+(.*?)\">+(.*?)";
                int max = 0;
                foreach (Match match2 in Regex.Matches(page, name2))
                {
                    String link = match2.Groups[1].Value;
                    if (link.Contains("&amp;file=global_test&amp;name=eplik&amp;dalej=Szukaj&amp;strona=") == true)
                    {
                        int pos = link.LastIndexOf("=");
                        link = link.Substring(pos+1);
                        if (int.Parse(link) > max)
                        {
                            max = int.Parse(link);
                        }
                       
                    }
                }
                imax.Add(max);
                toolStripStatusLabel1.Text = index[indexlist]+1.ToString() + " / " + imax[indexlist]+1.ToString();
                button4.Visible=true;
                label1.Text = "Ilość piosenek wynosi = " + listbox[indexlist].Items.Count.ToString();
                if (index[tabControl1.SelectedIndex] > 0)
                {
                    label2.Text = p + zamiananaHTML(wysz) + k + "&strona=" + index[tabControl1.SelectedIndex];
                }
                else
                {
                    label2.Text = p + zamiananaHTML(wysz) + k;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void listbox_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2(linki[tabControl1.SelectedIndex][listbox[tabControl1.SelectedIndex].SelectedIndex].ToString());
            f.Show();
        }
        private string zamiananaHTML(String s)
        {
            string wysz = "";
            for (int i = 0; i < s.Length; i++)
            {
                switch (s[i])
                {
                    case ' ': wysz += "%20"; break;
                    case '!': wysz += "%21"; break;
                    case '"': wysz += "%22"; break;
                    case '#': wysz += "%23"; break;
                    case '$': wysz += "%24"; break;
                    case '%': wysz += "%25"; break;
                    case '&': wysz += "%26"; break;
                    case (char)39: wysz += "%27"; break;
                    case '(': wysz += "%28"; break;
                    case ')': wysz += "%29"; break;
                    case '*': wysz += "%2A"; break;
                    case '+': wysz += "%2B"; break;
                    case ',': wysz += "%2C"; break;
                    case '-': wysz += "%2D"; break;
                    case '.': wysz += "%2E"; break;
                    case '/': wysz += "%2F"; break;
                    case ':': wysz += "%3A"; break;
                    case ';': wysz += "%3B"; break;
                    case '<': wysz += "%3C"; break;
                    case '=': wysz += "%3D"; break;
                    case '>': wysz += "%3E"; break;
                    case '?': wysz += "%3F"; break;
                    case '@': wysz += "%40"; break;
                    case '[': wysz += "%5B"; break;
                    case ']': wysz += "%5D"; break;
                    case '^': wysz += "%5E"; break;
                    case '_': wysz += "%5F"; break;
                    case '`': wysz += "%60"; break;
                    case '{': wysz += "%7B"; break;
                    case '|': wysz += "%7C"; break;
                    case '}': wysz += "%7D"; break;
                    case '~': wysz += "%7E"; break;
                    case 'Ą': wysz += "%A1"; break;
                    case 'Ł': wysz += "%A3"; break;
                    case 'Ś': wysz += "%A6"; break;
                    case 'Ź': wysz += "%AC"; break;
                    case 'Ż': wysz += "%AF"; break;
                    case 'ą': wysz += "%B1"; break;
                    case 'ł': wysz += "%B3"; break;
                    case 'ś': wysz += "%B6"; break;
                    case 'ź': wysz += "%BC"; break;
                    case 'ż': wysz += "%BF"; break;
                    case 'Ę': wysz += "%CA"; break;
                    case 'Ń': wysz += "%D1"; break;
                    case 'Ó': wysz += "%D3"; break;
                    case 'ę': wysz += "%EA"; break;
                    case 'ń': wysz += "%F1"; break;
                    case 'ó': wysz += "%F3"; break;
                    default: wysz += s[i]; break;
                }

            }
            return wysz;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.FileName);
                foreach (String s in linki[tabControl1.SelectedIndex])
                {
                    sw.WriteLine(s);
                }
                sw.Close();

            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            this.tabControl1.Width = this.Width-10;
            this.tabControl1.Height = this.Height - 141;
            this.button3.Location = new Point(this.button3.Location.X,this.Height - 86);
            this.button4.Location = new Point(this.button4.Location.X, this.Height - 86);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            indexlist--;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (index[tabControl1.SelectedIndex]-1 >= 0)
            {
                index[tabControl1.SelectedIndex]--;
                if (index[tabControl1.SelectedIndex] ==0)
                {
                    Wyszukaniestrony(tabControl1.SelectedTab.Text, "");
                }
                else
                {
                    Wyszukaniestrony(tabControl1.SelectedTab.Text, "&strona=" + index[tabControl1.SelectedIndex].ToString());
                }
            }
            if (index[tabControl1.SelectedIndex] == 0)
            {
                button3.Visible = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
                    index[tabControl1.SelectedIndex]++;
                    Wyszukaniestrony(tabControl1.SelectedTab.Text, "&strona=" + index[tabControl1.SelectedIndex].ToString());
                    if (index[tabControl1.SelectedIndex] > 0)
                    {
                        button3.Visible = true;
                    }
         
        }



        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                indexlist++;
                TabPage tabpage1 = new TabPage();
                tabpage1.Text = textBox1.Text;
                tabControl1.TabPages.Add(tabpage1);
                listbox.Add(new ListBox());
                listbox[indexlist].Dock = DockStyle.Fill;
                listbox[indexlist].ScrollAlwaysVisible = true;
                tabpage1.Controls.Add(listbox[indexlist]);
                linki.Add(new List<String>());
                index.Add(new int());
                index[index.Count - 1] = 0;
                tabControl1.SelectedTab = tabpage1;
                listbox[indexlist].SelectionMode = SelectionMode.One;
                this.listbox[indexlist].Click += new System.EventHandler(this.listbox_Click);
                tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
                Wyszukaniestrony(textBox1.Text,"");

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String line ="";
                StreamReader sr = new StreamReader(openFileDialog1.FileName);
                while (sr.EndOfStream != true)
                {
                    line = sr.ReadLine();
                    indexlist++;
                    TabPage tabpage1 = new TabPage();
                    tabpage1.Text = line;
                    tabControl1.TabPages.Add(tabpage1);
                    listbox.Add(new ListBox());
                    listbox[indexlist].Dock = DockStyle.Fill;
                    listbox[indexlist].ScrollAlwaysVisible = true;
                    tabpage1.Controls.Add(listbox[indexlist]);
                    linki.Add(new List<String>());
                    index.Add(new int());
                    index[index.Count - 1] = 0;
                    tabControl1.SelectedTab = tabpage1;
                    listbox[indexlist].SelectionMode = SelectionMode.One;
                    this.listbox[indexlist].Click += new System.EventHandler(this.listbox_Click);
                    tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
                    Wyszukaniestrony(line, "");
                }

            }
        }

       

      

     

     

       
    }
}
