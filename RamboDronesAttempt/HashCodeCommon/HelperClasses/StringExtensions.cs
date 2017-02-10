using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon.HelperClasses
{
	public static class StringExtensions
	{
		public static string Repeat(this string s, int n)
		{
			StringBuilder builder = new StringBuilder(s.Length * n);

			for (int i = 0; i < n; i++)
				builder.Append(s);

			return builder.ToString();
		}
	}
}
