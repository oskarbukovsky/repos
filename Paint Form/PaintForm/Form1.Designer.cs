namespace PaintForm
{
    partial class Malování
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Malování));
            this.ControllPanel = new System.Windows.Forms.ToolStrip();
            this.NewFileButton = new System.Windows.Forms.ToolStripButton();
            this.OpenFileButton = new System.Windows.Forms.ToolStripButton();
            this.SaveFileButton = new System.Windows.Forms.ToolStripButton();
            this.PrintButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.vyjmoutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.kopírovatToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.vložitToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.nápovědaToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.ToolsPanel = new System.Windows.Forms.ToolStrip();
            this.ForegroundColorPicker = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.BackgroundColorPicker = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripProgressBar2 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripProgressBar3 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.SizeBar = new System.Windows.Forms.TrackBar();
            this.SizeUpDown = new System.Windows.Forms.NumericUpDown();
            this.DrawingArea = new System.Windows.Forms.Panel();
            this.PenButton = new System.Windows.Forms.Button();
            this.LineButton = new System.Windows.Forms.Button();
            this.RectangleButton = new System.Windows.Forms.Button();
            this.CircleButton = new System.Windows.Forms.Button();
            this.TriangleButton = new System.Windows.Forms.Button();
            this.FillCheckbox = new System.Windows.Forms.CheckBox();
            this.ControllPanel.SuspendLayout();
            this.ToolsPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SizeBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // ControllPanel
            // 
            this.ControllPanel.BackColor = System.Drawing.Color.LightGray;
            this.ControllPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewFileButton,
            this.OpenFileButton,
            this.SaveFileButton,
            this.PrintButton,
            this.toolStripSeparator,
            this.vyjmoutToolStripButton,
            this.kopírovatToolStripButton,
            this.vložitToolStripButton,
            this.toolStripSeparator1,
            this.nápovědaToolStripButton});
            this.ControllPanel.Location = new System.Drawing.Point(0, 0);
            this.ControllPanel.Name = "ControllPanel";
            this.ControllPanel.Size = new System.Drawing.Size(903, 25);
            this.ControllPanel.TabIndex = 6;
            this.ControllPanel.Text = "toolStrip1";
            // 
            // NewFileButton
            // 
            this.NewFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NewFileButton.Image = ((System.Drawing.Image)(resources.GetObject("NewFileButton.Image")));
            this.NewFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NewFileButton.Name = "NewFileButton";
            this.NewFileButton.Size = new System.Drawing.Size(23, 22);
            this.NewFileButton.Text = "&Nový";
            this.NewFileButton.Click += new System.EventHandler(this.FileNew);
            // 
            // OpenFileButton
            // 
            this.OpenFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenFileButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenFileButton.Image")));
            this.OpenFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenFileButton.Name = "OpenFileButton";
            this.OpenFileButton.Size = new System.Drawing.Size(23, 22);
            this.OpenFileButton.Text = "&Otevřít";
            this.OpenFileButton.Click += new System.EventHandler(this.FileOpen);
            // 
            // SaveFileButton
            // 
            this.SaveFileButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SaveFileButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveFileButton.Image")));
            this.SaveFileButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveFileButton.Name = "SaveFileButton";
            this.SaveFileButton.Size = new System.Drawing.Size(23, 22);
            this.SaveFileButton.Text = "&Uložit";
            this.SaveFileButton.Click += new System.EventHandler(this.FileSave);
            // 
            // PrintButton
            // 
            this.PrintButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PrintButton.Image = ((System.Drawing.Image)(resources.GetObject("PrintButton.Image")));
            this.PrintButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PrintButton.Name = "PrintButton";
            this.PrintButton.Size = new System.Drawing.Size(23, 22);
            this.PrintButton.Text = "&Tisk";
            this.PrintButton.Click += new System.EventHandler(this.FilePrint);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // vyjmoutToolStripButton
            // 
            this.vyjmoutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.vyjmoutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("vyjmoutToolStripButton.Image")));
            this.vyjmoutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.vyjmoutToolStripButton.Name = "vyjmoutToolStripButton";
            this.vyjmoutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.vyjmoutToolStripButton.Text = "&Vyjmout";
            // 
            // kopírovatToolStripButton
            // 
            this.kopírovatToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.kopírovatToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("kopírovatToolStripButton.Image")));
            this.kopírovatToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.kopírovatToolStripButton.Name = "kopírovatToolStripButton";
            this.kopírovatToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.kopírovatToolStripButton.Text = "&Kopírovat";
            // 
            // vložitToolStripButton
            // 
            this.vložitToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.vložitToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("vložitToolStripButton.Image")));
            this.vložitToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.vložitToolStripButton.Name = "vložitToolStripButton";
            this.vložitToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.vložitToolStripButton.Text = "&Vložit";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // nápovědaToolStripButton
            // 
            this.nápovědaToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.nápovědaToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("nápovědaToolStripButton.Image")));
            this.nápovědaToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.nápovědaToolStripButton.Name = "nápovědaToolStripButton";
            this.nápovědaToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.nápovědaToolStripButton.Text = "&Nápověda";
            // 
            // ToolsPanel
            // 
            this.ToolsPanel.AutoSize = false;
            this.ToolsPanel.BackColor = System.Drawing.Color.LightGray;
            this.ToolsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.ToolsPanel.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.ToolsPanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ForegroundColorPicker,
            this.toolStripSeparator2,
            this.BackgroundColorPicker,
            this.toolStripSeparator3,
            this.toolStripProgressBar2,
            this.toolStripSeparator4,
            this.toolStripProgressBar3,
            this.toolStripSeparator5});
            this.ToolsPanel.Location = new System.Drawing.Point(0, 25);
            this.ToolsPanel.Name = "ToolsPanel";
            this.ToolsPanel.Size = new System.Drawing.Size(90, 532);
            this.ToolsPanel.TabIndex = 9;
            this.ToolsPanel.Text = "toolStrip2";
            // 
            // ForegroundColorPicker
            // 
            this.ForegroundColorPicker.AutoSize = false;
            this.ForegroundColorPicker.BackColor = System.Drawing.Color.Black;
            this.ForegroundColorPicker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ForegroundColorPicker.Image = ((System.Drawing.Image)(resources.GetObject("ForegroundColorPicker.Image")));
            this.ForegroundColorPicker.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ForegroundColorPicker.Name = "ForegroundColorPicker";
            this.ForegroundColorPicker.Size = new System.Drawing.Size(32, 32);
            this.ForegroundColorPicker.Text = "toolStripButton1";
            this.ForegroundColorPicker.Click += new System.EventHandler(this.ForegroundColorPickerButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(88, 6);
            // 
            // BackgroundColorPicker
            // 
            this.BackgroundColorPicker.AutoSize = false;
            this.BackgroundColorPicker.BackColor = System.Drawing.Color.White;
            this.BackgroundColorPicker.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BackgroundColorPicker.Image = ((System.Drawing.Image)(resources.GetObject("BackgroundColorPicker.Image")));
            this.BackgroundColorPicker.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BackgroundColorPicker.Name = "BackgroundColorPicker";
            this.BackgroundColorPicker.Size = new System.Drawing.Size(32, 32);
            this.BackgroundColorPicker.Text = "toolStripButton2";
            this.BackgroundColorPicker.Click += new System.EventHandler(this.BackgroundColorPickerButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(88, 6);
            // 
            // toolStripProgressBar2
            // 
            this.toolStripProgressBar2.Name = "toolStripProgressBar2";
            this.toolStripProgressBar2.Size = new System.Drawing.Size(86, 45);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(88, 6);
            // 
            // toolStripProgressBar3
            // 
            this.toolStripProgressBar3.Name = "toolStripProgressBar3";
            this.toolStripProgressBar3.Size = new System.Drawing.Size(86, 20);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(88, 6);
            // 
            // SizeBar
            // 
            this.SizeBar.LargeChange = 15;
            this.SizeBar.Location = new System.Drawing.Point(0, 110);
            this.SizeBar.Maximum = 150;
            this.SizeBar.Minimum = 1;
            this.SizeBar.Name = "SizeBar";
            this.SizeBar.Size = new System.Drawing.Size(87, 45);
            this.SizeBar.TabIndex = 10;
            this.SizeBar.Value = 25;
            this.SizeBar.ValueChanged += new System.EventHandler(this.SizeBar_ValueChanged);
            // 
            // SizeUpDown
            // 
            this.SizeUpDown.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.SizeUpDown.Location = new System.Drawing.Point(0, 162);
            this.SizeUpDown.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.SizeUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SizeUpDown.Name = "SizeUpDown";
            this.SizeUpDown.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.SizeUpDown.Size = new System.Drawing.Size(87, 20);
            this.SizeUpDown.TabIndex = 11;
            this.SizeUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.SizeUpDown.ValueChanged += new System.EventHandler(this.SizeUpDown_ValueChanged);
            // 
            // DrawingArea
            // 
            this.DrawingArea.BackColor = System.Drawing.Color.DarkGray;
            this.DrawingArea.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DrawingArea.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.DrawingArea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DrawingArea.Location = new System.Drawing.Point(90, 25);
            this.DrawingArea.Name = "DrawingArea";
            this.DrawingArea.Size = new System.Drawing.Size(813, 532);
            this.DrawingArea.TabIndex = 12;
            // 
            // PenButton
            // 
            this.PenButton.Location = new System.Drawing.Point(-5, 216);
            this.PenButton.Name = "PenButton";
            this.PenButton.Size = new System.Drawing.Size(92, 45);
            this.PenButton.TabIndex = 13;
            this.PenButton.Text = "Pen";
            this.PenButton.UseVisualStyleBackColor = true;
            this.PenButton.Click += new System.EventHandler(this.PenButtond_Click);
            // 
            // LineButton
            // 
            this.LineButton.Location = new System.Drawing.Point(-5, 267);
            this.LineButton.Name = "LineButton";
            this.LineButton.Size = new System.Drawing.Size(92, 45);
            this.LineButton.TabIndex = 14;
            this.LineButton.Text = "Line";
            this.LineButton.UseVisualStyleBackColor = true;
            this.LineButton.Click += new System.EventHandler(this.PenButtond_Click);
            // 
            // RectangleButton
            // 
            this.RectangleButton.Location = new System.Drawing.Point(0, 318);
            this.RectangleButton.Name = "RectangleButton";
            this.RectangleButton.Size = new System.Drawing.Size(87, 45);
            this.RectangleButton.TabIndex = 15;
            this.RectangleButton.Text = "Rectangle";
            this.RectangleButton.UseVisualStyleBackColor = true;
            this.RectangleButton.Click += new System.EventHandler(this.PenButtond_Click);
            // 
            // CircleButton
            // 
            this.CircleButton.Location = new System.Drawing.Point(0, 369);
            this.CircleButton.Name = "CircleButton";
            this.CircleButton.Size = new System.Drawing.Size(87, 45);
            this.CircleButton.TabIndex = 16;
            this.CircleButton.Text = "Circle";
            this.CircleButton.UseVisualStyleBackColor = true;
            this.CircleButton.Click += new System.EventHandler(this.PenButtond_Click);
            // 
            // TriangleButton
            // 
            this.TriangleButton.Location = new System.Drawing.Point(0, 420);
            this.TriangleButton.Name = "TriangleButton";
            this.TriangleButton.Size = new System.Drawing.Size(87, 45);
            this.TriangleButton.TabIndex = 17;
            this.TriangleButton.Text = "Triangle";
            this.TriangleButton.UseVisualStyleBackColor = true;
            this.TriangleButton.Click += new System.EventHandler(this.PenButtond_Click);
            // 
            // FillCheckbox
            // 
            this.FillCheckbox.AutoSize = true;
            this.FillCheckbox.Location = new System.Drawing.Point(12, 471);
            this.FillCheckbox.Name = "FillCheckbox";
            this.FillCheckbox.Size = new System.Drawing.Size(65, 17);
            this.FillCheckbox.TabIndex = 18;
            this.FillCheckbox.Text = "Fill Color";
            this.FillCheckbox.UseVisualStyleBackColor = true;
            // 
            // Malování
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(903, 557);
            this.Controls.Add(this.FillCheckbox);
            this.Controls.Add(this.TriangleButton);
            this.Controls.Add(this.CircleButton);
            this.Controls.Add(this.RectangleButton);
            this.Controls.Add(this.LineButton);
            this.Controls.Add(this.PenButton);
            this.Controls.Add(this.DrawingArea);
            this.Controls.Add(this.SizeUpDown);
            this.Controls.Add(this.SizeBar);
            this.Controls.Add(this.ToolsPanel);
            this.Controls.Add(this.ControllPanel);
            this.Name = "Malování";
            this.Text = "Malování";
            this.ControllPanel.ResumeLayout(false);
            this.ControllPanel.PerformLayout();
            this.ToolsPanel.ResumeLayout(false);
            this.ToolsPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SizeBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SizeUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip ControllPanel;
        private System.Windows.Forms.ToolStripButton NewFileButton;
        private System.Windows.Forms.ToolStripButton OpenFileButton;
        private System.Windows.Forms.ToolStripButton SaveFileButton;
        private System.Windows.Forms.ToolStripButton PrintButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton vyjmoutToolStripButton;
        private System.Windows.Forms.ToolStripButton kopírovatToolStripButton;
        private System.Windows.Forms.ToolStripButton vložitToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton nápovědaToolStripButton;
        private System.Windows.Forms.ToolStrip ToolsPanel;
        private System.Windows.Forms.ToolStripButton ForegroundColorPicker;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton BackgroundColorPicker;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.TrackBar SizeBar;
        private System.Windows.Forms.NumericUpDown SizeUpDown;
        private System.Windows.Forms.Panel DrawingArea;
        private System.Windows.Forms.Button PenButton;
        private System.Windows.Forms.Button LineButton;
        private System.Windows.Forms.Button RectangleButton;
        private System.Windows.Forms.Button CircleButton;
        private System.Windows.Forms.Button TriangleButton;
        private System.Windows.Forms.CheckBox FillCheckbox;
    }
}

