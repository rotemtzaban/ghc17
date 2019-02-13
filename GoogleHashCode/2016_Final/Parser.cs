using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HashCodeCommon.HelperClasses;

namespace _2016_Final
{
    public class Parser : ParserBase<ProblemInput>
    {
        protected override ProblemInput ParseFromStream(TextReader reader)
        {
            var satCount = reader.GetInt();
            Satallite[] satallites = new Satallite[satCount];
            for (int i = 0; i < satCount; i++)
            {
                var longList = reader.GetLongList();
                satallites[i] = new Satallite(i, longList[0], longList[1], longList[2], longList[3], longList[4]);
            }

            var colCount = reader.GetInt();
            Collection[] collections = new Collection[colCount];
            for (int i = 0; i < colCount; i++)
            {
                var longList = reader.GetLongList();
                long value = longList[0], locationCount = longList[1], timeRangeCount = longList[2];
                var locations = new Location[locationCount];
                for (int j = 0; j < locationCount; j++)
                {
                    var location = reader.GetLongList();
                    locations[j] = new Location { Lat = location[0], Lon = location[1] };
                }

                TimeRange[] timeRanges = new TimeRange[timeRangeCount];

                for (int j = 0; j < timeRangeCount; j++)
                {
                    var timeRange = reader.GetLongList();
                    timeRanges[j] = new TimeRange { Start = timeRange[0], End = timeRange[1] };
                }

                collections[i] = new Collection {TimeRanges = timeRanges, Locations = locations, Value = value};
            }


            return new ProblemInput
            {
                Satallites = satallites,
                Collections = collections
            };
        }
    }
}
