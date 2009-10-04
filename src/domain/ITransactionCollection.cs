using System.Collections;
using System.Collections.Generic;

namespace Sharekhan.domain
{
    public interface ITransactionCollection
    {
        bool Add(Transaction transaction);
        IEnumerable<Transaction> TransactionList { get; }

        void BuildDictionariesWithSellingAmounts(IDictionary<Instrument, Price> realizedProfitsDictionary,
                                                 IDictionary<Instrument, int> instrumentQuantities);
        void UpdateRealizedProfits(IDictionary<Instrument, Price> realizedProfitsDictionary,
                                                  IDictionary<Instrument, int> instrumentQuantities);
    }
}