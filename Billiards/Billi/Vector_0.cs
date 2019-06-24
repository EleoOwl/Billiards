using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billi
{
    class Vector_0
    {
        public double X { get; }
        public double Y { get; }

        public Vector_0() { X = 0; Y = 0; }
        public Vector_0(Point a)
        {
            X = a.X; Y = a.Y; 
        }
        public Vector_0(double x, double y)
        {
            X = x; Y = y; 
        }
    }
}
