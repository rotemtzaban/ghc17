using HashCodeCommon;
using System.Collections.Generic;

namespace _2018_Final
{
    public class CellType
    {
        public BuildingType BuildingType { get; set; }
        public int BuildingIndex { get; set; }
        public int BuildingUniqueIndex { get; set; }
        public bool IsOccupied { get; set; }
        public HashSet<int> NearUtilities { get; set; }
        public int UtilityIndex { get; set; }

        public override string ToString()
        {
            return BuildingIndex.ToString();
        }
    }

    public class ProblemOutput
    {
        public List<OutputBuilding> Buildings { get; set; }
    }

    public class OutputBuilding
    {
        public int ProjectNumber { get; set; }

        public MatrixCoordinate Coordinate { get; set; }
    }
}