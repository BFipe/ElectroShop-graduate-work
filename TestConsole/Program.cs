using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace HelloApp
{
    class Program
    {
        public static void Main()
        {
            List<int> list = new List<int>() { 1,2,3,4,5,6,7,8};
            List<int> list2 = new List<int>(list);

            list[1] = 10000;

            foreach (var item in list2)
            {
                Console.WriteLine(item);
            }
        }
    }
}
