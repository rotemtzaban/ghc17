using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
    public class Warehouse : IndexedObject
    {
        public Coordinate Location { get; set; }

        public Dictionary<Product, int> Products { get; set; }

        public Warehouse(int index)
            :base (index)
        {
        }
    }
}
