using System;
using System.Threading.Tasks;

namespace NumbersGenerator
{
    public class Generator : IGenerator
    {
        private readonly Random _random;

        public Generator()
        {
            _random = new Random();
        }

        public Task<int> Generate(int min, int max)
        {
            return Task.FromResult(_random.Next(min, max));
        }
    }
}
