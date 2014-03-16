using nmap_tools;
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
    public partial class HostDetails : Form
    {
        private class PortRow
        {
            public int Id { get; set; }
            public string Protocol { get; set; }
            public string Status { get; set; }
            public string Service { get; set; }

            public PortRow(Port port)
            {
                Id = port.Id;
                Protocol = port.Protocol;
                Status = port.State.Value;
                Service = (port.Service != null) ? port.Service.Name : "";
            }

        }

        private Host host;
        public Host Host 
        { 
            get { return host; }
            set
            {
                host = value;
                HostChanged = true;
                Text = host.ToString();
                this.portRows = new BindingList<PortRow>();
                foreach (Port port in host.Ports)
                {
                    portRows.Add(new PortRow(port));
                }

                cbAddresses.DataSource = host.Addresses;
                cbHostnames.DataSource = host.Hostnames;
                dgvPorts.DataSource = portRows;
                dgvPorts.Columns[0].Width = 40;    //Id
                dgvPorts.Columns[1].Width = 50;    //Protocol
                dgvPorts.Columns[2].Width = 50;    //Status
                dgvPorts.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; //Service

                if (host.DetectedOS())
                {
                    if (host.OS.Match.Name.Contains("Windows"))
                        pictureBox1.Image = Properties.Resources.windows_64x64;

                    else if (host.OS.Match.Name.Contains("Solaris"))
                        pictureBox1.Image = Properties.Resources.solaris;

                    else if (host.OS.Match.Name.Contains("rinter"))
                        pictureBox1.Image = Properties.Resources.printer;

                    else if (host.OS.Match.Name.Contains("Linux"))
                        pictureBox1.Image = Properties.Resources.linux;

                    else if (host.OS.Match.Name.Contains("AIX"))
                        pictureBox1.Image = Properties.Resources.aix;

                }
            }
        }

        public bool HostChanged { get; set; }
        private BindingList<PortRow> portRows;

        public HostDetails(Host host)
        {
            InitializeComponent();
            Host = host;
            HostChanged = false;
        }

        private void HostDetails_Load(object sender, EventArgs e)
        {
            Text = host.ToString();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Dispose();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ScanOptions options = new ScanOptions();
            options.Targets = String.Format("{0}/{1}", host.Addresses[0].ToString(), 32);
            options.ScanType = new ScanOptions.Option("-sT");
            options.EnableHostTimeout = false;
            foreach (ScanOptions.Option option in options.Options)
            {
                if (option.Value.Equals("-F"))
                    option.Selected = true;
                
            }

            NmapProcess process = new NmapProcess(options);
            process.ShowDialog(this);
            if (process.CompletedScan)
            {
                NmapRun scan = NmapRun.Parse(process.TempFile);
                Host = scan.Hosts[0];
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ScanOptions options = new ScanOptions();
            options.Targets = String.Format("{0}/{1}", host.Addresses[0].ToString(), 32);
            DialogResult r = options.ShowDialog(this);
            if (r != System.Windows.Forms.DialogResult.Cancel)
            {
                NmapProcess process = new NmapProcess(options);
                process.ShowDialog(this);
                if (process.CompletedScan)
                {
                    NmapRun scan = NmapRun.Parse(process.TempFile);
                    Host = scan.Hosts[0];
                }
            }

        }
    }
}
