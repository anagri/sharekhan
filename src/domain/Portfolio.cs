using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sharekhan.domain;

namespace ShareKhan.domain
{
    [TestFixture]
    public class PorfolioTest
    {
        [Test]
        public void should_get_realised_value()
        {
            Portfolio portfolio = new Portfolio();
            portfolio.getRealisedValue();

        }
    }



    class Portfolio
    {
        private PortfolioStatement portfolioStatement;
        private TransactionStatement transactionStatement;




        public void getRealisedValue()
        {
            
        }
    }
}


