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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title1 = new System.Windows.Forms.DataVisualization.Charting.Title();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
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
            chartArea1.Area3DStyle.Enable3D = true;
            chartArea1.Name = "ChartArea1";
            this.chartPorts.ChartAreas.Add(chartArea1);
            this.chartPorts.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartPorts.Legends.Add(legend1);
            this.chartPorts.Location = new System.Drawing.Point(0, 0);
            this.chartPorts.Name = "chartPorts";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.SmartLabelStyle.Enabled = false;
            this.chartPorts.Series.Add(series1);
            this.chartPorts.Size = new System.Drawing.Size(188, 148);
            this.chartPorts.TabIndex = 0;
            this.chartPorts.Text = "Top Ports";
            title1.Name = "Title1";
            title1.Text = "Top Ports";
            this.chartPorts.Titles.Add(title1);
            this.chartPorts.Click += new System.EventHandler(this.chartPorts_Click);
            // 
            // chartOS
            // 
            chartArea2.Area3DStyle.Enable3D = true;
            chartArea2.Name = "ChartArea1";
            this.chartOS.ChartAreas.Add(chartArea2);
            this.chartOS.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.chartOS.Legends.Add(legend2);
            this.chartOS.Location = new System.Drawing.Point(0, 0);
            this.chartOS.Name = "chartOS";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            series2.ToolTip = "Operating Systems";
            this.chartOS.Series.Add(series2);
            this.chartOS.Size = new System.Drawing.Size(188, 184);
            this.chartOS.TabIndex = 0;
            this.chartOS.Text = "Top OS";
            title2.Name = "Title1";
            title2.Text = "Operating Systems";
            this.chartOS.Titles.Add(title2);
            this.chartOS.Click += new System.EventHandler(this.chartOS_Click);
            // 
            // lvHosts
            // 
            this.lvHosts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colHostname,
            this.colIP,
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
            // 
            // colIP
            // 
            this.colIP.Text = "IP";
            // 
            // colOS
            // 
            this.colOS.Text = "Operating System";
            // 
            // colPorts
            // 
            this.colPorts.Text = "Ports";
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
    }
}
