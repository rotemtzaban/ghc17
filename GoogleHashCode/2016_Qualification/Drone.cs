using HashCodeCommon;

namespace _2016_Qualification
{
    class Drone : IndexedObject
    {
        public MatrixCoordinate CurrentPosition { get; set; }

        public Drone(int index) : base(index)
        {
            CurrentPosition = new MatrixCoordinate(0, 0);
        }
    }
}