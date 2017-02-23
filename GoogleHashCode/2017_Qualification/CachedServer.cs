using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017_Qualification
{
    public class CachedServer : IndexedObject
    {
        public int Capacity { get; set; }

        public CachedServer(int index)
            : base(index)
        {
        }
    }
}
