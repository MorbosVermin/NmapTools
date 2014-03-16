using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NmapVisualizer
{
    public partial class NmapDownloader : Form
    {

        private class Progress
        {
            public string Status { get; set; }
            public int Value { get; set; }

            public Progress(string s, int v)
            {
                Status = s;
                Value = v;
            }
        }

        private bool Wait
        {
            get { return Application.UseWaitCursor;  }
            set
            {
                Application.UseWaitCursor = value;
                if (value)
                {
                    Application.DoEvents();
                    progressBar1.Style = ProgressBarStyle.Marquee;
                }
            }
        }

        public string InstallationPath { get { return label3.Text;  } }
        public bool Successful { get; set; }
        public string ErrorMessage { get; set; }

        public NmapDownloader()
        {
            Successful = false;
            ErrorMessage = "";
            InitializeComponent();
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Wait = false;
            Successful = (bool)e.Result;
            Dispose();
        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label1.Text = (string)e.UserState;
            if (e.ProgressPercentage == 0)
            {
                label1.ForeColor = Color.Red;
                ErrorMessage = (string)e.UserState;
                return;
            }

            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Value = e.ProgressPercentage;

            if (e.ProgressPercentage == 70)
            {
                label3.ForeColor = Color.DarkGray;
                linkLabel1.Enabled = false;
            }
        }

        private void NmapDownloader_Load(object sender, EventArgs e)
        {
            string name = Properties.Settings.Default["download_folder"].ToString();
            label3.Text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), name);
            
            Wait = true;
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = false;
            string url = Properties.Settings.Default["download_url"].ToString();
            string tmpFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() +".zip");
            using (WebClient client = new WebClient())
            {
                try
                {
                    string name = Properties.Settings.Default["download_name"].ToString();
                    backgroundWorker1.ReportProgress(10, String.Format("Downloading {0}, please wait...", name));
                    client.DownloadFile(url, tmpFile);

                    backgroundWorker1.ReportProgress(30, "Completed download, checking integrity...");
                    StringBuilder buff = new StringBuilder();
                    SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
                    byte[] data = File.ReadAllBytes(tmpFile);
                    string hash = BitConverter.ToString(sha1.ComputeHash(data)).Replace("-", "");
                    if (hash.Equals(Properties.Settings.Default["download_hash"].ToString()))
                    {
                        backgroundWorker1.ReportProgress(70, "Download successful. Extracting archive, please wait...");
                        using (ZipArchive zip = ZipFile.OpenRead(tmpFile))
                        {
                            string path = label3.Text;
                            zip.ExtractToDirectory(Path.GetTempPath());
                            Directory.Move(Path.Combine(Path.GetTempPath(), Properties.Settings.Default["download_folder"].ToString()), path);
                        }
                    }
                    else
                    {
                        backgroundWorker1.ReportProgress(0, "Could not verify integrity of downloaded file(s). Please try again.");
                    }

                    backgroundWorker1.ReportProgress(100, "Installation successful.");
                    e.Result = true;
                }
                catch (Exception ex) 
                {
                    backgroundWorker1.ReportProgress(0, String.Format("An error occurred: {0} {1}", ex.GetType().Name, ex.Message));
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            folderBrowserDialog1.RootFolder = Environment.SpecialFolder.MyDocuments;
            DialogResult r = folderBrowserDialog1.ShowDialog(this);
            if (r == System.Windows.Forms.DialogResult.OK)
                label3.Text = folderBrowserDialog1.SelectedPath;

        }
    }
}
