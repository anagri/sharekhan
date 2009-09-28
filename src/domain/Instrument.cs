using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharekhan.domain;

namespace Sharekhan.domain
{
    public abstract class Instrument
    {
        public virtual int Id { get; set; }
        public virtual string Symbol { get; set; }
        public virtual string Description { get; set; }
        public virtual Price CurrentValue { get; set; }

        public Instrument()
        {
            CurrentValue = Price.Null;
        }

        public void UpdateCurrentPrice(Price price)
        {
            CurrentValue = price;
        }

        public Price currentPrice()
        {
            return CurrentValue;
        }
    }
}
