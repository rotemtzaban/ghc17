using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public struct Coordinate
    {
        public Coordinate(int x, int y) : this()
        {
            X = x;
            Y = y;
        }

        public int X { get; private set; }
        public int Y { get; private set; }

		// TODO: test this
		public double CalcEucledianDistance(Coordinate other)
		{
			double result = Math.Sqrt ((this.X - other.X) * (this.X - other.X) + (this.Y - other.Y) * (this.Y - other.Y));

			return result;
		}
    }
}
