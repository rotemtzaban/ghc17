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

        public int GurranteedCapacity(ProblemOutput output)
        {
            Dictionary<int, int> rowsCpacity = new Dictionary<int, int>();
	        var allocated = output._allocations.Values.Where(allocation => Equals(allocation.Pool, this));

            foreach (var server in allocated)
            {
                if (rowsCpacity.ContainsKey(server.Row))
                {
                    rowsCpacity[server.Row] += server.Server.Capacity;
                }
                else
                {
                    rowsCpacity.Add(server.Row,  server.Server.Capacity);
                }
            }

            var maxRow = rowsCpacity.Max(_ => _.Value);
            rowsCpacity.Remove(maxRow);
            return rowsCpacity.Sum(_ => _.Value);
        }
    }
}
