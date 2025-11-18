using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomianLayer.Exceptions;

namespace DomainLayer.Exceptions
{
    public sealed class UserNotFoundException(string Email):NotFoundException($"User With Email {Email} Is Not Found")
    {
    }
}
