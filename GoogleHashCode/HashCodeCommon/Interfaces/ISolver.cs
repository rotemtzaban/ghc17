using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public interface ISolver<TInput, TOutput>
    {
        TOutput Solve(TInput input, Random random, string problemName);
    }
}
