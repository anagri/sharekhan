using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharekhan.domain;

namespace ShareKhan.domain
{
    public class TransactionStatement : Statement
    {
        private readonly TransactionCollection _transactionList;

        public TransactionStatement()
        {
            _transactionList = new TransactionCollection();
        }

        public Price getInvestedValue()
        {
            double investedValue = 0.0;

            foreach (Transaction transaction in _transactionList)
            {
                investedValue += transaction.UnitPrice.Value;
            }
            return new Price(investedValue);
        }


        public void addTransaction(Transaction transaction)
        {
            _transactionList.Add(transaction);
        }

        public TransactionCollection GetTransactionCollection()
        {
            return _transactionList;
        }
    }
}