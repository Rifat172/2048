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
        public bool NewNumber = false;
        public Button Btn;
        private int number;


        public delegate void WinnderHandler(object sender, string message);
        public event WinnderHandler Notify;

        public int Number
        {
            get => number; set
            {
                switch (value)
                {
                    case 2:
                        Btn.BackColor = Color.Orange;
                        break;
                    case 4:
                        Btn.BackColor = Color.OrangeRed;
                        break;
                    case 8:
                        Btn.BackColor = Color.Orchid;
                        break;
                    case 16:
                        Btn.BackColor = Color.PaleGoldenrod;
                        break;
                    case 32:
                        Btn.BackColor = Color.PaleGreen;
                        break;
                    case 64:
                        Btn.BackColor = Color.PaleTurquoise;
                        break;
                    case 128:
                        Btn.BackColor = Color.PaleVioletRed;
                        break;
                    case 256:
                        Btn.BackColor = Color.PapayaWhip;
                        break;
                    case 512:
                        Btn.BackColor = Color.PeachPuff;
                        break;
                    case 1024:
                        Btn.BackColor = Color.Peru;
                        break;
                }
                if (value == 2048)
                {
                    Notify?.Invoke(this, "Вы собрали 2048");
                    Btn.BackColor = Color.Pink;
                }
                number = value;
            }
        }

        public Unit(int x, int y, int number)
        {
            Btn = new Button();
            Number = number;
            Btn.Location = new Point(x * 100, y * 100);
            Btn.Size = new Size(100, 100);
            Btn.Text = Number.ToString();
            Btn.Font = new Font("Arial", 16);
            Btn.Enabled = false;
        }
    }
}
