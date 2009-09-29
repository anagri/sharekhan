using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sharekhan.common
{
    public class UnsatisfiedDependencyException : Exception
    {
        public UnsatisfiedDependencyException(string message)
            : base(message)
        {
        }
    }
}
