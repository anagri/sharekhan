using System;
using NUnit.Framework;
using System.Text;

namespace Sharekhan.domain
{
    public class MutualFund : Instrument
    {
        protected MutualFund()
        {
        }

        public MutualFund(Symbol symbol, Price currentPrice, String description)
            : base(symbol, currentPrice, description)
        {
        }

        public virtual bool Equals(MutualFund other)
        {
            return base.Equals(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as MutualFund);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(MutualFund left, MutualFund right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(MutualFund left, MutualFund right)
        {
            return !Equals(left, right);
        }
    }
}