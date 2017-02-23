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

        protected override RequestsDescription GetBestCurrentRequest()
        {
            int number = this.NumbersGenerator.Next();
            if (number % m_rand == 0)
            {
                var availableDescriptions = _input.RequestsDescriptions.Where(HasAvailableDescription).ToList();
                if (!availableDescriptions.Any())
                    return null;
                RequestsDescription max = availableDescriptions.ArgMax(CalculateRequestValue);
                availableDescriptions.Remove(max);

                return availableDescriptions.ArgMax(CalculateRequestValue);
            }
            else
            {
                return base.GetBestCurrentRequest();
            }
        }
    }
}
