using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Billi
{
    public partial class BallOut : Form
    {
        string st;
        public BallOut(string S)
        {
           
            st = S;
            InitializeComponent();
            this.Opacity = 0.4;
        }

        private void BallOut_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(this.Owner.Location.X + this.Owner.Width / 3, this.Owner.Location.Y + this.Owner.Height / 3);
            
            label1.Text = st;
            this.Width = label1.Width + 50;
            t.Interval = 1000;
            t.Tick += new EventHandler(Tickkk);
            t.Start();
        }
        Timer t = new Timer();
        int Col = 0;
        private void Tickkk(object c, EventArgs e)
        {
            if (++Col > 2) { t.Stop();  this.Close(); }
        }
    }
}
