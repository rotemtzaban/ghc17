using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
	public struct Coordinate
	{
		public Coordinate(int x, int y)
			: this()
		{
			X = x;
			Y = y;
		}

		public int X { get; private set; }
		public int Y { get; private set; }

		public double CalcEucledianDistance(Coordinate other)
		{
			var deltaX = this.X - other.X;
			var deltaY = this.Y - other.Y;
			double result = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

			return result;
		}

	    public int CalcGridDistance(Coordinate other)
	    {
            return Math.Abs(this.X - other.X) + Math.Abs(this.Y - other.Y);
	    }

	    public override string ToString()
	    {
	        return $"{X}, {Y}";
	    }

        public bool InMatrix<T>(T[,] matrix)
        {
            return X >= 0 && Y >= 0 && X < matrix.GetLength(0) && Y < matrix.GetLength(1);
        }

        public override bool Equals(object obj)
        {
            if (obj is Coordinate coordinate)
            {
                return coordinate.X == X && coordinate.Y == Y;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hash = 23;
            hash = hash * 31 + X;
            hash = hash * 31 + Y;
            return hash;
        }
    }
}
