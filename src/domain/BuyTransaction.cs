using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharekhan.domain;

namespace ShareKhan.domain
{
    public class BuyTransaction : Transaction
    {
        public virtual double Tax { get; set; }
        public virtual double Brokerage { get; set; }

        public BuyTransaction()
        {
        }


        public BuyTransaction(DateTime date, Instrument instrument, int quantity, Price amount, double tax,
                              double brokerage)
            : base(date, instrument, quantity, amount)
        {
            this.Tax = tax;
            this.Brokerage = brokerage;
        }


        public override Price TransactionAmount()
        {
            return new Price((UnitPrice.Value*Quantity) + Tax + Brokerage);
        }
    }
}