using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
    public class Warehouse
    {
        public Coordinate Location { get; set; }

        public Dictionary<Product, int> Products { get; set; }
    }
}
