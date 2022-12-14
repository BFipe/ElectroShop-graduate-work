using System;

namespace HelloApp
{
    using System;
    using System.Text;

    namespace HelloApp
    {
        public struct S : IDisposable
        {
            private bool dispose;
            public void Dispose()
            {
                dispose = true;
            }
            public bool GetDispose()
            {
                return dispose;
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                int i = 1;
                int obj = i;
                ++i;
                Console.WriteLine(i);
                Console.WriteLine(obj);
            }
        }
    }
}