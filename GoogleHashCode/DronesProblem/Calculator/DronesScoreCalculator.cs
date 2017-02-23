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
		public override long Calculate(DronesInput input, DronesOutput output)
		{
			var events = CreateEvents(input, output);

			int score = 0;
			foreach (Event currEvent in events)
			{
                // Console.WriteLine(currEvent);
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
                currEvent.Warehouse.Products[currEvent.ProductTaken] -= 1;
                if (currEvent.Warehouse.Products[currEvent.ProductTaken] < 0)
				{
					throw new Exception("item not in warehouse");
				}
			}
		}

		public override DronesOutput GetResultFromReader(DronesInput input, TextReader reader)
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
			foreach (var droneCommands in output.Commands.GroupBy(c => c.Drone.Index))
			{
				HandleSingleDroneCommands(input, droneCommands, allEvents);
			}

			allEvents.Sort((a, b) => a.Turn.CompareTo(b.Turn));

			return allEvents;
		}

		private static void HandleSingleDroneCommands(DronesInput input, IGrouping<int, CommandBase> droneCommands, List<Event> allEvents)
		{
			var drone = input.Drones[droneCommands.Key];
			long currentTurn = 0;
			var droneLocation = drone.Location;
			long carriedWeight = 0;
			var carriedProducts = new Dictionary<Product, int>();

			foreach (var command in droneCommands)
			{
				if (command is WaitCommand)
					currentTurn += (command as WaitCommand).TurnCount;
				else if (command is LoadCommand)
					allEvents.Add(HandleLoadCommand(input, command as LoadCommand, carriedProducts, drone, ref droneLocation, ref currentTurn, ref carriedWeight));
				else if (command is UnloadCommand)
					allEvents.Add(HandleUnloadCommand(command as UnloadCommand, carriedProducts, drone, ref droneLocation, ref currentTurn, ref carriedWeight));
				else if (command is DeliverCommand)
					allEvents.Add(HandleDeliverCommand(command as DeliverCommand, carriedProducts, drone, ref droneLocation, ref currentTurn, ref carriedWeight));
			}
		}

		private static Event HandleDeliverCommand(DeliverCommand deliverCommand, Dictionary<Product, int> carriedProducts, Drone drone,
			ref Coordinate droneLocation, ref long currentTurn, ref long carriedWeight)
		{
			var distance = droneLocation.CalcEucledianDistance(deliverCommand.Order.Location);
			currentTurn += ((int) Math.Ceiling(distance)) + 1;
			droneLocation = deliverCommand.Order.Location;
			carriedWeight -= deliverCommand.Product.Weight*deliverCommand.ProductCount;
			var newCount = carriedProducts.GetOrDefault(deliverCommand.Product, 0) - deliverCommand.ProductCount;
			if (newCount < 0)
				throw new Exception(string.Format("Drone {0} attempted to deliver {1} products of type {2} which he doesn't have",
					drone.Index, deliverCommand.ProductCount, deliverCommand.Product.Index));

			carriedProducts[deliverCommand.Product] = newCount;

			var ev = new Event
			{
				Turn = currentTurn,
				CurrentOrder = deliverCommand.Order,
				ProductDelivered = deliverCommand.Product,
				DeliveredCount = deliverCommand.ProductCount,
				Drone = drone
			};
			return ev;
		}

		private static Event HandleUnloadCommand(UnloadCommand unloadCommand, Dictionary<Product, int> carriedProducts, Drone drone,
			ref Coordinate droneLocation, ref long currentTurn, ref long carriedWeight)
		{
			var distance = droneLocation.CalcEucledianDistance(unloadCommand.Warehouse.Location);
			currentTurn += ((int) Math.Ceiling(distance)) + 1;
			droneLocation = unloadCommand.Warehouse.Location;
			carriedWeight -= unloadCommand.Product.Weight*unloadCommand.ProductCount;
			var newCount = carriedProducts.GetOrDefault(unloadCommand.Product, 0) - unloadCommand.ProductCount;
			if (newCount < 0)
				throw new Exception(string.Format("Drone {0} attempted to unload {1} products of type {2} which he doesn't have",
					drone.Index, unloadCommand.ProductCount, unloadCommand.Product.Index));

			carriedProducts[unloadCommand.Product] = newCount;

			var ev = new Event
			{
				Turn = currentTurn,
				Warehouse = unloadCommand.Warehouse,
				ProductDelivered = unloadCommand.Product,
				DeliveredCount = unloadCommand.ProductCount,
				Drone = drone
			};
			return ev;
		}

		private static Event HandleLoadCommand(DronesInput input, LoadCommand loadCommand, Dictionary<Product, int> carriedProducts,
			Drone drone, ref Coordinate droneLocation, ref long currentTurn, ref long carriedWeight)
		{
			var distance = droneLocation.CalcEucledianDistance(loadCommand.Warehouse.Location);
			currentTurn += ((int) Math.Ceiling(distance)) + 1;
			droneLocation = loadCommand.Warehouse.Location;
			carriedWeight += loadCommand.Product.Weight*loadCommand.ProductCount;
			carriedProducts[loadCommand.Product] = carriedProducts.GetOrDefault(loadCommand.Product, 0) + loadCommand.ProductCount;

			if (carriedWeight > input.MaxWeight)
				throw new Exception(string.Format("Drone {0} is carrying {1} weight in turn {2}, which is more than maximum ({3})",
					drone.Index, carriedWeight, currentTurn, input.MaxWeight));

			var ev = new Event
			{
				Turn = currentTurn,
				Warehouse = loadCommand.Warehouse,
				ProductTaken = loadCommand.Product,
				TakenCount = loadCommand.ProductCount,
				Drone = drone
			};
			return ev;
		}
	}
}
