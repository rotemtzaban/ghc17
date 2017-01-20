using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
    public class Order : ClonedIndexedObject<Order>
    {
        public Coordinate Location { get; set; }

        public List<Product> WantedProducts { get; set; }

        public Order(int index)
            :base (index)
        {
        }

        public override Order Clone()
        {
            Order cloned = new DronesProblem.Order(this.Index);
            cloned.Location = this.Location;
            cloned.WantedProducts = WantedProducts.DeepClone();

            return cloned;
        }
    }
}
