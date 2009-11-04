using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharekhan.domain;

namespace Sharekhan.domain
{
    public class SellTransaction : Transaction
    {
        public virtual double Tax { get; set; }
        public virtual double Brokerage { get; set; }

        public SellTransaction()
        {
        }


        public SellTransaction(DateTime date, Instrument instrument, int quantity, Price amount, double tax,
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
            return new Price((UnitPrice.Value*Quantity) - Tax - Brokerage);
        }

        public override void UpdateSoldAmounts(IDictionary<Instrument, Price> realizedProfitsDictionary)
        {
            if (!realizedProfitsDictionary.ContainsKey(Instrument))
            {
                realizedProfitsDictionary[Instrument] = Price.Null;
            }
            realizedProfitsDictionary[Instrument] += EffectiveTransactionAmount();
        }

        public override void UpdateSoldQuantities(IDictionary<Instrument, int> instrumentQuantities)
        {
            if(!instrumentQuantities.ContainsKey(Instrument))
            {
                instrumentQuantities[Instrument] = 0;
            }
            instrumentQuantities[Instrument] += Quantity;
        }

        public override void UpdateBoughtAmounts(IDictionary<Instrument, Price> dictionary, int quantity)
        {
            
        }

        public override void UpdateBoughtQuantities(IDictionary<Instrument, int> dictionary)
        {
            
        }

        public override Price GetEffectiveValue(DateTime time, double rate)
        {
            throw new NotImplementedException();
        }
    }
}