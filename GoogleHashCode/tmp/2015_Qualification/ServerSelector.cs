using System.Collections.Generic;
using System.Linq;

namespace _2015_Qualification
{
	public class ServerSelector
	{
		private readonly ProblemInput _input;
		private readonly ProblemOutput _result;
		private Stack<Server> _availableServersByCapacity;

		public ServerSelector(ProblemInput input, ProblemOutput result)
		{
			_input = input;
			_result = result;

			_availableServersByCapacity = new Stack<Server>(GetServerListByPreference(input));
		}

		public bool HasAvailableServer
		{
			get { return _availableServersByCapacity.Count > 0; }
		}

		private IOrderedEnumerable<Server> GetServerListByPreference(ProblemInput input)
		{
			return input.Servers.OrderBy(x => ((double)x.Capacity) / x.Slots);
		}

		public Server UseNextServer()
		{
			return _availableServersByCapacity.Pop();
		}
	}
}