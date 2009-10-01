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
    }
}