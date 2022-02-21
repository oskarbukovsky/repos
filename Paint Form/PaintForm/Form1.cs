using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PaintForm
{
    public partial class Malování : Form
    {
        public Malování()
        {
            InitializeComponent();

            LoadPanel();

            // Make the pen selected by default
            selectedShapeButton = PenButton;
            selectedShapeButton.BackColor = Color.Red;

            //Update some images and values
            ForegroundColorPicker.Image = null;
            BackgroundColorPicker.Image = null;
            SizeUpDown.Text = "" + SizeBar.Value;
        }

        Bitmap paintImage;
        Graphics paintGraphics;

        private void LoadPanel()
        {
            int width = DrawingArea.Width;
            int height = DrawingArea.Height;

            paintImage = new Bitmap(width, height);
            DrawingArea.DrawToBitmap(paintImage, new Rectangle(0, 0, width, height));

            paintGraphics = Graphics.FromImage(paintImage);

            //paintGraphics.FillRectangle(Brushes.White, 0, 0, width, height);

            DrawingArea.BackgroundImage = paintImage;

            DrawingArea.MouseDown += new MouseEventHandler(Panel1_MouseDown);
            DrawingArea.MouseMove += new MouseEventHandler(Panel1_MouseMove);
            DrawingArea.MouseUp += new MouseEventHandler(Panel1_MouseUp);
        }

        Point lastPoint;
        bool isMouseDown = false;
        MouseButtons mouseButtonType = MouseButtons.Left;

        void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = e.Location;
            mouseButtonType = e.Button;
            isMouseDown = true;
        }

        void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                if (selectedShapeButton.Text == "Pen")
                {
                    DrawLineInCanvas(e.Location);
                }
                else
                {
                    DrawShapeInWorkingImage(e.Location);
                }
            }
        }

        void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;

            if (selectedShapeButton.Text != "Pen")
            {
                // Draw again to get the shape without border
                DrawShapeInWorkingImage(e.Location);

                paintImage = new Bitmap(workingImage);

                // Re initialize graphics object as the bitmap is new
                paintGraphics = Graphics.FromImage(paintImage);

                DrawingArea.BackgroundImage = paintImage;
                GC.Collect();
            }
        }

        private void DrawLineInCanvas(Point currentPoint)
        {
            Pen pen;
            if (mouseButtonType == MouseButtons.Left)
            {
                // Form pen with the color selected and the size value in tracker
                pen = new Pen(ForegroundColorPicker.BackColor, SizeBar.Value);
            }
            else
            {
                pen = new Pen(BackgroundColorPicker.BackColor, SizeBar.Value);
            }

            paintGraphics.DrawLine(pen, lastPoint, currentPoint);

            lastPoint = currentPoint;

            DrawingArea.Refresh();
        }

        private readonly ColorDialog ForegroundColorPickerDialog = new ColorDialog();
        private readonly ColorDialog BackgroundColorPickerDialog = new ColorDialog();

        private void ForegroundColorPickerButton_Click(object sender, EventArgs e)
        {
            ForegroundColorPickerDialog.ShowDialog();
            ForegroundColorPicker.BackColor = ForegroundColorPickerDialog.Color;
        }

        private void BackgroundColorPickerButton_Click(object sender, EventArgs e)
        {
            BackgroundColorPickerDialog.ShowDialog();
            BackgroundColorPicker.BackColor = BackgroundColorPickerDialog.Color;
        }

        Bitmap workingImage;
        Graphics workingGraphics;

        private void DrawShapeInWorkingImage(Point currentPoint)
        {
            Pen pen;
            if (mouseButtonType == MouseButtons.Left)
            {
                // Form pen with the color selected and the size value in tracker
                pen = new Pen(ForegroundColorPicker.BackColor, SizeBar.Value);
            }
            else
            {
                pen = new Pen(BackgroundColorPicker.BackColor, SizeBar.Value);
            }

            workingImage = new Bitmap(paintImage);
            workingGraphics = Graphics.FromImage(workingImage);

            int startPointX = lastPoint.X < currentPoint.X ? lastPoint.X : currentPoint.X;
            int startPointY = lastPoint.Y < currentPoint.Y ? lastPoint.Y : currentPoint.Y;

            int shapeWidth = (lastPoint.X > currentPoint.X ? lastPoint.X : currentPoint.X) - startPointX;
            int shapeHeight = (lastPoint.Y > currentPoint.Y ? lastPoint.Y : currentPoint.Y) - startPointY;

            switch (selectedShapeButton.Text)
            {
                case "Rectangle":
                    // Check if it is to draw or fill shape
                    if (!FillCheckbox.Checked)
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

                case "Circle":
                    if (!FillCheckbox.Checked)
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

                    if (!FillCheckbox.Checked)
                    {
                        workingGraphics.DrawPolygon(pen, new Point[] { point1, point2, point3 });
                    }
                    else
                    {
                        workingGraphics.FillPolygon(pen.Brush, new Point[] { point1, point2, point3 });
                    }
                    break;

                case "Line":
                    workingGraphics.DrawLine(pen, lastPoint, currentPoint);
                    break;
            }

            // The outline should be shown only if it is not a line and the drawing is on. 
            if (isMouseDown && selectedShapeButton.Text != "Line")
            {
                // Draw outline while drawing shapes
                Pen outLinePen = new Pen(ForegroundColorPicker.BackColor)
                {
                    DashStyle = System.Drawing.Drawing2D.DashStyle.Dash
                };

                workingGraphics.DrawRectangle(outLinePen, startPointX, startPointY, shapeWidth, shapeHeight);
            }

            DrawingArea.BackgroundImage = workingImage;
        }

        Button selectedShapeButton;

        private void PenButtond_Click(object sender, EventArgs e)
        {
            selectedShapeButton.BackColor = SystemColors.Control;

            Button clickedButton = sender as Button;
            clickedButton.BackColor = Color.Red;

            selectedShapeButton = clickedButton;
        }

        private void SizeBar_ValueChanged(object sender, EventArgs e)
        {
            SizeUpDown.Text = "" + SizeBar.Value;
        }
        private void SizeUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (SizeUpDown.Text != String.Empty)
            {
                if (Convert.ToInt32(SizeUpDown.Text) != 0)
                {
                    if (Convert.ToInt32(SizeUpDown.Text) > 150)
                    {
                        SizeBar.Value = SizeBar.Maximum;
                    }
                    else
                    {
                        SizeBar.Value = Convert.ToInt32(SizeUpDown.Text);
                    }
                }
                else
                {
                    SizeBar.Value = SizeBar.Minimum;
                }
            }
            else
            {
                SizeBar.Value = SizeBar.Minimum;
            }
        }

        private void FileNew(object sender, EventArgs e)
        {
            paintImage = new Bitmap(DrawingArea.Width, DrawingArea.Height);
            paintGraphics = Graphics.FromImage(paintImage);
            DrawingArea.BackgroundImage = paintImage;
            GC.Collect();
        }
        private void FileOpen(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openDialog = new OpenFileDialog
                {
                    Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.svg; *.gif)|*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.svg; *.gif"
                };
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    paintImage = new Bitmap(Image.FromFile(openDialog.FileName));
                    paintGraphics = Graphics.FromImage(paintImage);
                    DrawingArea.BackgroundImage = paintImage;
                    GC.Collect();
                }
            }
            catch (Exception)
            {
                throw new ApplicationException("Failed loading image");
            }
        }

        private void DrawingArea_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap bitmap = new Bitmap(DrawingArea.Width, DrawingArea.Height);
            DrawingArea.DrawToBitmap(bitmap, new Rectangle(0, 0, DrawingArea.Width, DrawingArea.Height));
            e.Graphics.DrawImage(bitmap, 0, 0);
            bitmap.Dispose();
        }

        private void FilePrint(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(DrawingArea.Width, DrawingArea.Height);
            DrawingArea.DrawToBitmap(bmp, new Rectangle(0, 0, DrawingArea.Width, DrawingArea.Height));
            try
            {
                PrintDocument print = new PrintDocument();
                PrintDialog printDialog = new PrintDialog();
                print.PrintPage += new PrintPageEventHandler(DrawingArea_PrintPage);
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
        private void FileSave(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(DrawingArea.Width, DrawingArea.Height);
            DrawingArea.DrawToBitmap(bmp, new Rectangle(0, 0, DrawingArea.Width, DrawingArea.Height));
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "Png Image (.png)|*.png|JPG Image (.jpg)|*.jpg|Bitmap Image (.bmp)|*.bmp|Gif Image (.gif)|*.gif"
                };

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
