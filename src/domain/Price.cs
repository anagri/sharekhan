using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sharekhan.domain
{
    public class Price
    {
        private readonly double _price;

        public static readonly Price Null = new Price(0.0);


        public Price(double price)
        {
            _price = price;
        }

        

        public override bool Equals(Object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (other.GetType() != this.GetType()) return false; 
            return ((Price )other)._price == _price;
        }

        public override int GetHashCode()
        {
            return _price.GetHashCode();
        }
    }
}