using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
	public class Product : IndexedObject<Product>
	{
		public uint Weight { get; private set; }

		public Product(int index, string weight)
            :base (index)
		{
			Weight = uint.Parse(weight);
		}

        public Product(int index, uint weight)
            : base(index)
        {
            Weight = weight;
        }

        public override Product Clone()
        {
            return new Product(this.Index, this.Weight);
        }
    }
}
