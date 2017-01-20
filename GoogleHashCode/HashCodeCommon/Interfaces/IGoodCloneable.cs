using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon.Interfaces
{
    public interface IGoodCloneable<T>
    {
        T Clone();
    }
}
