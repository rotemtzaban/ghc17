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
	    private ProblemInput _input;
	    private ProblemOutput _output;

	    protected override ProblemOutput Solve(ProblemInput input)
        {
	        _input = input;
	        _output = new ProblemOutput();

	        while (true)
	        {
		        var request = GetBestCurrentRequest();
		        if (request == null)
			        break;

				var availableServers = _input.CachedServers.Where(s => IsServerAvailableForVideo(s, request.Video)).ToList();
		        if (!availableServers.Any())
			        continue;

				var selectedServer = availableServers.ArgMin(s => CalculateServerTimeForRequest(s, request));

		        AssignVideoToServer(selectedServer, request);
	        }

			return _output;
        }

		private bool IsServerAvailableForVideo(CachedServer cachedServer, Video video)
		{
            return video.Size <= cachedServer.Capacity;
		}

		private void AssignVideoToServer(CachedServer selectedServer, RequestsDescription request)
		{
			selectedServer.Capacity -= request.Video.Size;
			_output.ServerAssignments.GetOrCreate(selectedServer, _ => new List<Video>()).Add(request.Video);
		}

	    private double CalculateServerTimeForRequest(CachedServer cachedServer, RequestsDescription request)
	    {
		    return request.Endpoint.ServersLatency.GetOrDefault(cachedServer, request.Endpoint.DataCenterLatency);
	    }

	    private RequestsDescription GetBestCurrentRequest()
	    {
		    var availableDescriptions = _input.RequestsDescriptions.Where(HasAvailableDescription).ToList();
		    if (!availableDescriptions.Any())
			    return null;
		    return availableDescriptions.ArgMax(CalculateRequestValue);
	    }

	    private bool HasAvailableDescription(RequestsDescription requestsDescription)
	    {
		    return _input.CachedServers.Any(s => IsServerAvailableForVideo(s, requestsDescription.Video));
	    }

	    private double CalculateRequestValue(RequestsDescription requestsDescription)
	    {
		    double currentTime = CalculateCurrentTime(requestsDescription);
			double bestTime = _input.CachedServers.Min(s => CalculateServerTimeForRequest(s, requestsDescription));
		    return requestsDescription.NumOfRequests*(bestTime - currentTime)/requestsDescription.Video.Size;
	    }

	    private double CalculateCurrentTime(RequestsDescription requestsDescription)
	    {
		    var serversWithVideo = _output.ServerAssignments.Where(kvp => kvp.Value.Contains(requestsDescription.Video)).ToList();
		    if (!serversWithVideo.Any())
			    return requestsDescription.Endpoint.DataCenterLatency;

		    return serversWithVideo.Min(kvp => CalculateServerTimeForRequest(kvp.Key, requestsDescription));
	    }
    }
}
