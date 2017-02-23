using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017_Qualification
{
    public class Video : IndexedObject
    {
        public int Size { get; set; }

        public Video(int index)
            :base(index)
        {

        }
    }
}
