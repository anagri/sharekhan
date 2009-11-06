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
            throw new NotImplementedException();
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
