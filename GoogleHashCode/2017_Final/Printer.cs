using System;
using HashCodeCommon;
using System.IO;

namespace _2017_Final
{
    internal class Printer : PrinterBase<ProblemOutput>
    {
        public override void PrintToConsole(ProblemOutput result)
        {
            //throw new NotImplementedException();
        }

        public override void PrintToFile(ProblemOutput result, string outputPath)
        {
            using (var writer = new StreamWriter(outputPath))
            {
                WriteCoordinateArray(writer, result.BackBoneCoordinates);
                WriteCoordinateArray(writer, result.RouterCoordinates);
            }
        }

        private void WriteCoordinateArray(StreamWriter writer, MatrixCoordinate[] backBoneCoordinates)
        {
            writer.WriteLine(backBoneCoordinates.Length);
            foreach (var item in backBoneCoordinates)
            {
                writer.WriteLine(item.Row  + " " + item.Column);
            }
        }
    }
}