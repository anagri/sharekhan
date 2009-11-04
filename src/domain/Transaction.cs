using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShareKhan.domain;


namespace Sharekhan.domain
{
    public abstract class Transaction
    {
        public virtual int Id { get; set; }
        public virtual int Quantity { get; set; }
        public virtual Instrument Instrument { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual Price UnitPrice { get; set; }

        public static int NUMBER_OF_DAYS_IN_YEAR = 365;


        public Transaction()
        {
        }

        public Transaction(DateTime date, Instrument instrument, int quantity, Price unitPrice)
        {
            this.Quantity = quantity;
            this.Instrument = instrument;
            this.Date = date;
            this.UnitPrice = unitPrice;
        }


        public abstract Price TransactionAmount();

        public virtual bool IsLongTerm(FinYear year)
        {
            return (this.Date < year.GetTaxationPeriod().Value.StartDate);
        }

        



        public abstract Price EffectiveTransactionAmount();

        public abstract void UpdateSoldAmounts(IDictionary<Instrument, Price> realizedProfitsDictionary);

        public abstract void UpdateSoldQuantities(IDictionary<Instrument, int> instrumentQuantities);

        public abstract void UpdateBoughtAmounts(IDictionary<Instrument, Price> dictionary, int quantity);

        public abstract void UpdateBoughtQuantities(IDictionary<Instrument, int> dictionary);

        public virtual Price GetEffectiveValue(DateTime referenceDate, double rate)
        {
            var duration = ((double)(referenceDate - Date).Days) / 365.0;
            return EffectiveTransactionAmount().GetEffectiveValue(duration, rate);
        }
    }
}