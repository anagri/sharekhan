using System;
using NUnit.Framework;
using Sharekhan.domain;

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

        [Test]
        
        public void ShouldBeAbleToCalcSTCGTax()
        {
            Portfolio portfolio = new Portfolio();
            try
            {
                portfolio.CalcShortTermCapitalGainTax(new FinYear(2009, 2010));
                Assert.Fail("Should Throw Exception for now");
            }catch(NotImplementedException e)
            {
                
            }
        }
    }
}