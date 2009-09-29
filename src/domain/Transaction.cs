using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sharekhan.domain
{
    public abstract class Transaction
    {
        public virtual int Id { get; set;}
        public virtual int Quantity { get; set; }
        public virtual Instrument Instrument { get; set; }
        public virtual DateTime Date{ get; set; }
        public virtual double Amount{ get; set; }


        public Transaction(){ }

        public Transaction( DateTime date,Instrument instrument,int quantity, double amount)
        {
            this.Quantity = quantity;
            this.Instrument = instrument;
            this.Date = date;
            this.Amount = amount;

        }

        public abstract Price TransactionAmount();
      
    }
}
