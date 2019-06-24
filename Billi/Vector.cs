using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billi
{
    struct Point
    {
        public double X;
        public double Y;
    }

    class Vector
    {
        public double X { get;}
        public double Y { get; }
        public double Length { get; } 

        public Vector() { X = 0; Y = 0; Length = 0; }
        public Vector(Point a)
        {
            X = a.X; Y = a.Y; Length = Math.Sqrt( X*X + Y*Y );
        }
        public Vector(double x, double y)
        {
            X = x; Y = y; Length = Math.Sqrt(X * X + Y * Y);
        }
        
        public static double cosAlpha(Vector a, Vector b)
        {
            return ((a.X * b.X + a.Y * b.Y) / (a.Length * b.Length));
        }
        public static double sinAlpha(Vector a, Vector b)
        {
            return ((a.X * b.Y - a.X * b.Y) / (a.Length * b.Length));
        }
        public Vector Expand(double angle)
        {
            return new Vector(
                this.X * Math.Cos(angle) - this.Y * Math.Sin(angle),
                this.X * Math.Sin(angle) + this.Y * Math.Cos(angle)
                );
        }
        /*    | cos(a)  -sin(a) | 
              |                 | * ( Direction.X, Direction.Y)
              | sin(a)   cos(a) |                                           */

        public override string ToString()
        {
            return String.Format("[{0:F4}, {1:F4}], len = {2:F4}", X, Y, Length);
        }
    }
}
