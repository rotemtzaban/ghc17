using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public interface IScoreCalculator<T>
    {
        int Calculate(T result);
        int Calculate(string path);
    }
}
