using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BubbleBreaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoadGame(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog
            {
                Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.svg; *.gif)|*.jpg; *.jpeg; *.gif; *.bmp; *.png; *.svg; *.gif"
            };
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.BackgroundImage = new Bitmap(Image.FromFile(openDialog.FileName));

                /*TableLayoutControlCollection controls = tableLayoutPanel1.Controls;
                for (int i = 0; i < controls.Count; i++)
                {
                    if (controls[i] is Label)
                    {
                        controls[i].BackgroundImage = new Bitmap(Image.FromFile(openDialog.FileName));
                        controls[i].Text = "test" + i;
                    }
                }*/
            }
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.CreateControl();
            tableLayoutPanel1.SetColumn(tableLayoutPanel1.Controls[0], 5);
        }
    }
}
