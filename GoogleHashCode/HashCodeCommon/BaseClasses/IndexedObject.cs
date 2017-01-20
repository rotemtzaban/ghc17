using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public abstract class IndexedObject
    {
        public int Index { get; private set; }

        public IndexedObject(int index)
        {
            this.Index = index;
        }

        public override bool Equals(object obj)
        {
            IndexedObject other = obj as IndexedObject;
            if (other == null)
            {
                return false;
            }

            return this.Index == other.Index;
        }

        public override int GetHashCode()
        {
            return this.Index;
        }

        public override string ToString()
        {
            return "Index: " + this.Index;
        }
    }
}
