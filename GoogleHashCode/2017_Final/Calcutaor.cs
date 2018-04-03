using System;
using System.Linq;
using System.IO;
using HashCodeCommon;
using HashCodeCommon.HelperClasses;

namespace _2017_Final
{
    internal class Calcutaor : ScoreCalculatorBase<ProblemInput, ProblemOutput>
    {
        public override long Calculate(ProblemInput input, ProblemOutput output)
        {
            int moneyLeft = input.StartingBudger -
                            output.RouterCoordinates.Length * input.RouterPrice -
                            output.BackBoneCoordinates.Length * input.BackBonePrice;

            Cell[,] cells = input.Cells;
            int[,] cellScores = new int[cells.GetLength(0), cells.GetLength(1)];
            foreach (var item in output.RouterCoordinates)
            {
                Solver.InitScoreMatrix(input, cells, cellScores, item.Row, item.Column);
            }


            foreach (var item in cellScores)
            {
                if (item == 0)
                    continue;
                // moneyLeft += 1000;
            }

            return moneyLeft;
        }

        public override ProblemOutput GetResultFromReader(ProblemInput input, TextReader reader)
        {
            ProblemOutput output = new ProblemOutput();
            MatrixCoordinate[] coordaintes = NewMethod(reader);
            MatrixCoordinate[] coordaintes2 = NewMethod(reader);

            output.BackBoneCoordinates = coordaintes.ToArray();
            output.RouterCoordinates = coordaintes2.ToArray();
            return output;
        }

        private static MatrixCoordinate[] NewMethod(TextReader reader)
        {
            int numOfBackbones = int.Parse(reader.ReadLine());
            MatrixCoordinate[] coordaintes = new MatrixCoordinate[numOfBackbones];
            for (int i = 0; i < numOfBackbones; i++)
            {
                string line = reader.ReadLine();
                coordaintes[i] = new MatrixCoordinate(line[0], line[1]);
            }

            return coordaintes;
        }
    }
}