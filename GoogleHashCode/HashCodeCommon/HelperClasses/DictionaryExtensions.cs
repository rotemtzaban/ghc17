using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashCodeCommon
{
	public static class DictionaryExtensions
	{
		public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
		{
			TValue result;
			if (dictionary.TryGetValue(key, out result)) 
				return result;
			return defaultValue;
		}

		public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> valueFactory)
		{
			TValue result;
			if (dictionary.TryGetValue(key, out result))
				return result;

			var newValue = valueFactory(key);
			dictionary.Add(key, newValue);
			return newValue;
		}
	}
}
