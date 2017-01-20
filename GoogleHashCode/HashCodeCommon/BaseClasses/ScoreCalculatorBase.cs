using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public abstract class ScoreCalculatorBase<TIn, TOut> : IScoreCalculator<TIn, TOut>
    {
	    public abstract int Calculate(TIn input, TOut output);

	    public int Calculate(TIn input, string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                var lastResult = GetResultFromReader(reader);
                return Calculate(input, lastResult);
            }
        }

        protected abstract TOut GetResultFromReader(StreamReader reader);
    }
}
