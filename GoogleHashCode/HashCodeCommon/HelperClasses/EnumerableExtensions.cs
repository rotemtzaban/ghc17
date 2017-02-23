using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
	public static class EnumerableExtensions
	{
		public static IEnumerable<T> DeepClone<T>(this IEnumerable<T> lst)
			where T : IGoodCloneable<T>
		{
			return lst.Select(x => x.Clone());
		}

		public static void ArgMax<T>(this IEnumerable<T> list, out T max, out int argMax)
			where T : IComparable<T>
		{
			if (!list.Any())
			{
				argMax = -1;
				max = default(T);
				return;
			}

			argMax = 0;
			max = list.First();
			int i = 1;
			foreach (T t in list.Skip(1))
			{
				if (t.CompareTo(max) > 0)
				{
					max = t;
					argMax = i;
				}
				i++;
			}
		}

		public static T ArgMax<T, S>(this IEnumerable<T> list, Func<T, S> selector)
			where S : IComparable<S>
		{
			if (!list.Any())
			{
				return default(T);
			}
			T argMax = list.First();
			S max = selector(argMax);
			foreach (T t in list.Skip(1))
			{
				S selected = selector(t);
				if (selected.CompareTo(max) > 0)
				{
					max = selected;
					argMax = t;
				}
			}
			return argMax;
		}

		public static void ArgMin<T>(this IEnumerable<T> list, out T min, out int argMin)
			where T : IComparable<T>
		{
			if (!list.Any())
			{
				argMin = -1;
				min = default(T);
				return;
			}

			argMin = 0;
			min = list.First();
			int i = 1;
			foreach (T t in list.Skip(1))
			{
				if (t.CompareTo(min) < 0)
				{
					min = t;
					argMin = i;
				}
				i++;
			}
		}

		public static T ArgMin<T, S>(this IEnumerable<T> list, Func<T, S> selector)
			where S : IComparable<S>
		{
			if (!list.Any())
			{
				return default(T);
			}
			T argMin = list.First();
			S min = selector(argMin);
			foreach (T t in list.Skip(1))
			{
				S selected = selector(t);
				if (selected.CompareTo(min) < 0)
				{
					min = selected;
					argMin = t;
				}
			}
			return argMin;
		}

		public static T ArgMin<T, S>(this IEnumerable<T> list, Func<T, S> selector, out S value)
			where S : IComparable<S>
		{
			if (!list.Any())
			{
				value = default(S);
				return default(T);
			}
			T argMin = list.First();
			S min = selector(argMin);
			foreach (T t in list.Skip(1))
			{
				S selected = selector(t);
				if (selected.CompareTo(min) < 0)
				{
					min = selected;
					argMin = t;
				}
			}

			value = min;
			return argMin;
		}

		public static T GetMedian<T>(this IEnumerable<T> list, Comparison<T> comparison = null)
		{
			var l = list.ToList();
			if (comparison != null)
			{
				l.Sort(comparison);
			}
			else
			{
				l.Sort();
			}
			return l[l.Count / 2];
		}

		public static T RandomElement<T>(this IList<T> list, Random random = null)
		{
			if(!list.Any())
				throw new IndexOutOfRangeException();
			random = random ?? new Random();
			return list[random.Next(0, list.Count)];
		} 
	}
}
