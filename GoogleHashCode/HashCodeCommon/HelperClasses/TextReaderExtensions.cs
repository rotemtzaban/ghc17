using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon.HelperClasses
{
	public static class TextReaderExtensions
	{
		public static int GetInt(this TextReader reader)
		{
			return int.Parse(reader.ReadLine());
		}

		public static string[] GetStringList(this TextReader reader)
		{
			return reader.ReadLine().Split(' ');
		}

		public static List<int> GetIntList(this TextReader reader)
		{
			return reader.GetStringList().Select(int.Parse).ToList();
		}

		public static long GetLong(this TextReader reader)
		{
			return long.Parse(reader.ReadLine());
		}

		public static List<long> GetLongList(this TextReader reader)
		{
			return reader.GetStringList().Select(long.Parse).ToList();
		}

		public static BigInteger GetBigInt(this TextReader reader)
		{
			BigInteger value;
			BigInteger.TryParse(reader.ReadLine(), out value);
			return value;
		}

		public static List<BigInteger> GetBigIntList(this TextReader reader)
		{
			return reader.GetStringList().Select(BigInteger.Parse).ToList();
		}

		public static double GetDouble(this TextReader reader)
		{
			return double.Parse(reader.ReadLine());
		}

		public static List<double> GetDoubleList(this TextReader reader)
		{
			return reader.GetStringList().Select(double.Parse).ToList();
		}
	}
}
