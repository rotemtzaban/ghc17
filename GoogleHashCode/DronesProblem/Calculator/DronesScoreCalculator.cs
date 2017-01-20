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
			var events = CreateEvents(input, output);

            int score = 0;
            foreach (Event currEvent in events)
            {
                ValidateEvent(currEvent);

                score += CalculateScore(currEvent, input);
            }

			return score;
		}

        private int CalculateScore(Event currEvent, DronesInput input)
        {
            if (currEvent.ProductDelivered != null)
            {
                if (!currEvent.CurrentOrder.WantedProducts.Remove(currEvent.ProductDelivered))
                {
                    throw new Exception("Deliver not existing item");
                }

                if (currEvent.CurrentOrder.WantedProducts.Count == 0)
                {
                    int mone = input.NumOfTurns - (int)currEvent.Turn;
                    double mechane = (double)input.NumOfTurns;
                    return (int)Math.Ceiling((mone / mechane) * 100);
                }
            }

            return 0;
        }

        private void ValidateEvent(Event currEvent)
        {
            if (currEvent.ProductTaken != null)
            {
                if (!currEvent.Warehouse.Products.Remove(currEvent.ProductTaken))
                {
                    throw new Exception("item not in warehouse");
                }
            }
        }

        public override DronesOutput GetResultFromReader(DronesInput input, StreamReader reader)
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

		private static List<Event> CreateEvents(DronesInput input, DronesOutput output)
		{
			var allEvents = new List<Event>();
			foreach (var droneCommands in output.Commands.GroupBy(c => c.Drone.ID))
			{
				var drone = input.Drones[(int)droneCommands.Key];
				long currentTurn = 0;
				var droneLocation = drone.Location;
				foreach (var command in droneCommands)
				{
					if (command is WaitCommand)
					{
						var waitCommand = command as WaitCommand;
						currentTurn += waitCommand.TurnCount;
						continue;
					}

					if (command is LoadCommand)
					{
						var loadCommand = command as LoadCommand;
						var distance = droneLocation.CalcEucledianDistance(loadCommand.Warehouse.Location);
						currentTurn += ((int)Math.Ceiling(distance)) + 1;
						droneLocation = loadCommand.Warehouse.Location;

						var ev = new Event
						{
							Turn = currentTurn,
							Warehouse = loadCommand.Warehouse,
							ProductTaken = loadCommand.Product,
							TakenCount = loadCommand.ProductCount
						};

						allEvents.Add(ev);
						continue;
					}

					if (command is UnloadCommand)
					{
						var unloadCommand = command as UnloadCommand;
						var distance = droneLocation.CalcEucledianDistance(unloadCommand.Warehouse.Location);
						currentTurn += ((int)Math.Ceiling(distance)) + 1;
						droneLocation = unloadCommand.Warehouse.Location;

						var ev = new Event
						{
							Turn = currentTurn,
							Warehouse = unloadCommand.Warehouse,
							ProductDelivered = unloadCommand.Product,
							DeliveredCount = unloadCommand.ProductCount
						};

						allEvents.Add(ev);
						continue;
					}

					if (command is DeliverCommand)
					{
						var deliverCommand = command as DeliverCommand;
						var distance = droneLocation.CalcEucledianDistance(deliverCommand.Order.Location);
						currentTurn += ((int)Math.Ceiling(distance)) + 1;
						droneLocation = deliverCommand.Order.Location;

						var ev = new Event
						{
							Turn = currentTurn,
							CurrentOrder = deliverCommand.Order,
							ProductDelivered = deliverCommand.Product,
							DeliveredCount = deliverCommand.ProductCount
						};

						allEvents.Add(ev);
						continue;
					}
				}
			}

			allEvents.Sort((a, b) => a.Turn.CompareTo(b.Turn));

			return allEvents;
		}
	}
}
