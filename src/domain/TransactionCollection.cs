using System;
using System.Collections;
using System.Collections.Generic;
using ShareKhan.persist;

namespace Sharekhan.domain
{
    public class TransactionCollection : ITransactionCollection
    {
        private readonly IList<Transaction> _transactionList = new List<Transaction>();

        public IEnumerable<Transaction> TransactionList
        {
            get { return _transactionList; }
        }

        public bool Add(Transaction transaction)
        {
            if (transaction != null && _transactionList.IndexOf(transaction) == -1)
            {
                _transactionList.Add(transaction);
                return true;
            }
            return false;
        }

        public void BuildDictionariesWithSellingAmounts(IDictionary<Instrument, Price> realizedProfitsDictionary,
                                                        IDictionary<Instrument, int> instrumentQuantities)
        {
            foreach (var transaction in TransactionList)
            {
                transaction.UpdateSoldAmounts(realizedProfitsDictionary);
                transaction.UpdateSoldQuantities(instrumentQuantities);
            }
        }

        public void UpdateRealizedProfits(IDictionary<Instrument, Price> realizedProfitsDictionary,
                                          IDictionary<Instrument, int> instrumentQuantities)
        {
            foreach (var transaction in TransactionList)
            {
                if (!instrumentQuantities.ContainsKey(transaction.Instrument) ||
                    instrumentQuantities[transaction.Instrument] == 0)
                    continue;

                transaction.UpdateBoughtAmounts(realizedProfitsDictionary, instrumentQuantities[transaction.Instrument]);
                transaction.UpdateBoughtQuantities(instrumentQuantities);
            }
        }
    }
}