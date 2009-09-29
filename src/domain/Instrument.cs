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
        public virtual String Symbol { get; set; }
        public virtual String Description { get; set; }
        public virtual Price CurrentPrice { get; set; }

        public Instrument(int id, String Symbol, String Description, Price CurrentPrice)
        {
            this.Id = Id;
            this.Symbol = Symbol;
            this.Description = Description;
            this.CurrentPrice = CurrentPrice;
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
