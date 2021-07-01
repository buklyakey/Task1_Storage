using System;

namespace Task1_Storage
{
    class InputService
    {
        private Random Rnd { get; }

        public InputService()
        {
            Rnd = new Random();
        }

        public int InputFactoriesCount()
        {
            Console.WriteLine("Необходимо указать количество заводов");
            return InputIntWithMin(3);
        }

        public int InputN()
        {
            Console.WriteLine("Необходимо указать число N.");
            return InputIntWithMin(50);
        }

        public Factory[] GetFactories(int count, int n)
        {
            var factories = new Factory[count];

            float mult = 1;
            for (int i = 0; i < count; i++, mult += 0.1f)
            {
                factories[i] = new Factory()
                {
                    PackagingType = (PackagingType)GetRandomEnumIndex(typeof(PackagingType)),
                    ProductCount = (int)(mult * n),
                    ProductName = $"Product from factory {i + 1}",
                    Weight = Rnd.Next(50, 100)
                };
            }

            return factories;
        }

        public Storage InputStorage(int factoriesSum)
        {
            Console.WriteLine("Необходимо указать число M.");
            int M = InputIntWithMin(100);

            return new Storage(M * factoriesSum);
        }

        private int GetRandomEnumIndex(Type t) => Rnd.Next(0, t.GetEnumValues().Length);

        private int InputIntWithMin(int min)
        {
            bool validCount = false;

            while (!validCount)
            {
                Console.WriteLine($"Введите число >={min}");
                validCount = int.TryParse(Console.ReadLine(), out int count);
                if (validCount && count >= min)
                    return count;

                validCount = false;
                Console.WriteLine($"Ошибка ввода! Число должно быть целым положительным числом >= {min}. Повторите попытку.");
            }

            throw new NotImplementedException();
        }
    }
}
