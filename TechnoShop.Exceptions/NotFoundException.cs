using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoShop.Exceptions
{
    public class NotFoundException<T> : Exception
    {
        public NotFoundException(T element) : base($"{element.ToString()} не найден!")
        {

        }
    }
}
