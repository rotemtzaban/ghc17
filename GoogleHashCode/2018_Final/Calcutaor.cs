using System;
using System.IO;
using HashCodeCommon;

namespace _2018_Final
{
    public class Calcutaor : ScoreCalculatorBase<ProblemInput, ProblemOutput>
    {
        public override long Calculate(ProblemInput input, ProblemOutput output)
        {
            return 0;
        }

        public override ProblemOutput GetResultFromReader(ProblemInput input, TextReader reader)
        {
            ProblemOutput output = new ProblemOutput();
            // reader.readOutputFile();

            return output;
        }
    }
}