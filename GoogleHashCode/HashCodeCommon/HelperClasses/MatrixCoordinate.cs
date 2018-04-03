using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public struct MatrixCoordinate
    {
        public MatrixCoordinate(int row, int column)
        {
            Column = column;
            Row = row;
        }

        public int Column { get; private set; }
        public int Row { get; private set; }

        public double CalcEucledianDistance(Coordinate other)
        {
            var deltaX = this.Column - other.X;
            var deltaY = this.Row - other.Y;
            double result = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            return result;
        }

        public int CalcGridDistance(Coordinate other)
        {
            return Math.Abs(this.Column - other.X) + Math.Abs(this.Row - other.Y);
        }
    }
}
