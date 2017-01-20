using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
    public struct Product
    {
        private static int s_ID = 0;

        public int ID { get; private set; }

        public uint Weight { get; private set; }

        public Product(string weight)
        {
            Weight = uint.Parse(weight);
            this.ID = s_ID++;
        }
    }
}
