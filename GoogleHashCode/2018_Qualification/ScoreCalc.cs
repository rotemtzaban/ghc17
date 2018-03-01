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
        public long GetScore(Ride ride, Coordinate location, int currTime, ProblemInput input)
        {
            long distance = GetDistance(ride.Start, location);
            long timeToStart = currTime - ride.StartTime;
            timeToStart = timeToStart < 0 ? 0 : timeToStart;

            long turnsToWaitTillExacStart = ride.StartTime - currTime - distance;

            return (distance + timeToStart) * 10 / ride.Distance;
        }

        public long GetDistance(Coordinate x, Coordinate y)
        {
            return Math.Abs(x.X - y.X) + Math.Abs(x.Y - y.Y);
        }
    }
}
