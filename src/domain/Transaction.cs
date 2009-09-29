using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Sharekhan.domain
{
    public class Transaction
    {
        public virtual String Id { get; set;}
        public virtual int Quantity { get; set; }
        public virtual Instrument Instrument { get; set; }
        public virtual DateTime Date{ get; set; }
        public virtual double Amount{ get; set; }
        public virtual double Tax { get; set; }
        public virtual double Brokerage { get; set; }

        public Transaction(){ }

        public Transaction(String id, int quantity,Instrument instrument,DateTime date,double tax, double brokerage)
        {
            this.Id = id;
            this.Quantity = quantity;
            this.Instrument = instrument;
            this.Date = date;
            this.Tax = tax;
            this.Brokerage = brokerage;
        }

        public Price getTotalCost()
        {
            return new Price(Amount+Tax+Brokerage);
        }
         

    }
}
