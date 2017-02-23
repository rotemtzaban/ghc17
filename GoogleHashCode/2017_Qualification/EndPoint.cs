using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017_Qualification
{
    public class EndPoint : IndexedObject
    {
        public Dictionary<CachedServer, int> ServersLatency { get; set; }
        public int DataCenterLatency { get; set; }

        public EndPoint(int index)
            :base (index)
        {
        }
    }
}
