using System.Collections.Generic;

namespace DronesProblem
{
	public class Event
	{
        public long Turn { get; set; }

		public Warehouse Warehouse { get; set; }
		public Order CurrentOrder { get; set; }
		
		public Product ProductTaken { get; set; }
		public int TakenCount { get; set; }

		public Product ProductDelivered { get; set; }
		public int DeliveredCount{ get; set; }
	}
}