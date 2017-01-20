using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon.BaseClasses
{
    public class ObjectBase
    {
        private static int s_ID = 0;
        public int ID { get; private set; }

        public ObjectBase()
        {
            this.ID = s_ID++;
        }

        public ObjectBase(ObjectBase other)
        {
            this.ID = other.ID;
        }

        public override bool Equals(object obj)
        {
            ObjectBase other = obj as ObjectBase;
            if (other == null)
            {
                return false;
            }

            return this.ID == other.ID;
        }

        public override int GetHashCode()
        {
            return this.ID;
        }
    }
}
