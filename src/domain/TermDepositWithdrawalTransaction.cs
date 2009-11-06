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
            int noOfYears = DateTime.Now.Subtract(Date).Days / 365;
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

        public override void Update(RealizedProfit realizedProfit)
        {
            throw new NotImplementedException();
        }
    }
}
