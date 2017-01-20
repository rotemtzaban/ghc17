using HashCodeCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronesProblem
{
    public class DronesPrinter : IPrinter<DronesOutput>
    {
        public void PrintToConsole(DronesOutput result)
        {
            throw new NotImplementedException();
        }

        public void PrintToFile(DronesOutput result, string outputPath)
        {
            throw new NotImplementedException();
        }
    }
}
