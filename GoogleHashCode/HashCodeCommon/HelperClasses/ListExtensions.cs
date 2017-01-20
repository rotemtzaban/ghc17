using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public static class ListExtensions
    {
        public static List<T> DeepClone<T>(this List<T> lst)
            where T : IGoodCloneable<T>
        {
            List<T> clonedList = new List<T>(lst.Count);
            foreach (var item in lst)
            {
                clonedList.Add(item.Clone());
            }

            return clonedList;
        }
    }
}
