using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza_problem
{
	public class ScoreCalc
	{
		public ScoreCalc(string resultPath)
		{
			Score = GetScoreFromFile(resultPath);
		}

		public ScoreCalc(IEnumerable<PizzaSlice> slices)
		{
			Score = GetScoreFromSlices(slices);
		}

		private int GetScoreFromFile(string resultPath)
		{
			var slices = new List<PizzaSlice>();
			using (var reader = new StreamReader(resultPath))
			{
				var slicesCount = int.Parse(reader.ReadLine());
				for (int i = 0; i < slicesCount; i++)
				{
					var line = reader.ReadLine();
					var spl = line.Split(' ').Select(int.Parse).ToList();

					slices.Add(new PizzaSlice(spl[1], spl[0], spl[3], spl[2]));
				}
			}
			return GetScoreFromSlices(slices);
		}

		public static int GetScoreFromSlices(IEnumerable<PizzaSlice> slices)
		{
			if (CheckForOverlap(slices))
				return -1;
			return slices.Sum(slice => slice.Size);
		}

		private static bool CheckForOverlap(IEnumerable<PizzaSlice> slices)
		{
			/*foreach (var slice1 in slices)
			{
				foreach (var slice2 in slices)
				{
					if (slice1 == slice2)
						continue;

					if (slice1.DoesOverlap(slice2))
						return true;
				}
			}*/

			return false;
		}

		public int Score { get; private set; }
	}
}
