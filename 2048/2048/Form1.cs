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

            Units = new Unit[4, 4];

            GenerateBorder();

            GenerateBtn();
            GenerateBtn();
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

        private void swapPos(int x, int y, out Unit CurrentUnit, int j, int i)
        {
            Units[x, y] = Units[j, i];
            CurrentUnit = Units[j, i];
            Units[j, i] = null;
            CanMove = true;
        }
        private void swapPos(int x, int y, int j, int i)
        {
            Units[x, y] = Units[j, i];
            Units[j, i] = null;
            CanMove = true;
        }

        private void _Move(int xFin, int yFin, Button btn)
        {
            btn.Location = new Point(xFin, yFin);
        }

        private void CheckCanGenerateBtn()
        {
            if (CanMove)
            {
                GenerateBtn();
                CanMove = false;
            }
        }

        private void MoveRigth()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 3; j >= 0; j--)
                {
                    Unit CurrentUnit = null;
                    if (Units[j, i] != null)
                    {
                        if (CurrentUnit == null)
                        {
                            CurrentUnit = Units[j, i];
                            if (CurrentUnit.Btn.Location.X < MaxX)
                            {
                                _Move(MaxX, CurrentUnit.Btn.Location.Y, CurrentUnit.Btn);
                                swapPos(CurrentUnit.Btn.Location.X / 100, CurrentUnit.Btn.Location.Y / 100, j, i);
                            }
                            continue;
                        }
                        if (CurrentUnit.Number == Units[j, i].Number && CurrentUnit.NewNumber == false)
                        {
                            _Move(CurrentUnit.Btn.Location.X, Units[j, i].Btn.Location.Y, Units[j, i].Btn);

                            Units[j, i].Number = CurrentUnit.Number * 2;
                            Units[j, i].Btn.Text = Units[j, i].Number.ToString();
                            Units[j, i].NewNumber = true;

                            Controls.Remove(CurrentUnit.Btn);
                            Units[CurrentUnit.Btn.Location.X / 100, CurrentUnit.Btn.Location.Y / 100] = null;

                            swapPos(Units[j, i].Btn.Location.X / 100, Units[j, i].Btn.Location.Y / 100, out CurrentUnit, j, i);
                        }
                        else
                        {
                            int start = Units[j, i].Btn.Location.X;
                            int fin = CurrentUnit.Btn.Location.X - 100;

                            if (start == fin)
                            {
                                CurrentUnit = Units[j, i];
                                continue;
                            }

                            _Move(fin, CurrentUnit.Btn.Location.Y, Units[j, i].Btn);
                            swapPos((CurrentUnit.Btn.Location.X - 100) / 100, CurrentUnit.Btn.Location.Y / 100, out CurrentUnit, j, i);
                        }
                    }
                }
            }
            CheckCanGenerateBtn();
        }
        private void MoveDown()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 3; j >= 0; j--)
                {
                    Unit CurrentUnit = null;
                    if (Units[i, j] != null)
                    {
                        if (CurrentUnit == null)
                        {
                            CurrentUnit = Units[i, j];
                            if (CurrentUnit.Btn.Location.Y < MaxY)
                            {
                                _Move(CurrentUnit.Btn.Location.X, MaxY, CurrentUnit.Btn);

                                swapPos(CurrentUnit.Btn.Location.X / 100, CurrentUnit.Btn.Location.Y / 100, i, j);
                            }
                            continue;
                        }
                        if (CurrentUnit.Number == Units[i, j].Number && CurrentUnit.NewNumber == false)
                        {
                            _Move(Units[i, j].Btn.Location.X, CurrentUnit.Btn.Location.Y, Units[i, j].Btn);

                            Units[i, j].Number = CurrentUnit.Number * 2;
                            Units[i, j].Btn.Text = Units[i, j].Number.ToString();
                            Units[i, j].NewNumber = true;

                            Controls.Remove(CurrentUnit.Btn);
                            Units[CurrentUnit.Btn.Location.X / 100, CurrentUnit.Btn.Location.Y / 100] = null;

                            swapPos(Units[i, j].Btn.Location.X / 100, Units[i, j].Btn.Location.Y / 100, out CurrentUnit, i, j);
                        }
                        else
                        {
                            int start = Units[i, j].Btn.Location.Y;
                            int fin = CurrentUnit.Btn.Location.Y - 100;

                            if (start == fin)
                            {
                                CurrentUnit = Units[i, j];
                                continue;
                            }

                            _Move(Units[i, j].Btn.Location.X, fin, Units[i, j].Btn);
                            swapPos(CurrentUnit.Btn.Location.X / 100, (CurrentUnit.Btn.Location.Y - 100) / 100, out CurrentUnit, i, j);
                        }
                    }
                }
            }
            CheckCanGenerateBtn();
        }
        private void MoveLeft()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Unit CurrentUnit = null;
                    if (Units[j, i] != null)
                    {
                        if (CurrentUnit == null)
                        {
                            CurrentUnit = Units[j, i];
                            if (CurrentUnit.Btn.Location.X > 0)
                            {
                                _Move(0, CurrentUnit.Btn.Location.Y, CurrentUnit.Btn);
                                swapPos(CurrentUnit.Btn.Location.X / 100, CurrentUnit.Btn.Location.Y / 100, j, i);
                            }
                            continue;
                        }

                        if (CurrentUnit.Number == Units[j, i].Number && CurrentUnit.NewNumber == false)
                        {
                            _Move(CurrentUnit.Btn.Location.X, Units[j, i].Btn.Location.Y, Units[j, i].Btn);

                            Units[j, i].Number = CurrentUnit.Number * 2;
                            Units[j, i].Btn.Text = Units[j, i].Number.ToString();
                            Units[j, i].NewNumber = true;

                            Controls.Remove(CurrentUnit.Btn);
                            Units[CurrentUnit.Btn.Location.X / 100, CurrentUnit.Btn.Location.Y / 100] = null;

                            swapPos(Units[j, i].Btn.Location.X / 100, Units[j, i].Btn.Location.Y / 100, out CurrentUnit, j, i);
                        }
                        else
                        {
                            int start = Units[j, i].Btn.Location.X;
                            int fin = CurrentUnit.Btn.Location.X + 100;

                            if (start == fin)
                            {
                                CurrentUnit = Units[j, i];
                                continue;
                            }

                            _Move(fin, Units[j, i].Btn.Location.Y, Units[j, i].Btn);
                            swapPos(fin / 100, CurrentUnit.Btn.Location.Y / 100, out CurrentUnit, j, i);
                        }
                    }
                }
            }
            CheckCanGenerateBtn();
        }
        private void MoveUp()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    Unit CurrentUnit = null;
                    if (Units[i, j] != null)
                    {
                        if (CurrentUnit == null)
                        {
                            CurrentUnit = Units[i, j];
                            if (CurrentUnit.Btn.Location.Y > 0)
                            {
                                _Move(CurrentUnit.Btn.Location.X, 0, CurrentUnit.Btn);
                                swapPos(CurrentUnit.Btn.Location.X / 100, CurrentUnit.Btn.Location.Y / 100, i, j);
                            }
                            continue;
                        }
                        if (CurrentUnit.Number == Units[i, j].Number && CurrentUnit.NewNumber == false)
                        {
                            _Move(Units[i, j].Btn.Location.X, CurrentUnit.Btn.Location.Y, Units[i, j].Btn);

                            Units[i, j].Number = CurrentUnit.Number * 2;
                            Units[i, j].Btn.Text = Units[i, j].Number.ToString();
                            Units[i, j].NewNumber = true;

                            Controls.Remove(CurrentUnit.Btn);
                            Units[CurrentUnit.Btn.Location.X / 100, CurrentUnit.Btn.Location.Y / 100] = null;
                            //CurrentUnit = Units[j, i];

                            swapPos(Units[i, j].Btn.Location.X / 100, Units[i, j].Btn.Location.Y / 100, out CurrentUnit, i, j);
                            //Units[Units[j, i].Btn.Location.X / 100, Units[j, i].Btn.Location.Y / 100] = Units[j, i];
                            //CurrentUnit = Units[j, i];
                            //Units[j, i] = null;
                        }
                        else
                        {
                            int start = Units[i, j].Btn.Location.Y;
                            int fin = CurrentUnit.Btn.Location.Y + 100;

                            if (start == fin)
                            {
                                CurrentUnit = Units[i, j];
                                continue;
                            }

                            _Move(Units[i, j].Btn.Location.X, fin, Units[i, j].Btn);
                            swapPos(CurrentUnit.Btn.Location.X / 100, (CurrentUnit.Btn.Location.Y + 100) / 100, out CurrentUnit, i, j);
                            //Units[(CurrentUnit.Btn.Location.X - 100) / 100, CurrentUnit.Btn.Location.Y / 100] = Units[j, i];
                            //CurrentUnit = Units[j, i];
                            //Units[j, i] = null;
                        }
                    }
                }
            }
            CheckCanGenerateBtn();
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
        private void GenerateBorder()
        {
            for (int i = 0; i < 4; i++)
            {
                PictureBox pb = new PictureBox();
                pb.Location = new Point(0, 0 + (i * 100));
                pb.Size = new Size(1, 400);
                pb.BackColor = Color.Black;
                Controls.Add(pb);
            }
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