using System;
using System.Collections.Generic;

namespace Sharekhan.domain
{
    public class TermDepositTransaction : Transaction
    {
        public TermDepositTransaction(DateTime dateTime, SinglePayOut instrument, Price price)
            : base(dateTime, instrument, 0, price)
        {
            instrument.DepositDate = new DepositDate(dateTime);
        }


        public override Price Amount()
        {
            throw new NotImplementedException();
        }

        public override Price EffectiveTransactionAmount()
        {
            return new Price(0);
        }

        public override int EffectiveTransactionQuantity()
        {
            throw new NotImplementedException();
        }

        public override void UpdateSoldAmounts(IDictionary<Instrument, Price> realizedProfitsDictionary)
        {
            throw new NotImplementedException();
        }

        public override void UpdateSoldQuantities(IDictionary<Instrument, int> instrumentQuantities)
        {
            throw new NotImplementedException();
        }

        public override void UpdateBoughtAmounts(IDictionary<Instrument, Price> dictionary, int quantity)
        {
            throw new NotImplementedException();
        }

        public override void UpdateBoughtQuantities(IDictionary<Instrument, int> dictionary)
        {
            throw new NotImplementedException();
        }
    }
}