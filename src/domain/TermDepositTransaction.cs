using System;
using System.Collections.Generic;

namespace Sharekhan.domain
{
    public class TermDepositTransaction : Transaction
    {
        public TermDepositTransaction(DateTime dateTime, TermDeposit instrument, Price price)
            : base(dateTime, instrument, 0, price)
        {
            instrument.DepositDate = new DepositDate(dateTime);
        }


        public override Price Amount()
        {
            int noOfYears = DateTime.Now.Subtract(Date).Days / 365;
            return
                new Price(Math.Round(
                    UnitPrice.GetEffectiveReturn(noOfYears, ((TermDeposit)Instrument).InterestRate.RateOfInterest/100).
                         Value));
        }

        public override Price EffectiveTransactionAmount()
        {
            return new Price(0);
        }

        public override int EffectiveTransactionQuantity()
        {
            throw new NotImplementedException();
        }

        public override void ComputeCapitalRealization(RealizedProfit realizedProfit)
        {
            throw new NotImplementedException();
        }
    }
}