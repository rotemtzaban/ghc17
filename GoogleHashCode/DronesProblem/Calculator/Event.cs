using System.Collections.Generic;

namespace DronesProblem
{
	public class Event
	{
		int Turn { get; set; }

		List<Product> ProductsTaken { get; set; }
		public Warehouse Warehouse { get; set; }

		List<Product> ProductsDelivered { get; set; }
		public Order CurrentOrder { get; set; }
	}
}