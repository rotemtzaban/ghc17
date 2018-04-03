using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public abstract class ParserBase<T> : IParser<T>
    {
        public T ParseFromData(string data)
        {
            using (var reader = new StringReader(data))
            {
                return ParseFromStream(reader);
            }
        }

        public T ParseFromPath(string path)
        {
            using (var reader = new StreamReader(path))
            {
                return ParseFromStream(reader);
            }
        }

        public static Coordinate ReadLineAsCoordinate(TextReader reader)
        {
            string[] locationAsString = reader.ReadLine().Split(' ');
            return new Coordinate(int.Parse(locationAsString[0]), int.Parse(locationAsString[1]));
        }

        public long ReadLineAsLong(TextReader reader)
        {
            string line = reader.ReadLine();
            long result = long.Parse(line);
            return result;
        }

        public long[] ReadLineAsLongArray(TextReader reader)
        {
            string line = reader.ReadLine();
            string[] splitedLine = line.Split(' ');
            long[] result = new long[splitedLine.Length];
            for (int index = 0; index < splitedLine.Length; index++)
            {
                result[index] = long.Parse(splitedLine[index]);
            }

            return result;
        }

        public int[] ReadLineAsIntArray(TextReader reader)
        {
            string line = reader.ReadLine();
            string[] splitedLine = line.Split(' ');
            int[] result = new int[splitedLine.Length];
            for (int index = 0; index < splitedLine.Length; index++)
            {
                result[index] = int.Parse(splitedLine[index]);
            }

            return result;
        }

        protected abstract T ParseFromStream(TextReader reader);
    }
}
