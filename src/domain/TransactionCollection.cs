using System;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using System.Linq;

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


        public ISet<Instrument> GetAllUniqueInstruments()
        {
            ISet<Instrument> instruments = new HashedSet<Instrument>();
            foreach (Transaction transaction in _transactionList)
            {
                instruments.Add(transaction.Instrument);
            }
            return instruments;
        }

        public Price GetEffectiveReturn(DateTime effectiveDate, double rate)
        {
            return new Price(_transactionList.Sum(s => s.GetEffectiveReturn(effectiveDate, rate).Value));

        }
    }
}