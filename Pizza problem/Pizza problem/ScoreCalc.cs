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
			Score = 0;
			var slices = new List<PizzaSlice>();
			using (var reader = new StreamReader(resultPath))
			{
				var slicesCount = int.Parse(reader.ReadLine());
				for (int i = 0; i < slicesCount; i++)
				{
					var line = reader.ReadLine();
					var spl = line.Split(' ').Select(int.Parse).ToList();

					var slice = new PizzaSlice(spl[1], spl[0], spl[3], spl[2]);
					Score += slice.Size;
				}
			}
		}

		public int Score { get; private set; }
	}
}
