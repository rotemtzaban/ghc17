using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem.Commands
{
	public class LoadCommand : InventoryCommand
	{
		public LoadCommand(Drone drone, Warehouse warehouse, Product product, int productCount)
		{
			Drone = drone;
			Warehouse = warehouse;
			Product = product;
			ProductCount = productCount;
			this.TurnsToComplete = 1;
		}

		public override string Tag
		{
			get { return "L"; }
		}
	}
}
