using System;
using System.Security.Cryptography;
using System.Text;

namespace Mocking.Units
{
    public class PropertyHash
    {
        public virtual string Hash<T>(T input, params Func<T, string>[] selectors)
        {
            StringBuilder builder = new();
            foreach (var selector in selectors)
            {
                builder.Append(selector(input));
            }

            return builder.ToString();
        }
    }

    public interface IHashAlgorithmFactory
    {
        public HashAlgorithm Create();
    }

    public class AlgorithmPropertyHash
    {
        private readonly IHashAlgorithmFactory _algorithmFactory;

        public AlgorithmPropertyHash(IHashAlgorithmFactory algorithmFactory)
        {
            _algorithmFactory = algorithmFactory;
        }

        public string Hash(string seed)
        {
            var seedBytes = Encoding.UTF8.GetBytes(seed);
            using var algo = _algorithmFactory.Create();
            var hashBytes = algo.ComputeHash(seedBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}