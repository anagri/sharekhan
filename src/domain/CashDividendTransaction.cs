using System;
using System.Collections.Generic;

namespace Sharekhan.domain
{
    public class CashDividendTransaction : DividendTransaction
    {
        protected CashDividendTransaction()
        {
        }

        public CashDividendTransaction(Instrument instrument, Price dividendAmount, DateTime transactionDate) : base(instrument, dividendAmount, transactionDate)
        {
        }

        public virtual bool Equals(CashDividendTransaction other)
        {
            return base.Equals(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as CashDividendTransaction);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(CashDividendTransaction left, CashDividendTransaction right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(CashDividendTransaction left, CashDividendTransaction right)
        {
            return !Equals(left, right);
        }

        public override void ComputeCapitalRealization(RealizedProfit realizedProfit)
        {
            realizedProfit.For(Instrument).Profit += Amount();
        }

        public override Price Amount()
        {
            return new Price((UnitPrice.Value));
        }
    }
}