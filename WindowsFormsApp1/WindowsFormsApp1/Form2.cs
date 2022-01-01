using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal class Form2 : Form
    {
        private System.ComponentModel.IContainer components;
        private FlowLayoutPanel flowLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Label label1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart6;
        private Label label2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart5;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart7;
        private TableLayoutPanel tableLayoutPanel4;
        private Label label4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart8;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart9;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart10;
        private TableLayoutPanel tableLayoutPanel3;
        private Label label3;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart11;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart12;
        private ToolStripComboBox toolStripComboBox1;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem_0;
        private ToolStripMenuItem toolStripMenuItem_1;
        private ToolStripMenuItem toolStripMenuItem_2;
        private ToolStripMenuItem node3ToolStripMenuItem;
        private ToolStripMenuItem formátToolStripMenuItem;
        private ToolStripMenuItem seřaditToolStripMenuItem;
        private ToolStripMenuItem podleNázvuSestupněToolStripMenuItem;
        private ToolStripMenuItem podleNázvuVzestupněToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem addNodeToolStripMenuItem;
        private ToolStripMenuItem test0ToolStripMenuItem;
        private ToolStripMenuItem test1ToolStripMenuItem;
        private Label label5;
        private ToolStripMenuItem automatickyToolStripMenuItem;
        private ToolStripMenuItem manuálněToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart13;

        public Form2()
        {
            InitializeComponent();
        }

        private void control_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void panel_DragEnter(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(string)))
                return;

            var name = e.Data.GetData(typeof(string)) as string;
            var control = this.Controls.Find(name, true).FirstOrDefault();
            if (control != null)
            {
                e.Effect = DragDropEffects.Move;
            }
        }

        private void panel_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(string)))
                return;

            var name = e.Data.GetData(typeof(string)) as string;
            var control = this.Controls.Find(name, true).FirstOrDefault();
            if (control != null)
            {
                control.Parent.Controls.Remove(control);
                var panel = sender as FlowLayoutPanel;
                ((FlowLayoutPanel)sender).Controls.Add(control);
            }
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea25 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend25 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series25 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea26 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend26 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series26 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea27 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend27 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series27 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea28 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend28 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series28 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea29 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend29 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series29 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea30 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend30 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series30 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea31 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend31 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series31 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea32 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend32 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series32 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea33 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend33 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series33 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea34 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend34 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series34 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea35 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend35 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series35 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea36 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend36 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series36 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart3 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart6 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.chart4 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart5 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart7 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.chart8 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart9 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart10 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.chart11 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart12 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chart13 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_0 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_2 = new System.Windows.Forms.ToolStripMenuItem();
            this.node3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addNodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.formátToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seřaditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.podleNázvuSestupněToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.podleNázvuVzestupněToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.test0ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.test1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label5 = new System.Windows.Forms.Label();
            this.automatickyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.manuálněToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart6)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart7)).BeginInit();
            this.tableLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart10)).BeginInit();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart13)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AllowDrop = true;
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel1);
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel2);
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel4);
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel3);
            this.flowLayoutPanel1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 27);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(670, 273);
            this.flowLayoutPanel1.TabIndex = 2;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AllowDrop = true;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.chart1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.chart3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.chart6, 3, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(562, 100);
            this.tableLayoutPanel1.TabIndex = 0;
            this.tableLayoutPanel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.control_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // chart1
            // 
            chartArea25.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea25);
            legend25.Name = "Legend1";
            this.chart1.Legends.Add(legend25);
            this.chart1.Location = new System.Drawing.Point(144, 4);
            this.chart1.Name = "chart1";
            series25.ChartArea = "ChartArea1";
            series25.Legend = "Legend1";
            series25.Name = "Series1";
            this.chart1.Series.Add(series25);
            this.chart1.Size = new System.Drawing.Size(133, 92);
            this.chart1.TabIndex = 1;
            this.chart1.Text = "chart1";
            // 
            // chart3
            // 
            chartArea26.Name = "ChartArea1";
            this.chart3.ChartAreas.Add(chartArea26);
            legend26.Name = "Legend1";
            this.chart3.Legends.Add(legend26);
            this.chart3.Location = new System.Drawing.Point(284, 4);
            this.chart3.Name = "chart3";
            series26.ChartArea = "ChartArea1";
            series26.Legend = "Legend1";
            series26.Name = "Series1";
            this.chart3.Series.Add(series26);
            this.chart3.Size = new System.Drawing.Size(133, 92);
            this.chart3.TabIndex = 2;
            this.chart3.Text = "chart3";
            // 
            // chart6
            // 
            chartArea27.Name = "ChartArea1";
            this.chart6.ChartAreas.Add(chartArea27);
            legend27.Name = "Legend1";
            this.chart6.Legends.Add(legend27);
            this.chart6.Location = new System.Drawing.Point(424, 4);
            this.chart6.Name = "chart6";
            series27.ChartArea = "ChartArea1";
            series27.Legend = "Legend1";
            series27.Name = "Series1";
            this.chart6.Series.Add(series27);
            this.chart6.Size = new System.Drawing.Size(134, 92);
            this.chart6.TabIndex = 3;
            this.chart6.Text = "chart6";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AllowDrop = true;
            this.tableLayoutPanel2.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel2.ColumnCount = 4;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.chart4, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.chart5, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.chart7, 3, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 109);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(562, 100);
            this.tableLayoutPanel2.TabIndex = 1;
            this.tableLayoutPanel2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.control_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "label2";
            // 
            // chart4
            // 
            chartArea28.Name = "ChartArea1";
            this.chart4.ChartAreas.Add(chartArea28);
            legend28.Name = "Legend1";
            this.chart4.Legends.Add(legend28);
            this.chart4.Location = new System.Drawing.Point(144, 4);
            this.chart4.Name = "chart4";
            series28.ChartArea = "ChartArea1";
            series28.Legend = "Legend1";
            series28.Name = "Series1";
            this.chart4.Series.Add(series28);
            this.chart4.Size = new System.Drawing.Size(133, 92);
            this.chart4.TabIndex = 1;
            this.chart4.Text = "chart4";
            // 
            // chart5
            // 
            chartArea29.Name = "ChartArea1";
            this.chart5.ChartAreas.Add(chartArea29);
            legend29.Name = "Legend1";
            this.chart5.Legends.Add(legend29);
            this.chart5.Location = new System.Drawing.Point(284, 4);
            this.chart5.Name = "chart5";
            series29.ChartArea = "ChartArea1";
            series29.Legend = "Legend1";
            series29.Name = "Series1";
            this.chart5.Series.Add(series29);
            this.chart5.Size = new System.Drawing.Size(133, 92);
            this.chart5.TabIndex = 2;
            this.chart5.Text = "chart5";
            // 
            // chart7
            // 
            chartArea30.Name = "ChartArea1";
            this.chart7.ChartAreas.Add(chartArea30);
            legend30.Name = "Legend1";
            this.chart7.Legends.Add(legend30);
            this.chart7.Location = new System.Drawing.Point(424, 4);
            this.chart7.Name = "chart7";
            series30.ChartArea = "ChartArea1";
            series30.Legend = "Legend1";
            series30.Name = "Series1";
            this.chart7.Series.Add(series30);
            this.chart7.Size = new System.Drawing.Size(134, 92);
            this.chart7.TabIndex = 3;
            this.chart7.Text = "chart7";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.AllowDrop = true;
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 4;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel4.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.chart8, 1, 0);
            this.tableLayoutPanel4.Controls.Add(this.chart9, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.chart10, 3, 0);
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 215);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(562, 100);
            this.tableLayoutPanel4.TabIndex = 3;
            this.tableLayoutPanel4.MouseDown += new System.Windows.Forms.MouseEventHandler(this.control_MouseDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "label4";
            // 
            // chart8
            // 
            chartArea31.Name = "ChartArea1";
            this.chart8.ChartAreas.Add(chartArea31);
            legend31.Name = "Legend1";
            this.chart8.Legends.Add(legend31);
            this.chart8.Location = new System.Drawing.Point(144, 4);
            this.chart8.Name = "chart8";
            series31.ChartArea = "ChartArea1";
            series31.Legend = "Legend1";
            series31.Name = "Series1";
            this.chart8.Series.Add(series31);
            this.chart8.Size = new System.Drawing.Size(133, 92);
            this.chart8.TabIndex = 1;
            this.chart8.Text = "chart8";
            // 
            // chart9
            // 
            chartArea32.Name = "ChartArea1";
            this.chart9.ChartAreas.Add(chartArea32);
            legend32.Name = "Legend1";
            this.chart9.Legends.Add(legend32);
            this.chart9.Location = new System.Drawing.Point(284, 4);
            this.chart9.Name = "chart9";
            series32.ChartArea = "ChartArea1";
            series32.Legend = "Legend1";
            series32.Name = "Series1";
            this.chart9.Series.Add(series32);
            this.chart9.Size = new System.Drawing.Size(133, 92);
            this.chart9.TabIndex = 2;
            this.chart9.Text = "chart9";
            // 
            // chart10
            // 
            chartArea33.Name = "ChartArea1";
            this.chart10.ChartAreas.Add(chartArea33);
            legend33.Name = "Legend1";
            this.chart10.Legends.Add(legend33);
            this.chart10.Location = new System.Drawing.Point(424, 4);
            this.chart10.Name = "chart10";
            series33.ChartArea = "ChartArea1";
            series33.Legend = "Legend1";
            series33.Name = "Series1";
            this.chart10.Series.Add(series33);
            this.chart10.Size = new System.Drawing.Size(134, 92);
            this.chart10.TabIndex = 3;
            this.chart10.Text = "chart10";
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.AllowDrop = true;
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 4;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel3.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.chart11, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.chart12, 2, 0);
            this.tableLayoutPanel3.Controls.Add(this.chart13, 3, 0);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 321);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(562, 100);
            this.tableLayoutPanel3.TabIndex = 5;
            this.tableLayoutPanel3.MouseDown += new System.Windows.Forms.MouseEventHandler(this.control_MouseDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "label3";
            // 
            // chart11
            // 
            chartArea34.Name = "ChartArea1";
            this.chart11.ChartAreas.Add(chartArea34);
            legend34.Name = "Legend1";
            this.chart11.Legends.Add(legend34);
            this.chart11.Location = new System.Drawing.Point(144, 4);
            this.chart11.Name = "chart11";
            series34.ChartArea = "ChartArea1";
            series34.Legend = "Legend1";
            series34.Name = "Series1";
            this.chart11.Series.Add(series34);
            this.chart11.Size = new System.Drawing.Size(133, 92);
            this.chart11.TabIndex = 1;
            this.chart11.Text = "chart11";
            // 
            // chart12
            // 
            chartArea35.Name = "ChartArea1";
            this.chart12.ChartAreas.Add(chartArea35);
            legend35.Name = "Legend1";
            this.chart12.Legends.Add(legend35);
            this.chart12.Location = new System.Drawing.Point(284, 4);
            this.chart12.Name = "chart12";
            series35.ChartArea = "ChartArea1";
            series35.Legend = "Legend1";
            series35.Name = "Series1";
            this.chart12.Series.Add(series35);
            this.chart12.Size = new System.Drawing.Size(133, 92);
            this.chart12.TabIndex = 2;
            this.chart12.Text = "chart12";
            // 
            // chart13
            // 
            chartArea36.Name = "ChartArea1";
            this.chart13.ChartAreas.Add(chartArea36);
            legend36.Name = "Legend1";
            this.chart13.Legends.Add(legend36);
            this.chart13.Location = new System.Drawing.Point(424, 4);
            this.chart13.Name = "chart13";
            series36.ChartArea = "ChartArea1";
            series36.Legend = "Legend1";
            series36.Name = "Series1";
            this.chart13.Series.Add(series36);
            this.chart13.Size = new System.Drawing.Size(134, 92);
            this.chart13.TabIndex = 3;
            this.chart13.Text = "chart13";
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "Node0",
            "Node1",
            "Node2",
            "Node3"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripMenuItem1.Checked = true;
            this.toolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_0,
            this.toolStripMenuItem_1,
            this.toolStripMenuItem_2,
            this.node3ToolStripMenuItem,
            this.addNodeToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(62, 23);
            this.toolStripMenuItem1.Text = "Zobrazit";
            // 
            // toolStripMenuItem_0
            // 
            this.toolStripMenuItem_0.CheckOnClick = true;
            this.toolStripMenuItem_0.Name = "toolStripMenuItem_0";
            this.toolStripMenuItem_0.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem_0.Text = "Node0";
            // 
            // toolStripMenuItem_1
            // 
            this.toolStripMenuItem_1.CheckOnClick = true;
            this.toolStripMenuItem_1.Name = "toolStripMenuItem_1";
            this.toolStripMenuItem_1.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem_1.Text = "Node1";
            // 
            // toolStripMenuItem_2
            // 
            this.toolStripMenuItem_2.CheckOnClick = true;
            this.toolStripMenuItem_2.Name = "toolStripMenuItem_2";
            this.toolStripMenuItem_2.Size = new System.Drawing.Size(180, 22);
            this.toolStripMenuItem_2.Text = "Node2";
            // 
            // node3ToolStripMenuItem
            // 
            this.node3ToolStripMenuItem.Name = "node3ToolStripMenuItem";
            this.node3ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.node3ToolStripMenuItem.Text = "Node3";
            // 
            // addNodeToolStripMenuItem
            // 
            this.addNodeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.automatickyToolStripMenuItem,
            this.manuálněToolStripMenuItem});
            this.addNodeToolStripMenuItem.Name = "addNodeToolStripMenuItem";
            this.addNodeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.addNodeToolStripMenuItem.Text = "Add node";
            // 
            // formátToolStripMenuItem
            // 
            this.formátToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.seřaditToolStripMenuItem});
            this.formátToolStripMenuItem.Name = "formátToolStripMenuItem";
            this.formátToolStripMenuItem.Size = new System.Drawing.Size(57, 23);
            this.formátToolStripMenuItem.Text = "Formát";
            // 
            // seřaditToolStripMenuItem
            // 
            this.seřaditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.podleNázvuSestupněToolStripMenuItem,
            this.podleNázvuVzestupněToolStripMenuItem});
            this.seřaditToolStripMenuItem.Name = "seřaditToolStripMenuItem";
            this.seřaditToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.seřaditToolStripMenuItem.Text = "Seřadit";
            // 
            // podleNázvuSestupněToolStripMenuItem
            // 
            this.podleNázvuSestupněToolStripMenuItem.Checked = true;
            this.podleNázvuSestupněToolStripMenuItem.CheckOnClick = true;
            this.podleNázvuSestupněToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.podleNázvuSestupněToolStripMenuItem.Name = "podleNázvuSestupněToolStripMenuItem";
            this.podleNázvuSestupněToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.podleNázvuSestupněToolStripMenuItem.Text = "Podle názvu sestupně";
            // 
            // podleNázvuVzestupněToolStripMenuItem
            // 
            this.podleNázvuVzestupněToolStripMenuItem.CheckOnClick = true;
            this.podleNázvuVzestupněToolStripMenuItem.Name = "podleNázvuVzestupněToolStripMenuItem";
            this.podleNázvuVzestupněToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
            this.podleNázvuVzestupněToolStripMenuItem.Text = "Podle názvu vzestupně";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBox1,
            this.toolStripMenuItem1,
            this.formátToolStripMenuItem,
            this.test0ToolStripMenuItem,
            this.test1ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(670, 27);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // test0ToolStripMenuItem
            // 
            this.test0ToolStripMenuItem.Name = "test0ToolStripMenuItem";
            this.test0ToolStripMenuItem.Size = new System.Drawing.Size(45, 23);
            this.test0ToolStripMenuItem.Text = "Test0";
            this.test0ToolStripMenuItem.Click += new System.EventHandler(this.test0ToolStripMenuItem_Click);
            // 
            // test1ToolStripMenuItem
            // 
            this.test1ToolStripMenuItem.Name = "test1ToolStripMenuItem";
            this.test1ToolStripMenuItem.Size = new System.Drawing.Size(45, 23);
            this.test1ToolStripMenuItem.Text = "Test1";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 303);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "label5";
            // 
            // automatickyToolStripMenuItem
            // 
            this.automatickyToolStripMenuItem.Name = "automatickyToolStripMenuItem";
            this.automatickyToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.automatickyToolStripMenuItem.Text = "Automaticky";
            // 
            // manuálněToolStripMenuItem
            // 
            this.manuálněToolStripMenuItem.Name = "manuálněToolStripMenuItem";
            this.manuálněToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.manuálněToolStripMenuItem.Text = "Manuálně";
            // 
            // Form2
            // 
            this.ClientSize = new System.Drawing.Size(670, 325);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form2";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart6)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart7)).EndInit();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart10)).EndInit();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart13)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void test0ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var test = flowLayoutPanel1.Controls.OfType<Control>().ToArray();
            flowLayoutPanel1.Controls.SetChildIndex(test[0], 1);
        }
    }
}