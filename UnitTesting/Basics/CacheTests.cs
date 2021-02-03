using System;
using Basics.Units;
using Xunit;

namespace Basics
{
    public class CacheTests
    {
        [Fact]
        public void CachesItemWithinTimeSpan()
        {
            var cache = new Cache(TimeSpan.FromDays(1));
            cache.Add(new("url", "content", DateTime.Now));

            var contains = cache.Contains("url");

            Assert.True(contains);
        }

        [Fact]
        public void Contains_ReturnsFalse_WhenOutsideTimeSpan()
        {
            var cache = new Cache(TimeSpan.FromDays(1));
            cache.Add(new("url", "content", DateTime.Now.AddDays(-2)));

            var contains = cache.Contains("url");

            Assert.False(contains);
        }

        [Fact]
        public void Contains_ReturnsFalse_WhenDoesntContainItem()
        {
            var cache = new Cache(TimeSpan.FromDays(1));

            var contains = cache.Contains("url");

            Assert.False(contains);
        }
    }
}