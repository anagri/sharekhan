using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharekhan.domain;

namespace Sharekhan.domain
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


        public override Price Amount()
        {
            return new Price((UnitPrice.Value * Quantity) + Tax + Brokerage);
        }

        public override Price EffectiveTransactionAmount()
        {
            return  new Price(-Amount().Value);
        }

        public override int EffectiveTransactionQuantity()
        {
            return Quantity;
        }

        public override void ComputeCapitalRealization(RealizedProfit realizedProfit)
        {
            Net net = realizedProfit.For(Instrument);
            if (net.Quantity < Quantity)
            {
                net.Profit -= new Price(net.Quantity * UnitPrice.Value + Tax + Brokerage);
                net.Quantity = 0;
            }
            else
            {
                // TODO: We probably want EffectiveTransactionAmount here
                net.Profit -= Amount();
                net.Quantity -= Quantity;
            }
        }

    }
}
//some other comment
