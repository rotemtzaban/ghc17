using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2018_Qualification
{
    public class Ride : IndexedObject
    {
        public Ride(int index) : base(index)
        {
        }

        public int LatestFinish { get; set; }

        public int StartTime { get; set; }

        public Coordinate Start {get; set;}

        public Coordinate End { get; set;}

        public long Distance
        {
            get { return End.CalcGridDistance(Start); }
        }
    }
}
