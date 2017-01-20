using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using DronesProblem.Commands;

namespace DronesProblem
{
	public class DronesScoreCalculator : ScoreCalculatorBase<DronesInput, DronesOutput>
	{
		public override int Calculate(DronesInput input, DronesOutput output)
		{
			foreach (var droneCommands in output.Commands.GroupBy(c => c.Drone.ID))
			{
				var drone = input.Drones[(int)droneCommands.Key];
				foreach (var command in droneCommands)
				{
					
				}
			}

			return -1;
		}

		protected override DronesOutput GetResultFromReader(DronesInput input, StreamReader reader)
		{
			var commands = new List<CommandBase>();
			var commandCount = int.Parse(reader.ReadLine());
			for (int i = 0; i < commandCount; i++)
			{
				var line = reader.ReadLine();
				var spl = line.Split(' ');
				var drone = input.Drones[int.Parse(spl[0])];

				var others = spl.Skip(2).Select(int.Parse).ToList();

				CommandBase newCommand;
				switch (spl[1])
				{
					case "D":
						newCommand = new DeliverCommand(drone, input.Orders[others[0]], input.Products[others[1]], others[2]);
						break;
					case "W":
						newCommand = new WaitCommand(drone, (uint)others[0]);
						break;
					case "U":
						newCommand = new UnloadCommand(drone, input.WareHouses[others[0]], input.Products[others[1]], others[2]);
						break;
					case "L":
						newCommand = new LoadCommand(drone, input.WareHouses[others[0]], input.Products[others[1]], others[2]);
						break;
					default:
						throw new ArgumentException(string.Format("Unknown command {0}", spl[1]));
				}

				commands.Add(newCommand);
			}

			return new DronesOutput { Commands = commands };
		}
	}
}
