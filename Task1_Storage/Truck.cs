using System;
using System.Threading;
using System.Threading.Tasks;

namespace Task1_Storage
{
    class Truck
    {
        public int MaxProductCount { get; set; }
        Random rnd = new Random();

        public async Task<int> Delivery(Factory f, int index)
        {
            Thread.Sleep(rnd.Next(1000, 3000));

            Console.WriteLine($"Грузовик #{index} вывозит {MaxProductCount} единиц товара {f.ProductName}");
            return MaxProductCount;
        }
    }
}