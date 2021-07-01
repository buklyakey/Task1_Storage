namespace Task1_Storage
{
    class Storage
    {
        public int MaxProductsCount { get; }
        public int CurrentProductsCount { get; set; }

        public Storage(int maxCount)
        {
            MaxProductsCount = maxCount;
            CurrentProductsCount = 0;
        }
    }
}
