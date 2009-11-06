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
            TermDeposit termDeposit = (TermDeposit)Instrument;
            int noOfYears = 0;

            if (termDeposit.IsMatured())
                noOfYears = termDeposit.Term.DepositTerm;
            else
                noOfYears = DateTime.Now.Subtract(Date).Days / 365;
            //int noOfYears = DateTime.Now.Subtract(Date).Days / 365;
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

        public override void UpdateSoldAmounts(RealizedProfit realizedProfit)
        {
            throw new NotImplementedException();
        }

        public override void UpdateSoldQuantities(RealizedProfit realizedProfit)
        {
            throw new NotImplementedException();
        }

        public override void UpdateBoughtAmounts(RealizedProfit realizedProfit)
        {
            throw new NotImplementedException();
        }

        public override void UpdateBoughtQuantities(RealizedProfit realizedProfit)
        {
            throw new NotImplementedException();
        }
    }
}