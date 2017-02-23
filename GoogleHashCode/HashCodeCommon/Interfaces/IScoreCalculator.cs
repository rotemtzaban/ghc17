using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public interface IScoreCalculator<TIn, TOut>
    {
        long Calculate(TIn input, TOut output);
        long Calculate(TIn input, string path);
    }
}
