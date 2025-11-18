using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomianLayer.Exceptions
{
    public class BasketNotFoundException(string Id):NotFoundException($"Basket With Id ={Id} is not Found")
    {
    }
}
