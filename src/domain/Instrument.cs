﻿using System;
using System.Collections.Generic;
using ShareKhan.persist;

namespace Sharekhan.domain
{
    public abstract class Instrument
    {
        public Instrument()
        {
            CurrentPrice = Price.Null;
        }

        protected Instrument(Symbol symbol, Price price, string description)
        {
            Symbol = symbol;
            CurrentPrice = price;
            Description = description;
        }


        public virtual int Id { get; set; }
        public virtual Symbol Symbol { get; set; }
        public virtual String Description { get; set; }
        public virtual Price CurrentPrice { get; private set; }


        public virtual IRepository PersistenceRepository { get; set; }


        public virtual void UpdateCurrentPrice(Price price)
        {
            CurrentPrice = price;
        }

        public virtual bool Equals(Instrument other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(Instrument)) return false;
            return Equals((Instrument)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static bool operator ==(Instrument left, Instrument right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Instrument left, Instrument right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("Id: {0}", Id);
        }

        public virtual Price CurrentMarketValue(IList<Transaction> transactions)
        {
            Price CurrentPrice = this.CurrentPrice;
            Price value = new Price(0.0);
            int count = 0;

            foreach (Transaction trans in transactions)
            {
                if (trans is BuyTransaction)
                {
                    count += trans.Quantity;
                }
                else if (trans is SellTransaction)
                {
                    count -= trans.Quantity;
                }
            }

            value.Value = count*CurrentPrice.Value;
            return value;
        }

        public virtual Price CalculateShortTermTax(BuyTransaction buyTransaction, SellTransaction sellTransaction)
        {

            return sellTransaction.CalculateShortTermTax(buyTransaction);
           
        }
    }

}