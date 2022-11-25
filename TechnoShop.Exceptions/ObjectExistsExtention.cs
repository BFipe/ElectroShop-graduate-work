using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoShop.Exceptions
{
    public class ObjectExistsException : Exception
    {
        public ObjectExistsException() : base("Объект уже существует!")
        {

        }
        public ObjectExistsException(string name) : base($"{name} уже существует!")
        {

        }
    }
}
