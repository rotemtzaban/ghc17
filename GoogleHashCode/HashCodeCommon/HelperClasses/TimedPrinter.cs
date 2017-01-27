using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon.HelperClasses
{
	public class TimedPrinter
	{
		#region Fields

		private readonly int _timeBetweennPrintsMs;
		private DateTime _lastPrint = DateTime.MinValue;

		#endregion

		#region C'tor

		public TimedPrinter(int timeBetweennPrintsMs)
		{
			_timeBetweennPrintsMs = timeBetweennPrintsMs;
		}

		#endregion

		#region Public Methods

		public void Print(string s, params object[] args)
		{
			if ((DateTime.Now - _lastPrint).TotalMilliseconds < _timeBetweennPrintsMs)
				return;

			_lastPrint = DateTime.Now;
			Console.WriteLine(s, args);
		}

		#endregion
	}
}
