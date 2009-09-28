using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharekhan.domain;
using ShareKhan.service;

namespace Sharekhan.domain
{
    public abstract class Instrument
    {
        public virtual int Id { get; set; }
        public virtual string Symbol { get; set; }
        public virtual string Description { get; set; }
        public virtual Price CurrentPrice { get; set; }


        public Instrument()
        {
            CurrentPrice = Price.Null;
        }

        public virtual void UpdateCurrentPrice(Price price)
        {
            CurrentPrice = price;
        }

        public virtual Price currentPrice()
        {
            return CurrentPrice;
        }
    }
}
