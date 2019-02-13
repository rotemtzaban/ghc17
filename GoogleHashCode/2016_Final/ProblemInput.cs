using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashCodeCommon;

namespace _2016_Final
{
    public class ProblemInput
    {
        public Satallite[] Satallites { get; set; }

        public Collection[] Collections { get; set; }
    }

    public class Collection
    {
        public long Value { get; set; }

        public Location[] Locations { get; set; }

        public TimeRange[] TimeRanges { get; set; }
    }

    public class TimeRange
    {
        public long Start { get; set; }

        public long End { get; set; }
    }

    public class Location
    {
        public long Lat { get; set; }

        public long Lon { get; set; }
    }

    public class Satallite : IndexedObject
    {
        public long Lat { get; set; }

        public long Lon { get; set; }

        public long Velocity { get; set; }

        public long InitialLat { get; set; }

        public long InitialLon { get; set; }

        public long InitialVelocity { get; set; }

        public long MaxOrientationChange { get; set; }

        public long MaxOrientation { get; set; }

        public Satallite(int index, long lat, long lon, long velocity) : base(index)
        {
            this.InitialLat = lat;
            this.InitialLon = lon;
            this.InitialVelocity = velocity;
        }

        public void CalcPosition(long turn)
        {
            throw new NotImplementedException();

        }
    }
}
