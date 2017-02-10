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
        public Server(int index)
            : base (index)
        {
        }

        public int Capacity { get; set; }

        public int Slots { get; set; }
    }
}
