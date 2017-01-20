using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HashCodeCommon;

namespace DronesProblem.Commands
{
	public class DeliverCommand : CommandBase
	{
		public DeliverCommand(Drone drone, Order order, Product product, int productCount)
		{
			Drone = drone;
			Order = order;
			Product = product;
			ProductCount = productCount;

			this.TurnsToComplete = (uint)Math.Ceiling(drone.GetExpectedLocation().CalcEucledianDistance(order.Location)) + 1; // TODO: debug correctness
			this.ExpectedLocation = Order.Location;
		}

		public Order Order { get; set; }
		public Product Product { get; set; }
		public int ProductCount { get; set; }
		public override string Tag
		{
			get { return "D"; }
		}

		public override string GetOutputLine()
		{
			return string.Format("{0} {1} {2} {3} {4}", Drone.Index, Tag, Order.Index, Product.Index, ProductCount);
		}
	}
}
