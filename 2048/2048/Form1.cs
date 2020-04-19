using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048
{
    public partial class Form1 : Form
    {
        private Unit[,] Units;

        int MaxY = 300;
        int MaxX = 300;

        public Form1()
        {
            InitializeComponent();
            GenerateBorder();

            Units = new Unit[4, 4];

            int x1 = 3;
            int x2 = 1;
            int y = 0;
            Units[x1, y] = new Unit(x1, y, 2);
            //Units[x2, y] = new Unit(x2, y, 2);
            //Units[2, y] = new Unit(2, y, 2);

            Controls.Add(Units[x1, y].Btn);
            //Controls.Add(Units[x2, y].Btn);
            //Controls.Add(Units[2, y].Btn);
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
                    MoveRigth();
                    break;
                case Keys.Down:
                    MoveDown();
                    break;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void MoveLeft()
        {

        }

        private void MoveRigth()
        {
            Unit temp = null;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 3; j >= 0; j--)
                {
                    if (Units[j, i] != null)
                    {
                        Units[j, i].Btn.BringToFront();
                        if (temp == null)
                        {
                            temp = Units[j, i];
                            if (temp.Btn.Location.X < MaxX)
                            {
                                _MoveRight(temp.Btn.Location.X, MaxX, temp.Btn);

                                swapPos(temp.Btn.Location.X / 100, temp.Btn.Location.Y / 100, j, i);
                                //Units[temp.Btn.Location.X / 100, temp.Btn.Location.Y / 100] = temp;
                                //Units[j, i] = null;
                            }
                            continue;
                        }
                        if (temp.Number == Units[j, i].Number)
                        {
                            _MoveRight(Units[j, i].Btn.Location.X, temp.Btn.Location.X, Units[j, i].Btn);

                            Units[j, i].Number = temp.Number * 2;
                            Units[j, i].Btn.Text = Units[j, i].Number.ToString();

                            Controls.Remove(temp.Btn);
                            Units[temp.Btn.Location.X / 100, temp.Btn.Location.Y / 100] = null;
                            //temp = Units[j, i];

                            swapPos(Units[j, i].Btn.Location.X / 100, Units[j, i].Btn.Location.Y / 100, out temp, j, i);
                            //Units[Units[j, i].Btn.Location.X / 100, Units[j, i].Btn.Location.Y / 100] = Units[j, i];
                            //temp = Units[j, i];
                            //Units[j, i] = null;
                        }
                        else
                        {
                            int start = Units[j, i].Btn.Location.X;
                            int fin = temp.Btn.Location.X - 100;
                            if (start == fin)
                                continue;
                            _MoveRight(start, fin, Units[j, i].Btn);


                            swapPos((temp.Btn.Location.X - 100) / 100, temp.Btn.Location.Y / 100, out temp, j, i);
                            //Units[(temp.Btn.Location.X - 100) / 100, temp.Btn.Location.Y / 100] = Units[j, i];
                            //temp = Units[j, i];
                            //Units[j, i] = null;
                        }
                    }
                }
                temp = null;
            }
        }

        private void swapPos(int x, int y, out Unit temp, int j, int i)
        {
            Units[x, y] = Units[j, i];
            temp = Units[j, i];
            Units[j, i] = null;
        }
        private void swapPos(int x, int y, int j, int i)
        {
            Units[x, y] = Units[j, i];
            Units[j, i] = null;
        }

        private void _MoveRight(int start, int finish, Button btn)
        {
            for (int x = start; x <= finish; x += 10)
            {
                btn.Location = new Point(x, btn.Location.Y);
                Thread.Sleep(1);
            }
        }



        private void MoveDown()
        {

        }

        private void MoveUp()
        {

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



        private void Button_Click(object sender, EventArgs e)
        {
            MessageBox.Show(((Button)sender).Text);
        }
    }
}

//Rectangle r1 = button1.DisplayRectangle;
//Rectangle r2 = button2.DisplayRectangle;
//r1.Location = button1.Location;
//r2.Location = button2.Location;

//if (r1.IntersectsWith(r2))
//    MessageBox.Show("yes");
//else
//    MessageBox.Show("no");

//private void initbtn()
//{
//    for (int y = 0; y < 4; y++)
//    {
//        for (int x = 0; x < 4; x++)
//        {
//            if (num[x, y] != 0)
//            {
//                Button button = new Button();
//                button.Location = new Point(x * 100, y * 100);
//                button.Size = new Size(100, 100);
//                button.Click += Button_Click;
//                button.Text = num[x, y].ToString();
//                Controls.Add(button);
//            }
//        }
//    }
//}