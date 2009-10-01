using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sharekhan.domain
{
    public class Price
    {

        public virtual double Value { get; set; }

        public static readonly Price Null = new Price(0.0);
        private Price()
        {
        }

        public Price(double price)
        {
            Value = price;
        }

        public Price Compute(Func<double,double> func)
        {
            return new Price(func(Value));
        }

        public bool Equals(Price other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Value == Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Price)) return false;
            return Equals((Price) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(Price left, Price right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Price left, Price right)
        {
            return !Equals(left, right);
        }

        public static Price operator +(Price left, Price right)
        {
            return new Price(left.Value + right.Value);
        }

        public static Price operator -(Price left, Price right)
        {
            return new Price(left.Value - right.Value);
        }
    }
}