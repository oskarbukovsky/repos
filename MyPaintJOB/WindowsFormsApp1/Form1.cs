using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Application : Form
    {
        public Application()
        {
            InitializeComponent();
            panel1Graphics = panel1.CreateGraphics();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            mousePath = new System.Drawing.Drawing2D.GraphicsPath();
            toolStripButton1.Image = null;
            toolStripButton2.Image = null;
            numericUpDown1.Text = "" + trackBar1.Value;
        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            var test = colorDialog1.ShowDialog();
            toolStripButton1.BackColor = colorDialog1.Color;
        }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var test = colorDialog2.ShowDialog();
            toolStripButton2.BackColor = colorDialog2.Color;
        }
        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.Text = "" + trackBar1.Value;
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            var test = numericUpDown1.Text;
            if (numericUpDown1.Text != String.Empty)
            {
                if (Convert.ToInt32(numericUpDown1.Text) != 0)
                {
                    if (Convert.ToInt32(numericUpDown1.Text) > 150)
                    {
                        trackBar1.Value = trackBar1.Maximum;
                    }
                    else
                    {
                        trackBar1.Value = Convert.ToInt32(numericUpDown1.Text);
                    }
                }
                else
                {
                    trackBar1.Value = trackBar1.Minimum;
                }
            }
            else
            {
                trackBar1.Value = trackBar1.Minimum;
            }
        }
        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            panel1.BackgroundImage = null;

        }
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.svg; *.gif)|*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.svg; *.gif";
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    //panel1.BackgroundImage = Image.FromFile(openDialog.FileName);
                    panel1.CreateGraphics().DrawImage(Image.FromFile(openDialog.FileName), 0, 0, panel1.Width, panel1.Height);
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed loading image");
            }
        }
        private GraphicsPath mousePath;
        private int mouseX, mouseY;
        Point mouseDownLocation;
        Graphics panel1Graphics;
        //Pen pen = new Pen(new SolidBrush(toolStripButton1.BackColor), trackBar1.Value);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownLocation = new Point(e.X, e.Y);
            mousePath.StartFigure();
            panel1.Focus();
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            panel1.Invalidate();
            //mousePath.Reset();
        }
        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            mouseX = e.X;
            mouseY = e.Y;
            if (e.Button == MouseButtons.Left && e.Button == Button.MouseButtons)
            {
                mousePath.AddLine(mouseX, mouseY, mouseX, mouseY);
                panel1.Invalidate();
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Pen pen = new Pen(new SolidBrush(toolStripButton1.BackColor), trackBar1.Value);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            pen.LineJoin = LineJoin.Round;

            e.Graphics.DrawPath(pen, mousePath);

            //GC.Collect();
        }


        private void panel1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Bitmap bitmap = new Bitmap(panel1.Width, panel1.Height);
            panel1.DrawToBitmap(bitmap, new Rectangle(0, 0, panel1.Width, panel1.Height));
            e.Graphics.DrawImage(bitmap, 0, 0);
            bitmap.Dispose();
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(panel1.Width, panel1.Height);
            panel1.DrawToBitmap(bmp, new Rectangle(0, 0, panel1.Width, panel1.Height));
            try
            {
                PrintDocument print = new PrintDocument();
                PrintDialog printDialog = new PrintDialog();
                print.PrintPage += new PrintPageEventHandler(panel1_PrintPage);
                printDialog.Document = print;
                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    print.Print();
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed saving image");
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(panel1.Width, panel1.Height);
            panel1.DrawToBitmap(bmp, new Rectangle(0, 0, panel1.Width, panel1.Height));
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Png Image (.png)|*.png|JPG Image (.jpg)|*.jpg|Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    bmp.Save(saveDialog.FileName);
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed saving image");
            }
        }
    }
}
