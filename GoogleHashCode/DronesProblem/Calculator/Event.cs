using System.Collections.Generic;

namespace DronesProblem
{
	public class Event
	{
        public int Turn { get; set; }

		public List<Product> ProductsTaken { get; set; }
		public Warehouse Warehouse { get; set; }

        public List<Product> ProductsDelivered { get; set; }
		public Order CurrentOrder { get; set; }
	}
}