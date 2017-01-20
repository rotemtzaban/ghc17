using HashCodeCommon.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
	public class Product : IndexedObject
	{
		public uint Weight { get; private set; }

		public Product(int index, string weight)
            :base (index)
		{
			Weight = uint.Parse(weight);
		}

        public Product(Product other)
            :base (other.Index)
        {
            this.Weight = other.Weight;
        }
    }
}
