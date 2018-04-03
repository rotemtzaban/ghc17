using System;
using System.IO;
using HashCodeCommon;

namespace _2017_Final
{
    internal class Parser : ParserBase<ProblemInput>
    {
        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            ProblemInput input = new ProblemInput();
            int[] firstLineSplited = ReadLineAsIntArray(reader);
            input.Cells = new Cell[firstLineSplited[1], firstLineSplited[0]];
            input.RouterRadius = firstLineSplited[2];

            int[] secondLineSplited = ReadLineAsIntArray(reader);
            input.BackBonePrice = secondLineSplited[0];
            input.RouterPrice = secondLineSplited[1];
            input.StartingBudger = secondLineSplited[2];

            int[] thirdLine = ReadLineAsIntArray(reader);
            input.StartingBackbonePosition = new Coordinate(thirdLine[1], thirdLine[0]);

            for (int i = 0; i < firstLineSplited[0]; i++)
            {
                string line = reader.ReadLine();
                for (int j = 0; j < line.Length; j++)
                {
                    Char curr = line[j];
                    if (curr == '-')
                    {
                        input.Cells[j, i] = Cell.Empty;
                    }
                    else if (curr == '#')
                    {
                        input.Cells[j, i] = Cell.Wall;
                    }
                    else if (curr == '.')
                    {
                        input.Cells[j, i] = Cell.Traget;
                    }
                    else { throw new Exception(); }
                }
            }

            return input;
        }
    }
}