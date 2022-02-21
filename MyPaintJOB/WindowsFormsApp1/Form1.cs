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
            toolStripButton3.BackColor = Color.CornflowerBlue;

            loadPictureBox();
        }

        Bitmap paintImage;
        Graphics paintGraphics;

        private void loadPictureBox()
        {
            int width = panel1.Width;
            int height = panel1.Height;

            paintImage = new Bitmap(width, height);

            paintGraphics = Graphics.FromImage(paintImage);

            paintGraphics.FillRectangle(Brushes.White, 0, 0, width, height);

            panel1.MouseDown += new MouseEventHandler(panel1_MouseDown);
            panel1.MouseMove += new MouseEventHandler(panel1_MouseMove);
            panel1.MouseUp += new MouseEventHandler(panel1_MouseUp);
        }

        Point lastPoint;
        bool isMouseDown = false;
        Bitmap workingImage;
        Graphics workingGraphics;

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

        private void DrawLineInCanvas(Point currentPoint)
        {
            // Form pen with the color selected and the size value in tracker
            Pen pen = new Pen(toolStripButton1.BackColor, trackBar1.Value);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            pen.LineJoin = LineJoin.Round;

            paintGraphics.DrawLine(pen, lastPoint, currentPoint);

            lastPoint = currentPoint;

            panel1.Invalidate();
        }
        private void DrawShapeInWorkingImage(Point currentPoint)
        {
            Pen pen = new Pen(toolStripButton1.BackColor, trackBar1.Value);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            pen.LineJoin = LineJoin.Round;

            workingImage = new Bitmap(paintImage);
            workingGraphics = Graphics.FromImage(workingImage);

            int startPointX = lastPoint.X < currentPoint.X ? lastPoint.X : currentPoint.X;
            int startPointY = lastPoint.Y < currentPoint.Y ? lastPoint.Y : currentPoint.Y;

            int shapeWidth = (lastPoint.X > currentPoint.X ? lastPoint.X : currentPoint.X) - startPointX;
            int shapeHeight = (lastPoint.Y > currentPoint.Y ? lastPoint.Y : currentPoint.Y) - startPointY;

            switch (paintType.type)
            {
                case "rectangle":
                    // Check if it is to draw or fill shape
                    if (!checkBox1.Checked)
                    {
                        // Draw Rectangle
                        workingGraphics.DrawRectangle(pen, startPointX, startPointY, shapeWidth, shapeHeight);
                    }
                    else
                    {
                        // Fill Rectangle
                        workingGraphics.FillRectangle(pen.Brush, startPointX, startPointY, shapeWidth, shapeHeight);
                    }
                    break;

                case "elipse":
                    if (!checkBox1.Checked)
                    {
                        workingGraphics.DrawEllipse(pen, startPointX, startPointY, shapeWidth, shapeHeight);
                    }
                    else
                    {
                        workingGraphics.FillEllipse(pen.Brush, startPointX, startPointY, shapeWidth, shapeHeight);
                    }
                    break;

                case "Triangle":
                    Point point1 = new Point() { X = startPointX, Y = startPointY + shapeHeight };
                    Point point2 = new Point() { X = startPointX + (shapeWidth / 2), Y = startPointY };
                    Point point3 = new Point() { X = startPointX + shapeWidth, Y = startPointY + shapeHeight };

                    if (!checkBox1.Checked)
                    {
                        workingGraphics.DrawPolygon(pen, new Point[] { point1, point2, point3 });
                    }
                    else
                    {
                        workingGraphics.FillPolygon(pen.Brush, new Point[] { point1, point2, point3 });
                    }
                    break;

                case "line":
                    workingGraphics.DrawLine(pen, lastPoint, currentPoint);
                    break;
            }

            // The outline should be shown only if it is not a line and the drawing is on. 
            if (isMouseDown && paintType.type != "line")
            {
                // Draw outline while drawing shapes
                Pen outLinePen = new Pen(Color.Black);
                outLinePen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                workingGraphics.DrawRectangle(outLinePen, startPointX, startPointY, shapeWidth, shapeHeight);
            }

            panel1Graphics.DrawImage(workingImage, 0, 0, panel1.Width, panel1.Height);
        }

        //Pen pen = new Pen(new SolidBrush(toolStripButton1.BackColor), trackBar1.Value);
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDownLocation = new Point(e.X, e.Y);
            mousePath.StartFigure();
            panel1.Focus();

            lastPoint = e.Location;
            isMouseDown = true;
        }
        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            panel1.Invalidate();
            //mousePath.Reset();

            isMouseDown = false;

            if (paintType.type != "pen")
            {
                // Draw again to get the shape without border
                DrawShapeInWorkingImage(e.Location);

                paintImage = new Bitmap(workingImage);

                // Re initialize graphics object as the bitmap is new
                paintGraphics = Graphics.FromImage(paintImage);

                panel1Graphics.DrawImage(paintImage, 0, 0, panel1.Width, panel1.Height);
            }
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

            if (isMouseDown)
            {
                if (paintType.type == "pen")
                {
                    DrawLineInCanvas(e.Location);
                }
                else
                {
                    DrawShapeInWorkingImage(e.Location);
                }
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Pen pen = new Pen(new SolidBrush(toolStripButton1.BackColor), trackBar1.Value);
            pen.StartCap = LineCap.Round;
            pen.EndCap = LineCap.Round;
            pen.LineJoin = LineJoin.Round;

            //e.Graphics.DrawPath(pen, mousePath);

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
        public class PaintType
        {
            public string type { get; set; }
        }

        PaintType paintType = new PaintType();

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            paintType.type = "pen";
            toolStripButton3.BackColor = Color.CornflowerBlue;
            toolStripButton4.BackColor = Color.Empty;
            toolStripButton5.BackColor = Color.Empty;
            toolStripButton6.BackColor = Color.Empty;
            toolStripButton7.BackColor = Color.Empty;
            toolStripButton8.BackColor = Color.Empty;
            toolStripButton9.BackColor = Color.Empty;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            paintType.type = "bucket";
            toolStripButton3.BackColor = Color.Empty;
            toolStripButton4.BackColor = Color.CornflowerBlue;
            toolStripButton5.BackColor = Color.Empty;
            toolStripButton6.BackColor = Color.Empty;
            toolStripButton7.BackColor = Color.Empty;
            toolStripButton8.BackColor = Color.Empty;
            toolStripButton9.BackColor = Color.Empty;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            paintType.type = "spray";
            toolStripButton3.BackColor = Color.Empty;
            toolStripButton4.BackColor = Color.Empty;
            toolStripButton5.BackColor = Color.CornflowerBlue;
            toolStripButton6.BackColor = Color.Empty;
            toolStripButton7.BackColor = Color.Empty;
            toolStripButton8.BackColor = Color.Empty;
            toolStripButton9.BackColor = Color.Empty;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            paintType.type = "erase";
            toolStripButton3.BackColor = Color.Empty;
            toolStripButton4.BackColor = Color.Empty;
            toolStripButton5.BackColor = Color.Empty;
            toolStripButton6.BackColor = Color.CornflowerBlue;
            toolStripButton7.BackColor = Color.Empty;
            toolStripButton8.BackColor = Color.Empty;
            toolStripButton9.BackColor = Color.Empty;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            paintType.type = "rectangle";
            toolStripButton3.BackColor = Color.Empty;
            toolStripButton4.BackColor = Color.Empty;
            toolStripButton5.BackColor = Color.Empty;
            toolStripButton6.BackColor = Color.Empty;
            toolStripButton7.BackColor = Color.CornflowerBlue;
            toolStripButton8.BackColor = Color.Empty;
            toolStripButton9.BackColor = Color.Empty;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            paintType.type = "elipse";
            toolStripButton3.BackColor = Color.Empty;
            toolStripButton4.BackColor = Color.Empty;
            toolStripButton5.BackColor = Color.Empty;
            toolStripButton6.BackColor = Color.Empty;
            toolStripButton7.BackColor = Color.Empty;
            toolStripButton8.BackColor = Color.CornflowerBlue;
            toolStripButton9.BackColor = Color.Empty;
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            paintType.type = "line";
            toolStripButton3.BackColor = Color.Empty;
            toolStripButton4.BackColor = Color.Empty;
            toolStripButton5.BackColor = Color.Empty;
            toolStripButton6.BackColor = Color.Empty;
            toolStripButton7.BackColor = Color.Empty;
            toolStripButton8.BackColor = Color.Empty;
            toolStripButton9.BackColor = Color.CornflowerBlue;
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
