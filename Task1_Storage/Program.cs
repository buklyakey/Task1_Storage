using System;
using System.Linq;
using System.Threading.Tasks;

namespace Task1_Storage
{
    class Program
    {
        public static void Main(string[] args)
        {
            new FactoriesModel()
                .Model();
        }
    }

    class FactoriesModel
    {
        public async void Model()
        {
            var inputService = new InputService();

            var factoriesCount = inputService.InputFactoriesCount();
            var n = inputService.InputN();

            var factories = inputService.GetFactories(factoriesCount, n);
            OutputFactoriesInfo(factories);

            // все заводы за час
            var productSum = factories.Sum(x => x.ProductCount);
            var storage = inputService.InputStorage(productSum);


            var prodCount95 = storage.MaxProductsCount / 100 * 95; // 95%

            var hours = prodCount95 / productSum;
            storage.CurrentProductsCount = productSum * hours;

            Console.WriteLine($"За {hours} часов склад заполнится на 95% и начнется вывоз продукции");

            var trucks = new Truck[factories.Length];
            for (int i = 0; i < trucks.Length; i++)
            {
                trucks[i] = new Truck() { MaxProductCount = factories[i].ProductCount };
            }


            Console.WriteLine("Для продолжения моделирования нажмите ENTER, для выхода любую другую клавишу");
            var key = Console.ReadKey();

            while (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine("Заводы работают...");
                Console.WriteLine();

                var tasks = new Task<int>[factories.Length];
                for (int i = 0; i < factories.Length; i++)
                {
                    tasks[i] = factories[i].FactoryWork(i + 1);
                }

                var prods = await Task.WhenAll(tasks);
                storage.CurrentProductsCount += prods.Sum();

                Console.WriteLine("Заводы произвели товар.");
                Console.WriteLine($"Сейчас на складе {storage.CurrentProductsCount} товаров");

                Console.WriteLine("Производится вывоз товара...");

                var truckTasks = new Task<int>[trucks.Length];
                for (int i = 0; i < trucks.Length; i++)
                {
                    truckTasks[i] = trucks[i].Delivery(factories[i], i + 1);
                    storage.CurrentProductsCount -= trucks[i].MaxProductCount;
                }

                await Task.WhenAll(truckTasks);
                Console.WriteLine($"Сейчас на складе {storage.CurrentProductsCount} товаров");

                Console.WriteLine("Для продолжения моделирования нажмите ENTER, для выхода любую другую клавишу");
                key = Console.ReadKey();
            }
        }

        private static void OutputFactoriesInfo(Factory[] factories)
        {
            Console.WriteLine();
            Console.WriteLine();
            foreach (var fac in factories)
            {
                Console.WriteLine($"{fac.ProductName} \t {fac.PackagingType} \t {fac.Weight}kg \t Count = {fac.ProductCount}");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
