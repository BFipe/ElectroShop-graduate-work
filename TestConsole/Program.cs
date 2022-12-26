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
            string phoneNumber = String.Empty;
            var charArray = "80296111953".ToArray();
            if (charArray[0] == '3')
            {
                phoneNumber = $"+{charArray[0]}{charArray[1]}{charArray[2]} ({charArray[3]}{charArray[4]}) {charArray[5]}{charArray[6]}{charArray[7]}-{charArray[8]}{charArray[9]}-{charArray[10]}{charArray[11]}";
                Console.WriteLine(phoneNumber);
            }
            if (charArray[0] == '8')
            {
                phoneNumber = $"{charArray[0]} ({charArray[1]}{charArray[2]}{charArray[3]}) {charArray[4]}{charArray[5]}{charArray[6]}-{charArray[7]}{charArray[8]}-{charArray[9]}{charArray[10]}";
                Console.WriteLine(phoneNumber);
            }
        }
    }
}
