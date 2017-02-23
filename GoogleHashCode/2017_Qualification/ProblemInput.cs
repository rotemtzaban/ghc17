using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017_Qualification
{
    public class ProblemInput
    {
        public int NumberOfVideos { get; set; }

        public int NumberOfEndpoints { get; set; }
        public int NumberOfRequestDescription { get; set; }
        public int NumberOfCachedServers { get; set; }
        public int ServerCapacity { get; set; }

        public List<Video> Videos { get; set; }

        public List<RequestsDescription> RequestsDescriptions { get; set; }

        public List<CachedServer> CachedServers { get; set; }

        public List<EndPoint> Endpoints { get; set; }
    }
}
