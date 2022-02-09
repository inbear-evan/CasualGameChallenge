using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
    public static partial class ExtensionMethods
    {
		public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
		{
			if (!dictionary.TryGetValue(key, out var value))
				value = defaultValue;
			return value;
		}

		public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, System.Func<TKey, TValue> defaultFactory)
		{
			if (!dictionary.TryGetValue(key, out var value))
				value = defaultFactory(key);
			return value;
		}
		public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default(TValue))
		{
			if (!dictionary.TryGetValue(key, out var value))
				dictionary.Add(key, value = defaultValue);
			return value;
		}
		public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, System.Func<TKey, TValue> defaultFactory)
		{
			if (!dictionary.TryGetValue(key, out var value))
				dictionary.Add(key, value = defaultFactory(key));
			return value;
		}		
	}
}
