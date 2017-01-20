using System.Collections.Generic;

namespace DronesProblem
{
	public class Event
	{
        public long Turn { get; set; }

		public Drone Drone { get; set; }

		public Warehouse Warehouse { get; set; }
        public Order CurrentOrder { get; set; }
		
		public Product ProductTaken { get; set; }
		public int TakenCount { get; set; }

		public Product ProductDelivered { get; set; }
		public int DeliveredCount{ get; set; }

        public override string ToString()
        {
            if (ProductTaken != null)
            {
                return string.Format("drone {0} take product {1} from warehous {2} ", Drone.Index, ProductTaken.Index, Warehouse.Index);
            }
            else
            {
                return string.Format("drone {0} delivered product {1} to order {2} ", Drone.Index, ProductDelivered.Index, CurrentOrder.Index);
            }
        }
    }
}