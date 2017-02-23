using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using HashCodeCommon.HelperClasses;

namespace _2017_Qualification
{
    public class ScoreCalculator : ScoreCalculatorBase<ProblemInput, ProblemOutput>
    {
        public override int Calculate(ProblemInput input, ProblemOutput output)
        {
	        return 0;
			long savedTime = 0;

	        foreach (var req in input.RequestsDescriptions)
	        {

		        long timeFromDataCenter = req.NumOfRequests*req.Endpoint.DataCenterLatency;
		        long fromCache = timeFromDataCenter;
		        foreach (var kvp in output.ServerAssignments)
		        {
			        if (kvp.Value.Contains(req.Video))
			        {
				        // Can fetch from cache
				        long fromCurrentCache = req.Endpoint.ServersLatency[kvp.Key]*req.NumOfRequests;
				        fromCache = Math.Min(fromCache, fromCurrentCache);
			        }
		        }

		        // Can save to list here for algo.
		        savedTime += timeFromDataCenter - fromCache;
	        }

	        // cast to int - maybe bug
			return (int)savedTime;
        }

        public override ProblemOutput GetResultFromReader(ProblemInput input, TextReader reader)
        {
            ProblemOutput output = new ProblemOutput{ ServerAssignments = new Dictionary<CachedServer, List<Video>>()};

	        var n = reader.GetInt();
	        for (int i = 0; i < n; i++)
	        {
		        var vals = reader.GetIntList();
		        var server = new CachedServer(vals[0]);
		        output.ServerAssignments.Add(server, new List<Video>());
		        foreach (var v in vals.Skip(1))
		        {
			        output.ServerAssignments[server].Add(new Video(v));
		        }
	        }

            return output;
        }
    }
}
