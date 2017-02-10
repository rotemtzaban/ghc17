using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015_Qualification
{
    public class Pool : IndexedObject
    {
        public Pool(int index)
            :base(index)
        {
        }

        public List<Server> Servers { get; private set; }
    }
}
