using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace Billi
{

    public partial class Form1 : Form
    {

        private BufferedGraphics gBuff;
        private BufferedGraphicsContext context;
        private List<Ball> Objects;
        private Cue cue;
        private Point CoordinatesDelta;
        private BallOut bo;
        private bool HitMode = false;
        private double TimeSpeed = 1;
        private int plus = 0;
        private int minus = 0;
        private int Ability = 0;
        private Point[] loses = new Point[] { new Point {X = 0, Y =  160},
                                              new Point {X = 0, Y =  -160},
                                              new Point {X = -350, Y = 160},
                                              new Point {X = -350, Y = -160},
                                              new Point {X = 330, Y = 160},
                                              new Point {X = 330, Y = -160}};

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST) m.Result = (IntPtr)(HT_CAPTION);
        }

        public Form1()
        {
            InitializeComponent();
            this.Visible = false;
            Previev pv = new Previev();
            pv.ShowDialog();
            this.StartPosition = FormStartPosition.CenterScreen;

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Objects = new List<Ball>();
            this.Size = pictureBox1.Size;
            Plus.Location = new System.Drawing.Point(Plus.Location.X, 5);
            shtr.Location = new System.Drawing.Point(shtr.Location.X, 5);
            pictureBox1.Dock = DockStyle.Fill;
            CoordinatesDelta = new Point { X = this.Width / 2, Y = this.Height / 2 };
            sb = new SolidBrush(this.BackColor);
            TIMER.Start();
            Ball.acceleration_koef = 0.005;
            Cue.MaxForce = 5;
            StartingPosition();
            context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size(pictureBox1.Width + 1, pictureBox1.Height + 1);

            gBuff = context.Allocate(pictureBox1.CreateGraphics(), new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            
            DrawToBuffer(gBuff.Graphics, TimeSpeed);
        }


        // рисует все объекты игры - кий, шары
        private void ToDrawAll(Graphics g, double width)
        {
            foreach (Ball bd in Objects) bd.DrawBody(g, 3, CoordinatesDelta);
            cue.DrawBody(g, width, CoordinatesDelta);
        }

        private bool InLose(Ball a)
        {
            //for (int i = a.Y < 0 ? 0 : 1; i < loses.Length; i += 2)
            for (int i = 0; i < loses.Length; i++)
            {
                if ((a.X - loses[i].X) * (a.X - loses[i].X) + (a.Y - loses[i].Y) * (a.Y - loses[i].Y) - a.R * a.R < 100)
                    return true;
            }
            return false;
        }
        private bool NearLose(Ball a)
        {
            //for (int i = a.Y < 0 ? 0 : 1; i < loses.Length; i += 2)
            for (int i = 0; i < loses.Length; i++)
            {
                if ((a.X - loses[i].X) * (a.X - loses[i].X) + (a.Y - loses[i].Y) * (a.Y - loses[i].Y) - a.R * a.R < 200)
                    return true;
            }
            return false;
        }

        private void TIMER_Tick(object sender, EventArgs e)
        {
            DrawToBuffer(gBuff.Graphics, TimeSpeed);
            gBuff.Render(Graphics.FromHwnd(pictureBox1.Handle));
        }

        private void StartingPosition()
        {
            Plus.Text = "Количество набранных очков:  0";
            shtr.Text = "Количество штрафных очков:  0";
            timelabel.Text = "Ускорение времени: х1";
            Ball.Counter = 0;
            Objects.Clear();
            double StartX = -200, StartY = -70;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 5 - i; j > 0; j--)
                {
                    Objects.Add(new Ball(i * 20 + StartX, j * 20 + StartY, 10, new Vector(), Color.White));
                }
                StartY += 10;
            }
            Objects.Add(new Ball(200, 0, 10, new Vector(), Color.Brown));
            cue = new Cue(Objects[Objects.Count - 1], 100);
        }

        private void DrawToBuffer(Graphics g, double k)
        {
            for (int i = 0; i < Objects.Count; i++)
                for (int j = i + 1; j < Objects.Count; j++)
                    if (Ball.Collided(Objects[i], Objects[j])) Ball.Collision(Objects[i], Objects[j]);
            for (int i = 0; i < Objects.Count; i++)
            {
                Objects[i].Move(k * 3);
                if (!NearLose(Objects[i]))
                {
                    Objects[i].Borders(pictureBox1.Width - 110, pictureBox1.Height - 110, new Point { X = CoordinatesDelta.X - 50, Y = CoordinatesDelta.Y - 50 }
                                                                                                                           , k);
                    if (Objects[i].OutOfBorders(pictureBox1.Width - 110, pictureBox1.Height - 110, new Point { X = CoordinatesDelta.X - 50, Y = CoordinatesDelta.Y - 50 }
                , k * 3))
                    {
                        
                        Objects.Remove(Objects[i]);
                        bo = new BallOut("Мяч вылетел"); bo.Owner = this;
                        shtr.Text = shtr.Text.Replace(minus.ToString(), (minus + 100).ToString()); minus += 100;
                        bo.Show();
                        cue.Owner = NextBall(cue.Owner.Numb);
                        DoLessNumb(i);
                        i--;
                        continue;
                    }
                }
                else
                {
                    if (Objects[i] != bitok)
                    {
                        bo = new BallOut("Мяч попал в лузу"); bo.Owner = this;
                        Plus.Text = Plus.Text.Replace(plus.ToString(), (plus + 100).ToString()); plus += 100;
                    }
                    else
                    {
                        bo = new BallOut("Биток попал в лузу"); bo.Owner = this;
                        shtr.Text = shtr.Text.Replace(minus.ToString(), (minus + 100).ToString()); minus += 50;
                        Plus.Text = Plus.Text.Replace(plus.ToString(), (plus + 100).ToString()); plus += 100;
                    }
                    Objects.Remove(Objects[i]);
                    bo.Show();
                    cue.Owner = NextBall(cue.Owner.Numb);
                    DoLessNumb(i);
                    i--;
                    continue;
                }
            }
            if (BallsStop()) { this.HitMode = true; cue.ChangeOwner(cue.Owner); }
            if (Objects.Count == 0)
            {
                MessageBox.Show(this, "");
            }
            g.FillRectangle(sb, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            g.DrawImage(global::Billi.Properties.Resources.table3, 0, 0, pictureBox1.Width, pictureBox1.Height);
            ToDrawAll(g, 3);

        }

        private void DoLessNumb(int id)
        {
            for (int i=id; i<Objects.Count; i++)
            {
                Objects[i].SetNumb(i);
            }
        }
        SolidBrush sb;
        private bool BallsStop()
        {
            bool f = true;
            for (int i = 0; i < Objects.Count; i++)
                if (!Objects[i].Stop()) f = false;
            return f;
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            gBuff.Render(e.Graphics);
        }

        TextBox Win;
        private void EndOfGame()
        {
            TIMER.Stop();
            Win = new TextBox();
            Win.Enabled = false; ;
            Win.Multiline = true; Win.Height = this.Height; Win.Width = this.Width;
            Win.BackColor = this.ForeColor = Color.Aqua; Win.BorderStyle = BorderStyle.None;
            VisibleData(false);
            Win.Parent = pictureBox1;
            gBuff.Graphics.FillRectangle(sb, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            Win.Location = new System.Drawing.Point(pictureBox1.Width / 3, pictureBox1.Height / 3);
            Win.Text = "Игра окончена!\r\nРезультат:" + (plus - minus).ToString();
            Win.Font = new Font("Microsoft Sans Serif", 24, FontStyle.Bold);
            Controls.Add(Win);
        }
        #region Hit by cue
        private void DoHit()
        {
            TimesHit.Start();
        }

        private void EndHit()
        {
            TimesHit.Stop();
            cue.Hit(CueHit / 10);
            bitok = cue.Owner;
        }
        private Ball bitok;
        private int CueHit = 10;
        private void TimesHit_Tick(object sender, EventArgs e)
        {
            CueHit += 5;
            double newX = cue.Start.X; if (cue.Direction.X != 0) newX += cue.Direction.X / Math.Abs(cue.Direction.X) * 2;
            cue.Start = new Point
            {
                X = newX,
                Y = cue.Start.Y + 2//(cue.Direction.Y / cue.Direction.X)*newX + cue.Start.Y
            };
            ToDrawAll(gBuff.Graphics, TimeSpeed);
            gBuff.Render(Graphics.FromHwnd(pictureBox1.Handle));
        }
        #endregion
        private void VisibleData(bool f)
        {
            pictureBox1.Visible = Plus.Visible = shtr.Visible = timelabel.Visible = f;
            
            try { Win.Visible = !f; } catch { }
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.A) cue.Expand(0.1);
            if (e.KeyCode == Keys.D) cue.Expand(-0.1);
            if (e.KeyCode == Keys.C)
                cue.ChangeOwner(NextBall(cue.Owner.Numb));
            if (e.KeyCode == Keys.F)
            {
                if (HitMode)
                { DoHit(); HitMode = false; }
                else
                {
                    bo = new BallOut("Шары не остановились");
                    shtr.Text = shtr.Text.Replace(minus.ToString(), (minus + 50).ToString()); minus += 50;
                    bo.Owner = this;
                    bo.Show();
                }
            }
            if (e.KeyCode == Keys.L) EndOfGame();
            if (e.KeyCode == Keys.Escape)
            {
                TIMER.Stop();

                Menu m = new Menu(pictureBox1.Image); m.Owner = this;
                m.ShowDialog();
                if (m.DialogResult == DialogResult.Cancel) this.Close();
                if (m.DialogResult == DialogResult.Retry) { VisibleData(true); StartingPosition(); HitMode = true; }
                
                TIMER.Start();
            }

            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Right) { if (TimeSpeed < 5) { timelabel.Text = timelabel.Text.Replace((char)(TimeSpeed + 48), (char)(TimeSpeed * 2 + 48)); TimeSpeed *= 2; } }
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Left) { if (TimeSpeed > 1) { timelabel.Text = timelabel.Text.Replace((char)(TimeSpeed + 48), (char)(TimeSpeed / 2 + 48)); TimeSpeed /= 2; } }

        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F)
            { EndHit(); HitMode = false; }
        }

        private Ball NextBall(int id)
        {
            return Objects[(id + 1 < Objects.Count) ? id + 1 : 0];
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            PostMessage(this.Handle, WM_SYSCOMMAND, DOMOVE, 0);
        }
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool PostMessage(IntPtr hWnd, uint Msg, uint WParam, uint LParam);
        [DllImport("user32", CharSet = CharSet.Auto)]
        internal extern static bool ReleaseCapture();

        const uint WM_SYSCOMMAND = 0x0112;
        const uint DOMOVE = 0xF012;
        const uint DOSIZE = 0xF008;


        //private void pictureBox1_Click(object sender, EventArgs e)
        //{
        //    FileStream fs = new FileStream(@"..\..\..\coo.txt", FileMode.Open);
        //    byte[] arr = Encoding.Default.GetBytes(MousePosition.X.ToString()+" "+MousePosition.Y.ToString()+" ");
        //    fs.Seek(0, SeekOrigin.End);
        //    fs.Write(arr, 0, arr.Length);
        //    fs.Close();
        //}
    }
}
