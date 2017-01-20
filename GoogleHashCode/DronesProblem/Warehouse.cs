using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
    public class Warehouse : ClonedIndexedObject<Warehouse>
    {
        public Coordinate Location { get; set; }

        public Dictionary<Product, int> Products { get; set; }

        public Warehouse(int index)
            :base (index)
        {
        }

        public override Warehouse Clone()
        {
            Warehouse cloned = new Warehouse(this.Index);
            cloned.Location = this.Location;
            cloned.Products = new Dictionary<DronesProblem.Product, int>();

            foreach (var item in this.Products)
            {
                Product clone = item.Key.Clone();
                cloned.Products.Add(clone, item.Value);
            }
            return cloned;
        }
    }
}
