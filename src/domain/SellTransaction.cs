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


        public override Price Amount()
        {
            return new Price((UnitPrice.Value*Quantity) - Tax - Brokerage);
        }

        public override Price EffectiveTransactionAmount()
        {
            return Amount();
        }

        public override int EffectiveTransactionQuantity()
        {
            return -Quantity;
        }

        public override void UpdateSoldAmounts(IDictionary<Instrument, Price> realizedProfitsDictionary)
        {
            if (!realizedProfitsDictionary.ContainsKey(Instrument))
            {
                realizedProfitsDictionary[Instrument] = Price.Null;
            }
            realizedProfitsDictionary[Instrument] += Amount();
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

        public virtual Price CalculateShortTermTax(BuyTransaction buyTransaction)
        {
            if (IsShortTerm(buyTransaction))
            {
                double profitOrLoss = CalculateTotalPrice(this.UnitPrice, this.Quantity) -
                                      CalculateTotalPrice(buyTransaction.UnitPrice, buyTransaction.Quantity);
                if(profitOrLoss>0)
                {
                    return new Price(0.20*profitOrLoss);
                }
            }
            
            return new Price(0);
        }

        private double CalculateTotalPrice(Price unitPrice, int quantity)
        {

            return unitPrice.Value*quantity;
        }


        private bool IsShortTerm(Transaction transaction)
        {
            DateTime sellDate;
            DateTime buyDate;
            if (this.Date.CompareTo(transaction.Date) > 0)
            {
                sellDate = Date;
                buyDate = transaction.Date;
            }
            else
            {
                buyDate = Date;
                sellDate = transaction.Date;
            }

            return sellDate.Subtract(buyDate).Days < NUMBER_OF_DAYS_IN_YEAR;
        }
    }
}
