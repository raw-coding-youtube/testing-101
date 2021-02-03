using System;
using Basics.Units;
using Xunit;

namespace Basics
{
    public class PropertyHashTests
    {
        [Fact]
        public void PropertyHash_ConcatenatesSelectedFieldsInOrder()
        {
            var hasher = new PropertyHash();
            var item = new Cache.Item("url", "content", DateTime.Now);

            var hash = hasher.Hash(item, i => i.Url, i => i.Content);

            Assert.Equal("urlcontent", hash);
        }

        [Fact]
        public void AlgorithmPropertyHash_AppliesHashingAlgorithmToSeed()
        {
            var hasher = new AlgorithmPropertyHash("sha256");
            var item = new Cache.Item("url", "content", DateTime.Now);

            var hash = hasher.Hash(item, i => i.Url, i => i.Content);

            Assert.Equal("9FyLxk+9z73XO8xhZ15emMaK+oa8aDg6LWiY6y40KyQ=", hash);
        }
    }
}