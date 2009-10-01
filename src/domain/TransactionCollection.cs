using System;
using System.Collections;
using System.Collections.Generic;
using ShareKhan.persist;

namespace Sharekhan.domain
{
    public class TransactionCollection : ITransactionCollection
    {
        private readonly IList<Transaction> _transactionList = new List<Transaction>();

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public Transaction Current
        {
            get { throw new NotImplementedException(); }
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }


        public bool Add(Transaction item)
        {
            _transactionList.Add(item);
            return true;
        }

        public IEnumerator<Transaction> GetEnumerator()
        {
            return _transactionList.GetEnumerator();
        }

        public bool Remove(int Id)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void BuildDictionariesWithSellingAmounts(Dictionary<Instrument, double> realizedProfitsDictionary,
                                                        Dictionary<Instrument, int> instrumentQuantities)
        {
            foreach (var transaction in this)
            {
                if (transaction.GetType() != typeof (SellTransaction))
                    continue;
                if (!realizedProfitsDictionary.ContainsKey(transaction.Instrument))
                {
                    realizedProfitsDictionary[transaction.Instrument] = 0;
                    instrumentQuantities[transaction.Instrument] = 0;
                }
                realizedProfitsDictionary[transaction.Instrument] += transaction.UnitPrice.Value*transaction.Quantity;
                realizedProfitsDictionary[transaction.Instrument] -= ((SellTransaction) transaction).Tax +
                                                                     ((SellTransaction) transaction).Brokerage;
                instrumentQuantities[transaction.Instrument] += transaction.Quantity;
            }
        }

        public void UpdateRealizedProfits(Dictionary<Instrument, double> realizedProfitsDictionary,
                                          Dictionary<Instrument, int> instrumentQuantities)
        {
            foreach (var transaction in this)
            {
                if (transaction.GetType() != typeof (BuyTransaction) ||
                    !instrumentQuantities.ContainsKey(transaction.Instrument) ||
                    instrumentQuantities[transaction.Instrument] == 0)
                    continue;

                double buyingPrice;
                if (instrumentQuantities[transaction.Instrument] < transaction.Quantity)
                {
                    buyingPrice = instrumentQuantities[transaction.Instrument]*transaction.UnitPrice.Value;
                    instrumentQuantities[transaction.Instrument] = 0;
                }
                else
                {
                    buyingPrice = transaction.Quantity*transaction.UnitPrice.Value;
                    instrumentQuantities[transaction.Instrument] -= transaction.Quantity;
                }
                realizedProfitsDictionary[transaction.Instrument] -= buyingPrice + ((BuyTransaction) transaction).Tax +
                                                                     ((BuyTransaction) transaction).Brokerage;
            }
        }
    }
}