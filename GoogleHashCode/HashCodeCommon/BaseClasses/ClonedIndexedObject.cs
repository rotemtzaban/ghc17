using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public abstract class ClonedIndexedObject<T> : IndexedObject, IGoodCloneable<T>
    {
        public ClonedIndexedObject(int index)
            :base (index)
        {
        }

        public abstract T Clone();
    }
}
