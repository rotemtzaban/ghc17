using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
    public class Order : IndexedObject
    {
        public Coordinate Location { get; set; }

        public List<Product> WantedProducts { get; set; }

        public Order(int index)
            :base (index)
        {
        }
    }
}
