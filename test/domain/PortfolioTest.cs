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
        public void CurrentMarketValue()
        {
            Portfolio portfolio = new Portfolio();
            portfolio.CurrentMarketValue(new Symbol("RILMF"));

            

        }
    }
}
