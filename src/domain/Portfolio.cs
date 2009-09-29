using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sharekhan.domain;

namespace ShareKhan.domain
{
    public class Portfolio
    {
        private PortfolioStatement portfolioStatement=new PortfolioStatement();
        private TransactionStatement transactionStatement=new TransactionStatement();


        public Price getInvestedValue()
        {
            return transactionStatement.getInvestedValue();
        }

        public void addTransaction(Transaction transaction)
        {
            transactionStatement.addTransaction(transaction);
        }
    }
}


