using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace Lama.Api.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException() { }
        public UnauthorizedException(string message):base(message) { }
    }
}
