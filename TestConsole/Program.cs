namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HashSet<Guid> hashSet = new HashSet<Guid>();
            var counter = 1;
            while (true)
            {
                if (counter % 10000 == 0)
                {
                    Console.WriteLine(counter);
                }
                var guid = Guid.NewGuid();
                if (hashSet.Any(q => q == guid)) return;
                hashSet.Add(guid);
                counter++;
            }
            Console.WriteLine("l ", counter);

        }
    }
}