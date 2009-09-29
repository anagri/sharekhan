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
        public void should_get_realised_value()
        {
            Portfolio portfolio = new Portfolio();
            portfolio.addTransaction(new Transaction("Trans001", 10,
                                                     new MutualFund("Mutual001", "RIL", "Reliance MF", new Price(1000)),
                                                     DateTime.Today, 100, 100));


            Assert.AreEqual(10200,portfolio.getInvestedValue().Value);

        }
    }
}
