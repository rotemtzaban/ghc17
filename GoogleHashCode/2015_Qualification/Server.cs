using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015_Qualification
{
    public class Server : IndexedObject
    {
        public Server(int index, int slots, int capacity)
            : base (index)
        {
            this.Capacity = capacity;
            this.Slots = slots;
        }

        public int Capacity { get; set; }

        public int Slots { get; set; }

        public override string ToString()
        {
            return "Index: " + this.Index + ",Slots: " + this.Slots + ",Capacity: " + this.Capacity;
        }
    }
}
