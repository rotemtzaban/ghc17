using System;
using System.Collections.Generic;
using System.Linq;
using HashCodeCommon;

namespace _2018_Qualification
{
    public class Solver : SolverBase<ProblemInput, ProblemOutput>
    {
        protected override ProblemOutput Solve(ProblemInput input)
        {
            ProblemOutput output = new ProblemOutput();

            while (true)
            {
                bool assignedRide = false;
                foreach (var car in input.Cars)
                {
                    double maxScore = 0;
                    Ride maxRide = null;
                    Coordinate currLoc = car.CurrentTime > 0 ? car.RidesTaken.Last().End : new Coordinate(0, 0);
                    foreach (var ride in input.Rides)
                    {
                        // TODO: break cond
                        var score = ScoreCalc.GetScore(car.IsOnRide, ride, currLoc, car.CurrentTime, input);
                        if (score != -1 && score > maxScore)
                        {
                            maxRide = ride;
                            assignedRide = true;
                        }
                    }

                    if (maxRide != null)
                    {
                        input.Rides.Remove(maxRide);
                        car.IsOnRide = true;
                        car.RidesTaken.Add(maxRide);
                        long minStartTurn = Math.Max(car.CurrentTime + currLoc.CalcGridDistance(maxRide.Start),
                            maxRide.StartTime);

                        car.CurrentTime = minStartTurn + maxRide.Distance;
                    }
                }

                if (!assignedRide)
                {
                    return output;
                }
            }
        }

        private long ScoreRide(bool isOnRide, Ride ride, Coordinate currentLocation, long currentTurn, ProblemInput input)
        {
            if (isOnRide)
            {
                return -1;
            }

            int toRideStartDistance = currentLocation.CalcGridDistance(ride.Start);

            long minStartTurn = Math.Min(currentTurn + toRideStartDistance, ride.StartTime);
            if (minStartTurn + ride.Distance >= input.NumberOfSteps || minStartTurn + ride.Distance >= ride.LatestFinish)
            {
                return -1; // invlaid ride for this car
            }


            // distance to start location + ride distance + current_time < numOfSteps
            throw new NotImplementedException();
        }
    }
}