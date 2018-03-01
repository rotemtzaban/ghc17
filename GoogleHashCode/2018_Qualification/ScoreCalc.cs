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
        public static double GetScore(bool isOnRide, Ride ride, Coordinate location, long currTime, ProblemInput input)
        {
            long distance = GetDistance(ride.Start, location);
            long timeToStart = currTime - ride.StartTime - distance;
            timeToStart = timeToStart < 0 ? 0 : timeToStart;

            if (timeToStart < 0)
                return -1;

            return timeToStart * 10.0 / ride.Distance;
        }

        public static double GetScoreBonus(Ride ride, Coordinate location, int currTime, ProblemInput input)
        {
            long distance = GetDistance(ride.Start, location);
            long timeToStart = currTime - ride.StartTime - distance;
            timeToStart = timeToStart < 0 ? 0 : timeToStart;

            if (timeToStart < 0)
                return -1;

            double scoreWithBonus = timeToStart * 10.0 / ride.Distance;


            return scoreWithBonus / Math.Sqrt(input.Bonus);
        }

        public static  long GetDistance(Coordinate x, Coordinate y)
        {
            return x.CalcGridDistance(y);
        }
    }
}
