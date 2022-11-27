using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoShop.Exceptions
{
    public class ObjectNotExistsException : Exception
    {
        public ObjectNotExistsException() : base("Объект не существует!")
        {

        }
        public ObjectNotExistsException(string name) : base($"{name} не существует!")
        {

        }
    }
}
