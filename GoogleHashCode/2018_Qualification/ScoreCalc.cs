using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_Qualification
{
    public class ScoreCalc
    {
        public double GetScore(Ride ride, Coordinate location, int currTime, ProblemInput input)
        {
            long distance = GetDistance(ride.Start, location);
            long timeToStart = currTime - ride.StartTime - distance;
            timeToStart = timeToStart < 0 ? 0 : timeToStart;

            if (timeToStart < 0)
                return -1;

            return timeToStart * 10.0 / ride.Distance;
        }

        public double GetScoreBonus(Ride ride, Coordinate location, int currTime, ProblemInput input)
        {
            long distance = GetDistance(ride.Start, location);
            long timeToStart = currTime - ride.StartTime - distance;
            timeToStart = timeToStart < 0 ? 0 : timeToStart;

            if (timeToStart < 0)
                return -1;

            double scoreWithBonus = timeToStart * 10.0 / ride.Distance;


            return scoreWithBonus / Math.Sqrt(input.Bonus);
        }

        public long GetDistance(Coordinate x, Coordinate y)
        {
            return Math.Abs(x.X - y.X) + Math.Abs(x.Y - y.Y);
        }
    }
}
