using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceKomodo.Utilities
{
    [Serializable]
    public class SerializedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<SerializedKeyValuePair> _keyValuePairs = new List<SerializedKeyValuePair>();
        
        [Serializable]
        public struct SerializedKeyValuePair
        {
            public TKey Key;
            public TValue Value;
        }
        
        private Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();
        
        public void OnBeforeSerialize()
        {
            _keyValuePairs.Clear();
            
            foreach (var kvp in _dictionary)
            {
                _keyValuePairs.Add(new SerializedKeyValuePair
                {
                    Key = kvp.Key,
                    Value = kvp.Value
                });
            }
        }
        
        public void OnAfterDeserialize()
        {
            _dictionary.Clear();
            
            foreach (var kvp in _keyValuePairs)
            {
                if (kvp.Key != null)
                {
                    _dictionary.TryAdd(kvp.Key, kvp.Value);
                }
            }
        }
        
        public TValue this[TKey key]
        {
            get => _dictionary[key];
            set => _dictionary[key] = value;
        }
        
        public int Count => _dictionary.Count;
        
        public bool IsReadOnly => false;
        
        public ICollection<TKey> Keys => _dictionary.Keys;
        
        public ICollection<TValue> Values => _dictionary.Values;
        
        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
        }
        
        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _dictionary.Add(item.Key, item.Value);
        }
        
        public void Clear()
        {
            _dictionary.Clear();
        }
        
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.TryGetValue(item.Key, out var value) && EqualityComparer<TValue>.Default.Equals(value, item.Value);
        }
        
        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }
        
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
                
            if (arrayIndex < 0 || arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));
                
            if (array.Length - arrayIndex < Count)
                throw new ArgumentException("The number of elements in the source dictionary is greater than the available space from arrayIndex to the end of the destination array.");
                
            foreach (var kvp in _dictionary)
            {
                array[arrayIndex++] = new KeyValuePair<TKey, TValue>(kvp.Key, kvp.Value);
            }
        }
        
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }
        
        public bool Remove(TKey key)
        {
            return _dictionary.Remove(key);
        }
        
        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            if (!Contains(item))
                return false;
                
            return _dictionary.Remove(item.Key);
        }
        
        public bool TryGetValue(TKey key, out TValue value)
        {
            return _dictionary.TryGetValue(key, out value);
        }
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}