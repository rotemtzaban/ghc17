using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DronesProblem
{
    public class DronesScoreCalculator : ScoreClaculatorBase<DronesOutput>
    {
        public override int Calculate(DronesOutput result)
        {
            throw new NotImplementedException();
        }

        protected override DronesOutput GetResultFromReader(StreamReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
