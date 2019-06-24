using System;
using System.Drawing;

namespace Billi
{
    abstract class BodyDrawer
    {
        abstract public void DrawBody( Graphics g ,  double width, Point cd);
        abstract public void Move(double dt);
    }
}
