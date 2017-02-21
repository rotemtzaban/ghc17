using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2017_Qualification
{
    public class Parser : ParserBase<ProblemInput>
    {
        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            ProblemInput input = new ProblemInput();
            string[] firstLineSplited = reader.ReadLine().Split(' ');
            input.Rows = int.Parse(firstLineSplited[0]);
            input.Columns = int.Parse(firstLineSplited[1]);

            int someCount = int.Parse(firstLineSplited[2]);
            input.FirstObject = new List<Object1>();
            for (int i = 0; i < someCount; i++)
            {
                string[] lineSplitted = reader.ReadLine().Split(' ');
                // int x = int.Parse(lineSplitted[0]);
                // new Coordinate(int.Parse(locationAsString[0]), int.Parse(locationAsString[1])
                // ReadLineAsCoordinate(reader)
                Object1 newObj = new Object1(i);
                input.FirstObject.Add(newObj);
            }

            int secondSomeCount = int.Parse(firstLineSplited[3]);
            input.SecondObject = new List<Object2>();
            for (int i = 0; i < someCount; i++)
            {
                string[] lineSplitted = reader.ReadLine().Split(' ');
                // int x = int.Parse(lineSplitted[0]);
                Object2 newObj = new Object2(i);
                input.SecondObject.Add(newObj);
            }

            return input;
        }
    }
}
