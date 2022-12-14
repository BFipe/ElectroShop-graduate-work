using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoShop.Exceptions
{
    public class OutOfRangeException : Exception
    {
        public OutOfRangeException(int value) : base($"Число {value} слишком большое!")
        {

        } 
    }
}
