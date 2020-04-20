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
            int y = 0;
            //Units[0, y] = new Unit(0, y, 1024);
            //Units[1, y] = new Unit(1, y, 1024);
            //Units[2, y] = new Unit(2, y, 1024);
            //Units[3, y] = new Unit(3, y, 1024);

            //Units[0, y].Notify += DisplayMessage;
            //Units[1, y].Notify += DisplayMessage;
            //Units[2, y].Notify += DisplayMessage;
            //Units[3, y].Notify += DisplayMessage;

            //Controls.Add(Units[0, y].Btn);
            //Controls.Add(Units[1, y].Btn);
            //Controls.Add(Units[2, y].Btn);
            //Controls.Add(Units[3, y].Btn);

            y = 1;
            Units[1, 1] = new Unit(1, 1, 2);
            Units[1, 2] = new Unit(1, 2, 4);
            //Units[2, y] = new Unit(2, y, 8);
            //Units[3, y] = new Unit(3, y, 16);

            Units[1, 1].Notify += DisplayMessage;
            Units[1, 2].Notify += DisplayMessage;
            //Units[2, y].Notify += DisplayMessage;
            //Units[3, y].Notify += DisplayMessage;

            Controls.Add(Units[1, 1].Btn);
            Controls.Add(Units[1, 2].Btn);
            //Controls.Add(Units[2, y].Btn);
            //Controls.Add(Units[3, y].Btn);
        }
        private void DisplayMessage(object sender, string message)
        {
            (sender as Unit).Btn.Text = "2048";
            MessageBox.Show(message);
            foreach (var un in Units)
            {
                if (un != null)
                    un.Notify -= DisplayMessage;
            }
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

                            swapPos(Units[j, i].Btn.Location.X / 100, Units[j, i].Btn.Location.Y / 100, out temp, j, i);
                        }
                        else
                        {
                            int start = Units[j, i].Btn.Location.X;
                            int fin = temp.Btn.Location.X - 100;
                            if (start == fin)
                                continue;

                            _MoveRight(start, fin, Units[j, i].Btn);
                            swapPos((temp.Btn.Location.X - 100) / 100, temp.Btn.Location.Y / 100, out temp, j, i);
                        }
                    }
                }
                temp = null;
            }
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
            Unit temp = null;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 3; j >= 0; j--)
                {
                    if (Units[i, j] != null)
                    {
                        Units[i, j].Btn.BringToFront();
                        if (temp == null)
                        {
                            temp = Units[i, j];
                            if (temp.Btn.Location.Y < MaxY)
                            {
                                _MoveDown(temp.Btn.Location.Y, MaxY, temp.Btn);

                                swapPos(temp.Btn.Location.X / 100, temp.Btn.Location.Y / 100, i, j);
                            }
                            continue;
                        }
                        if (temp.Number == Units[i, j].Number)
                        {
                            _MoveDown(Units[i, j].Btn.Location.Y, temp.Btn.Location.Y, Units[i, j].Btn);

                            Units[i, j].Number = temp.Number * 2;
                            Units[i, j].Btn.Text = Units[i, j].Number.ToString();

                            Controls.Remove(temp.Btn);
                            Units[temp.Btn.Location.X / 100, temp.Btn.Location.Y / 100] = null;

                            swapPos(Units[i, j].Btn.Location.X / 100, Units[i, j].Btn.Location.Y / 100, out temp, i, j);
                        }
                        else
                        {
                            int start = Units[i, j].Btn.Location.Y;
                            int fin = temp.Btn.Location.Y - 100;
                            if (start == fin)
                                continue;

                            _MoveDown(start, fin, Units[i, j].Btn);
                            swapPos(temp.Btn.Location.X / 100, (temp.Btn.Location.Y - 100) / 100, out temp, i, j);
                        }
                    }
                }
                temp = null;
            }
        }
        private void _MoveDown(int start, int finish, Button btn)
        {
            for (int y = start; y <= finish; y += 10)
            {
                btn.Location = new Point(btn.Location.X, y);
                Thread.Sleep(1);
            }
        }

        private void MoveLeft()
        {
            Unit temp = null;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Units[j, i] != null)
                    {
                        Units[j, i].Btn.BringToFront();
                        if (temp == null)
                        {
                            temp = Units[j, i];
                            if (temp.Btn.Location.X > 0)
                            {
                                _MoveLeft(temp.Btn.Location.X, 0, temp.Btn);
                                swapPos(temp.Btn.Location.X / 100, temp.Btn.Location.Y / 100, j, i);
                            }
                            continue;
                        }

                        if (temp.Number == Units[j, i].Number)
                        {
                            _MoveLeft(Units[j, i].Btn.Location.X, temp.Btn.Location.X, Units[j, i].Btn);

                            Units[j, i].Number = temp.Number * 2;
                            Units[j, i].Btn.Text = Units[j, i].Number.ToString();

                            Controls.Remove(temp.Btn);
                            Units[temp.Btn.Location.X / 100, temp.Btn.Location.Y / 100] = null;

                            swapPos(Units[j, i].Btn.Location.X / 100, Units[j, i].Btn.Location.Y / 100, out temp, j, i);
                        }
                        else
                        {
                            int start = Units[j, i].Btn.Location.X;
                            int fin = temp.Btn.Location.X + 100;
                            if (start == fin)
                                continue;

                            _MoveLeft(start, fin, Units[j, i].Btn);
                            swapPos(fin / 100, temp.Btn.Location.Y / 100, out temp, j, i);
                        }
                    }
                }
                temp = null;
            }
        }
        private void _MoveLeft(int start, int finish, Button btn)
        {
            for (int x = start; x >= finish; x -= 10)
            {
                btn.Location = new Point(x, btn.Location.Y);
                Thread.Sleep(1);
            }
        }

        private void MoveUp()
        {
            Unit temp = null;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Units[i, j] != null)
                    {
                        Units[i, j].Btn.BringToFront();
                        if (temp == null)
                        {
                            temp = Units[i, j];
                            if (temp.Btn.Location.Y > 0)
                            {
                                _MoveUp(temp.Btn.Location.Y, 0, temp.Btn);
                                swapPos(temp.Btn.Location.X / 100, temp.Btn.Location.Y / 100, i, j);
                            }
                            continue;
                        }
                        if (temp.Number == Units[i, j].Number)
                        {
                            _MoveUp(Units[i, j].Btn.Location.Y, temp.Btn.Location.Y, Units[i, j].Btn);

                            Units[i, j].Number = temp.Number * 2;
                            Units[i, j].Btn.Text = Units[i, j].Number.ToString();

                            Controls.Remove(temp.Btn);
                            Units[temp.Btn.Location.X / 100, temp.Btn.Location.Y / 100] = null;
                            //temp = Units[j, i];

                            swapPos(Units[i, j].Btn.Location.X / 100, Units[i, j].Btn.Location.Y / 100, out temp, i, j);
                            //Units[Units[j, i].Btn.Location.X / 100, Units[j, i].Btn.Location.Y / 100] = Units[j, i];
                            //temp = Units[j, i];
                            //Units[j, i] = null;
                        }
                        else
                        {
                            int start = Units[i, j].Btn.Location.Y;
                            int fin = temp.Btn.Location.Y + 100;

                            if (start == fin)
                                continue;

                            _MoveUp(start, fin, Units[i, j].Btn);
                            swapPos(temp.Btn.Location.X / 100, (temp.Btn.Location.Y + 100) / 100, out temp, i, j);
                            //Units[(temp.Btn.Location.X - 100) / 100, temp.Btn.Location.Y / 100] = Units[j, i];
                            //temp = Units[j, i];
                            //Units[j, i] = null;
                        }
                    }
                }
                temp = null;
            }
        }
        private void _MoveUp(int start, int finish, Button btn)
        {
            for (int y = start; y >= finish; y -= 10)
            {
                btn.Location = new Point(btn.Location.X, y);
                Thread.Sleep(1);
            }
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