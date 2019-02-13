using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_Final
{
    public class Scorer : ScoreCalculatorBase<ProblemInput, ProblemOutput>
    {
        public override long Calculate(ProblemInput input, ProblemOutput output)
        {
            long score = 0;
            foreach (var collection in input.Collections)
            {
                if (IsCollectionCovered(collection, input, output))
                {
                    score += collection.Value;
                }
            }

            return score;
        }

        private bool IsCollectionCovered(Collection collection, ProblemInput input, ProblemOutput output)
        {
            throw new NotImplementedException();
        }

        public override ProblemOutput GetResultFromReader(ProblemInput input, TextReader reader)
        {
            var numOfPicsTaken = long.Parse(reader.ReadLine());

            var list = new List<ImageTakenDetails>();

            while (true)
            {
                string line = reader.ReadLine();
                if (line == null)
                {
                    break;
                }
                var sep = line.Split(' ');

                var a = new ImageTakenDetails()
                {
                    Latitude = long.Parse(sep[0]),
                    Longitude = long.Parse(sep[1]),
                    TurnTaken = long.Parse(sep[2]),
                    SatelliteId = long.Parse(sep[3]),
                };
                list.Add(a);
            }

            return new ProblemOutput()
            {
                ImagesTaken = list
            };
        }
    }
}
