using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public abstract class SolverBase<TInput, TOutput> : ISolver<TInput, TOutput>
    {
        protected Random NumbersGenerator { get; private set; }

        protected abstract TOutput Solve(TInput input);

        public TOutput Solve(TInput input, Random random)
        {
            this.NumbersGenerator = random;
            return Solve(input);
        }
    }
}
