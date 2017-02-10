using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public interface IPrinter<T>
    {
        void PrintToFile(T result, string outputPath);

        void PrintToConsole(T result);
    }
}
