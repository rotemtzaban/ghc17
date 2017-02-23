using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017_Qualification
{
	public class Solver : SolverBase<ProblemInput, ProblemOutput>
	{
		private ProblemInput _input;
		private ProblemOutput _output;

		private Dictionary<RequestsDescription, double> _currentTime;
		private Dictionary<RequestsDescription, Tuple<CachedServer, double>> _bestTime;

		private Dictionary<CachedServer, HashSet<RequestsDescription>> _serverToRequests;

		private Dictionary<Video, List<RequestsDescription>> _videoToDescription; 

		protected override ProblemOutput Solve(ProblemInput input)
		{
			_input = input;
			_output = new ProblemOutput { ServerAssignments = new Dictionary<CachedServer, List<Video>>() };
			_currentTime = new Dictionary<RequestsDescription, double>();
			_bestTime = new Dictionary<RequestsDescription, Tuple<CachedServer, double>>();

			_videoToDescription = new Dictionary<Video, List<RequestsDescription>>();
			foreach (var req in _input.RequestsDescriptions)
				_videoToDescription.GetOrCreate(req.Video, _ => new List<RequestsDescription>()).Add(req);

			_serverToRequests = new Dictionary<CachedServer, HashSet<RequestsDescription>>();

			var bulkSize = 200;

			while (true)
			{
				var requests = GetBestCurrentRequests(bulkSize).ToList();
				if (!requests.Any())
					break;

				foreach (var request in requests)
				{
					var availableServers = _input.CachedServers.Where(s => IsServerAvailableForVideo(s, request.Video)).ToList();
					if (!availableServers.Any())
						continue;

					var selectedServer = availableServers.ArgMin(s => CalculateServerTimeForRequest(s, request));

					AssignVideoToServer(selectedServer, request);
				}
			}

			return _output;
		}

		private bool IsServerAvailableForVideo(CachedServer cachedServer, Video video)
		{
			return video.Size <= cachedServer.Capacity;
		}

		private int assigned = 0;
		private void AssignVideoToServer(CachedServer selectedServer, RequestsDescription request)
		{
			assigned++;
			if (assigned % 200 == 0)
				Console.WriteLine("Assigned " + assigned);

			_input.RequestsDescriptions.Remove(request);
            if (CalculateServerTimeForRequest(selectedServer, request) >= request.Endpoint.DataCenterLatency)
            {
                return;
            }
			if (_output.ServerAssignments.GetOrDefault(selectedServer, new List<Video>()).Contains(request.Video))
				return;

			selectedServer.Capacity -= request.Video.Size;
			_output.ServerAssignments.GetOrCreate(selectedServer, _ => new List<Video>()).Add(request.Video);

			foreach (var rr in _videoToDescription[request.Video])
				if (rr.Endpoint.ServersLatency.ContainsKey(selectedServer))
					_currentTime.Remove(rr);

			foreach (
				var rr in
					_serverToRequests.GetOrDefault(selectedServer, new HashSet<RequestsDescription>())
						.Where(rrr => selectedServer.Capacity < rrr.Video.Size)
						.ToList())
			{
				_bestTime.Remove(rr);
				_serverToRequests[selectedServer].Remove(rr);
			}
		}

		private double CalculateServerTimeForRequest(CachedServer cachedServer, RequestsDescription request)
		{
			return request.Endpoint.ServersLatency.GetOrDefault(cachedServer, request.Endpoint.DataCenterLatency);
		}

		protected virtual IEnumerable<RequestsDescription> GetBestCurrentRequests(int bulkSize)
		{
			var availableDescriptions = _input.RequestsDescriptions.Where(HasAvailableServer).ToList();
			if (!availableDescriptions.Any())
				return Enumerable.Empty<RequestsDescription>();
			return availableDescriptions.OrderBy(CalculateRequestValue).Take(bulkSize);
		}

		private bool HasAvailableServer(RequestsDescription requestsDescription)
		{
			return _input.CachedServers.Any(s => IsServerAvailableForVideo(s, requestsDescription.Video));
		}

		private double CalculateRequestValue(RequestsDescription requestsDescription)
		{
			double currentTime = _currentTime.GetOrCreate(requestsDescription, CalculateCurrentTime);
			var bestTuple = _bestTime.GetOrCreate(requestsDescription, GetBestTimeForRequest);
			_serverToRequests.GetOrCreate(bestTuple.Item1, _ => new HashSet<RequestsDescription>()).Add(requestsDescription);
			double bestTime = bestTuple.Item2;
			return requestsDescription.NumOfRequests * (currentTime - bestTime) / (requestsDescription.Video.Size);
		}

		private Tuple<CachedServer, double> GetBestTimeForRequest(RequestsDescription requestsDescription)
		{
			double time;
			var server = _input.CachedServers.ArgMin(s => CalculateServerTimeForRequest(s, requestsDescription), out time);
			return new Tuple<CachedServer, double>(server, time);
		}

		private double CalculateCurrentTime(RequestsDescription requestsDescription)
		{
			var serversWithVideo =
				_output.ServerAssignments.Where(kvp => kvp.Value.Contains(requestsDescription.Video)).ToList();
			if (!serversWithVideo.Any())
				return requestsDescription.Endpoint.DataCenterLatency;

			return serversWithVideo.Min(kvp => CalculateServerTimeForRequest(kvp.Key, requestsDescription));
		}
	}
}
