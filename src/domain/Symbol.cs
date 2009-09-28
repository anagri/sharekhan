using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sharekhan.domain
{
    public class Symbol
    {
        private String _value;

        protected virtual String Value { get; set; }

        private Symbol()
        {
        }

        public Symbol(String value)
        {
            this._value = value;
        }

    }
}
