using System;
using System.IO;
using HashCodeCommon;
using HashCodeCommon.HelperClasses;

namespace _2018_Final
{
    public class Calculator : ScoreCalculatorBase<ProblemInput, ProblemOutput>
    {
        public override long Calculate(ProblemInput input, ProblemOutput output)
        {
            return 0;
        }

        public override ProblemOutput GetResultFromReader(ProblemInput input, TextReader reader)
        {
            ProblemOutput output = new ProblemOutput();
            var buildings = reader.GetInt();
            output.Buildings = new OutputBuilding[buildings];
            for (int i = 0; i < buildings; i++)
            {
                var intList = reader.GetIntList();
                output.Buildings[i] = new OutputBuilding
                {
                    ProjectNumber = intList[0],
                    Coordinate = new MatrixCoordinate(intList[1], intList[2])
                };
            }

            return output;
        }
    }
}