using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sharekhan.domain
{
    public class Price
    {
        public virtual double Value { get; set; }

        protected virtual double Value { get; set; }

        public static readonly Price Null = new Price(0.0);
        private Price()
        {
        }

        public Price(double price)
        {
            Value = price;
        }

        

        public override bool Equals(Object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.GetType() != this.GetType()) return false; 
            return ((Price )other).Value == Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}