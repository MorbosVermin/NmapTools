using nmap_tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NmapVisualizer
{
    public partial class Form1 : Form
    {

        private bool Wait
        {
            get { return Application.UseWaitCursor; }
            set
            {
                Application.UseWaitCursor = value;
                if (value)
                {
                    Application.DoEvents();
                }
            }
        }

        public string ScanPath
        {
            get;
            set;
        }

        public Form1()
        {
            InitializeComponent();
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Wait = false;
            ProgressBar.Style = ProgressBarStyle.Continuous;
            ProgressBar.Value = 0;
            NmapRun scan = (NmapRun)e.Result;
            if (scan != null)
            {
                Status.Text = scan.RunStats.Finished.Summary;
                reportPanel1.Scan = scan;
            }
        }

        private void mnuFileScan_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default["nmap_path"].ToString().Length == 0)
            {
                OptionsDialog od = new OptionsDialog();
                DialogResult dr = od.ShowDialog(this);
                if (dr == System.Windows.Forms.DialogResult.Cancel)
                    return;

            }

            ScanOptions scanOptions = new ScanOptions();
            DialogResult r = scanOptions.ShowDialog(this);
            if (r != System.Windows.Forms.DialogResult.Cancel)
            {
                NmapProcess process = new NmapProcess(scanOptions);
                process.ShowDialog(this);
                if (process.CompletedScan)
                {
                    saveFileDialog1.Title = "Save Scan...";
                    saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    saveFileDialog1.FileName = scanOptions.Targets.Replace("/", "-").Replace(",", "_");
                    saveFileDialog1.Filter = "Nmap Scan Results (*.xml)|*.xml";
                    r = saveFileDialog1.ShowDialog(this);
                    if (r == System.Windows.Forms.DialogResult.OK)
                    {
                        if (File.Exists(saveFileDialog1.FileName))
                            File.Delete(saveFileDialog1.FileName);

                        File.Move(process.TempFile, saveFileDialog1.FileName);
                        ScanPath = saveFileDialog1.FileName;
                        backgroundWorker1.RunWorkerAsync(saveFileDialog1.FileName);
                    }

                }
                else
                    MessageBox.Show("The scan was incomplete. Please try again.", "What the...", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void mnuFileOpen_Click(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog1.Filter = "Nmap Scan Results (XML)|*.xml";
            openFileDialog1.FileName = "";
            openFileDialog1.Multiselect = false;
            DialogResult r = openFileDialog1.ShowDialog(this);
            if (r == System.Windows.Forms.DialogResult.OK)
            {
                Wait = true;
                Status.Text = String.Format("Parsing {0}", openFileDialog1.FileName);
                ProgressBar.Style = ProgressBarStyle.Marquee;
                ScanPath = openFileDialog1.FileName;
                backgroundWorker1.RunWorkerAsync(openFileDialog1.FileName);
            }
        }

        private void mnuFileExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            NmapRun scan = NmapRun.Parse((string)e.Argument);
            e.Result = scan;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            reportPanel1.SetView(View.LargeIcon);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            reportPanel1.SetView(View.Details);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            new OptionsDialog().ShowDialog(this);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (reportPanel1.Scan == null)
                return;

            if (!reportPanel1.Scan.Save(ScanPath))
            {
                MessageBox.Show(String.Format("Could not save Nmap scan to file {0}.", ScanPath), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (reportPanel1.Scan == null)
                return;

            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog1.FileName = "";
            saveFileDialog1.Filter = "Nmap Scan Results (*.xml)|*.xml";
            DialogResult r = saveFileDialog1.ShowDialog(this);
            if (r == System.Windows.Forms.DialogResult.OK)
            {
                if (!reportPanel1.Scan.Save(saveFileDialog1.FileName))
                {
                    MessageBox.Show(String.Format("Could not save Nmap scan to file {0}.", saveFileDialog1.FileName), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    ScanPath = saveFileDialog1.FileName;
                }
            }

        }
    }
}
