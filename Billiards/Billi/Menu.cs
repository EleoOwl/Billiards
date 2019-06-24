using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Billi
{
    public partial class Menu : Form
    {
        public System.Drawing.Point Loc;
        public Menu(Image a)
        {
            InitializeComponent();
           // this.BackgroundImageLayout = ImageLayout.Zoom;
           // this.BackgroundImage = a;
            
        }
        PictureBox hole = new PictureBox();
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST) m.Result = (IntPtr)(HT_CAPTION);
        }

        private string TT="";
        private void Menu_Load(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(@"..\..\..\Text.txt", Encoding.Default);
            TT = sr.ReadToEnd();
            //while (!sr.EndOfStream)
            //{
            //    TT += sr.ReadLine();
            //}
            sr.Close();
            //this.Opacity = 0.5;
            this.Location = Owner.Location;
            this.Width = Owner.Width ;
            this.Height = Owner.Height;
            button1.Height= button2.Height = button3.Height =  button4.Height = this.Height / 4;
            button1.Width = button2.Width = button3.Width =  button4.Width = this.Width/3;
            button6.Visible = false;
            for (int i = 1; i <5; i++)
                (Controls["button" + i] as Button).Location = new System.Drawing.Point(0,(i-1) * button1.Height);
            hole.Size = new Size(this.Width - button1.Width, this.Height);
            hole.Location = new System.Drawing.Point( button1.Width, 0);
            //hole.Image = global::Billi.Properties.Resources.table3;
            hole.BackColor = System.Drawing.Color.FromArgb(
                (System.Byte)(255),
                (System.Byte)(128),
                (System.Byte)(128)
                );
            this.TransparencyKey = System.Drawing.Color.FromArgb(
                (System.Byte)(255),
                (System.Byte)(128),
                (System.Byte)(128)
                );
            hole.Visible = true;
            this.Controls.Add(hole);
        }

        //Continue
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            Loc = this.Location;
            this.Close();
        }
        //new game
        private void button2_Click(object sender, EventArgs e)
        {
            
            if (MessageBox.Show(this, "Нaчать новую игру?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.DialogResult = DialogResult.Retry;
                this.Close();
            }
        }
        TextBox tb = new TextBox();
        //rules
        private void button3_Click(object sender, EventArgs e)
        {
           // hole.BackColor = Color.Gray;
            tb.Parent = hole;
            tb.Visible = true; tb.Location = new System.Drawing.Point(0, 70);
            tb.Height = hole.Height; tb.Width = hole.Width;
            tb.ReadOnly = true;
            tb.Multiline = true;
            tb.BackColor = hole.BackColor;
            tb.Text = TT;
            tb.Font = new Font("Times New Roman", 14, FontStyle.Bold);
            tb.BorderStyle = BorderStyle.None;
            tb.ForeColor = Color.Black;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "Вы точно хотите выйти?", "", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
