using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MicroWorld
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private int resolution;
        private ModelGame modelGame;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartGame();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawNextGen();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            StopGame();
        }

        private void StartGame()
        {
            if (timer1.Enabled)
                return;

            numericUpDown1.Enabled = false;
            numericUpDown2.Enabled = false;
            resolution = (int)numericUpDown1.Value;

            modelGame = new ModelGame
                (                   
                    pictureBox1.Height / resolution,
                    pictureBox1.Width / resolution,
                    (int)numericUpDown2.Minimum + (int)numericUpDown2.Maximum - (int)numericUpDown2.Value
                );
          
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(pictureBox1.Image);
            timer1.Start();
        }
        private void DrawNextGen()
        {
            graphics.Clear(Color.White);

            var Field = modelGame.GetGen();

            for (int x = 0; x < Field.GetLength(0); x++)
            {
                for (int y = 0; y < Field.GetLength(1); y++)
                {
                    if(Field[x,y])
                        graphics.FillRectangle(Brushes.Blue, x * resolution, y * resolution, resolution - 1, resolution - 1);
                }
            }           
            pictureBox1.Refresh();
            modelGame.NextGen();
        }
        private void StopGame()
        {
            if (!timer1.Enabled)
                return;

            timer1.Stop();
            numericUpDown1.Enabled = true;
            numericUpDown2.Enabled = true;
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!timer1.Enabled)
                return;
          
            if (e.Button == MouseButtons.Left)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                modelGame.AddCell(x, y);
            }
            if (e.Button == MouseButtons.Right)
            {
                var x = e.Location.X / resolution;
                var y = e.Location.Y / resolution;
                modelGame.RemoveCell(x, y);
            }          
        }
    }
}
