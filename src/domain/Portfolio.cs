using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sharekhan.domain;

namespace ShareKhan.domain
{
<<<<<<< HEAD:src/domain/Portfolio.cs
    public class Portfolio
=======
    [TestFixture]
    public class PorfolioTest
    {
        [Test]
        public void should_get_realised_value()
        {
            Portfolio portfolio = new Portfolio();
            portfolio.addTransaction(new Transaction("Trans001", 10,
                                                     new MutualFund(new Symbol("RELMF"), new Price(1000),"Reliance MF"),
                                                     DateTime.Today, 100, 100));

            Price expectedInvestment = new Price(10200); // got to expand as formula
            Assert.AreEqual(expectedInvestment, portfolio.getInvestedValue());

        }
    }



    class Portfolio
>>>>>>> e855e45eebbb02c10271b68afb938583ab9af207:src/domain/Portfolio.cs
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


