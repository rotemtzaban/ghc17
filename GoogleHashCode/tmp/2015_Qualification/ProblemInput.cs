using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2015_Qualification
{
    public class ProblemInput
    {
        public List<Coordinate> UnavilableSlots { get; set; }

        public List<Server> Servers { get; set; }

        public List<Pool> Pools { get; set; }

        public int Rows { get; set; }

        public int Columns { get; set; }
    }
}
