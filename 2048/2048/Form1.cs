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

        private bool CanMove = false;

        int MaxY = 300;
        int MaxX = 300;

        public Form1()
        {
            InitializeComponent();
            timer1.Start();
            timer1.Tick += Timer1_Tick;

            Units = new Unit[4, 4];

            GenerateBtn();
            GenerateBtn();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.Second.ToString();
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

        private void swapPos(int x, int y, out Unit temp, int j, int i)
        {
            Units[x, y] = Units[j, i];
            temp = Units[j, i];
            Units[j, i] = null;
            CanMove = true;
        }
        private void swapPos(int x, int y, int j, int i)
        {
            Units[x, y] = Units[j, i];
            Units[j, i] = null;
            CanMove = true;
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
                        if (temp.Number == Units[j, i].Number && temp.NewNumber == false)
                        {
                            _MoveRight(Units[j, i].Btn.Location.X, temp.Btn.Location.X, Units[j, i].Btn);

                            Units[j, i].Number = temp.Number * 2;
                            Units[j, i].Btn.Text = Units[j, i].Number.ToString();
                            Units[j, i].NewNumber = true;

                            Controls.Remove(temp.Btn);
                            Units[temp.Btn.Location.X / 100, temp.Btn.Location.Y / 100] = null;

                            swapPos(Units[j, i].Btn.Location.X / 100, Units[j, i].Btn.Location.Y / 100, out temp, j, i);
                        }
                        else
                        {
                            int start = Units[j, i].Btn.Location.X;
                            int fin = temp.Btn.Location.X - 100;

                            if (start == fin)
                            {
                                temp = Units[j, i];
                                continue;
                            }

                            _MoveRight(start, fin, Units[j, i].Btn);
                            swapPos((temp.Btn.Location.X - 100) / 100, temp.Btn.Location.Y / 100, out temp, j, i);
                        }
                    }
                }
                temp = null;
            }
            if (CanMove)
            {
                GenerateBtn();
                CanMove = false;
            }
        }
        private void _MoveRight(int start, int finish, Button btn)
        {
            for (int x = start; x <= finish; x += 10)
            {
                btn.Location = new Point(x, btn.Location.Y);
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
                        if (temp.Number == Units[i, j].Number && temp.NewNumber == false)
                        {
                            _MoveDown(Units[i, j].Btn.Location.Y, temp.Btn.Location.Y, Units[i, j].Btn);

                            Units[i, j].Number = temp.Number * 2;
                            Units[i, j].Btn.Text = Units[i, j].Number.ToString();
                            Units[i, j].NewNumber = true;

                            Controls.Remove(temp.Btn);
                            Units[temp.Btn.Location.X / 100, temp.Btn.Location.Y / 100] = null;

                            swapPos(Units[i, j].Btn.Location.X / 100, Units[i, j].Btn.Location.Y / 100, out temp, i, j);
                        }
                        else
                        {
                            int start = Units[i, j].Btn.Location.Y;
                            int fin = temp.Btn.Location.Y - 100;

                            if (start == fin)
                            {
                                temp = Units[i, j];
                                continue;
                            }

                            _MoveDown(start, fin, Units[i, j].Btn);
                            swapPos(temp.Btn.Location.X / 100, (temp.Btn.Location.Y - 100) / 100, out temp, i, j);
                        }
                    }
                }
                temp = null;
            }
            if (CanMove)
            {
                GenerateBtn();
                CanMove = false;
            }
        }
        private void _MoveDown(int start, int finish, Button btn)
        {
            for (int y = start; y <= finish; y += 10)
            {
                btn.Location = new Point(btn.Location.X, y);
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

                        if (temp.Number == Units[j, i].Number && temp.NewNumber == false)
                        {
                            _MoveLeft(Units[j, i].Btn.Location.X, temp.Btn.Location.X, Units[j, i].Btn);

                            Units[j, i].Number = temp.Number * 2;
                            Units[j, i].Btn.Text = Units[j, i].Number.ToString();
                            Units[j, i].NewNumber = true;

                            Controls.Remove(temp.Btn);
                            Units[temp.Btn.Location.X / 100, temp.Btn.Location.Y / 100] = null;

                            swapPos(Units[j, i].Btn.Location.X / 100, Units[j, i].Btn.Location.Y / 100, out temp, j, i);
                        }
                        else
                        {
                            int start = Units[j, i].Btn.Location.X;
                            int fin = temp.Btn.Location.X + 100;

                            if (start == fin)
                            {
                                temp = Units[j, i];
                                continue;
                            }

                            _MoveLeft(start, fin, Units[j, i].Btn);
                            swapPos(fin / 100, temp.Btn.Location.Y / 100, out temp, j, i);
                        }
                    }
                }
                temp = null;
            }
            if (CanMove)
            {
                GenerateBtn();
                CanMove = false;
            }
        }
        private void _MoveLeft(int start, int finish, Button btn)
        {
            for (int x = start; x >= finish; x -= 10)
            {
                btn.Location = new Point(x, btn.Location.Y);
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
                        if (temp.Number == Units[i, j].Number && temp.NewNumber == false)
                        {
                            _MoveUp(Units[i, j].Btn.Location.Y, temp.Btn.Location.Y, Units[i, j].Btn);

                            Units[i, j].Number = temp.Number * 2;
                            Units[i, j].Btn.Text = Units[i, j].Number.ToString();
                            Units[i, j].NewNumber = true;

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
                            {
                                temp = Units[i, j];
                                continue;
                            }

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
            if (CanMove)
            {
                GenerateBtn();
                CanMove = false;
            }
        }
        private void _MoveUp(int start, int finish, Button btn)
        {
            for (int y = start; y >= finish; y -= 10)
            {
                btn.Location = new Point(btn.Location.X, y);
            }
        }

        private void GenerateBtn()
        {
            foreach (var u in Units)
                if (u != null)
                    u.NewNumber = false;
            Random rand = new Random();
            int x = rand.Next(0, 3);
            int y = rand.Next(0, 3);
            while (Units[x, y] != null)
            {
                x = rand.Next(0, 3);
                y = rand.Next(0, 3);
            }

            Units[x, y] = new Unit(x, y, 2);
            Units[x, y].Notify += DisplayMessage;

            Controls.Add(Units[x, y].Btn);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
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
        }
    }
}