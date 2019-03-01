using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public class ParallelHelper
    {
        public static bool InterlockedExchangeIfGreaterThan(ref long variable, long newValue)
        {
            return InterlockedExchangeIfGreaterThan(ref variable, newValue, newValue);
        }

        public static bool InterlockedExchangeIfGreaterThan(ref long variable, long valueToCompare, long newValue)
        {
            long initialValue;
            do
            {
                initialValue = Interlocked.Read(ref variable);
                if (initialValue >= valueToCompare) return false;
            }
            while (Interlocked.CompareExchange(ref variable, newValue, initialValue) != initialValue);
            return true;
        }
    }
}
