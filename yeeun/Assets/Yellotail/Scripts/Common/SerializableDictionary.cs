using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yellotail
{
	[System.Serializable]
	public class SerializableDictionary<TKey, TValue> : 
		Dictionary<TKey, TValue>, ISerializationCallbackReceiver
	{
		[SerializeField, Delayed] List<TKey> keys = new List<TKey>();
		[SerializeField, Delayed] List<TValue> values = new List<TValue>();

		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			keys.Clear();
			values.Clear();
			foreach (var pair in this)
			{
				keys.Add(pair.Key);
				values.Add(pair.Value);
			}
		}

		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			Clear();
			int count = Mathf.Min(keys.Count, values.Count);
			for (int i = 0; i < count; i++)
				Add(keys[i], values[i]);
		}
	}
}
