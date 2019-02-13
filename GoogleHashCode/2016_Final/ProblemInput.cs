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
        public long Turns { get; set; }

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

        public long InitialLat { get; }

        public long InitialLon { get; }

        public long InitialVelocity { get; }

        public long MaxOrientationChange { get; }

        public long MaxOrientation { get; }

        public Satallite(int index, long lat, long lon, long velocity, long maxOrientationChange, long maxOrientation) : base(index)
        {
            this.InitialLat = this.Lat = lat;
            this.InitialLon = this.Lon = lon;
            this.InitialVelocity = velocity;
            MaxOrientationChange = maxOrientationChange;
            MaxOrientation = maxOrientation;
        }

        public void CalcPosition(long turn)
        {
            // TODO: caching!!
            long curr_lat = this.InitialLat;
            long curr_lon = this.InitialLon;
            long curr_velocity = this.InitialVelocity;

            for (int i=0;i<turn;i++)
            {
                long comp = curr_lat + curr_velocity;
                long lat_next;
                long lon_next;
                long velocity_next;
                if (comp >= -324000 && comp < 324000)
                {
                    lat_next = comp;                    
                    lon_next = curr_lon - 90 * 3600;
                    velocity_next = curr_velocity;                    
                }
                else if (comp > 324000)
                {
                    lat_next = 180 * 3600 - comp;
                    lon_next = -180 * 3600 + (curr_lon - 90 * 3600);
                    velocity_next = -curr_velocity;
                }
                else // if (comp < -324000)
                {
                    lat_next = -180 * 3600 - comp;
                    lon_next = -180 * 3600 + (curr_lon - 90 * 3600);
                    velocity_next = -curr_velocity;
                }

                // prep for next iteration
                curr_lat = lat_next;
                curr_lon = lon_next;
                curr_velocity = velocity_next;

                // final iteration - update properties
                if (i == turn -1)
                {
                    this.Lat = curr_lat;
                    this.Lon = curr_lon;
                    this.Velocity = curr_velocity;
                }
            }            
        }
    }
}
