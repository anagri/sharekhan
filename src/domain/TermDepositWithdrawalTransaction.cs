﻿using System;
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
