using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public abstract class PrinterBase<T> : IPrinter<T>
    {
        private int m_ConsoleColorCount = 0;
        private static ConsoleColor[] s_PrintedColors = new ConsoleColor[]
        {
            ConsoleColor.Red,
            ConsoleColor.Yellow,
            ConsoleColor.Magenta,
            ConsoleColor.White,
            ConsoleColor.Cyan
        };

        protected ConsoleColor GetNextColor()
        {
            int index = m_ConsoleColorCount % s_PrintedColors.Length;
            m_ConsoleColorCount++;
            return s_PrintedColors[index];
        }

        public abstract void PrintToFile(T result, string outputPath);

        public abstract void PrintToConsole(T result);
    }
}
