using System.Collections.Generic;
using HashCodeCommon;

namespace _2016_Qualification
{
    public class Drone : IndexedObject
    {
        public MatrixCoordinate CurrentPosition { get; set; }

        public Drone(int index) : base(index)
        {
            CurrentPosition = new MatrixCoordinate(0, 0);
        }
    }
}