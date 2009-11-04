using System;
using System.Collections.Generic;

namespace Sharekhan.domain
{
    public class UnitDividendTransaction : DividendTransaction
    {
        protected UnitDividendTransaction()
        {
        }

        public UnitDividendTransaction(Instrument instrument, int quantity, DateTime transactionDate)
            : base(instrument, quantity, transactionDate)
        {
        }

        public virtual bool Equals(UnitDividendTransaction other)
        {
            return base.Equals(other);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return Equals(obj as UnitDividendTransaction);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(UnitDividendTransaction left, UnitDividendTransaction right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(UnitDividendTransaction left, UnitDividendTransaction right)
        {
            return !Equals(left, right);
        }

        public override void UpdateBoughtQuantities(IDictionary<Instrument, int> instrumentQuantities)
        {
            if (instrumentQuantities[Instrument] < Quantity)
            {
                instrumentQuantities[Instrument] = 0;
            }
            else
            {
                instrumentQuantities[Instrument] -= Quantity;
            }
        }
    }
}