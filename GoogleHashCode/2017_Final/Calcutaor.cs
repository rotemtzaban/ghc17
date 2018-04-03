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
            throw new NotImplementedException();
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