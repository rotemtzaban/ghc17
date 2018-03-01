using System;
using System.IO;
using HashCodeCommon;

namespace _2018_Qualification
{
    public class Parser : ParserBase<ProblemInput>
    {
        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            ProblemInput input = new ProblemInput();
            string[] firstLineSplited = reader.ReadLine().Split(' ');
            input.NumberOfRows = long.Parse(firstLineSplited[0]);
            input.NumberOfCols = long.Parse(firstLineSplited[1]);
            input.NumberOfVheicles = long.Parse(firstLineSplited[2]);
            input.NumberOfRides = long.Parse(firstLineSplited[3]);
            input.Bonus = long.Parse(firstLineSplited[4]);
            input.NumberOfSteps = long.Parse(firstLineSplited[5]);

            for (int i=0;i<input.NumberOfRides;i++)
            {
                string[] rideStr = reader.ReadLine().Split(' ');
                Ride ride = new Ride(i);
                checked
                {
                    ride.Start = new Coordinate(int.Parse(rideStr[0]), int.Parse(rideStr[1]));
                    ride.End = new Coordinate(int.Parse(rideStr[2]), int.Parse(rideStr[3]));
                    ride.StartTime = int.Parse(rideStr[4]);
                    ride.LatestFinish = int.Parse(rideStr[5]);
                }

                input.Rides.Add(ride);
            }

            for (var i = 0; i < input.NumberOfVheicles; i++)
            {
                input.Cars.Add(new Car(i));
            }
            
            return input;
        }
    }
}