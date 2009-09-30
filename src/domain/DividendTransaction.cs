using System;

namespace Sharekhan.domain
{
    public class DividendTransaction
    {
        public virtual int Id { get; set; }
        public virtual Price DividendAmount { get; set; }
        public virtual int Quantity { get; set; }
        public virtual DateTime TransactionDate { get; set; }
        public virtual Instrument Instrument { get; set; }

        protected DividendTransaction()
        {
        }

        public DividendTransaction(Instrument instrument, Price dividendAmount, DateTime transactionDate)
        {
            DividendAmount = dividendAmount;
            TransactionDate = transactionDate;
            Instrument = instrument;
        }

        public DividendTransaction(Instrument instrument, int quantity, DateTime transactionDate)
        {
            Quantity = quantity;
            TransactionDate = transactionDate;
            Instrument = instrument;
        }

        public virtual bool Equals(DividendTransaction other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (DividendTransaction)) return false;
            return Equals((DividendTransaction) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }

        public static bool operator ==(DividendTransaction left, DividendTransaction right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(DividendTransaction left, DividendTransaction right)
        {
            return !Equals(left, right);
        }
    }
}