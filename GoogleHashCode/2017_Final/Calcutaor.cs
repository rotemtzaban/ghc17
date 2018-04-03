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

            foreach (var item in output.RouterCoordinates)
            {
                for (int i = output.RouterCoordinates.Length - input.RouterRadius;
                    i <= output.RouterCoordinates.Length + input.RouterRadius; i++)
                {
                    // AddScoreByRotemFunc();
                }
            }

            return moneyLeft;
        }

        public override ProblemOutput GetResultFromReader(ProblemInput input, TextReader reader)
        {
            ProblemOutput output = new ProblemOutput();
            MatrixCoordinate[] coordaintes = NewMethod(reader);
            MatrixCoordinate[] coordaintes2 = NewMethod(reader);

            output.BackBoneCoordinates = coordaintes.ToArray();
            output.RouterCoordinates = coordaintes.ToArray();
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