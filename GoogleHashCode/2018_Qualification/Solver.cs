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
                    List<Ride> ridesAssigned = new List<Ride>();

                    foreach (var ride in input.Rides)
                    {
                        Coordinate currLoc = car.CurrentTime > 0 ? car.RidesTaken.Last().End : new Coordinate(0, 0);
                        if (ScoreRide(car.IsOnRide, ride, currLoc, car.CurrentTime, input) != -1)
                        {

                            ridesAssigned.Add(ride);
                            assignedRide = true;

                        }
                    }

                    foreach (var ride in ridesAssigned)
                    {
                        input.Rides.Remove(ride);
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