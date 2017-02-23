using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017_Qualification
{
    public class Solver : SolverBase<ProblemInput, ProblemOutput>
    {
        protected override ProblemOutput Solve(ProblemInput input)
        {
	        var result = new ProblemOutput();

	        while (true)
	        {
		        var request = GetBestCurrentRequest(input);
		        if (request == null)
			        break;
		        var availableServers = input.CachedServers.Where(s => IsServerAvailableForVideo(s, request.Video));
		        if (!availableServers.Any())
			        continue;

				var selectedServer = availableServers.Min(s => CalculateServerDistanceToRequest(s, request));

		        AssignVideoToServer(selectedServer, request, result);
	        }

			return result;
        }

		private bool IsServerAvailableForVideo(CachedServer cachedServer, Video video)
		{
			throw new Exception();
		}

	    private void AssignVideoToServer(double selectedServer, RequestsDescription request, ProblemOutput result)
	    {
		    throw new NotImplementedException();
	    }

	    private double CalculateServerDistanceToRequest(CachedServer cachedServer, RequestsDescription request)
	    {
		    throw new NotImplementedException();
	    }

	    private RequestsDescription GetBestCurrentRequest(ProblemInput input)
	    {
		    throw new NotImplementedException();
	    }
    }
}
