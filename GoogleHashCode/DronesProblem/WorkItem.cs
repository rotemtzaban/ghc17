using System;
using HashCodeCommon;

namespace DronesProblem
{
	public class WorkItem
	{
		public Order ParentOrder { get; set;}

		public Product Item { get; set; }
		public Coordinate Destination { get; set; }

		public WorkItem (Product item, Coordinate dest, Order parentOrder)
		{
			this.Item = item;
			this.Destination = dest;
			this.ParentOrder = parentOrder;
		}
	}
}

