using System;
using NUnit.Framework;

namespace ShareKhan.domain
{
    [TestFixture]
    public class PorfolioTest
    {
        [Test]
        public void ShouldBeAbleToCalcSTCGTax()
        {
            Portfolio portfolio = new Portfolio();
            portfolio.CalcShortTermCapitalGainTax(new FinYear(2009, 2010));
        }
    }
}