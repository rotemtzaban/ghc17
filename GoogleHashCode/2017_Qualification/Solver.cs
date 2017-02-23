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

		private Dictionary<RequestsDescription, double> _currentTime;
		private Dictionary<RequestsDescription, Tuple<CachedServer, double>> _bestTime;

		protected override ProblemOutput Solve(ProblemInput input)
		{
			_input = input;
			_output = new ProblemOutput { ServerAssignments = new Dictionary<CachedServer, List<Video>>() };
			_currentTime = new Dictionary<RequestsDescription, double>();
			_bestTime = new Dictionary<RequestsDescription, Tuple<CachedServer, double>>();

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
			Console.WriteLine("Assigned");
			selectedServer.Capacity -= request.Video.Size;
			_output.ServerAssignments.GetOrCreate(selectedServer, _ => new List<Video>()).Add(request.Video);

			foreach(var rr in _input.RequestsDescriptions.Where(r => Equals(r.Video, request.Video)))
				_currentTime.Remove(rr);

			foreach (var rr in _bestTime.Where(kvp => Equals(kvp.Value.Item1, selectedServer)).ToList())
				_bestTime.Remove(rr.Key);
		}

		private double CalculateServerTimeForRequest(CachedServer cachedServer, RequestsDescription request)
		{
			return request.Endpoint.ServersLatency.GetOrDefault(cachedServer, request.Endpoint.DataCenterLatency);
		}

		protected virtual RequestsDescription GetBestCurrentRequest()
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
			double currentTime = _currentTime.GetOrCreate(requestsDescription, CalculateCurrentTime);
			double bestTime = _bestTime.GetOrCreate(requestsDescription, GetBestTimeForRequest).Item2;
			return requestsDescription.NumOfRequests * (bestTime - currentTime) / requestsDescription.Video.Size;
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
