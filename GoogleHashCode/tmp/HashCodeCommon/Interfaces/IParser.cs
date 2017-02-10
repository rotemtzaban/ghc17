using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public interface IParser<T>
    {
        T ParseFromPath(string path);

        T ParseFromData(string path);
    }
}
