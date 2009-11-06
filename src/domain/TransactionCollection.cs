using System;
using System.Collections;
using System.Collections.Generic;
using Iesi.Collections.Generic;
using System.Linq;

namespace Sharekhan.domain
{
    public class TransactionCollection : ITransactionCollection
    {
        private readonly List<Transaction> _transactionList = new List<Transaction>();

        public List<Transaction> TransactionList
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
            List<Transaction> sellTransactions = TransactionList.FindAll(transaction => !(transaction is BuyTransaction));
            foreach (var transaction in sellTransactions)
            {
                transaction.Update(realizedProfit);
            }
            List<Transaction> buyTransactions = TransactionList.FindAll(transaction => transaction is BuyTransaction);
            foreach (var transaction in buyTransactions)
            {
                if (realizedProfit.For(transaction.Instrument).Quantity ==0 )
                    continue;

                transaction.Update(realizedProfit);
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