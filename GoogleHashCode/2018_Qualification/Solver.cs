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
            foreach (var inputCar in input.Cars)
            {
                output.Cars.Add(inputCar);
            }
            while (true)
            {
                bool assignedRide = false;
                IEnumerable<Car> cars = input.Cars.OrderBy(_ => _.CurrentTime).Take(input.Cars.Count - 1);
                assignedRide = Apply(input, assignedRide, cars);

                if (!assignedRide)
                {
                    assignedRide = Apply(input, assignedRide, input.Cars);
                    if (!assignedRide)
                    {
                        return output;
                    }
                }
            }
        }

        private static bool Apply(ProblemInput input, bool assignedRide, IEnumerable<Car> cars)
        {
            foreach (var car in cars)
            // foreach (var car in input.Cars)
            {
                double minScore = double.MaxValue;
                Ride maxRide = null;
                Coordinate currLoc = car.CurrentTime > 0 ? car.RidesTaken.Last().End : new Coordinate(0, 0);
                foreach (var ride in input.Rides)
                {
                    // TODO: break cond
                    var score = ScoreCalc.GetScore(ride, currLoc, car.CurrentTime, input);
                    if (score < minScore)
                    {
                        minScore = score;
                        maxRide = ride;
                        assignedRide = true;
                    }
                }

                if (maxRide != null)
                {
                    input.Rides.Remove(maxRide);
                    car.RidesTaken.Add(maxRide);
                    long minStartTurn = Math.Max(car.CurrentTime + currLoc.CalcGridDistance(maxRide.Start),
                        maxRide.StartTime);

                    car.CurrentTime = minStartTurn + maxRide.Distance;
                }
            }

            return assignedRide;
        }
    }
}