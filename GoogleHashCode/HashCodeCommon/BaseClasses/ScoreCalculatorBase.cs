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
	    public abstract long Calculate(TIn input, TOut output);

	    public long Calculate(TIn input, string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                var lastResult = GetResultFromReader(input, reader);
                return Calculate(input, lastResult);
            }
        }

        public abstract TOut GetResultFromReader(TIn input, TextReader reader);
    }
}
