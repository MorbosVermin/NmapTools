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
    public partial class ScanOptions : Form
    {
        public class Option
        {
            public bool Selected { get; set; }
            public string Description { get; set; }
            public string Value { get; set; }

            public Option(string desc, string val, bool sel)
            {
                Description = desc;
                Value = val;
                Selected = sel;
            }

            public Option(string desc, string val) : this(desc, val, false) { }

            public Option(string value) : this("", value, false) { }

            public override string ToString()
            {
                return Description;
            }
        }

        public class TimeVariable
        {
            public string Name { get; set; }
            public string Value { get; set; }

            public TimeVariable(string name, string value)
            {
                Name = name;
                Value = value;
            }

            public override string ToString()
            {
                return Name;
            }
        }

        public bool UdpScan
        {
            get { return checkBox1.Checked; }
            set { checkBox1.Checked = value; }
        }

        private BindingList<Option> options;

        public BindingList<Option> Options
        {
            get { return options; }
            set { options = value; }
        }

        public string Arguments
        {
            get
            {
                StringBuilder options = new StringBuilder();
                options.Append(((Option)cbScanTypes.SelectedItem).Value +" ");

                if (UdpScan)
                    options.Append("-sU ");

                if (txtPorts.Enabled)
                    options.Append(String.Format("-p {0} ", txtPorts.Text));

                foreach (Option option in this.options)
                {
                    if (option.Selected)
                    {
                        //Cannot use fast mode when defining ports.
                        if (option.Value.Equals("-F") && txtPorts.Enabled)
                            continue;
                        //Cannot use 'no ICMP option' on a Ping sweep.
                        else if (option.Value.Equals("-Pn") && ((Option)cbScanTypes.SelectedItem).Value.Equals("-sn"))
                            continue;

                        options.Append(option.Value + " ");
                    }
                }

                if(EnableHostTimeout)
                    options.Append(String.Format("--host-timeout={0}{1}", numHostTimeout.Value, ((TimeVariable)cbTimeVars.SelectedItem).Value));
                
                return options.ToString();
            }
        }

        private BindingList<TimeVariable> timeVars;
        public BindingList<TimeVariable> TimeVariables { get { return timeVars; } }

        private BindingList<Option> scanTypes;
        public BindingList<Option> ScanTypes { get { return scanTypes; } }

        public Option ScanType
        {
            get { return (Option)cbScanTypes.SelectedItem; }
            set 
            {
                //TODO: I loath this. I must change it soon.
                Option v = value;
                int i = 0;
                foreach (Option o in cbScanTypes.Items)
                {
                    if (o.Value.Equals(v.Value))
                    {
                        cbScanTypes.SelectedIndex = i;
                        break;
                    }

                    i++;
                }
            }
        }

        public string Targets 
        { 
            get { return txtTargets.Text; }
            set { txtTargets.Text = value; }
        }

        public bool EnableHostTimeout 
        { 
            get { return checkBox3.Checked; }
            set { checkBox3.Checked = value; } 
        }

        public ScanOptions()
        {
            InitializeComponent();
            options = new BindingList<Option>();
            options.Add(new Option("OS Fingerprinting", "-O", true));
            options.Add(new Option("Do not use ICMP to determine status of hosts. Assume host is always up.", "-Pn"));
            options.Add(new Option("Service Version Detection", "-sV", true));
            options.Add(new Option("Run available LUA scripts on services found.", "-sC"));
            options.Add(new Option("Verbose Mode", "-v", true));
            options.Add(new Option("Fast Mode.", "-F"));
            options.Add(new Option("Randomize ports scanned.", "-r"));

            scanTypes = new BindingList<Option>();
            scanTypes.Add(new Option("TCP SYN Scan", "-sS"));
            scanTypes.Add(new Option("TCP Connect() Scan", "-sT"));
            scanTypes.Add(new Option("Ping Scan (disables port scanning)", "-sn"));

            timeVars = new BindingList<TimeVariable>();
            timeVars.Add(new TimeVariable("second(s)", "s"));
            timeVars.Add(new TimeVariable("minute(s)", "m"));
            timeVars.Add(new TimeVariable("Hour(s)", "h"));

            EnableHostTimeout = false;

            UdpScan = false;
            cbScanTypes.DataSource = scanTypes;
            cbScanTypes.SelectedIndex = 0;

            dataGridView1.DataSource = options;

            cbTimeVars.DataSource = timeVars;
            cbTimeVars.SelectedIndex = 0;

        }

        private void ScanOptions_Load(object sender, EventArgs e)
        {
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[1].Width = 250;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            string defTarget = "127.0.0.1";
            if (Properties.Settings.Default["last_target"].ToString().Length > 0)
                defTarget = Properties.Settings.Default["last_target"].ToString();

            txtTargets.Text = defTarget;
        }

        private void txtTargets_TextChanged(object sender, EventArgs e)
        {
            if (!txtTargets.Text.StartsWith("127."))
            {
                pbWarning.Visible = true;
                lblWarning.Visible = true;
            }
            else
            {
                pbWarning.Visible = false;
                lblWarning.Visible = false;
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default["last_target"] = txtTargets.Text;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            UdpScan = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            txtPorts.Enabled = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            EnableHostTimeout = checkBox3.Checked;
            numHostTimeout.Enabled = checkBox3.Checked;
            cbTimeVars.Enabled = checkBox3.Checked;
        }

        private void cbScanTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((Option)cbScanTypes.SelectedItem).Value.Equals("-sn"))
            {
                checkBox2.Enabled = false;
            }
            else
            {
                checkBox2.Enabled = true;
            }
        }
    }
}
