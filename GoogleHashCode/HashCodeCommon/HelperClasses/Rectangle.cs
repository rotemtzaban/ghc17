using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public class Rectangle
    {
        public Rectangle()
        {
        }

        public Rectangle(int left, int top, int right, int bottom)
        {
            TopLeft = new Coordinate(left, top);
            BottomRight = new Coordinate(right, bottom);
        }

        public Rectangle(Coordinate topLeft, Coordinate bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }

        public Coordinate TopLeft { get; private set; }
        public Coordinate BottomRight { get; private set; }

        public int Size
        {
            get { return Width * Height; }
        }

        public int Width
        {
            get { return BottomRight.X - TopLeft.X + 1; }
        }

        public int Height
        {
            get { return BottomRight.Y - TopLeft.Y + 1; }
        }

        public bool Intersects(Rectangle other)
        {
            bool overlapX = BottomRight.X >= other.TopLeft.X && TopLeft.X <= other.BottomRight.X;
            bool overlapY = BottomRight.Y >= other.TopLeft.Y && TopLeft.Y <= other.BottomRight.Y;
            return overlapX && overlapY;
        }

        public override bool Equals(object obj)
        {
            Rectangle other = obj as Rectangle;
            if (other == null)
            {
                return false;
            }

            return TopLeft.Equals(other.TopLeft) && BottomRight.Equals(other.BottomRight);
        }

        public override int GetHashCode()
        {
            return TopLeft.GetHashCode() * 17 ^ BottomRight.GetHashCode() * 23;
        }
    }
}
