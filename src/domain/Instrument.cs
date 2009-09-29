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
<<<<<<< HEAD:src/domain/Instrument.cs
        public virtual String Symbol { get; set; }
        public virtual String Description { get; set; }
        public virtual Price CurrentPrice { get; set; }

        public Instrument(int id, String Symbol, String Description, Price CurrentPrice)
=======
        public virtual Symbol Symbol { get; set; }
        public virtual string Description { get; set; }
        public virtual Price CurrentPrice { get; private set; }


        public virtual IRepository PersistenceRepository { get; set; }


        public Instrument()
>>>>>>> e855e45eebbb02c10271b68afb938583ab9af207:src/domain/Instrument.cs
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
