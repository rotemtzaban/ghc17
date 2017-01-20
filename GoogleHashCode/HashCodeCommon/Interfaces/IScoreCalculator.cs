using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public interface IScoreCalculator<TIn, TOut>
    {
        int Calculate(TIn input, TOut output);
        int Calculate(TIn input, string path);
    }
}
