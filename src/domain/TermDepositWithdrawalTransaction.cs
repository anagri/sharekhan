using System;
using System.Collections.Generic;
using Sharekhan.domain;

namespace Sharekhan.domain
{
    class TermDepositWithdrawalTransaction : Transaction
    {
        public TermDepositWithdrawalTransaction(DateTime dateTime, TermDeposit instrument, Price price)
            : base(dateTime, instrument, 0, price)
        {
            
        }
        
        public override Price Amount()
        {
            TermDeposit termDeposit = (TermDeposit)Instrument;
            int noOfYears = 0;

            if (termDeposit.IsMatured())
                return new Price(-EffectiveTransactionAmount().Value);
            else
                noOfYears = DateTime.Now.Subtract(Date).Days / 365;
            
            return
                new Price(Math.Round(
                    -UnitPrice.GetEffectiveReturn(noOfYears, ((TermDeposit) Instrument).InterestRate.RateOfInterest/100).
                         Value));
          
        }

        public override Price EffectiveTransactionAmount()
        {
            return UnitPrice;
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
