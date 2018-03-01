using System;
using System.IO;
using HashCodeCommon;
using System.Collections.Generic;

namespace _2018_Qualification
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
            output.Cars = new List<Car>();
            string s = reader.ReadLine();
            while (s != null)
            {
                string[] splited = s.Split(' ');
                for (int i = 1; i < splited.Length; i++)
                {

                }
            }
        }
    }
}