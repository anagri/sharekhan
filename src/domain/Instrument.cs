using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharekhan.common;
using Sharekhan.domain;
using ShareKhan.service;

namespace Sharekhan.domain
{
    public abstract class Instrument
    {
        public virtual int Id { get; set; }
        public virtual Symbol Symbol { get; set; }
        public virtual string Description { get; set; }
        public virtual Price CurrentPrice { get; private set; }


        public virtual IRepository PersistenceRepository { get; set; }


        public Instrument()
        {
            CurrentPrice = Price.Null;
        }

        protected Instrument(Symbol symbol, Price price, string description)
        {
            this.Symbol = symbol;
            this.CurrentPrice = price;
            this.Description = description;
            
        }

        public virtual void UpdateCurrentPrice(Price price)
        {
            CurrentPrice = price;
        }

    }

    
}
