using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_problem
{
	public class PizzaSlice
	{
		public PizzaSlice()
		{
		}

		public PizzaSlice(int left, int top, int right, int bottom)
		{
			TopLeft = new Coordinate(left, top);
			BottomRight = new Coordinate(right, bottom);
		}

        public PizzaSlice(Coordinate topLeft, Coordinate bottomRight)
        {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }

        public Coordinate TopLeft { get; set; }
		public Coordinate BottomRight { get; set; }

		public int Size
		{
			get { return Width * Height; }
		}

		public int Width {
			get { return BottomRight.X - TopLeft.X + 1; }
		}
		public int Height {
			get { return BottomRight.Y - TopLeft.Y + 1; }
		}

        public override bool Equals(object obj)
        {
            PizzaSlice other = obj as PizzaSlice;

            return other != null && Equals(this.TopLeft, other.TopLeft) && Equals(this.BottomRight, other.BottomRight);
        }

		public bool DoesOverlap(PizzaSlice other)
		{
			var overlapX = BottomRight.X >= other.TopLeft.X && TopLeft.X <= other.BottomRight.X;
			var overlapY = BottomRight.Y >= other.TopLeft.Y && TopLeft.Y <= other.BottomRight.Y;
			return overlapX && overlapY;
		}
	}
}
