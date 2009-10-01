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
                if (transaction.GetType() != typeof (SellTransaction))
                    continue;
                if (!realizedProfitsDictionary.ContainsKey(transaction.Instrument))
                {
                    realizedProfitsDictionary[transaction.Instrument] = Price.Null;
                    instrumentQuantities[transaction.Instrument] = 0;
                }
                realizedProfitsDictionary[transaction.Instrument] += transaction.EffectiveTransactionAmount();
                instrumentQuantities[transaction.Instrument] += transaction.Quantity;
            }
        }

        public void UpdateRealizedProfits(IDictionary<Instrument, Price> realizedProfitsDictionary,
                                          IDictionary<Instrument, int> instrumentQuantities)
        {
            foreach (var transaction in TransactionList)
            {
                if (transaction.GetType() != typeof (BuyTransaction) ||
                    !instrumentQuantities.ContainsKey(transaction.Instrument) ||
                    instrumentQuantities[transaction.Instrument] == 0)
                    continue;

                if (instrumentQuantities[transaction.Instrument] < transaction.Quantity)
                {
                    realizedProfitsDictionary[transaction.Instrument] -=
                        new Price(instrumentQuantities[transaction.Instrument]*transaction.UnitPrice.Value +
                                  ((BuyTransaction) transaction).Tax + ((BuyTransaction) transaction).Brokerage);
                    instrumentQuantities[transaction.Instrument] = 0;
                }
                else
                {
                    realizedProfitsDictionary[transaction.Instrument] -= transaction.EffectiveTransactionAmount();
                    instrumentQuantities[transaction.Instrument] -= transaction.Quantity;
                }
            }
        }
    }
}