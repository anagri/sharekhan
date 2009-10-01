﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharekhan.domain;

namespace Sharekhan.domain
{
    public class SellTransaction:Transaction
    {
        public virtual double Tax { get; set; }
        public virtual double Brokerage { get; set; }

        public SellTransaction(){}


        public SellTransaction(DateTime date, Instrument instrument, int quantity, Price amount, double tax, double brokerage)
            : base(date, instrument, quantity, amount)
        {
            this.Tax = tax;
            this.Brokerage = brokerage;
        }


        public override Price EffectiveTransactionAmount()
        {
            return new Price((UnitPrice.Value*Quantity) - Tax - Brokerage);
        }
    }
}
