using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NmapVisualizer
{
    public partial class NmapProcess : Form
    {

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
                else
                    progressBar1.Style = ProgressBarStyle.Continuous;

            }
        }

        private string Status
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        public bool CompletedScan { get; set; }
        private bool Expanded { get; set; }

        public string TempFile { get; set; }

        private ScanOptions options;

        public NmapProcess(ScanOptions options)
        {
            InitializeComponent();
            Expanded = false;
            this.options = options;
            CompletedScan = false;
            backgroundWorker1.ProgressChanged += backgroundWorker1_ProgressChanged;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result == null)
                CompletedScan = false;
            else
                CompletedScan = (bool)e.Result;
            
            Wait = false;
            if (CompletedScan)
                Status = "Completed scan successfully.";
            else
                Status = "Sorry, an error occurred which killed the scan.";

            if (!CompletedScan)
                button1.Text = "&Close";
            else
                Dispose();

        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label1.Text = (string)e.UserState;

            if (e.ProgressPercentage < 0)
                richTextBox1.ForeColor = Color.Red;
            else if (e.ProgressPercentage == 1)
                richTextBox1.ForeColor = Color.DarkGreen;

            richTextBox1.AppendText((string)e.UserState + Environment.NewLine);

            if (e.ProgressPercentage != 0)
                richTextBox1.ForeColor = Color.White;
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (Expanded)
                this.Height = 160;
            else
                this.Height = 400;

            Expanded = (!Expanded);
        }

        private void NmapProcess_Load(object sender, EventArgs e)
        {
            this.Height = 160;
            Wait = true;
            backgroundWorker1.RunWorkerAsync();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            backgroundWorker1.CancelAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = false;
            try
            {
                TempFile = Path.Combine(new string[] { Path.GetTempPath(), Guid.NewGuid().ToString() +".nmap" });

                backgroundWorker1.ReportProgress(0, "Scanning targets, please wait...");
                Process p = new Process();
                p.StartInfo.FileName = Properties.Settings.Default["nmap_path"].ToString();
                p.StartInfo.Arguments = String.Format("{0} --system-dns -oX \"{1}\" {2}", options.Arguments, TempFile, options.Targets);
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.RedirectStandardError = true;
                p.OutputDataReceived += p_OutputDataReceived;
                p.ErrorDataReceived += p_ErrorDataReceived;
                p.Start();
                p.BeginOutputReadLine();
                p.WaitForExit();

                backgroundWorker1.ReportProgress(0, "Completed scan successfully.");
                e.Result = true;
            }
            catch (Exception ex) 
            {
                backgroundWorker1.ReportProgress(-1, ex.Message);
            }
        }

        void p_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            backgroundWorker1.ReportProgress(-1, e.Data);
        }

        void p_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            backgroundWorker1.ReportProgress(1, e.Data);
        }
    }
}
