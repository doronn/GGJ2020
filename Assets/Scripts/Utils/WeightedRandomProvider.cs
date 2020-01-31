using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Utils
{
    public class WeightedRandomProvider<T>
    {
        private readonly List<KeyValuePair<int, T>> _items;
        private int _totalWeight;
        
        public WeightedRandomProvider()
        {
            _items = new List<KeyValuePair<int, T>>();
        }
        
        public void Add(T item, int weight)
        {
            _items.Add(new KeyValuePair<int, T>(weight, item));
            _totalWeight += weight;
        }
        
        public T GetRandomItem()
        {
            var randomWeight = Random.Range(0, _totalWeight);
            var currentWeight = 0;
            
            foreach (var kvp in _items)
            {
                if (randomWeight >= currentWeight)
                {
                    return kvp.Value;
                }
                
                currentWeight += kvp.Key;
            }
            
            throw new Exception($"{nameof(WeightedRandomProvider<T>)} error: could not generate a value");
        }
    }
}