using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
			return string.Format("{0} {1} {2} {3} {4}", Drone.ID, Tag, Order.ID, Product.ID, ProductCount);
		}
	}
}
