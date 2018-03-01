using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_Qualification
{
    public class ProblemInput
    {
        public long NumberOfRows { get; set; }
        public long NumberOfCols { get; set; }
        public long NumberOfVheicles { get; set; }
        public long NumberOfRides { get; set; }
        public long Bonus { get; set; }
        public long NumberOfSteps { get; set; }

        public List<Car> Cars { get; } = new List<Car>();

        public List<Ride> Rides { get; set; }

        public ProblemInput()
        {
            this.Rides = new List<Ride>();
        }
    }
}
