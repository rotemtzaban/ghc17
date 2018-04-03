using HashCodeCommon;

namespace _2017_Final
{
    public class ProblemInput
    {
        public Cell[,] Cells { get; set; }
        public int RouterRadius { get; set; }
        public int BackBonePrice { get; set; }
        public int RouterPrice { get; set; }
        public int StartingBudger { get; set; }
        public MatrixCoordinate StartingBackbonePosition { get; set; }
    }

    public enum Cell
    {
        Empty,
        Wall,
        Traget
    }
}