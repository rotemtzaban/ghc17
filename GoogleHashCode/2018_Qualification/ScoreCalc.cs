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
        public static double GetScore(Ride ride, Coordinate location, long currTime, ProblemInput input)
        {
            long distance = GetDistance(ride.Start, location);
            long minStartTurn = Math.Max(currTime + distance, ride.StartTime);
            long timeToStart = minStartTurn - currTime;
            timeToStart = timeToStart < 0 ? 0 : timeToStart;

            if (IsNotValid(ride, input, minStartTurn, timeToStart))
                return double.MaxValue;

            // double scoreWithoutBonus = timeToStart;
            double scoreWithoutBonus = timeToStart / Math.Pow(ride.Distance, 0.4);  // good for c  
            if (currTime + distance <= ride.StartTime)
            {
                // return scoreWithoutBonus - Math.Sqrt(input.Bonus);
                return scoreWithoutBonus - Math.Sqrt(input.Bonus) + Math.Log(ride.Distance); //   good for d for some reason
                // scoreWithoutBonus /= Math.Sqrt(input.Bonus);
            }

            return scoreWithoutBonus;
        }

        private static bool IsNotValid(Ride ride, ProblemInput input, long minStartTurn, long timeToStart)
        {
            return timeToStart < 0 || minStartTurn + ride.Distance >= ride.LatestFinish || minStartTurn + ride.Distance >= input.NumberOfSteps + 1;
        }

        //public static double GetScoreBonus(Ride ride, Coordinate location, long currTime, ProblemInput input)
        //{
        //    long distance = GetDistance(ride.Start, location);
        //    long minStartTurn = Math.Max(currTime + distance, ride.StartTime);
        //    long timeToStart = minStartTurn - currTime;
        //    timeToStart = timeToStart < 0 ? 0 : timeToStart;

        //    if (IsNotValid(ride, input, minStartTurn, timeToStart))
        //        return -1;

        //    return 1.0 / timeToStart;

        //    double scoreWithBonus = timeToStart * 2 / Math.Sqrt(ride.Distance);

        //    return scoreWithBonus / Math.Sqrt(input.Bonus);
        //}

        public static  long GetDistance(Coordinate x, Coordinate y)
        {
            return x.CalcGridDistance(y);
        }
    }
}
