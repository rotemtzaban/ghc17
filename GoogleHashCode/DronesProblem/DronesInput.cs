using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
    public class DronesInput
    {
        public List<Drone> Drones { get; set; }

        public List<Warehouse> WareHouses { get; set; }

        public List<Product> Products { get; set; }

        public List<Order> Orders { get; set; }

        public int MaxWeight { get; set; }

        public int NumOfTurns { get; set; }

        public int NumOfRows { get; set; }

        public int NumOfColumns { get; set; }
    }
}
