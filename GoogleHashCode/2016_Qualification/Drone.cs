using System.Collections.Generic;
using HashCodeCommon;

namespace _2016_Qualification
{
    public class Drone : IndexedObject
    {
        public MatrixCoordinate CurrentPosition { get; set; }

        public int CurrentTime { get; set; } = 0;

        public Drone(int index) : base(index)
        {
            CurrentPosition = new MatrixCoordinate(0, 0);
        }
    }
}