using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
	public class Product
	{
		private static int s_ID = 0;

		public int ID { get; private set; }

		public uint Weight { get; private set; }

		public Product(string weight)
		{
			Weight = uint.Parse(weight);
			this.ID = s_ID++;
		}

        public Product(Product other)
        {
            this.ID = other.ID;
            this.Weight = other.Weight;
        }

        public override bool Equals(object obj)
        {
            Product other = obj as Product;
            if (other == null)
            {
                return false;
            }

            return this.ID == other.ID && this.Weight == other.Weight;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }
    }
}
