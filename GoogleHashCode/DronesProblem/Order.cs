using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
    public class Order
    {
        private static int s_ID = 0;
        public int ID { get; set; }
        public Coordinate Location { get; set; }

        public List<Product> WantedProducts { get; set; }

        public Order()
        {
            this.ID = s_ID++;
        }

        public Order(Order other)
        {
            this.ID = other.ID;
            this.Location = other.Location;
            this.WantedProducts = new List<Product>();

            foreach (var item in other.WantedProducts)
            {
                this.WantedProducts.Add(new Product(item));
            }
        }

		public override bool Equals (object obj)
		{
			Order o = obj as Order;
			return o.ID.Equals (this.ID);
		}

		public override int GetHashCode ()
		{
			return this.ID;
		}
    }
}
