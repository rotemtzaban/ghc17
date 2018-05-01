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

        public int Column { get; }
        public int Row { get; }

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

        public int CalcShitDistance(MatrixCoordinate other)
        {
            return Math.Max(Math.Abs(this.Column - other.Column), Math.Abs(this.Row - other.Row));
        }

        public override string ToString()
        {
            return Row + " " + Column;
        }

        public bool InMatrix<T>(T[,] matrix)
        {
            return Row >= 0 && Column >= 0 && Row < matrix.GetLength(0) && Column < matrix.GetLength(1);
        }

        public override bool Equals(object obj)
        {
            if(obj is MatrixCoordinate coordinate)
            {
                return coordinate.Column == Column && coordinate.Row == Row;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hash = 23;
            hash = hash * 31 + Row;
            hash = hash * 31 + Column;
            return hash;
        }
    }
}
