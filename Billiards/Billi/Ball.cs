using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Billi
{
    
    class Ball : BodyDrawer
    {
       

        public static int Counter = 0;
        public  double Acceleration { get; set; }
        public static double acceleration_koef { get; set; }
        public double X { get; private set; }
        public double Y { get; private set; }

        public double R { get; private set; }
        public Vector V { get; private set; }
        public int Numb { get; private set; }
        public void SetNumb(int n) { this.Numb = n; }
        public Color Colour { get; }
        public List<int> BallsThatCollided;

        public Ball() { }
        public Ball(double x, double y, double r, Vector v, Color Col)
        {
            X = x; Y = y;
            R = r;
            V = v;
            Colour = Col;
            Numb = Counter;
            Counter++;
            BallsThatCollided = new List<int>();
            Acceleration = v.Length * v.Length * acceleration_koef;
        }

        //
        public override void Move(double dt)
        {
            if (V.Length != 0)
            { 
            X = X + V.X * dt + (Acceleration * V.X / V.Length) * dt * dt / 2;
            Y = Y + V.Y * dt + (Acceleration * V.Y / V.Length) * dt * dt / 2;
            V = new Vector(
                V.X + (Acceleration * V.X / V.Length) * dt,
                V.Y + (Acceleration * V.Y / V.Length) * dt
                );

                Acceleration = -V.Length * acceleration_koef;
            }
        }

        // Изменяет скорость ОСТАНОВИВШЕГОСЯ шара при ударениии кием
        public void Hit(Vector cue)
        {
            if (this.isStopped()) this.V = cue;
            else { throw new Exception("The ball wasn't stopped"); }
        }
    
        // проверяет два шара на столкновение
        public static bool Collided(Ball a, Ball b)
        {
            
            bool f = (((a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y))
                                             -
                               (a.R + b.R) * (a.R + b.R)) < -5 ;
            if (!f)
            {
                a.BallsThatCollided.Remove(b.Numb);
                b.BallsThatCollided.Remove(a.Numb);
            }

            if (a.BallsThatCollided.Contains(b.Numb))
                return false;

            if (f)
            {
                a.BallsThatCollided.Add(b.Numb);
                b.BallsThatCollided.Add(a.Numb);
            }


            return f;
        }

        // преобразовывает вектора скоростей столкнувшихся шаров
        public static void Collision(Ball a, Ball b) 
        {
            Vector normal = new Vector( a.X - b.X, a.Y - b.Y);
          //  Vector tan  = new Vector(b.Y - a.Y, a.X - b.X);

            double NX =Math.PI- Math.Acos(Vector.cosAlpha(normal, new Vector(1, 0)));
            a.V = a.V.Expand(-NX);
            b.V = b.V.Expand(-NX);
            //if (Vector.sinAlpha(normal, new Vector(1, 0)) > 0)
            
                double xx = a.V.X;
                a.V = new Vector(b.V.X, a.V.Y);
                b.V = new Vector(xx, b.V.Y);
            
            a.V = a.V.Expand(NX);
            b.V = b.V.Expand(NX);
        }

       public bool Stop()
        {
            bool f = false;
            if (V.Length < 0.1) { this.V = new Vector(); f = true; }
            return f;
        }

        public void Borders(double w, double h, Point cd, double dt)
        {
             if (Math.Abs(this.X    //+ V.X * dt                
                 - w + cd.X+this.R) < 5|| this.X+cd.X-this.R<5)
            //if (w - this.X-this.R-10 < 4)
                this.V = new Vector( -this.V.X, this.V.Y);
            if (Math.Abs(this.Y //+ V.Y * dt 
                - h + cd.Y+this.R) < 5||this.Y+cd.Y-this.R<5)
            //if (h - this.Y - this.R-10 < 4)
                this.V = new Vector(this.V.X, -this.V.Y);
        }
        public bool OutOfBorders(double w, double h, Point cd, double dt)
        {
            return this.X - w + cd.X  > 5 || 
                  this.X + cd.X  < 5 || 
                  this.Y  - h + cd.Y  > 5 || 
                  this.Y + cd.Y < 5;
        }
        // возвращает true, если тело остановилось (нулевой модуль скорости)
        public bool isStopped() { return (V.Length < 0.0001); }

        //возвращает true, если точка совпадает с радиусом шара
        public bool Centers(Point a)
        {
            return (this.X - a.X < 0.01) && (this.Y - a.Y < 0.01);
        }


        private static SolidBrush SB = new SolidBrush(Color.AliceBlue);
        public override void DrawBody(Graphics g, double width, Point cd)
        {
            SB.Color = this.Colour;
                g.FillEllipse(SB,
                   (float)(this.X - this.R + cd.X),
                   (float)(this.Y - this.R + cd.Y),
                   (float)this.R * 2, (float)this.R * 2
                    );
        }
    }
}
