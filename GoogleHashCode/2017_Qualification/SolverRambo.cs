using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017_Qualification
{
	public class SolverRambo : SolverBase<ProblemInput, ProblemOutput>
	{
		private ProblemInput _input;
		private ProblemOutput _output;

		private Dictionary<RequestsDescription, double> _currentTime;
		private Dictionary<RequestsDescription, Tuple<CachedServer, double>> _bestTime;

		private Dictionary<CachedServer, HashSet<RequestsDescription>> _serverToRequests;

		private Dictionary<Video, List<RequestsDescription>> _videoToDescription; 

		protected override ProblemOutput Solve(ProblemInput input)
		{
			Init(input);

			var bulkSize = 500;

			while (true)
			{
				var assignments = GetBestVideoAssignments(bulkSize);
				if (assignments == null || !assignments.Any())
					break;
				foreach (var assignment in assignments)
				{
					//Console.WriteLine(assignment.Item3);
					if (IsServerAvailableForVideo(assignment.Item2, assignment.Item1))
						AssignVideoToServer(assignment.Item2, assignment.Item1);
				}
			}

			return _output;
		}

		private List<Tuple<Video, CachedServer, double>> GetBestVideoAssignments(int bulkSize)
		{
			var bestVideos = new List<Tuple<Video, CachedServer, double>>();
			foreach (var video in _videoToDescription.Keys)
			{
				CachedServer bestServer = null;
				double bestValue = 0;
				foreach (var server in _input.CachedServers)
				{
					if (!IsServerAvailableForVideo(server, video))
						continue;

					var value = CalculateImprovement(video, server);
					if (value > bestValue)
					{
						bestValue = value;
						bestServer = server;
					}
				}

				if(bestServer != null)
					bestVideos.Add(new Tuple<Video, CachedServer, double>(video, bestServer, bestValue));
			}

			bestVideos.Sort((x, y) => -CalculateActualValue(x).CompareTo(CalculateActualValue(y)));

			return bestVideos.Take(bulkSize).ToList();
		}

		private static double CalculateActualValue(Tuple<Video, CachedServer, double> x)
		{
			return x.Item3 / x.Item1.Size;
		}

		private double CalculateImprovement(Video video, CachedServer server)
		{
			double improvement = 0;
			foreach (var req in _videoToDescription[video])
			{
				var current = _currentTime.GetOrCreate(req, CalculateCurrentTime);
				var newTime = CalculateServerTimeForRequest(server, req);
				improvement += req.NumOfRequests*(current - newTime);
			}
			return improvement;
		}

		private void Init(ProblemInput input)
		{
			_input = input;
			_output = new ProblemOutput {ServerAssignments = new Dictionary<CachedServer, List<Video>>()};
			_currentTime = new Dictionary<RequestsDescription, double>();
			_bestTime = new Dictionary<RequestsDescription, Tuple<CachedServer, double>>();

			_videoToDescription = new Dictionary<Video, List<RequestsDescription>>();
			foreach (var req in _input.RequestsDescriptions)
				_videoToDescription.GetOrCreate(req.Video, _ => new List<RequestsDescription>()).Add(req);

			_serverToRequests = new Dictionary<CachedServer, HashSet<RequestsDescription>>();
		}

		private bool IsServerAvailableForVideo(CachedServer cachedServer, Video video)
		{
			return video.Size <= cachedServer.Capacity;
		}

		private int assigned = 0;
		private void AssignVideoToServer(CachedServer selectedServer, Video video)
		{
			assigned++;
			if (assigned % 200 == 0)
				Console.WriteLine("Assigned " + assigned);

			if (_output.ServerAssignments.GetOrDefault(selectedServer, new List<Video>()).Contains(video))
				return;

			selectedServer.Capacity -= video.Size;
			_output.ServerAssignments.GetOrCreate(selectedServer, _ => new List<Video>()).Add(video);

			foreach (var rr in _videoToDescription[video])
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
