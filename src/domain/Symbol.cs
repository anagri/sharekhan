using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sharekhan.domain
{
    public class Symbol
    {

        public virtual String Value { get; protected set; }

        private Symbol()
        {
        }

        public Symbol(String value)
        {
            this.Value = value;
        }


        public bool Equals(Symbol other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Value, Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Symbol)) return false;
            return Equals((Symbol) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Symbol left, Symbol right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Symbol left, Symbol right)
        {
            return !Equals(left, right);
        }
    }
}
