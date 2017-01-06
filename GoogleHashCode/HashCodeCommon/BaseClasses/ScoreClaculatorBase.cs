using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public abstract class ScoreClaculatorBase<T> : IScoreCalculator<T>
    {
        public int Calculate(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                T lastResult = GetResultFromReader(reader);
                return Calculate(lastResult);
            }
        }

        protected abstract T GetResultFromReader(StreamReader reader);

        public abstract int Calculate(T result);
    }
}
