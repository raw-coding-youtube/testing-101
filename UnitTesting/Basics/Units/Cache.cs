using System;
using System.Collections.Generic;

namespace Basics.Units
{
    public class Cache
    {
        public record Item(string Url, string Content, DateTime LastCollected);

        private readonly TimeSpan _cacheTime;
        private readonly Dictionary<string, Item> _cache = new();

        public Cache(TimeSpan cacheTime)
        {
            _cacheTime = cacheTime;
        }

        public bool Contains(string url)
        {
            if (_cache.TryGetValue(url, out var item))
            {
                return DateTime.UtcNow.Subtract(item.LastCollected) < _cacheTime;
            }

            return false;
        }

        public void Add(Item item) => _cache.Add(item.Url, item);
    }
}