using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2017_Qualification
{
    public class RequestsDescription : IndexedObject
    {
        public int NumOfRequests { get; set; }
        public Video Video { get; set; }
        public EndPoint Endpoint { get; set; }

        public RequestsDescription(int index)
            :base(index)
        {
        }
    }
}
