using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Sharekhan.domain;
using ShareKhan.domain;

namespace ShareKhan.domain
{
    [TestFixture]
    public class PorfolioTest
    {
        [Test]
        public void should_get_invested_value()
        {
            Portfolio portfolio = new Portfolio();
            portfolio.addTransaction(new Transaction("Trans001", 10,
                                                     new MutualFund(new Symbol("RIL"), new Price(1000),"Reliance MF"),
                                                     DateTime.Today, 100, 100));


        }
    }
}
