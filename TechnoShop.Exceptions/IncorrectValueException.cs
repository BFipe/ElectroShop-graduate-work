using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoShop.Exceptions
{
    public class IncorrectValueException<T> : Exception
    {
        public IncorrectValueException() : base("Форма содержит пустое значение!")
        {

        }
        public IncorrectValueException(T value) : base($"Неверное значение - {value.ToString()}")
        {

        }
    }
}
