using System;
using System.IO;
using HashCodeCommon;
using System.Collections.Generic;

namespace _2018_Qualification
{
    internal class Calcutaor : ScoreCalculatorBase<ProblemInput, ProblemOutput>
    {
        public override long Calculate(ProblemInput input, ProblemOutput output)
        {
            long result = 0;
            foreach (var car in output.Cars)
            {
                Coordinate location = new Coordinate();
                long time = 0;
                foreach (var ride in car.RidesTaken)
                {
                    int distance = location.CalcGridDistance(ride.Start);
                    long minStartTurn = Math.Max(time + distance, ride.StartTime);
                    if (ride.StartTime >= minStartTurn)
                    {
                        result += input.Bonus;
                    }

                    time = minStartTurn + ride.Distance;
                    location = ride.End;
                    result += ride.Distance;
                }
            }

            return result;
        }

        public override ProblemOutput GetResultFromReader(ProblemInput input, TextReader reader)
        {
            ProblemOutput output = new ProblemOutput();
            output.Cars = new List<Car>();
            string s = reader.ReadLine();
            int j = 0;
            while (s != null)
            {
                output.Cars.Add(new Car(j));
                string[] splited = s.Split(' ');
                for (int i = 1; i < splited.Length; i++)
                {
                    output.Cars[j].RidesTaken.Add(input.Rides[int.Parse(splited[i])]);
                }

                j++;
                s = reader.ReadLine();
            }

            return output;
        }
    }
}