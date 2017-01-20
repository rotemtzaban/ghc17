using System;
using HashCodeCommon;

namespace DronesProblem
{
	public class WorkItem
	{
		public Product Item { get; set; }
		public Coordinate Destination { get; set; }

		public WorkItem (Product item, Coordinate dest)
		{
			this.Item = item;
			this.Destination = dest;
		}
	}
}

