using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using nmap_tools;

namespace NmapVisualizer
{
    public partial class ReportPanel : UserControl
    {
        private bool Wait
        {
            get { return Application.UseWaitCursor; }
            set
            {
                Application.UseWaitCursor = value;
                if (value)
                    Application.DoEvents();

            }
        }

        private NmapRun scan;
        public NmapRun Scan
        {
            get { return this.scan; }
            set
            {
                scan = value;
                DisplayScan();
            }
        }

        public ReportPanel()
        {
            InitializeComponent();
            lvHosts.MouseDoubleClick += lvHosts_MouseDoubleClick;
        }

        void DisplayScan()
        {
            if (scan == null)
                return;

            lvHosts.Items.Clear();
                Wait = true;
                ScoreBoard portScores = new ScoreBoard();
                ScoreBoard osScores = new ScoreBoard();

                bool r = false;
                foreach(Host host in scan.Hosts)  
                {
                    if (!host.Status.State.Equals("up", StringComparison.CurrentCultureIgnoreCase))
                        continue;

                    int i = 5;
                    if (host.DetectedOS())
                    {
                        Console.WriteLine("Detected OS: {0}", host.OS.Match.Name);

                        if (host.OS.Match.Name.Contains("Windows"))
                            i = 4;
                        else if (host.OS.Match.Name.Contains("Solaris"))
                            i = 3;
                        else if (host.OS.Match.Name.Contains("rinter"))
                            i = 2;
                        else if (host.OS.Match.Name.Contains("Linux"))
                            i = 1;
                        else if (host.OS.Match.Name.Contains("AIX"))
                            i = 0;

                        osScores.Score(host.OS.Match.Name);
                    }
                    else
                        osScores.Score("Unknown");

                    string[] values = { host.Addresses[0].ToString(), ((host.DetectedOS()) ? host.OS.Match.Name : ""), host.Ports.Count + "" };
                    ListViewItem item = new ListViewItem(host.ToString(), i);
                    //item.BackColor = (r) ? Color.LightGray : Color.White;
                    item.SubItems.AddRange(values);
                    lvHosts.Items.Add(item);

                    foreach (Port port in host.Ports)
                    {
                        string key = (port.Service.Name.Length > 0) ? port.Service.Name : String.Format("{0}/{1}", port.Id, port.Protocol);
                        if(port.State.Value.Equals("open", StringComparison.CurrentCultureIgnoreCase))
                            portScores.Score(key);

                    }

                    r = !r;
                }

                Dictionary<string, int> sortedScores = 
                    portScores.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
                if (sortedScores.Count > 0)
                {
                    int i = sortedScores.Count - 1;
                    while(i >= 0)
                    {
                        string port = sortedScores.Keys.ToList()[i];
                        chartPorts.Series["Series1"].Points.AddXY(port, portScores[port]);
                        i--;

                        if (chartPorts.Series["Series1"].Points.Count == 5)
                            break;

                    }
                }


                if (osScores.Count > 0)
                {
                    foreach (string os in osScores.Keys)
                    {
                        chartOS.Series["Series1"].Points.AddXY(os, osScores[os]);
                    }
                }


                chartPorts.Series[0]["PieLabelStyle"] = "Disabled";
                chartOS.Series[0]["PieLabelStyle"] = "Disabled";

                Wait = false;
        }

        void lvHosts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = lvHosts.GetItemAt(e.X, e.Y);
            Host host = GetHost(item.Text);
            if (host != null)
            {
                HostDetails dets = new HostDetails(host);
                dets.ShowDialog(this);
                if (dets.HostChanged)
                {
                    scan.ReplaceHost(host, dets.Host);
                    DisplayScan();
                }
            }
        }

        private Host GetHost(string ip)
        {
            foreach (Host host in scan.Hosts)
            {
                if (host.ToString().Equals(ip))
                {
                    return host;
                }
            }

            return null;
        }

        public void SetView(View view)
        {
            lvHosts.View = view;
        }

        private void chartPorts_Click(object sender, EventArgs e)
        {
            //TODO Open dialog showing all ports and services found.
        }

        private void chartOS_Click(object sender, EventArgs e)
        {
            //TODO Open dialog showing all operatings systems found.
        }

        private void lvHosts_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
