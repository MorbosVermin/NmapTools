using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NmapVisualizer
{
    public partial class OptionsDialog : Form
    {
        public OptionsDialog()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Multiselect = false;
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.Filter = "Nmap Executable (nmap.exe)|nmap.exe";
            DialogResult r = openFileDialog1.ShowDialog(this);
            if (r == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void OptionsDialog_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default["nmap_path"].ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["nmap_path"] = textBox1.Text;
            Properties.Settings.Default.Save();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NmapDownloader downloader = new NmapDownloader();
            downloader.ShowDialog(this);
            if (downloader.Successful)
            {
                textBox1.Text = downloader.InstallationPath;
            }
            else
            {
                MessageBox.Show(downloader.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
