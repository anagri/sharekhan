using System;
using System.Collections;
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

        public RealizedProfit RealizedProfit()
        {
            RealizedProfit realizedProfit = new RealizedProfit();
            foreach (var transaction in TransactionList)
            {
                transaction.UpdateSoldAmounts(realizedProfit);
                transaction.UpdateSoldQuantities(realizedProfit);
            }
            foreach (var transaction in TransactionList)
            {
                if (realizedProfit.For(transaction.Instrument).Quantity ==0 )
                    continue;

                transaction.UpdateBoughtAmounts(realizedProfit);
                transaction.UpdateBoughtQuantities(realizedProfit);
            }
            return realizedProfit;
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

        public Price GetEffectiveValue(DateTime effectiveDate, double rate)
        {
            return new Price(_transactionList.Sum(s => s.GetEffectiveValue(effectiveDate, rate).Value));

        }
    }
}