using System;
using System.IO;
using HashCodeCommon;

namespace _2018_Qualification
{
    public class Parser : ParserBase<ProblemInput>
    {
        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            ProblemInput input = new ProblemInput();
            string[] firstLineSplited = reader.ReadLine().Split(' ');
            input.NumberOfRows = long.Parse(firstLineSplited[0]);
            input.NumberOfCols = long.Parse(firstLineSplited[1]);
            input.NumberOfVheicles = long.Parse(firstLineSplited[2]);
            input.NumberOfRides = long.Parse(firstLineSplited[3]);
            input.Bonus = long.Parse(firstLineSplited[4]);
            input.NumberOfSteps = long.Parse(firstLineSplited[5]);


            int num = int.Parse(firstLineSplited[0]);

            // input.PropList = new PropList()
            for (int i = 0; i < num; i++)
            {
                // input.PropList.Add(new Item(i))
            }

            return input;
        }
    }
}