using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sharekhan.src
{
    class Transaction
    {
        public virtual int Id { get; set;}
        public virtual int Quantity { get; set; }
        public virtual Instrument Instrument { get; set; }
        public virtual DateTime Date{ get; set; }
        public virtual double Amount{ get; set; }
        public virtual double Tax { get; set; }
        public virtual double Brokerage { get; set; }
    }
}
