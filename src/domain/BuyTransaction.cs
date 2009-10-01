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


        public override Price TransactionAmount()
        {
            throw new NotImplementedException();
        }

        public override Price EffectiveTransactionAmount()
        {
            return new Price((UnitPrice.Value*Quantity) + Tax + Brokerage);
        }

        public override void UpdateSoldAmounts(IDictionary<Instrument, Price> realizedProfitsDictionary)
        {
        }

        public override void UpdateSoldQuantities(IDictionary<Instrument, int> instrumentQuantities)
        {
        }

        public override void UpdateBoughtAmounts(IDictionary<Instrument, Price> realizedProfitsDictionary, int quantity)
        {
            if (quantity < Quantity)
            {
                realizedProfitsDictionary[Instrument] -=
                    new Price(quantity * UnitPrice.Value +
                              Tax + Brokerage);
            }
            else
            {
                realizedProfitsDictionary[Instrument] -= EffectiveTransactionAmount();
            }
        }

        public override void UpdateBoughtQuantities(IDictionary<Instrument, int> instrumentQuantities)
        {
            if (instrumentQuantities[Instrument] < Quantity)
            {
                instrumentQuantities[Instrument] = 0;
            }
            else
            {
                instrumentQuantities[Instrument] -= Quantity;
            }
        }
    }
}