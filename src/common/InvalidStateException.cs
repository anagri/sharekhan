using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sharekhan.common
{
    public class InvalidStateException : Exception
    {
        public InvalidStateException(String message)
            : base(message)
        {
            
        }
    }
}
