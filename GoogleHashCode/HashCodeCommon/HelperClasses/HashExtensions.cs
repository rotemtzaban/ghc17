using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon.HelperClasses
{
    public static class HashExtensions
    {
        public static int GetHash(params object[] t)
        {
            int[] primeNumbers = new int[] { 17, 23, 29, 31 };

            int n = 0;
            int count = 0;
            unchecked
            {
                foreach (var item in t)
                {
                    n += item.GetHashCode() * primeNumbers[count % primeNumbers.Length];
                    count++;
                }
            }

            return n;
        }
    }
}
