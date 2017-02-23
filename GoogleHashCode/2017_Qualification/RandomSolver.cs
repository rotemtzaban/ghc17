using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017_Qualification
{
    public class RandomSolver : Solver
    {
        private int m_rand;

        protected override ProblemOutput Solve(ProblemInput input)
        {
            m_rand = this.NumbersGenerator.Next() % 10;
            return base.Solve(input);
        }

        protected override IEnumerable<RequestsDescription> GetBestCurrentRequests(int bulkSize)
        {
            var list = base.GetBestCurrentRequests(bulkSize).ToList();
            for (int i = 1; i < list.Count; i++)
            {
                int number = this.NumbersGenerator.Next();
                if (number % m_rand == 0)
                {
                    var temp = list[i];
                    list[i] = list[i + 1];
                    list[i + 1] = temp;
                }
            }

            return list;
        }
    }
}
