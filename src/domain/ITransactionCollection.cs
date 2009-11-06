using System;
using System.Collections;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace Sharekhan.domain
{
    public interface ITransactionCollection
    {
        bool Add(Transaction transaction);
        List<Transaction> TransactionList { get; }

        RealizedProfit RealizedProfit();

        ISet<Instrument> GetAllUniqueInstruments();
        Price GetEffectiveValue(DateTime effectiveDate, double rate);

    }
}