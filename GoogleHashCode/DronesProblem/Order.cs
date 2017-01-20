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
        public Coordinate Location { get; set; }

        public List<Product> WantedProducts { get; set; }
    }
}
