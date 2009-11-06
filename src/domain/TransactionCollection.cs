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
        public Rate GetXIRR(double lb, double ub)
        {
            const double tolerance = 1e-6;
            const int maxIterations = 100;

            if (_transactionList.Count == 0) return new Rate(0.0);
            DateTime effectiveDate = _transactionList[_transactionList.Count - 1].Date;

            double lbVal = GetEffectiveReturn(effectiveDate, lb).Value;
            double ubVal = GetEffectiveReturn(effectiveDate, ub).Value;

            bool returnIsAscending = (lbVal < 0);

            for( int i = 0; i < maxIterations; i++ )
            {
                double mid = (lb + ub)/2;

                double midVal = GetEffectiveReturn(effectiveDate, mid).Value;
                Console.WriteLine	("Iter {0}- {1} - {2}" , i,mid,midVal);
                if (Math.Abs(midVal) < tolerance) return new Rate( mid );

                bool lbAndMidValsHaveSameSign = (lbVal > 0 && midVal > 0 || lbVal < 0 && midVal < 0);

                if (lbAndMidValsHaveSameSign)
                {
                    lb = mid;
                }
                else // ubAndMidValsHaveSameSign
                {
                    ub = mid;
                }
            }

            return new Rate(double.NaN);
        }
    }
}