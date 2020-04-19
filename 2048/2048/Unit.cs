using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2048
{
    public class Unit
    {
        public int Number;
        public Button Btn;

        public Unit(int x, int y, int number)
        {
            Btn = new Button();
            Number = number;
            Btn.Location = new Point(x * 100, y * 100);
            Btn.Size = new Size(100, 100);
            Btn.Text = Number.ToString();
        }
    }
}
