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
		protected ProblemInput _input;
        protected ProblemOutput _output;

        protected Dictionary<RequestsDescription, double> _currentTime;
        protected Dictionary<RequestsDescription, Tuple<CachedServer, double>> _bestTime;

		protected override ProblemOutput Solve(ProblemInput input)
		{
			_input = input;
			_output = new ProblemOutput { ServerAssignments = new Dictionary<CachedServer, List<Video>>() };
			_currentTime = new Dictionary<RequestsDescription, double>();
			_bestTime = new Dictionary<RequestsDescription, Tuple<CachedServer, double>>();
			var bulkSize = 1000;

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
			if (assigned % 20 == 0)
				Console.WriteLine("Assigned " + assigned);
			selectedServer.Capacity -= request.Video.Size;
			_output.ServerAssignments.GetOrCreate(selectedServer, _ => new List<Video>()).Add(request.Video);
			_input.RequestsDescriptions.Remove(request);

			foreach (var rr in _input.RequestsDescriptions.Where(r => Equals(r.Video, request.Video)))
				_currentTime.Remove(rr);

			foreach (var rr in _bestTime.Where(kvp => Equals(kvp.Value.Item1, selectedServer) && selectedServer.Capacity < kvp.Key.Video.Size).ToList())
				_bestTime.Remove(rr.Key);
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

		protected virtual double CalculateRequestValue(RequestsDescription requestsDescription)
		{
            double currentTime = _currentTime.GetOrCreate(requestsDescription, CalculateCurrentTime);
            double bestTime = _bestTime.GetOrCreate(requestsDescription, GetBestTimeForRequest).Item2;
            return requestsDescription.NumOfRequests * (currentTime - bestTime); // / (requestsDescription.Video.Size);
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
