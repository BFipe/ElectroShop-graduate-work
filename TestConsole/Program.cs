namespace TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
           List<object> obj = new List<object>();
            obj.Add(1);
            obj.Add("Hello");
            obj.Add(2);
            obj.Add(DateTime.Now.ToShortDateString());

            foreach (var item in obj)
            {
                Console.WriteLine(item + " - " + item.GetType());
            }

        }
    }
}