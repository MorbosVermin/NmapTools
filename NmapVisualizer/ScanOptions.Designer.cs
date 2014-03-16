namespace NmapVisualizer
{
    partial class ScanOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanOptions));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblWarning = new System.Windows.Forms.Label();
            this.pbWarning = new System.Windows.Forms.PictureBox();
            this.txtTargets = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPorts = new System.Windows.Forms.TextBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cbTimeVars = new System.Windows.Forms.ComboBox();
            this.numHostTimeout = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cbScanTypes = new System.Windows.Forms.ComboBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWarning)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHostTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblWarning);
            this.groupBox1.Controls.Add(this.pbWarning);
            this.groupBox1.Controls.Add(this.txtTargets);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(347, 65);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Target(s) ";
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.Location = new System.Drawing.Point(30, 46);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(266, 13);
            this.lblWarning.TabIndex = 2;
            this.lblWarning.Text = "Warning, ensure you have permission before scanning.";
            this.lblWarning.Visible = false;
            // 
            // pbWarning
            // 
            this.pbWarning.Image = global::NmapVisualizer.Properties.Resources.error;
            this.pbWarning.Location = new System.Drawing.Point(15, 42);
            this.pbWarning.Name = "pbWarning";
            this.pbWarning.Size = new System.Drawing.Size(16, 16);
            this.pbWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbWarning.TabIndex = 1;
            this.pbWarning.TabStop = false;
            this.pbWarning.Visible = false;
            // 
            // txtTargets
            // 
            this.txtTargets.Location = new System.Drawing.Point(7, 20);
            this.txtTargets.Name = "txtTargets";
            this.txtTargets.Size = new System.Drawing.Size(334, 20);
            this.txtTargets.TabIndex = 0;
            this.toolTip1.SetToolTip(this.txtTargets, resources.GetString("txtTargets.ToolTip"));
            this.txtTargets.TextChanged += new System.EventHandler(this.txtTargets_TextChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.checkBox3);
            this.groupBox2.Controls.Add(this.cbScanTypes);
            this.groupBox2.Controls.Add(this.txtPorts);
            this.groupBox2.Controls.Add(this.checkBox2);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.cbTimeVars);
            this.groupBox2.Controls.Add(this.numHostTimeout);
            this.groupBox2.Location = new System.Drawing.Point(13, 85);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(347, 199);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " Options ";
            // 
            // txtPorts
            // 
            this.txtPorts.Enabled = false;
            this.txtPorts.Location = new System.Drawing.Point(226, 37);
            this.txtPorts.Name = "txtPorts";
            this.txtPorts.Size = new System.Drawing.Size(100, 20);
            this.txtPorts.TabIndex = 9;
            this.toolTip1.SetToolTip(this.txtPorts, resources.GetString("txtPorts.ToolTip"));
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(169, 40);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(59, 17);
            this.checkBox2.TabIndex = 8;
            this.checkBox2.Text = "Port(s):";
            this.toolTip1.SetToolTip(this.checkBox2, "Only scan specified ports. (-p)");
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.Location = new System.Drawing.Point(10, 63);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(330, 95);
            this.dataGridView1.TabIndex = 7;
            this.toolTip1.SetToolTip(this.dataGridView1, "Additional options can be selected here.");
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(169, 20);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(102, 17);
            this.checkBox1.TabIndex = 6;
            this.checkBox1.Text = "UDP Scan (-sU)";
            this.toolTip1.SetToolTip(this.checkBox1, resources.GetString("checkBox1.ToolTip"));
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // cbTimeVars
            // 
            this.cbTimeVars.Enabled = false;
            this.cbTimeVars.FormattingEnabled = true;
            this.cbTimeVars.Location = new System.Drawing.Point(265, 165);
            this.cbTimeVars.Name = "cbTimeVars";
            this.cbTimeVars.Size = new System.Drawing.Size(75, 21);
            this.cbTimeVars.TabIndex = 3;
            this.toolTip1.SetToolTip(this.cbTimeVars, "Select the unit of time to be used for the timeout.");
            // 
            // numHostTimeout
            // 
            this.numHostTimeout.Enabled = false;
            this.numHostTimeout.Location = new System.Drawing.Point(164, 165);
            this.numHostTimeout.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numHostTimeout.Name = "numHostTimeout";
            this.numHostTimeout.Size = new System.Drawing.Size(95, 20);
            this.numHostTimeout.TabIndex = 2;
            this.numHostTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.toolTip1.SetToolTip(this.numHostTimeout, "Numeric timeout value. Use this to adjust wait times for hosts to response potent" +
        "ially speeding up scans at the cost of host/service detection. Especially useful" +
        " during UDP scans.");
            this.numHostTimeout.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(278, 290);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnScan
            // 
            this.btnScan.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnScan.Location = new System.Drawing.Point(197, 290);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(75, 23);
            this.btnScan.TabIndex = 3;
            this.btnScan.Text = "&Scan...";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Help";
            // 
            // cbScanTypes
            // 
            this.cbScanTypes.FormattingEnabled = true;
            this.cbScanTypes.Location = new System.Drawing.Point(10, 20);
            this.cbScanTypes.Name = "cbScanTypes";
            this.cbScanTypes.Size = new System.Drawing.Size(153, 21);
            this.cbScanTypes.TabIndex = 10;
            this.cbScanTypes.SelectedIndexChanged += new System.EventHandler(this.cbScanTypes_SelectedIndexChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(64, 165);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(92, 17);
            this.checkBox3.TabIndex = 11;
            this.checkBox3.Text = "Host Timeout:";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // ScanOptions
            // 
            this.AcceptButton = this.btnScan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(372, 324);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ScanOptions";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Scan Options...";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.ScanOptions_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbWarning)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHostTimeout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtTargets;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.ComboBox cbTimeVars;
        private System.Windows.Forms.NumericUpDown numHostTimeout;
        private System.Windows.Forms.PictureBox pbWarning;
        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txtPorts;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.ComboBox cbScanTypes;
    }
}