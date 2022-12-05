using System;

namespace HelloApp
{
    using System;

    namespace HelloApp
    {
        
        class Program
        {
            static void Main(string[] args)
            {
                object[] j = new Exception[10];
                j[0] = new Exception("ddd");
                throw j[2] as Exception;
            }
        }
    }
}