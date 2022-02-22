using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexeso
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            var test = tableLayoutPanel1.GetControlFromPosition(0,0);
            var test1 = tableLayoutPanel1.RowCount * tableLayoutPanel1.ColumnCount;
            var controls = tableLayoutPanel1.Controls;
            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
            {
                for (int j = 0; j < tableLayoutPanel1.ColumnCount; j++)
                {
                    var every = tableLayoutPanel1.GetControlFromPosition(j, i);
                }
            }

                for (int i = 0; i < controls.Count; i++)
            {
                var pain = controls[i].Controls.Owner;
                var tmp = controls[i];
            }

            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                Console.WriteLine(ctrl);
                OpenFileDialog openDialog = new OpenFileDialog
                {
                    Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.svg; *.gif)|*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.svg; *.gif"
                };
                if (openDialog.ShowDialog() == DialogResult.OK)
                {
                    tableLayoutPanel1.GetControlFromPosition(0, 3).BackgroundImage = Image.FromFile(openDialog.FileName);
                    //tableLayoutPanel1.GetControlFromPosition(0, 3).BackgroundImage = Image.FromFile(@"C:\\Users\\bukovsky\\Downloads\\repos - main(1)\\repos\\Pexeso\\Pexeso\\Imgs\\arci.png");
                    //tableLayoutPanel1.GetControlFromPosition(0, 3).BackgroundImage = Image.FromFile(@"C:\\Users\\Oskar\\source\\repos\\Pexeso\\Pexeso\\Imgs\\arci.png");
                }
            }
            //pictureBox1.ImageLocation = @"C:\\Users\\Oskar\\source\\repos\\Pexeso\\Pexeso\\Imgs\\arci.png";
            Console.WriteLine(test);

            //pictureBox1.ImageLocation = Path.Combine(System.Windows.Forms.Application.StartupPath, "....\\Imgs\\arci.jpg");
            /*
             * static Image[] s_Images = new string[] {
     "cherry.jpg",
     "bell.jpg",
     "lemon.jpg", 
     "orange.jpg",
     "star.jpg", 
     "skull.jpg"}
  .Select(file => Path.Combine(@"C:\Users\seanb\OneDrive\Pictures", file))
  .Select(file => Image.FromFile(file)) 
  .ToArray();  

static Random random = new Random();
And you want to assign these images to the picture boxes randomly:

private void timer1_Tick(object sender, EventArgs e) {
  foreach (PictureBox box in new PictureBox[] { pictureBox1, pictureBox2, pictureBox2 }) {
    box.Image = s_Images[random.Next(s_Images.Length)];
  }
}
             */
        }
    }
}
