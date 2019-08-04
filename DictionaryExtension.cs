using System;
using System.Collections.Generic;
using System.Text;

namespace qpwakaba.Extensions
{
    public static class DictionaryExtension
    {
        public static V GetOrCreate<K, V>(this IDictionary<K, V> dictionary, K key) where V : new()
        {
            return dictionary.GetOrCreate(key, () => new V());
        }

        public static V GetOrCreate<K, V>(this IDictionary<K, V> dictionary, K key, Func<V> creator)
        {
            if (!dictionary.ContainsKey(key))
            {
                V value = creator();
                dictionary[key] = value;
                return value;
            }
            return dictionary[key];
        }

        public static V GetOrDefault<K, V>(this IDictionary<K, V> dictionary, K key)
        {
            return dictionary.GetOrDefault(key, default(V));
        }

        public static V GetOrDefault<K, V>(this IDictionary<K, V> dictionary, K key, V defaultValue)
        {
            if (!dictionary.ContainsKey(key)) return defaultValue;
            return dictionary[key];
        }

    }
}
