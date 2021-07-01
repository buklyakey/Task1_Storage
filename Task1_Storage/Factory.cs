using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task1_Storage
{
    class Factory
    {
        // at 1 hour
        public int ProductCount { get; set; }

        public string ProductName { get; set; }
        public int Weight { get; set; }
        public PackagingType PackagingType { get; set; }

        private Random rnd = new Random();

        public async Task<int> FactoryWork(int index)
        {
            Thread.Sleep(rnd.Next(1000, 3000));
            Console.WriteLine($"Завод #{index} произвел за час {ProductCount} единиц продукта");
            return ProductCount;
        }
    }

    public enum PackagingType
    {
        Strech,
        Pachage,
        Box
    }
}
