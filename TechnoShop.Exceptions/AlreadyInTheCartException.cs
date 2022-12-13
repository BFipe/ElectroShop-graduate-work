using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnoShop.Exceptions
{
    public class AlreadyInTheCartException : Exception
    {
        public AlreadyInTheCartException() : base("Вы уже добавили этот товар в корзину!")
        {

        }
        
    }
}
