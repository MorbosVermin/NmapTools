namespace NmapVisualizer
{
    partial class ReportPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title3 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title4 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportPanel));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.chartPorts = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartOS = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.lvHosts = new System.Windows.Forms.ListView();
            this.colHostname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colOS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colPorts = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgList64 = new System.Windows.Forms.ImageList(this.components);
            this.colMac = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartPorts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartOS)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lvHosts);
            this.splitContainer1.Size = new System.Drawing.Size(567, 336);
            this.splitContainer1.SplitterDistance = 188;
            this.splitContainer1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.chartPorts);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.chartOS);
            this.splitContainer2.Size = new System.Drawing.Size(188, 336);
            this.splitContainer2.SplitterDistance = 148;
            this.splitContainer2.TabIndex = 0;
            // 
            // chartPorts
            // 
            chartArea3.Area3DStyle.Enable3D = true;
            chartArea3.Name = "ChartArea1";
            this.chartPorts.ChartAreas.Add(chartArea3);
            this.chartPorts.Dock = System.Windows.Forms.DockStyle.Fill;
            legend3.Name = "Legend1";
            this.chartPorts.Legends.Add(legend3);
            this.chartPorts.Location = new System.Drawing.Point(0, 0);
            this.chartPorts.Name = "chartPorts";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            series3.SmartLabelStyle.Enabled = false;
            this.chartPorts.Series.Add(series3);
            this.chartPorts.Size = new System.Drawing.Size(188, 148);
            this.chartPorts.TabIndex = 0;
            this.chartPorts.Text = "Top Ports";
            title3.Name = "Title1";
            title3.Text = "Top Ports";
            this.chartPorts.Titles.Add(title3);
            this.chartPorts.Click += new System.EventHandler(this.chartPorts_Click);
            // 
            // chartOS
            // 
            chartArea4.Area3DStyle.Enable3D = true;
            chartArea4.Name = "ChartArea1";
            this.chartOS.ChartAreas.Add(chartArea4);
            this.chartOS.Dock = System.Windows.Forms.DockStyle.Fill;
            legend4.Name = "Legend1";
            this.chartOS.Legends.Add(legend4);
            this.chartOS.Location = new System.Drawing.Point(0, 0);
            this.chartOS.Name = "chartOS";
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series4.Legend = "Legend1";
            series4.Name = "Series1";
            series4.ToolTip = "Operating Systems";
            this.chartOS.Series.Add(series4);
            this.chartOS.Size = new System.Drawing.Size(188, 184);
            this.chartOS.TabIndex = 0;
            this.chartOS.Text = "Top OS";
            title4.Name = "Title1";
            title4.Text = "Operating Systems";
            this.chartOS.Titles.Add(title4);
            this.chartOS.Click += new System.EventHandler(this.chartOS_Click);
            // 
            // lvHosts
            // 
            this.lvHosts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHostname,
            this.colIP,
            this.colMac,
            this.colOS,
            this.colPorts});
            this.lvHosts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvHosts.FullRowSelect = true;
            this.lvHosts.LargeImageList = this.imgList64;
            this.lvHosts.Location = new System.Drawing.Point(0, 0);
            this.lvHosts.MultiSelect = false;
            this.lvHosts.Name = "lvHosts";
            this.lvHosts.ShowGroups = false;
            this.lvHosts.ShowItemToolTips = true;
            this.lvHosts.Size = new System.Drawing.Size(375, 336);
            this.lvHosts.TabIndex = 0;
            this.lvHosts.UseCompatibleStateImageBehavior = false;
            this.lvHosts.SelectedIndexChanged += new System.EventHandler(this.lvHosts_SelectedIndexChanged);
            // 
            // colHostname
            // 
            this.colHostname.Text = "Hostname";
            this.colHostname.Width = 150;
            // 
            // colIP
            // 
            this.colIP.Text = "IP";
            this.colIP.Width = 100;
            // 
            // colOS
            // 
            this.colOS.Text = "Operating System";
            this.colOS.Width = 200;
            // 
            // colPorts
            // 
            this.colPorts.Text = "Ports";
            this.colPorts.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colPorts.Width = 40;
            // 
            // imgList64
            // 
            this.imgList64.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgList64.ImageStream")));
            this.imgList64.TransparentColor = System.Drawing.Color.Transparent;
            this.imgList64.Images.SetKeyName(0, "AIX");
            this.imgList64.Images.SetKeyName(1, "Linux");
            this.imgList64.Images.SetKeyName(2, "Printer");
            this.imgList64.Images.SetKeyName(3, "Solaris");
            this.imgList64.Images.SetKeyName(4, "Windows");
            this.imgList64.Images.SetKeyName(5, "Unknown");
            // 
            // colMac
            // 
            this.colMac.Text = "MAC";
            this.colMac.Width = 100;
            // 
            // ReportPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ReportPanel";
            this.Size = new System.Drawing.Size(567, 336);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartPorts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartOS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView lvHosts;
        private System.Windows.Forms.ImageList imgList64;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPorts;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartOS;
        private System.Windows.Forms.ColumnHeader colHostname;
        private System.Windows.Forms.ColumnHeader colIP;
        private System.Windows.Forms.ColumnHeader colOS;
        private System.Windows.Forms.ColumnHeader colPorts;
        private System.Windows.Forms.ColumnHeader colMac;
    }
}
