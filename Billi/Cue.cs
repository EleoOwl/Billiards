using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Billi
{
   public enum Cues { Default }

    class Cue : BodyDrawer
    {
        public Point Start { get; set; }
        public Vector Direction { get; set; }
        public double Length { get; set; }
        public static double MaxForce;
       
        private double strength;
        public double Strength
        {
            get { return strength; }
            set { if (value > MaxForce) strength = MaxForce; else strength = value; }
        }
        public Ball Owner { get; set; }

        // cмена привязанного шара (смена битка)
        public void ChangeOwner(Ball newOwner)
        {
            this.Owner = newOwner;
            Start = new Point()
            {
                X = this.Owner.X,// - this.Direction.X,
                Y = this.Owner.Y// - this.Direction.Y
            };

        }

        //разворот кия на угол
        public void Expand(double angle)
        {
            // Direction = new Vector( Direction.X * Math.Cos(angle) - Direction.Y * Math.Sin(angle),Direction.X * Math.Sin(angle) + Direction.Y * Math.Cos(angle));
            Direction = Direction.Expand(angle);
        }
        /*    | cos(a)  sin(a) | 
              |                | * ( Direction.X, Direction.Y)
              | sin(a) -cos(a) |                                           */

        public Cue() { }
        public Cue(Ball O, double length)
        {
            this.Owner = O;
            this.strength = 0;
            this.Length = length;
            this.Direction = new Vector(0, length);
            Start = new Point()
            {
                X = this.Owner.X ,//- this.Direction.X,
                Y = this.Owner.Y //- this.Direction.Y
            };
              
        }
        
      

        // возвращает массив точек, которые будут представлять кий на плоскости
        public PointF[] ReturnArr(Point coordinates)
        {
            PointF[] arr = new PointF[(int)this.Length*50];
            double deltaX = this.Direction.X / arr.Length;
            double deltaY = this.Direction.Y / arr.Length;
            for (int i=0; i<arr.Length; i++)
            {
                arr[i] = new PointF(
                    (float)(this.Start.X + i*deltaX + coordinates.X),
                    (float)( this.Start.Y + i*deltaY + coordinates.Y)
                    );
            }

            return arr;
        }

        //изменяет вектор привязанного шара
        public void Hit(double time)
        {
            this.Strength = time; 
            this.Owner.Hit(new Vector(
                -(this.Direction.X / this.Length)*this.Strength,
                -(this.Direction.Y / this.Length) * this.Strength
                ));
        }

        private static Pen pen= new Pen(Color.Blue, 12);
        // наследует абстрактный класс, рисует заданным графом, заданным цветом и смещением
        public override void DrawBody(Graphics g,  double width, Point cd)
        {
            pen.Color = Color.Aqua; pen.Width = (float)width;
            g.DrawCurve(pen, this.ReturnArr(cd));
            Image a = global::Billi.Properties.Resources.кий;
            
            //g.RotateTransform((float)Math.Acos(Vector.cosAlpha(this.Direction, new Vector(1, 0))));
            g.DrawImage(global::Billi.Properties.Resources.кий,(float) this.Start.X,(float) this.Start.Y, 20, 20); //(float)Math.Abs(this.Direction.X), (float)Math.Abs(this.Direction.Y));
           // g.RotateTransform(-(float)Math.Acos(Vector.cosAlpha(this.Direction, new Vector(1, 0))));
        }
        public override void Move(double dt)
        {

        }
    }
}
