using System;
using System.Collections.Generic;

namespace Sharekhan.domain
{
    public class DividendTransaction : Transaction
    {
        public override Price Amount()
        {
            return Price.Null;
        }

        public override Price EffectiveTransactionAmount()
        {
            return Amount();
        }

        public override void UpdateSoldAmounts(IDictionary<Instrument, Price> realizedProfitsDictionary)
        {
        }

        public override void UpdateSoldQuantities(IDictionary<Instrument, int> instrumentQuantities)
        {
        }

        public override void UpdateBoughtAmounts(IDictionary<Instrument, Price> dictionary, int quantity)
        {
        }

        public override void UpdateBoughtQuantities(IDictionary<Instrument, int> dictionary)
        {
        }

        protected DividendTransaction()
        {
        }

        public DividendTransaction(Instrument instrument, Price dividendAmount, DateTime transactionDate) : base(transactionDate, instrument, 0,dividendAmount)
        {
        }

        public DividendTransaction(Instrument instrument, int quantity, DateTime transactionDate) : base(transactionDate, instrument, quantity, Price.Null)
        {
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