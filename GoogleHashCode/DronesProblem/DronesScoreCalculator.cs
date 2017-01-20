using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DronesProblem
{
	public class DronesScoreCalculator : ScoreCalculatorBase<DronesInput, DronesOutput>
	{
		public override int Calculate(DronesInput input, DronesOutput output)
		{
			throw new NotImplementedException();
		}

		protected override DronesOutput GetResultFromReader(DronesInput input, StreamReader reader)
		{
			var commands = new List<CommandBase>();
			var commandCount = int.Parse(reader.ReadLine());
			for (int i = 0; i < commandCount; i++)
			{
				var line = reader.ReadLine();
				
			}


			return null;
		}
	}
}
