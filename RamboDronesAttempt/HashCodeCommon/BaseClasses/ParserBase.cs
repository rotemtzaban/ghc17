﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
    public abstract class ParserBase<TInput> : IParser<TInput>
    {
        public TInput ParseFromData(string data)
        {
            using (var reader = new StringReader(data))
            {
                return ParseFromStream(reader);
            }
        }

        public TInput ParseFromPath(string path)
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

        public static long ReadLineAsLong(TextReader reader)
        {
            string line = reader.ReadLine();
            long result = long.Parse(line);
            return result;
        }

        public static long[] ReadLineAsLongArray(TextReader reader)
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

        protected abstract TInput ParseFromStream(TextReader reader);
    }
}
