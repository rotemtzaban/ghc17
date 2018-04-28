using HashCodeCommon;

namespace _2018_Final
{
    public class ProblemInput
    {
        public int Rows { get; set; }

        public int Columns { get; set; }

        public int MaxDistance { get; set; }

        public BuildingProject[] BuildingProjects { get; set; }
    }

    public class BuildingProject : IndexedObject
    {
        public BuildingProject(int index) : base(index)
        {
        }

        public bool[,] Plan { get; set; }

        public BuildingType BuildingType { get; set; }

        public int Capacity { get; set; }

        public int UtilityType { get; set; }
    }

    public enum BuildingType
    {
        Residential,
        Utility
    }
}