using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon.HelperClasses
{
    public class MatrixCoordinateSet : ISet<MatrixCoordinate>
    {
        bool[,] m_Matrix;
        int count = 0;
        public MatrixCoordinateSet(int maxRow, int maxCol)
        {
            m_Matrix = new bool[maxRow, maxCol];
        }
        public int Count => count;

        public bool IsReadOnly => false;

        public bool Add(MatrixCoordinate item)
        {
            if(!m_Matrix[item.Row, item.Column])
            {
                count++;
                return m_Matrix[item.Row, item.Column] = true;
            }

            return false;
        }

        public void Clear()
        {
            m_Matrix = new bool[m_Matrix.GetLength(0), m_Matrix.GetLength(1)];
        }

        public bool Contains(MatrixCoordinate item)
        {
            return m_Matrix[item.Row, item.Column];
        }

        public void CopyTo(MatrixCoordinate[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public void ExceptWith(IEnumerable<MatrixCoordinate> other)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<MatrixCoordinate> GetEnumerator()
        {
            for (int i = 0; i < m_Matrix.GetLength(0); i++)
            {
                for (int j = 0; j < m_Matrix.GetLength(1); j++)
                {
                    if(m_Matrix[i, j])
                    {
                        yield return new MatrixCoordinate(i, j);
                    }
                }
            }
        }

        public void IntersectWith(IEnumerable<MatrixCoordinate> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSubsetOf(IEnumerable<MatrixCoordinate> other)
        {
            throw new NotImplementedException();
        }

        public bool IsProperSupersetOf(IEnumerable<MatrixCoordinate> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSubsetOf(IEnumerable<MatrixCoordinate> other)
        {
            throw new NotImplementedException();
        }

        public bool IsSupersetOf(IEnumerable<MatrixCoordinate> other)
        {
            throw new NotImplementedException();
        }

        public bool Overlaps(IEnumerable<MatrixCoordinate> other)
        {
            throw new NotImplementedException();
        }

        public bool Remove(MatrixCoordinate item)
        {
            throw new NotImplementedException();
        }

        public bool SetEquals(IEnumerable<MatrixCoordinate> other)
        {
            throw new NotImplementedException();
        }

        public void SymmetricExceptWith(IEnumerable<MatrixCoordinate> other)
        {
            throw new NotImplementedException();
        }

        public void UnionWith(IEnumerable<MatrixCoordinate> other)
        {
            throw new NotImplementedException();
        }

        void ICollection<MatrixCoordinate>.Add(MatrixCoordinate item)
        {
            Add(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
