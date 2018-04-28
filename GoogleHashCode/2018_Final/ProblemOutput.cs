using HashCodeCommon;

namespace _2018_Final
{
    public class ProblemOutput
    {
        public OutputBuilding[] Buildings { get; set; }
    }

    public class OutputBuilding
    {
        public int ProjectNumber { get; set; }

        public MatrixCoordinate Coordinate { get; set; }
    }
}