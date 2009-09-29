using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sharekhan.domain;

namespace ShareKhan.domain
{
    public class TransactionStatement:Statement
    {
        private List<Transaction> transactionList = new List<Transaction>();

        public Price getInvestedValue()
        {
            double investedValue = 0.0;

            foreach(Transaction transaction in transactionList)
            {
                investedValue += transaction.UnitPrice.Value;
            }
            return new Price(investedValue);
        }


        public void addTransaction(Transaction transaction)
        {
            transactionList.Add(transaction);
        }

        public List<Transaction> listOfTransactions()
        {
            return new List<Transaction>(transactionList);
        }
    }
}