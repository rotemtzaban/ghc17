using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem.Commands
{
	public abstract class InventoryCommand : CommandBase
	{
		public Warehouse Warehouse { get; set; }
		public Product Product { get; set; }
		public int ProductCount { get; set; }

		public override string GetOutputLine()
		{
			return string.Format("{0} {1} {2} {3} {4}", Drone.Index, Tag, Warehouse.Index, Product.Index, ProductCount);
		}
	}
}
