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
        public virtual string Id { get; set; }
        public virtual Symbol Symbol { get; set; }
        public virtual string Description { get; set; }
        public virtual Price CurrentPrice { get; private set; }


        public virtual IRepository PersistenceRepository { get; set; }


        public Instrument(): this(null, null, null, null)
        {
            CurrentPrice = Price.Null;
        }

        protected Instrument(string id, Symbol symbol, Price price, string description)
        {
            this.Id = id;
            this.Symbol = symbol;
            this.CurrentPrice = price;
            this.Description = description;
            
        }
        protected Instrument(Symbol symbol, Price price, string description): this(null, symbol, price,description)
        {   
        }

        public virtual void UpdateCurrentPrice(Price price)
        {
            CurrentPrice = price;
        }

    }

    
}
