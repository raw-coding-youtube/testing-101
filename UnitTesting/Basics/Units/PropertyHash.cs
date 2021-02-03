using System;
using System.Buffers.Text;
using System.Security.Cryptography;
using System.Text;

namespace Basics.Units
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

    public class AlgorithmPropertyHash : PropertyHash, IDisposable
    {
        private readonly HashAlgorithm _algorithm;

        public AlgorithmPropertyHash(string algorithm)
        {
            _algorithm = HashAlgorithm.Create(algorithm) ?? throw new ArgumentException(algorithm);
        }

        public override string Hash<T>(T input, params Func<T, string>[] selectors)
        {
            var seed = base.Hash(input, selectors);
            var seedBytes = Encoding.UTF8.GetBytes(seed);
            var hashBytes = _algorithm.ComputeHash(seedBytes);
            return Convert.ToBase64String(hashBytes);
        }

        public void Dispose() => _algorithm.Dispose();
    }
}