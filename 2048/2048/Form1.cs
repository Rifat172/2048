using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048
{
    public partial class Form1 : Form
    {
        private int _Height = 400;
        private int _Width = 400;
        private List<Button> Buttons;

        public Form1()
        {
            InitializeComponent();
            GenerateBorder();

            Buttons = new List<Button>();
            
            GenerateButton();
            GenerateButton();
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {

            //Rectangle r1 = button1.DisplayRectangle;
            //Rectangle r2 = button2.DisplayRectangle;
            //r1.Location = button1.Location;
            //r2.Location = button2.Location;

            //if (r1.IntersectsWith(r2))
            //    MessageBox.Show("yes");
            //else
            //    MessageBox.Show("no");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    MoveLeft();
                    break;
                case Keys.Up:
                    MoveUp();
                    break;
                case Keys.Right:
                    MoveRight();
                    break;
                case Keys.Down:
                    MoveDown();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void GenerateBorder()
        {
            for (int i = 0; i < 5; i++)
            {
                PictureBox picture = new PictureBox();
                picture.Location = new Point(0 + (i * 100), 0);
                picture.Width = 1;
                picture.Height = 400;
                picture.BackColor = Color.Black;
                Controls.Add(picture);
            }
            for (int i = 0; i < 5; i++)
            {
                PictureBox picture = new PictureBox();
                picture.Location = new Point(0, 0 + (i * 100));
                picture.Width = 400;
                picture.Height = 1;
                picture.BackColor = Color.Black;
                Controls.Add(picture);
            }
        }
        private void GenerateButton()
        {
            int x, y;
            Random rand = new Random();
            x = rand.Next(0, 3) * 100;
            y = rand.Next(0, 3) * 100;

            Button b1 = new Button();
            b1.Location = new Point(x, y);
            b1.BackColor = Color.White;
            b1.Size = new Size(100, 100);
            b1.Text = "2";
            Controls.Add(b1);
            Buttons.Add(b1);
        }
        async private void MoveRight()
        {
            foreach(var b1 in Buttons)
            {
                for (int x = b1.Location.X; x <= 300; x += 10)
                {
                    b1.Location = new Point(x, b1.Location.Y);
                    await Task.Delay(1);
                }
            }
        }
        async private void MoveLeft()
        {
            foreach (var b1 in Buttons)
            {
                for (int x = b1.Location.X; x >= 0; x -= 10)
                {
                    b1.Location = new Point(x, b1.Location.Y);
                    await Task.Delay(1);
                }
            }
        }
        async private void MoveUp()
        {
            foreach (var b1 in Buttons)
            {
                for (int y = b1.Location.Y; y >= 0; y -= 10)
                {
                    b1.Location = new Point(b1.Location.X, y);
                    await Task.Delay(1);
                }
            }
        }
        async private void MoveDown()
        {
            foreach (var b1 in Buttons)
            {
                for (int y = b1.Location.Y; y <= 300; y += 10)
                {
                    b1.Location = new Point(b1.Location.X, y);
                    await Task.Delay(1);
                }
            }
        }
    }
}
